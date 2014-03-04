using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthyClub.Administration.DA;
using System.Transactions;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System.IO;
using System.Drawing;
using BCUtility;

namespace HealthyClub.Administration.BF
{
    public class AdministrationBFC
    {
        //
        #region category
        public bool DeleteCategories(int categoryID)
        {
            int activitiesInCategory = new AdministrationDAC().RetrieveActivitiesInCategoryCount(categoryID);
            if (activitiesInCategory != 0)
            {
                return false;
            }
            else
            {
                AdministrationDAC dac = new AdministrationDAC();

                // Delete the children first
                AdministrationEDSC.CategoryDTDataTable dt = dac.RetrieveAllSubCategories(categoryID);
                foreach (var dr in dt)
                {
                    dac.DeleteCategory(dr.ID);
                }
                //delete the parents
                dac.DeleteCategory(categoryID);
                return true;
            }
        }

        public void CreateCategory(int parentCategoryID, string userName, AdministrationEDSC.CategoryDTRow categoryDR)
        {
            AdministrationDAC dac = new AdministrationDAC();
            ManageCategoryLevel(parentCategoryID, categoryDR);

            categoryDR.CreatedBy = userName;
            categoryDR.CreatedDateTime = DateTime.Now;
            categoryDR.ModifiedBy = "";
            categoryDR.ModifiedDateTime = DateTime.Now;

            dac.CreateCategory(categoryDR);
        }

        private void ManageCategoryLevel(int parentCategoryID, AdministrationEDSC.CategoryDTRow categoryDR)
        {
            AdministrationDAC dac = new AdministrationDAC();
            int parentLevel = 0;

            var parentDR = dac.RetrieveCategory(parentCategoryID);

            if (parentDR != null)
            {
                parentLevel = dac.DetermineCategoryLevel(parentDR);
            }
            else
            {
                parentDR = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
            }

            if (!parentDR.IsLevel1ParentIDNull())
            {
                categoryDR.Level1ParentID = parentDR.Level1ParentID;
            }
            else
            {
                categoryDR.Level1ParentID = 0;
            }

            if (!parentDR.IsLevel2ParentIDNull())
            {
                categoryDR.Level2ParentID = parentDR.Level2ParentID;
            }
            else
            {
                categoryDR.Level2ParentID = 0;
            }

            switch (parentLevel)
            {
                case 0:
                    categoryDR.Level1ParentID = parentCategoryID;
                    break;
                case 1:
                    categoryDR.Level2ParentID = parentCategoryID;
                    break;
                case 2:
                    throw new Exception(SystemConstants.err_MaximumCategoryLevelExceeded);
            }
        }

        public void MoveCategory(int categoryID, int newParentCategoryID, string userName)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var categoryDR = dac.RetrieveCategory(categoryID);
            ManageCategoryLevel(newParentCategoryID, categoryDR);

            categoryDR.ModifiedBy = userName;
            categoryDR.ModifiedDateTime = DateTime.Now;

            dac.UpdateCategory(categoryDR);

            var childrenDT = dac.RetrieveAllSubCategories(categoryID).OrderBy(p => p.Level);
            if (childrenDT.Count() != 0)
                using (TransactionScope trans = new TransactionScope())
                {
                    foreach (var childDR in childrenDT)
                    {
                        switch (childDR.Level)
                        {
                            case 1:
                                ManageCategoryLevel(childDR.Level1ParentID, childDR);
                                break;
                            case 2:
                                ManageCategoryLevel(childDR.Level2ParentID, childDR);
                                break;
                        }

                        childDR.ModifiedBy = userName;
                        childDR.ModifiedDateTime = DateTime.Now;
                        dac.UpdateCategory(childDR);
                    }

                    trans.Complete();
                }

        }

        public void UpdateCategory(int categoryID, string name, int parentID, string userName)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrieveCategory(categoryID);
            dr.Name = name;

            dr.ModifiedDateTime = DateTime.Now;
            dr.ModifiedBy = userName;

            ManageCategoryLevel(parentID, dr);

            dac.UpdateCategory(dr);
        }
        #endregion

        #region ActivityImage
        public void CreateActivityImage(AdministrationEDSC.ActivityImageDetailDTRow dr, out int imageID, int filesize)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var iInfo = dac.RetrieveActivityImageInformation(dr.ActivityID);
            int iiID = 0;
            if (iInfo == null)
            {
                dr.isPrimaryImage = true;

                var ii = new AdministrationEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
                ii.ActivityID = dr.ActivityID;
                ii.StorageUsed = 0;
                ii.FreeStorage = SystemConstants.MaxActivityImageStorage;
                ii.ImageAmount = 0;
                dac.createActivityImageInformation(ii, out iiID);
            }
            else
                dr.ActivityImageID = iInfo.ID;

            using (TransactionScope trans = new TransactionScope())
            {
                dac.CreateActivityImage(dr, out imageID);

                var ii = dac.RetrieveActivityImageInformation(dr.ActivityID);
                ii.ActivityID = dr.ActivityID;
                ii.StorageUsed = ii.StorageUsed + filesize;
                ii.FreeStorage = ii.FreeStorage - filesize;
                ii.ImageAmount = ii.ImageAmount + 1;

                dac.UpdateImageInformation(dr.ActivityID, iiID, ii);
                trans.Complete();
            }
        }

        public void DeleteActivityImage(int ActivityID, int imageID, int filesize, out string imageThumbVirtualPath, out string imageVirtualPath)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrieveActivityImage(ActivityID, imageID);
            if (dr.isPrimaryImage == true)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dac.DeleteActivityImage(ActivityID, imageID, out imageThumbVirtualPath, out imageVirtualPath);

                    var ii = dac.RetrieveActivityImageInformation(dr.ActivityID);
                    ii.ActivityID = dr.ActivityID;
                    ii.StorageUsed = ii.StorageUsed - filesize;
                    ii.FreeStorage = ii.FreeStorage + filesize;
                    ii.ImageAmount = ii.ImageAmount - 1;

                    dac.UpdateImageInformation(dr.ActivityID, ii.ID, ii);
                    trans.Complete();
                }

                var dt = dac.RetrieveActivityImages(ActivityID);
                if (dt.Count() != 0)
                {
                    int ActivityID1 = dt[0].ActivityID;
                    int imageID1 = dt[0].ID;
                    dac.UpdateActivityPrimaryImage(ActivityID1, imageID1);
                }
            }
            else
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dac.DeleteActivityImage(ActivityID, imageID, out imageThumbVirtualPath, out imageVirtualPath);

                    var ii = dac.RetrieveActivityImageInformation(dr.ActivityID);
                    ii.ActivityID = dr.ActivityID;
                    ii.StorageUsed = ii.StorageUsed - filesize;
                    ii.FreeStorage = ii.FreeStorage + filesize;
                    ii.ImageAmount = ii.ImageAmount - 1;

                    dac.UpdateImageInformation(dr.ActivityID, ii.ID, ii);
                    trans.Complete();
                }
            }
        }

        public string RetrieveImageUrl(int activityID, int imageID)
        {
            var imgDR = new AdministrationDAC().RetrieveActivityImage(activityID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetActivityImageURL(activityID, imageID, imgDR.Filename);
            else
                return SystemConstants.ActImageDirectory + "No image.jpg";
        }

        public string RetrieveImageThumbUrl(int activityID, int imageID)
        {
            var imgDR = new AdministrationDAC().RetrieveActivityImage(activityID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetActivityImageThumbURL(activityID, imageID, imgDR.Filename);
            else
                return SystemConstants.ActImageDirectory + "No image.jpg";
        }

        public void CreateActivityImages(int ActivityID, AdministrationEDSC.ActivityImageDTRow drInfo, AdministrationEDSC.ActivityImageDetailDTDataTable dt)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var info = dac.RetrieveActivityImageInformation(ActivityID);
            int count = info.ImageAmount;
            using (TransactionScope trans = new TransactionScope())
            {

                //Insert Images
                foreach (var dr in dt)
                {
                    if (count == 0)
                        dr.isPrimaryImage = true;
                    else dr.isPrimaryImage = false;

                    dac.CreateActivityImage(dr);
                    count++;
                }
                //Update ImageInfo
                dac.UpdateImageInformation(ActivityID, drInfo.ID, drInfo);
                trans.Complete();
            }
        }

        #endregion

        #region Council

        public bool DeleteCouncil(int councilID)
        {
            int SuburbInCouncil = new AdministrationDAC().RetrieveCouncilSuburbsCount(councilID);
            if (SuburbInCouncil != 0)
            {
                return false;
            }
            else
            {
                new AdministrationDAC().DeleteCouncil(councilID);
                return true;
            }

        }

        #endregion

        public static void UpdateActivity(int activityID, AdministrationEDSC.ActivityDTRow ActivityDetailDR, AdministrationEDSC.ActivityContactDetailDTRow contactDetailsDR, AdministrationEDSC.ActivityGroupingDTRow ActGroupingDR, AdministrationEDSC.ActivityScheduleDTDataTable ActScheduleDT)
        {
            AdministrationDAC dac = new AdministrationDAC();
            dac.DeleteActivitySchedules(activityID);
            using (TransactionScope trans = new TransactionScope())
            {
                ActivityDetailDR.ID = contactDetailsDR.ActivityID = ActGroupingDR.ActivityID = activityID;
                //Activity was update, Create other
                dac.UpdateActivity(ActivityDetailDR);


                //CreateContactDetails
                dac.UpdateActivityContactDetail(contactDetailsDR);

                //Delete old Schedule before insert new one

                if (ActScheduleDT != null)
                    //Create Schedule
                    foreach (var ActScheduleDR in ActScheduleDT)
                    {
                        ActScheduleDR.ActivityID = activityID;
                        dac.CreateActivitySchedule(ActScheduleDR);
                    }
                //Create Grouping
                dac.UpdateActivityGrouping(ActGroupingDR);


                trans.Complete();
            }
        }

        public void ActivityMaintenance(string User)
        {
            int thisYear = DateTime.Now.Year;
            var DT = new AdministrationDAC().RetrieveActivities();
            int Modifiedindex = 0;

            //Create Maintenance Log
            var WebLog = new AdministrationEDSC.WebLogDTDataTable().NewWebLogDTRow();
            WebLog.Message = SystemConstants.ExecuteActivityMaintenance + "%NL%" + "Datetime:" + DateTime.Now + "Requested by:" + User;
            WebLog.LogCategory = (int)SystemConstants.maintenanceCategory.MaintenanceActivityStatus;
            WebLog.CreatedDateTime = DateTime.Now;
            WebLog.CreatedBy = User;
            WebLog.ReferenceNumber = SystemConstants.MaintenanceCode + " " + BCUtility.ObjectHandler.GetRandomKey(9);
            WebLog.Note = "";

            //Create Maintenance Log Action
            var webLogActionCollection = new AdministrationEDSC.WeblLogActionDTDataTable();
            //Create Activities Log Action
            var ActivitiesLogGroupCollection = new AdministrationEDSC.ActivitiesLogGroupDTDataTable();

            foreach (var dr in DT)
            {
                int today = DateTime.Now.DayOfYear;
                int expiryDays = dr.ExpiryDate.DayOfYear + ((dr.ExpiryDate.Year - thisYear) * 365);

                var status = "";
                if (dr.Status == (int)SystemConstants.ActivityStatus.Expired)
                    status = "EXPIRED";
                if (dr.Status == (int)SystemConstants.ActivityStatus.WillExpire)
                    status = "EXPIRING";

                if ((expiryDays - today) > SystemConstants.NotificationOneDays && dr.Status != (int)SystemConstants.ActivityStatus.Active && dr.Status != (int)SystemConstants.ActivityStatus.NotActive)
                {
                    new AdministrationDAC().ChangeStatus(dr.ID, (int)SystemConstants.ActivityStatus.Active);

                    var LogAction = webLogActionCollection.NewWeblLogActionDTRow();
                    LogAction.ActionType = (int)SystemConstants.NotificationType.NoNotification;
                    LogAction.Message = "Activity ID:" + dr.ID + "%NL%";
                    LogAction.Message += "Flag: invalid Status(" + status + "),  Expiry days is more 14 days.  ";
                    LogAction.Message += "Action Taken: Change status to ACTIVE" + "%NL%";
                    LogAction.LogCategory = (int)SystemConstants.maintenanceCategoryAction.ActivityStatusChange;
                    LogAction.CreatedDateTime = DateTime.Now;
                    LogAction.CreatedBy = User;
                    LogAction.Value = dr.ID.ToString() + "|" + dr.Name + "|" + dr.Status + "|" + dr.ExpiryDate + "|" + dr.ProviderID;
                    

                    webLogActionCollection.AddWeblLogActionDTRow(LogAction);
                    Modifiedindex++;

                }
                else if ((expiryDays - today) <= SystemConstants.NotificationOneDays && (expiryDays - today) >= SystemConstants.NotificationTwoDays && dr.Status != (int)SystemConstants.ActivityStatus.NotActive)
                {
                    new AdministrationDAC().ChangeStatus(dr.ID, (int)SystemConstants.ActivityStatus.WillExpire);

                    var LogAction = webLogActionCollection.NewWeblLogActionDTRow();
                    LogAction.ActionType = (int)SystemConstants.NotificationType.Expiring2week;
                    LogAction.Message = "Activity ID:" + dr.ID + "%NL%";
                    LogAction.Message += "Activity Name:" + dr.Name + "%NL%";
                    LogAction.Message += "Flag: Change Activity Status(" + status + "), Activity expire in 14 days.  ";
                    LogAction.Message += "Action Taken: Change status to EXPIRING, Sending Notification 1";
                    LogAction.LogCategory = (int)SystemConstants.maintenanceCategoryAction.ActivityStatusChangeAndSendingEmail;
                    LogAction.CreatedDateTime = DateTime.Now;
                    LogAction.CreatedBy = User;
                    LogAction.Value = dr.ID.ToString() + "|" + dr.Name + "|" + dr.Status + "|" + dr.ExpiryDate + "|" + dr.ProviderID;

                    webLogActionCollection.AddWeblLogActionDTRow(LogAction);
                    var actLogGroup = new AdministrationDAC().RetrieveActivitiesLogGroup(dr.ID, dr.ExpiryDate);
                    if (actLogGroup != null)
                    {
                        if (actLogGroup.LastNotificationType != (int)SystemConstants.NotificationType.Expiring2week)
                            Modifiedindex++;
                    }
                    else
                        Modifiedindex++;
                }
                else if ((expiryDays - today) <= SystemConstants.NotificationTwoDays && (expiryDays - today) >= 1 && dr.Status != (int)SystemConstants.ActivityStatus.NotActive)
                {
                    new AdministrationDAC().ChangeStatus(dr.ID, (int)SystemConstants.ActivityStatus.WillExpire);

                    var LogAction = webLogActionCollection.NewWeblLogActionDTRow();
                    LogAction.ActionType = (int)SystemConstants.NotificationType.Expiring1week;
                    LogAction.Message = "Activity ID:" + dr.ID + "%NL%";
                    LogAction.Message += "Activity Name:" + dr.Name + "%NL%";
                    LogAction.Message += "Flag: Change Activity Status(" + status + "), Activity expire in 7 days.  ";
                    LogAction.Message += "Action Taken: Change status to EXPIRING, Sending Notification 2";
                    LogAction.LogCategory = (int)SystemConstants.maintenanceCategoryAction.ActivityStatusChangeAndSendingEmail;
                    LogAction.CreatedDateTime = DateTime.Now;
                    LogAction.CreatedBy = User;
                    LogAction.Value = dr.ID.ToString() + "|" + dr.Name + "|" + dr.Status + "|" + dr.ExpiryDate + "|" + dr.ProviderID;

                    webLogActionCollection.AddWeblLogActionDTRow(LogAction);

                    var actLogGroup = new AdministrationDAC().RetrieveActivitiesLogGroup(dr.ID, dr.ExpiryDate);
                    if (actLogGroup != null)
                    {
                        if (actLogGroup.LastNotificationType != (int)SystemConstants.NotificationType.Expiring1week)
                            Modifiedindex++;
                    }
                    else
                        Modifiedindex++;
                }
                else if ((expiryDays - today) < 0 && dr.Status != (int)SystemConstants.ActivityStatus.Expired)
                {
                    new AdministrationDAC().ChangeStatus(dr.ID, (int)SystemConstants.ActivityStatus.Expired);

                    var LogAction = webLogActionCollection.NewWeblLogActionDTRow();
                    LogAction.ActionType = (int)SystemConstants.maintenanceCategoryAction.ActivityStatusChange;
                    LogAction.Message = "Activity ID:" + dr.ID + "%NL%";
                    LogAction.Message += "Activity Name:" + dr.Name + "%NL%";
                    LogAction.Message += "Flag: invalid Status(" + status + "),  Activity is Expired.  ";
                    LogAction.Message += "Action Taken: Change status to EXPIRED" + "%NL%";
                    LogAction.LogCategory = (int)SystemConstants.maintenanceCategory.MaintenanceActivityStatus;
                    LogAction.CreatedDateTime = DateTime.Now;
                    LogAction.CreatedBy = User;
                    webLogActionCollection.AddWeblLogActionDTRow(LogAction);
                    var actLogGroup = new AdministrationDAC().RetrieveActivitiesLogGroup(dr.ID, dr.ExpiryDate);
                    if (dr != null)
                    {
                        if (actLogGroup.LastNotificationType != (int)SystemConstants.NotificationType.Expired)
                            Modifiedindex++;
                    }
                    else
                        Modifiedindex++;
                }
            }
            int WebLogId = 0;

            if (Modifiedindex >= 1)
            {
                int ActivityLogGroupID = 0;
                new AdministrationDAC().SaveLog(WebLog, out WebLogId);

                foreach (var webLogAction in webLogActionCollection)
                {
                    // Check if notification 1 is already sent
                    int emailSent = 0;
                    var refnumber = "";
                    bool sendEmail = false;
                    int notificationType = 0;
                    // activityInfo Value Format
                    // [INT actID] | [STRING actName] | [INT actStatus] | [DATETIME actExpiryDate] | [GUID actProviderID]
                    // ==================================================================================================
                    string[] activityInfo = webLogAction.Value.Split('|');

                    int activityID = Convert.ToInt32(activityInfo[0]);
                    string activityName = activityInfo[1];
                    int activityStatus = Convert.ToInt32(activityInfo[2]);
                    DateTime activityExpDate = Convert.ToDateTime(activityInfo[3]);
                    Guid activityProviderID = new Guid(activityInfo[4]);
                    if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expiring2week)
                    {
                        var actLogGroup = new AdministrationDAC().RetrievePastActivityLogGroup(activityID, (int)SystemConstants.NotificationType.Expiring2week, activityExpDate.Date);
                        if (actLogGroup != null)
                        {
                            emailSent = actLogGroup.EmailSentNumber;
                            actLogGroup.ActivityID = activityID;
                            if (emailSent == (int)SystemConstants.NotificationType.Expiring2week)
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber;
                            else
                            {
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber++;
                                sendEmail = true;
                            }
                            notificationType = actLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring2week;
                            actLogGroup.ExpiryDate = activityExpDate;
                            refnumber = actLogGroup.ReferenceNumber;
                            new AdministrationDAC().UpdateActivityogGroup(actLogGroup, actLogGroup.ID);
                            ActivityLogGroupID = actLogGroup.ID;
                        }
                        else
                        {

                            var newactLogGroup = ActivitiesLogGroupCollection.NewActivitiesLogGroupDTRow();
                            newactLogGroup.ActivityID = activityID;
                            refnumber = newactLogGroup.ReferenceNumber = SystemConstants.ActivitiesCode + " " + BCUtility.ObjectHandler.GetRandomKey(9);
                            emailSent = 1;
                            sendEmail = true;
                            newactLogGroup.EmailSentNumber = 1;
                            newactLogGroup.LastSent = DateTime.Now;

                            notificationType = newactLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring2week;
                            newactLogGroup.ExpiryDate = activityExpDate;
                            newactLogGroup.LogActionID = 0;
                            newactLogGroup.CreatedDatetime = DateTime.Now;
                            newactLogGroup.CreatedBy = User;
                            new AdministrationDAC().createActivityLogGroup(newactLogGroup, out ActivityLogGroupID);
                        }
                    }

                    else if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expiring1week)
                    {
                        var actLogGroup = new AdministrationDAC().RetrievePastActivityLogGroup(activityID, (int)SystemConstants.NotificationType.Expiring2week, activityExpDate.Date);
                        var secActLogGroup = new AdministrationDAC().RetrievePastActivityLogGroup(activityID, (int)SystemConstants.NotificationType.Expiring1week, activityExpDate.Date);
                        if (actLogGroup != null)
                        {
                            emailSent = actLogGroup.EmailSentNumber;
                            actLogGroup.ActivityID = activityID;
                            if (emailSent == (int)SystemConstants.NotificationType.Expiring1week)
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber;
                            else
                            {
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber++;
                                sendEmail = true;
                            }
                            notificationType = actLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring1week;
                            actLogGroup.ExpiryDate = activityExpDate;
                            refnumber = actLogGroup.ReferenceNumber;
                            new AdministrationDAC().UpdateActivityogGroup(actLogGroup, actLogGroup.ID);

                            ActivityLogGroupID = actLogGroup.ID;
                        }
                        else if (secActLogGroup != null)
                        {
                            emailSent = secActLogGroup.EmailSentNumber;
                            secActLogGroup.ActivityID = activityID;
                            if (emailSent == (int)SystemConstants.NotificationType.Expiring1week)
                                secActLogGroup.EmailSentNumber = secActLogGroup.EmailSentNumber;
                            else
                            {
                                sendEmail = true;
                                secActLogGroup.EmailSentNumber = secActLogGroup.EmailSentNumber + 1;
                            }

                            notificationType = secActLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring1week;
                            secActLogGroup.ExpiryDate = activityExpDate;
                            refnumber = secActLogGroup.ReferenceNumber;
                            new AdministrationDAC().UpdateActivityogGroup(secActLogGroup, secActLogGroup.ID);
                            ActivityLogGroupID = secActLogGroup.ID;
                        }
                        else
                        {
                            sendEmail = true;
                            var newactLogGroup = ActivitiesLogGroupCollection.NewActivitiesLogGroupDTRow();
                            newactLogGroup.ActivityID = activityID;
                            refnumber = newactLogGroup.ReferenceNumber = SystemConstants.ActivitiesCode + " " + BCUtility.ObjectHandler.GetRandomKey(9);
                            emailSent = 1;
                            newactLogGroup.EmailSentNumber = 1;
                            newactLogGroup.LastSent = DateTime.Now;
                            notificationType = newactLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring1week;
                            newactLogGroup.ExpiryDate = activityExpDate;
                            newactLogGroup.LogActionID = 0;
                            newactLogGroup.CreatedDatetime = DateTime.Now;
                            newactLogGroup.CreatedBy = User;
                            new AdministrationDAC().createActivityLogGroup(newactLogGroup, out ActivityLogGroupID);
                        }
                    }
                    else if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expired)
                    {
                        var actLogGroup = new AdministrationDAC().RetrievePastActivityLogGroup(activityID, (int)SystemConstants.NotificationType.Expiring1week, activityExpDate.Date);
                        var secActLogGroup = new AdministrationDAC().RetrievePastActivityLogGroup(activityID, (int)SystemConstants.NotificationType.Expired, activityExpDate.Date);
                        if (actLogGroup != null)
                        {
                            emailSent = actLogGroup.EmailSentNumber;
                            actLogGroup.ActivityID = activityID;
                            if (emailSent == (int)SystemConstants.NotificationType.Expired)
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber;
                            else
                                actLogGroup.EmailSentNumber = actLogGroup.EmailSentNumber++;
                            notificationType = actLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring1week;
                            actLogGroup.ExpiryDate = activityExpDate;
                            refnumber = actLogGroup.ReferenceNumber;
                            new AdministrationDAC().UpdateActivityogGroup(actLogGroup, actLogGroup.ID);
                            ActivityLogGroupID = actLogGroup.ID;
                        }
                        else if (secActLogGroup != null)
                        {
                            emailSent = secActLogGroup.EmailSentNumber;
                            secActLogGroup.ActivityID = activityID;
                            if (emailSent == (int)SystemConstants.NotificationType.Expiring1week)
                                secActLogGroup.EmailSentNumber = secActLogGroup.EmailSentNumber;
                            else
                            {
                                sendEmail = true;
                                secActLogGroup.EmailSentNumber = secActLogGroup.EmailSentNumber + 1;
                            }
                            notificationType = secActLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring1week;
                            secActLogGroup.ExpiryDate = activityExpDate;
                            refnumber = secActLogGroup.ReferenceNumber;
                            new AdministrationDAC().UpdateActivityogGroup(secActLogGroup, secActLogGroup.ID);
                            ActivityLogGroupID = secActLogGroup.ID;
                        }
                        else
                        {
                            sendEmail = true;
                            var newactLogGroup = ActivitiesLogGroupCollection.NewActivitiesLogGroupDTRow();
                            newactLogGroup.ActivityID = activityID;
                            refnumber = newactLogGroup.ReferenceNumber = SystemConstants.ActivitiesCode + " " + BCUtility.ObjectHandler.GetRandomKey(9);
                            emailSent = 1;
                            newactLogGroup.EmailSentNumber = 1;
                            newactLogGroup.LastSent = DateTime.Now;
                            notificationType = newactLogGroup.LastNotificationType = (int)SystemConstants.NotificationType.Expiring2week;
                            newactLogGroup.ExpiryDate = activityExpDate;
                            newactLogGroup.LogActionID = 0;
                            newactLogGroup.CreatedDatetime = DateTime.Now;
                            newactLogGroup.CreatedBy = User;
                            new AdministrationDAC().createActivityLogGroup(newactLogGroup, out ActivityLogGroupID);
                        }
                    }

                    var actLog = new AdministrationEDSC.ActivitiesLogDTDataTable().NewActivitiesLogDTRow();
                    actLog.LogType = 1;
                    actLog.ReferenceNumber = refnumber;
                    actLog.Message = "Activity ID:" + activityID + "%NL%";
                    actLog.Message += "Activity Name:" + activityName + "%NL%";
                    actLog.Note = "";
                    actLog.CreatedBy = SystemConstants.System;
                    actLog.CreatedDatetime = DateTime.Now;
                    actLog.Value = activityExpDate.ToShortDateString();
                    actLog.NotificationNumber = notificationType;
                    actLog.ActivityID = activityID;
                    actLog.ProviderID = activityProviderID;
                    actLog.ActivityLogGroupID = ActivityLogGroupID;

                    int LogActionID = 0;

                    webLogAction.WebLogID = WebLogId;
                    new AdministrationDAC().SaveWebLogAction(webLogAction, out LogActionID);

                    if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expiring2week)
                    {
                        if (sendEmail)
                        {
                            actLog.Message += "Flag: invalid Status(" + activityStatus + "),  Activity is Expiring.  ";
                            actLog.Message += "Action Taken: Change status to EXPIRING" + "%NL%";
                            actLog.Message += "Number of Email sent: " + emailSent + "%NL%";
                            actLog.Message += "Today: " + DateTime.Now.ToShortDateString();

                            var SendEmail = SendNotificationEmail(actLog.ActivityID, actLog.NotificationNumber, actLog.ReferenceNumber, actLog.ProviderID);
                            actLog.LogActionID = LogActionID;
                            new AdministrationDAC().SaveActivityLog(actLog);
                        }
                    }
                    else if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expiring1week)
                    {
                        if (sendEmail)
                        {
                            actLog.Message += "Flag: invalid Status(" + activityStatus + "),  Activity is Expiring.  ";
                            actLog.Message += "Action Taken: Change status to EXPIRING" + "%NL%";
                            actLog.Message += "Number of Email sent: " + emailSent + "%NL%";
                            actLog.Message += "Today: " + DateTime.Now.ToShortDateString();

                            var SendEmail = SendNotificationEmail(actLog.ActivityID, actLog.NotificationNumber, actLog.ReferenceNumber, actLog.ProviderID);
                            actLog.LogActionID = LogActionID;
                            new AdministrationDAC().SaveActivityLog(actLog);
                        }
                    }
                    else if (webLogAction.ActionType == (int)SystemConstants.NotificationType.Expired)
                    {
                        if (sendEmail)
                        {
                            actLog.Message += "Flag: invalid Status(" + activityStatus + "),  Activity is Expired.  ";
                            actLog.Message += "Action Taken: Change status to EXPIRED" + "%NL%";
                            actLog.Message += "Number of Email sent: " + emailSent + "%NL%";
                            actLog.Message += "Today: " + DateTime.Now.ToShortDateString();

                            var SendEmail = SendNotificationEmail(actLog.ActivityID, actLog.NotificationNumber, actLog.ReferenceNumber, actLog.ProviderID);
                            actLog.LogActionID = LogActionID;
                            new AdministrationDAC().SaveActivityLog(actLog);
                        }
                    }
                }
                foreach (var actLogGroup in ActivitiesLogGroupCollection)
                {
                    new AdministrationDAC().UpdateActivityLogGroup(actLogGroup.ID, actLogGroup);
                }

            }
            else
            {
                var drLog = new AdministrationEDSC().WebLogDT.NewWebLogDTRow();
                drLog.Message = "Activities Status updated: No changes made %NL%";
                drLog.Message += "Date:" + DateTime.Now + "%NL%";
                drLog.LogCategory = (int)SystemConstants.maintenanceCategory.MaintenanceActivityStatus;
                drLog.CreatedBy = User;
                drLog.CreatedDateTime = DateTime.Now;
                drLog.ReferenceNumber = SystemConstants.MaintenanceCode + BCUtility.ObjectHandler.GetRandomKey(9);
                drLog.Note = "";
                new AdministrationDAC().SaveLog(drLog, out WebLogId);
            }
        }

        private bool SendNotificationEmail(int activityID, int notificationNumber, string referenceNumber, Guid userID)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var MailConf = dac.RetrieveWebConfiguration();
            var ProviderProfiles = dac.RetrieveProviderProfiles(userID);
            if (ProviderProfiles == null || string.IsNullOrEmpty(ProviderProfiles.Email))
            {
                return false;
            }
            else
            {
                AdministrationEDSC.v_EmailExplorerDTRow emTemp = new AdministrationEDSC.v_EmailExplorerDTDataTable().Newv_EmailExplorerDTRow();
                if (notificationNumber == 1)
                {

                    emTemp = dac.RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.Expired2week);
                    ParseEmail(emTemp, userID, referenceNumber, (int)SystemConstants.EmailTemplateType.Expired2week, activityID);
                }
                else if (notificationNumber == 2)
                {
                    emTemp = dac.RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.Expired1week);
                    ParseEmail(emTemp, userID, referenceNumber, (int)SystemConstants.EmailTemplateType.Expired1week, activityID);
                }
                else if (notificationNumber == 3)
                {
                    emTemp = dac.RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.Expired);
                    ParseEmail(emTemp, userID, referenceNumber, (int)SystemConstants.EmailTemplateType.Expired, activityID);
                }
                EmailSender.SendEmail(MailConf.SMTPAccount, ProviderProfiles.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                return true;
            }
        }

        public void SaveKeywords(AdministrationEDSC.KeyCollectionDTRow drKeyProperties, AdministrationEDSC.KeywordDTRow drKeywords, int mode)
        {
            AdministrationDAC dac = new AdministrationDAC();
            int KeyColID = 0;
            using (TransactionScope trans = new TransactionScope())
            {
                if (mode == (int)SystemConstants.FormMode.New)
                {
                    dac.CreateKeyCollection(drKeyProperties, out KeyColID);
                    dac.createKeywords(drKeywords, KeyColID);
                }
                else if (mode == (int)SystemConstants.FormMode.Edit)
                {
                    dac.UpdateKeyCollection(drKeyProperties);
                    dac.UpdateKeywords(drKeywords);
                }
                trans.Complete();
            }
        }

        public void DeleteMenuItem(int MenuItemID)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var childDTs = dac.RetrieveChildMenuItems(MenuItemID);

            int linkID;
            foreach (var childDR in childDTs)
            {
                dac.DeleteMenu(childDR.ID, out linkID);
                dac.DeleteLink(linkID);
            }

            dac.DeleteMenu(MenuItemID, out linkID);
            dac.DeleteLink(linkID);
        }

        public void SortMenuItem(int movedMenuItemID, bool isUp, out bool cannotChangePos)
        {
            AdministrationDAC dac = new AdministrationDAC();

            var movedDR = dac.RetrieveMenu(movedMenuItemID);
            var dt = dac.RetrieveMenus();

            IEnumerable<AdministrationEDSC.MenuDTRow> destQuery = null;

            AdministrationEDSC.MenuDTRow destDR = null;

            if (isUp)
            {
                destQuery = from dr in dt
                            where dr.ID == movedDR.ID &&
                                    dr.ParentMenuID == movedDR.ParentMenuID &&
                                    dr.Sequence < movedDR.Sequence
                            orderby dr.Sequence
                            select dr;

                destDR = destQuery.LastOrDefault();
            }
            else
            {
                destQuery = from dr in dt
                            where dr.ID == movedDR.ID &&
                                    dr.ParentMenuID == movedDR.ParentMenuID &&
                                    dr.Sequence > movedDR.Sequence
                            orderby dr.Sequence
                            select dr;

                destDR = destQuery.FirstOrDefault();
            }

            if (destDR == null)
            {
                cannotChangePos = true;
                return;
            }

            int temp = destDR.Sequence;

            destDR.Sequence = movedDR.Sequence;
            movedDR.Sequence = temp;

            using (TransactionScope trans = new TransactionScope())
            {
                dac.UpdateMenu(movedDR);
                dac.UpdateMenu(destDR);
                trans.Complete();
            }

            //SwapMenuItem(movedDR, destDR,isUp);
            cannotChangePos = false;
        }

        public void DuplicatePage(int OldDynamicPageID, out int newDynamicPageID)
        {
            AdministrationDAC dac = new AdministrationDAC();
            var drOldPage = dac.RetrievePage(OldDynamicPageID);

            drOldPage.ID = 0;
            drOldPage.Name = drOldPage.Name + "_Copy";
            dac.CreatePage(drOldPage);
            newDynamicPageID = drOldPage.ID;

        }

        public void CreateAssetsInformation(AdministrationEDSC.WebAssetsDTDataTable dt)
        {
            foreach (var dr in dt)
            {
                new AdministrationDAC().CreateAssetInformation(dr);
            }
        }

        public void AccountAudit()
        {
            var Activities = new AdministrationDAC().RetrieveActivities();
            {
                foreach (var activity in Activities)
                {
                    activity.SecondaryCategoryID1 = activity.SecondaryCategoryID2 = 0;
                    activity.isApproved = activity.isCommenceAnytime = activity.isMembershipRequired = true;
                    new AdministrationDAC().UpdateActivity(activity);
                }
            }

            /*var Providers = new AdministrationDAC().RetrieveProviderProfiles();
            {
                foreach (var provider in Providers)
                {
                   
                        provider.CreatedBy = provider.ModifiedBy = "System Registration";
                        provider.CreatedDatetime = provider.ModifiedDatetime = DateTime.Now;
                        provider.SecondarySuburb = "";
                    new AdministrationDAC().UpdateProviderProfiles(provider);
                    
                }
            }

            var Users = new AdministrationDAC().RetrieveUserProfiles();
            {
                foreach (var user in Users)
                {
                    
                        user.CreatedBy = user.ModifiedBy = "System Registration";
                        user.CreatedDatetime = user.ModifiedDatetime = DateTime.Now;
                        new AdministrationDAC().UpdateUserProfiles(user);
                    
                }
            }*/
        }

        public AdministrationEDSC.ActivityScheduleGridDTDataTable CalculateRecurrence(AdministrationEDSC.ActivityScheduleDTDataTable dtSched)
        {
            var timeTableDT = new AdministrationEDSC.ActivityScheduleGridDTDataTable();
            foreach (var drSched in dtSched)
            {
                if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.NotRecurring)
                {
                    //InsertRecurrenceStartTime
                    DateTime recurStartDate = drSched.ActivityStartDatetime;
                    DateTime recurEndDate = drSched.ActivityEndDatetime;

                    AdministrationEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                    dr.StartDateTime = recurStartDate;
                    dr.EndDateTime = recurEndDate;
                    timeTableDT.AddActivityScheduleGridDTRow(dr);
                }
                else
                {
                    int startYear = drSched.ActivityStartDatetime.Year;
                    int yearDifference = drSched.ActivityExpiryDate.Year - drSched.ActivityStartDatetime.Year;

                    if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Daily)
                    {
                        int dayDifference = (drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) - drSched.ActivityStartDatetime.DayOfYear;
                        int recurTimes = dayDifference / drSched.RecurEvery;

                        for (int i = 0; i <= recurTimes; i++)
                        {
                            //InsertRecurrenceStartTime
                            DateTime recurStartDate = drSched.ActivityStartDatetime.AddDays(drSched.RecurEvery * i);
                            DateTime recurEndDate = drSched.ActivityEndDatetime.AddDays(drSched.RecurEvery * i);

                            AdministrationEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                            dr.StartDateTime = recurStartDate;
                            dr.EndDateTime = recurEndDate;
                            timeTableDT.AddActivityScheduleGridDTRow(dr);
                        }
                    }
                    else if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Weekly)
                    {
                        decimal weekDifference = ((drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) / 7) - (drSched.ActivityStartDatetime.DayOfYear / 7);
                        decimal recurTimes = weekDifference / drSched.RecurEvery;

                        for (decimal i = 1; i <= recurTimes + 1; i++)
                        {
                            for (int weekday = 1; weekday <= 7; weekday++)
                            {
                                if ((drSched.ActivityStartDatetime.DayOfYear + ((drSched.ActivityStartDatetime.Year - startYear) * 365)) > (drSched.ActivityExpiryDate.DayOfYear) + (yearDifference * 365))
                                    break;

                                if (drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Monday && drSched.OnMonday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Tuesday && drSched.OnTuesday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Wednesday && drSched.OnWednesday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Thursday && drSched.OnThursday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Friday && drSched.OnFriday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Saturday && drSched.OnSaturday ||
                                    drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Sunday && drSched.OnSunday)
                                {
                                    AdministrationEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                                    dr.StartDateTime = drSched.ActivityStartDatetime;
                                    dr.EndDateTime = drSched.ActivityEndDatetime;
                                    timeTableDT.AddActivityScheduleGridDTRow(dr);
                                }


                                drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays(1);
                                drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays(1);
                                //recurStartDate = startDateTime.AddDays(1 * i + weekday);
                            }
                            drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                            drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                        }
                    }
                }

            }
            timeTableDT.DefaultView.Sort = "StartDateTime ASC";
            return timeTableDT;
        }

        public void ParseEmail(AdministrationEDSC.v_EmailExplorerDTRow emTemp, Guid userID, string token, int EmailTemplateType, int activityID)
        {
            var dr = new AdministrationDAC().RetrieveUserProfiles(userID);

            if (dr == null)
            {
                var drprov = new AdministrationDAC().RetrieveProviderProfiles(userID);
                if (drprov != null)
                {
                    if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                    {
                        //Provider Fullname
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        //Provider ConfirmationTokenuri
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        //Provider Confirmationurl
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ForgotPassword)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@recoverylinkwithtoken]", SystemConstants.CustomerUrl + "Account/PasswordRecovery.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired2week)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                }
                else
                {
                    if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                    {
                        //Provider Fullname
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                        //Provider ConfirmationTokenuri
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        //Provider Confirmationurl
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.ProviderUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.ProviderUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ForgotPassword)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@recoverylinkwithtoken]", SystemConstants.ProviderUrl + "Account/PasswordRecovery.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.ProviderUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired2week)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                    {
                        var activityDR = new AdministrationDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                }
            }
        }

        public void ConfirmActivities(List<int> activitiesID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (var item in activitiesID)
                {
                    if (item != 0 && item != -1)
                        new AdministrationDAC().ConfirmActivity(item);
                }
                trans.Complete();

            }
        }

        public void DeleteActivities(List<int> activitiesID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (var item in activitiesID)
                {
                    if (item != 0 && item != -1)
                        new AdministrationDAC().DeleteActivity(item);
                }
                trans.Complete();

            }
        }

        public void DeleteRewards(List<int> RewardID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (var item in RewardID)
                {
                    if (item != 0 && item != -1)
                        new AdministrationDAC().DeleteReward(item);
                }
                trans.Complete();

            }
        }

        public void DeleteSponsors(List<Guid> SponsorID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (Guid item in SponsorID)
                {
                    if (item != null)
                        new AdministrationDAC().DeleteSponsor(item);
                }
                trans.Complete();

            }
        }


        public void ExtendActivitiesExpiryDate(int daysExtended, out int actCount)
        {

            var ActivityDT = new AdministrationDAC().RetrieveExpiredActivities();
            List<int> selectedDT = new AdministrationDAC().RetrieveExpiredActivityIDs();
            var ActivityScheduleDT = new AdministrationDAC().RetrieveActivitySchedulesbyIDs(selectedDT);

            if (ActivityDT != null)
            {
                foreach (var actDr in ActivityDT)
                {
                    actDr.ExpiryDate = actDr.ExpiryDate.AddDays(daysExtended);
                    new AdministrationDAC().UpdateActivity(actDr);
                }
            }
            if (ActivityScheduleDT != null)
            {
                foreach (var schedDr in ActivityScheduleDT)
                {
                    schedDr.ActivityExpiryDate = schedDr.ActivityExpiryDate.AddDays(daysExtended);
                    new AdministrationDAC().UpdateActivitySchedule(schedDr);
                }
            }
            actCount = selectedDT.Count;
        }

        public void ExtendActivitiesExpiryDate(List<int> selectedDT, int daysExtended)
        {
            var ActivityDT = new AdministrationDAC().RetrieveActivitiesbyIDs(selectedDT);
            var ActivityScheduleDT = new AdministrationDAC().RetrieveActivitySchedulesbyIDs(selectedDT);

            if (ActivityDT != null)
            {
                foreach (var actDr in ActivityDT)
                {
                    actDr.ExpiryDate = actDr.ExpiryDate.AddDays(daysExtended);
                    new AdministrationDAC().UpdateActivity(actDr);
                }
            }
            if (ActivityScheduleDT != null)
            {
                foreach (var schedDr in ActivityScheduleDT)
                {
                    schedDr.ActivityExpiryDate = schedDr.ActivityExpiryDate.AddDays(daysExtended);
                    new AdministrationDAC().UpdateActivitySchedule(schedDr);
                }
            }
        }

        public void SaveRewards(AdministrationEDSC.RewardDTRow drReward, AdministrationEDSC.RewardsDetailsDTRow drRwrdDet, AdministrationEDSC.RewardImageDTRow drRwrdImage)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                int RewardID = 0;
                new AdministrationDAC().SaveReward(drReward, out RewardID);
                if (drRwrdImage != null)
                {
                    drRwrdImage.RewardID = RewardID;
                    new AdministrationDAC().SaveRewardImage(drRwrdImage);
                }
                drRwrdDet.RewardID = RewardID;
                new AdministrationDAC().SaveRewardDetail(drRwrdDet);

                trans.Complete();
            }

        }
        public void UpdateRewards(AdministrationEDSC.RewardDTRow drReward, AdministrationEDSC.RewardsDetailsDTRow drRwrdDet, AdministrationEDSC.RewardImageDTRow drRwrdImage)
        {
            using (TransactionScope trans = new TransactionScope())
            {

                new AdministrationDAC().UpdateReward(drReward);
                if (drRwrdImage != null)
                {

                    new AdministrationDAC().UpdateRewardImage(drRwrdImage);
                }
                if(drRwrdDet!=null)
                                      
                    new AdministrationDAC().UpdateRewardDetail(drRwrdDet);

                trans.Complete();
            }

        }

    }
}
