using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthyClub.Provider.EDS;
using System.Transactions;
using HealthyClub.Utility;
using System.Data;
using System.IO;
using System.Drawing;
using HealthyClub.Provider.DA;
using WebMatrix.WebData;

namespace HealthyClub.Provider.BF
{
    public class ProviderBFC
    {
        /*
        public static void UpdateActivity(ProviderEDSC.ActivityDTRow drDetail, ProviderEDSC.ActivityContactDetailDTRow contactDetails, ProviderEDSC.ActivityGroupingDTRow drActGrouping, ProviderEDSC.ActivityScheduleDTDataTable dtActSchedule)
        {
            ProviderDAC dac = new ProviderDAC();


            dac.UpdateActivities(drDetail);
            dac.UpdateActivityContactDetail(contactDetails);
            dac.DeleteActivitySchedules(drDetail.ID);
            foreach (var drActSchedule in dtActSchedule)
                dac.CreateActivitySchedule(drActSchedule);
            dac.UpdateActivityGrouping(drActGrouping);


        }

        public static void UpdateActivity(ProviderEDSC.ActivityDTRow drDetail, ProviderEDSC.ActivityContactDetailDTRow contactDetails, ProviderEDSC.ActivityGroupingDTRow drActGrouping)
        {
            ProviderDAC dac = new ProviderDAC();


            dac.UpdateActivities(drDetail);
            dac.UpdateActivityContactDetail(contactDetails);
            dac.DeleteActivitySchedules(drDetail.ID);
            dac.UpdateActivityGrouping(drActGrouping);
        }
        
        public static void SaveActivity(ProviderEDSC.ActivityDTRow ActivityDetailDR, ProviderEDSC.ActivityContactDetailDTRow contactDetailsDR, ProviderEDSC.ActivityGroupingDTRow ActGroupingDR, ProviderEDSC.ActivityScheduleDTDataTable ActScheduleDT, out int activityID)
        {
            ProviderDAC dac = new ProviderDAC();

            using (TransactionScope trans = new TransactionScope())
            {
                activityID = 0;
                dac.CreateActivities(ActivityDetailDR, out activityID);

                //Activity was Created, update all foreign key
                contactDetailsDR.ActivityID = activityID;
                ActGroupingDR.ActivityID = activityID;

                //CreateContactDetails
                dac.CreateActivityContactDetail(contactDetailsDR);

                //Create Schedule
                foreach (var ActScheduleDR in ActScheduleDT)
                {
                    ActScheduleDR.ActivityID = activityID;
                    dac.CreateActivitySchedule(ActScheduleDR);
                }
                //Create Grouping
                dac.CreateActivityGrouping(ActGroupingDR);

                trans.Complete();

            }
        }

        public static void SaveActivity(ProviderEDSC.ActivityDTRow ActivityDetailDR, ProviderEDSC.ActivityContactDetailDTRow contactDetailsDR, ProviderEDSC.ActivityGroupingDTRow ActGroupingDR, out int activityID)
        {
            ProviderDAC dac = new ProviderDAC();

            using (TransactionScope trans = new TransactionScope())
            {
                activityID = 0;
                dac.CreateActivities(ActivityDetailDR, out activityID);

                //Activity was Created, update all foreign key
                contactDetailsDR.ActivityID = activityID;
                ActGroupingDR.ActivityID = activityID;

                //CreateContactDetails
                dac.CreateActivityContactDetail(contactDetailsDR);

                //Create Grouping
                dac.CreateActivityGrouping(ActGroupingDR);

                trans.Complete();

            }
        }
        */
        public static void SaveActivity(ProviderEDSC.ActivityDTRow ActivityDetailDR, ProviderEDSC.ActivityContactDetailDTRow contactDetailsDR, ProviderEDSC.ActivityGroupingDTRow ActGroupingDR, ProviderEDSC.ActivityScheduleDTDataTable ActScheduleDT, ProviderEDSC.ActivityImageDTRow ImageDetail, ProviderEDSC.ActivityImageDetailDTDataTable Images, out int activityID)
        {
            ProviderDAC dac = new ProviderDAC();

            using (TransactionScope trans = new TransactionScope())
            {
                activityID = 0;
                dac.CreateActivities(ActivityDetailDR, out activityID);

                //Activity was Created, Creat other
                contactDetailsDR.ActivityID = activityID;
                ActGroupingDR.ActivityID = activityID;

                //CreateContactDetails
                dac.CreateActivityContactDetail(contactDetailsDR);

                if (ActScheduleDT != null)
                    //Create Schedule
                    foreach (var ActScheduleDR in ActScheduleDT)
                    {
                        ActScheduleDR.ActivityID = activityID;
                        dac.CreateActivitySchedule(ActScheduleDR);
                    }
                //Create Grouping
                dac.CreateActivityGrouping(ActGroupingDR);

                //Create Images
                if (ImageDetail.ImageAmount != 0)
                {
                    ImageDetail.ActivityID = activityID;
                    int imgDetID = 0;
                    dac.createActivityImageInformation(ImageDetail, out imgDetID);
                    int count = 1;
                    foreach (var drImageDetail in Images)
                    {
                        if (count == 1)
                            drImageDetail.isPrimaryImage = true;
                        drImageDetail.ActivityID = activityID;
                        drImageDetail.ActivityImageID = imgDetID;
                        dac.CreateActivityImage(drImageDetail);
                        count++;
                    }
                }
                trans.Complete();
            }
        }

        public static void UpdateActivity(int activityID, ProviderEDSC.ActivityDTRow ActivityDetailDR, ProviderEDSC.ActivityContactDetailDTRow contactDetailsDR, ProviderEDSC.ActivityGroupingDTRow ActGroupingDR, ProviderEDSC.ActivityScheduleDTDataTable ActScheduleDT)
        {
            ProviderDAC dac = new ProviderDAC();
            dac.DeleteActivitySchedules(activityID);
            using (TransactionScope trans = new TransactionScope())
            {
                ActivityDetailDR.ID = contactDetailsDR.ActivityID = ActGroupingDR.ActivityID = activityID;
                //Activity was update, Create other
                dac.UpdateActivities(ActivityDetailDR);


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

        public string RefineSearchKey(string SearchKey)
        {
            if (isImplementAdvanceSearch())
            {
                String SearchPhrase = "";
                var keyCollection = new ProviderDAC().SearchKeywordCollection(SearchKey);
                if (keyCollection.Count == 0 || keyCollection == null)
                    return SearchKey;
                else
                {
                    foreach (var key in keyCollection)
                    {
                        SearchPhrase = SearchPhrase + key.Keywords + ";";
                    }
                }
                return SearchPhrase;
            }
            else
                return SearchKey;


        }

        public bool isImplementAdvanceSearch()
        {
            bool isAdvanceSearch = new ProviderDAC().CheckAdvanceSearch();
            return isAdvanceSearch;
        }

        public bool CheckActivityOwner(int ActivityID, Guid providerID)
        {
            var dr = new ProviderDAC().RetrieveActivity(ActivityID);
            if (dr != null)
            {
                if (dr.ProviderID.Equals(providerID))
                    return true;
                else return false;
            }
            else return false;
        }

        #region ActivityListing
        /*
        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbySearchPhrase(string SearchPhrase, string ProviderID, int amount, int startIndex, String sortExpression)
        {
            ProviderDAC dac = new ProviderDAC();

            var dt = dac.RetrieveActivitiesbySearchPhrase(new Guid(ProviderID), SearchPhrase, startIndex, amount, sortExpression);
            List<int> ls = new List<int>();

            foreach (var dr in dt)
            {
                ls.Add(dr.ID);
            }

            var dtEx = new ProviderEDSC.v_ActivityExplorerDTDataTable();

            foreach (var lsItem in ls)
            {
                dtEx.Addv_ActivityExplorerDTRow(new ProviderDAC().RetrieveActivityExplorer(lsItem));
            }
            return dtEx;
        }

        public int RetrieveProviderActivitiesbySearchPhraseCount(string SearchPhrase, string ProviderID)
        {
            ProviderDAC dac = new ProviderDAC();

            int count = dac.RetrieveActivitiesbySearchPhraseCount(new Guid(ProviderID), SearchPhrase);

            return count;
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(string ProviderID, string categoryID, int amount, int startIndex, String sortExpression)
        {
            ProviderDAC dac = new ProviderDAC();

            var dt = dac.RetrieveProviderActivities(new Guid(ProviderID), startIndex, amount, sortExpression);
            List<int> ls = new List<int>();

            foreach (var dr in dt)
            {
                ls.Add(dr.ID);
            }

            var dtEx = new ProviderDAC().RetrieveActivityExplorersbyIDs(ls);
            return dtEx;
        }

        public int RetrieveProviderActivitiesCount(string ProviderID, string categoryID)
        {
            ProviderDAC dac = new ProviderDAC();

            int count = dac.RetrieveProviderActivitiesCount(new Guid(ProviderID));

            return count;
        }
*/
        # endregion

        #region ActivityImage
        public void CreateActivityImage(ProviderEDSC.ActivityImageDetailDTRow dr, out int imageID, int filesize)
        {
            ProviderDAC dac = new ProviderDAC();
            var iInfo = dac.RetrieveActivityImageInformation(dr.ActivityID);
            int iiID = 0;
            if (iInfo == null)
            {
                dr.isPrimaryImage = true;

                var ii = new ProviderEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
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
            ProviderDAC dac = new ProviderDAC();
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
            var imgDR = new ProviderDAC().RetrieveActivityImage(activityID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetActivityImageURL(activityID, imageID, imgDR.Filename);
            else
                return SystemConstants.ActImageDirectory + "No image.jpg";
        }

        public string RetrieveImageThumbUrl(int activityID, int imageID)
        {
            var imgDR = new ProviderDAC().RetrieveActivityImage(activityID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetActivityImageThumbURL(activityID, imageID, imgDR.Filename);
            else
                return SystemConstants.ActImageDirectory + "No image.jpg";
        }

        public void CreateActivityImages(int ActivityID, ProviderEDSC.ActivityImageDTRow drInfo, ProviderEDSC.ActivityImageDetailDTDataTable dt)
        {
            ProviderDAC dac = new ProviderDAC();
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

        public ProviderEDSC.ActivityScheduleGridDTDataTable RetrieveTimetableGrid(int startIndex, int amount, string activityID, string sortExpression)
        {
            var slots = new ProviderDAC().RetrieveActivitySchedules(Convert.ToInt32(activityID));
            var timeTableDT = CalculateRecurrence(slots);
            timeTableDT.DefaultView.Sort = "EndDateTime ASC";

            if (timeTableDT.Count() != 0)
            {
                var dtTimetable = new ProviderEDSC.ActivityScheduleGridDTDataTable();
                timeTableDT.Skip(startIndex).Take(amount).CopyToDataTable(dtTimetable, LoadOption.PreserveChanges);
                dtTimetable.DefaultView.Sort = "EndDateTime ASC";
                return dtTimetable;
            }
            else
                return null;
        }

        public int RetrieveTimetableGridCount(string activityID)
        {
            var dtSchedule = new ProviderDAC().RetrieveActivitySchedules(Convert.ToInt32(activityID));
            var timeTableDT = CalculateRecurrence(dtSchedule);


            return timeTableDT.Count();
        }

        public ProviderEDSC.ActivityScheduleGridDTDataTable CalculateRecurrence(ProviderEDSC.ActivityScheduleDTRow drSched)
        {
            var timeTableDT = new ProviderEDSC.ActivityScheduleGridDTDataTable();

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

                    ProviderEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
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
                            ProviderEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
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
            return timeTableDT;
        }

        public ProviderEDSC.ActivityScheduleGridDTDataTable CalculateRecurrence(ProviderEDSC.ActivityScheduleDTDataTable dtSched)
        {
            var timeTableDT = new ProviderEDSC.ActivityScheduleGridDTDataTable();
            foreach (var drSched in dtSched)
            {
                if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.NotRecurring)
                {
                    //InsertRecurrenceStartTime
                    DateTime recurStartDate = drSched.ActivityStartDatetime;
                    DateTime recurEndDate = drSched.ActivityEndDatetime;

                    ProviderEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
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

                            ProviderEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
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
                                    ProviderEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
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

        #region UserImage
        public void CreateUserImage(ProviderEDSC.UserImageDetailDTRow dr, out int imageID, int filesize)
        {
            ProviderDAC dac = new ProviderDAC();
            var iInfo = dac.RetrieveUserImageInformation(dr.UserID);
            int iiID = 0;
            if (iInfo == null)
            {
                dr.isPrimaryImage = true;

                var ii = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
                ii.UserID = dr.UserID;
                ii.StorageUsed = 0;
                ii.FreeStorage = SystemConstants.MaxActivityImageStorage;
                ii.ImageAmount = 0;
                dac.CreateUserImageInformation(ii, out iiID);
            }
            else
                dr.UserImageID = iInfo.ID;

            using (TransactionScope trans = new TransactionScope())
            {
                dac.CreateUserImage(dr, out imageID);

                var ii = dac.RetrieveUserImageInformation(dr.UserID);
                ii.UserID = dr.UserID;
                ii.StorageUsed = ii.StorageUsed + filesize;
                ii.FreeStorage = ii.FreeStorage - filesize;
                ii.ImageAmount = ii.ImageAmount + 1;

                dac.UpdateUserImageInformation(dr.UserID, iiID, ii);
                trans.Complete();
            }
        }

        public void DeleteUserImage(Guid UserID, int imageID, int filesize, out string imageThumbVirtualPath, out string imageVirtualPath)
        {
            ProviderDAC dac = new ProviderDAC();
            var dr = dac.RetrieveUserImage(UserID, imageID);
            if (dr.isPrimaryImage == true)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dac.DeleteUserImage(UserID, imageID, out imageThumbVirtualPath, out imageVirtualPath);

                    var ii = dac.RetrieveUserImageInformation(dr.UserID);
                    ii.UserID = dr.UserID;
                    ii.StorageUsed = ii.StorageUsed - filesize;
                    ii.FreeStorage = ii.FreeStorage + filesize;
                    ii.ImageAmount = ii.ImageAmount - 1;

                    dac.UpdateUserImageInformation(dr.UserID, ii.ID, ii);
                    trans.Complete();
                }

                var dt = dac.RetrieveUserImages(UserID);
                if (dt.Count() != 0)
                {
                    Guid UID1 = dt[0].UserID;
                    int imageID1 = dt[0].ID;
                    dac.UpdateUserPrimaryImage(UID1, imageID1);
                }
            }
            else
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dac.DeleteUserImage(UserID, imageID, out imageThumbVirtualPath, out imageVirtualPath);

                    var ii = dac.RetrieveUserImageInformation(dr.UserID);
                    ii.UserID = dr.UserID;
                    ii.StorageUsed = ii.StorageUsed - filesize;
                    ii.FreeStorage = ii.FreeStorage + filesize;
                    ii.ImageAmount = ii.ImageAmount - 1;

                    dac.UpdateUserImageInformation(dr.UserID, ii.ID, ii);
                    trans.Complete();
                }
            }
        }

        public string RetrieveUserImageUrl(Guid userID, int imageID)
        {
            var imgDR = new ProviderDAC().RetrieveUserImage(userID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetUserImageURL(userID, imageID, imgDR.Filename);
            else
                return SystemConstants.UsrImageDirectory + "No image.jpg";
        }

        public string RetrieveUserImageThumbUrl(Guid userID, int imageID)
        {
            var imgDR = new ProviderDAC().RetrieveUserImage(userID, imageID);

            if (!imgDR.IsImageTitleNull())
                return SystemConstants.GetUserImageThumbURL(userID, imageID, imgDR.Filename);
            else
                return SystemConstants.ActImageDirectory + "No image.jpg";
        }
        #endregion


        public void ParseEmail(ProviderEDSC.v_EmailExplorerDTRow emTemp, Guid userID, string token, int EmailTemplateType, int activityID)
        {
            var dr = new ProviderDAC().RetrieveUserProfiles(userID);
            if (dr == null)
            {
                var drprov = new ProviderDAC().RetrieveProviderProfiles(userID);
                if (drprov != null)
                {
                    if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                    {
                        //Provider Fullname
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.ProviderUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        //Provider ConfirmationTokenuri
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        //Provider Confirmationurl
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx");
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.ProviderUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
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
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@recoverylinkwithtoken]", SystemConstants.ProviderUrl + "Account/PasswordRecovery.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.ProviderUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired2week)
                    {
                        var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                    {
                        var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                    {
                        var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                }
            }
            else
            {
                if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                {
                    //Provider Fullname
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.ProviderUrl + "Account/login.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                    //Provider ConfirmationTokenuri
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                    //Provider ConfirmationToken
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                    //Provider Confirmationurl
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.ProviderUrl + "Account/Confirm.aspx");
                    //Provider ConfirmationToken
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.ProviderUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                {
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
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
                    var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                {
                    var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                {
                    var activityDR = new ProviderDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
            }

        }

        public void CreateEmptyUserImage(Guid userID)
        {
            ProviderDAC dac = new ProviderDAC();
            ProviderEDSC.UserImageDTRow userImage = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
            userImage.UserID = userID;
            userImage.StorageUsed = userImage.ImageAmount = 0;
            userImage.FreeStorage = SystemConstants.MaxUserImageStorage;

            int userImageID = 0;
            dac.CreateUserImageInformation(userImage, out userImageID);
        }

        public void CreateNewUserImage(Guid userID, ProviderEDSC.UserImageDTRow userImage, ProviderEDSC.UserImageDetailDTDataTable userImageDetail)
        {
            ProviderDAC dac = new ProviderDAC();
            userImage.UserID = userID;

            int userImageID = 0;
            int notUsed = 0;
            dac.CreateUserImageInformation(userImage, out userImageID);

            foreach (var dr in userImageDetail)
            {
                dr.UserID = userID;
                dr.UserImageID = userImageID;
                dac.CreateUserImage(dr, out  notUsed);
            }
        }

        
        public void AddNewProviderImages(Guid ProviderID, ProviderEDSC.UserImageDetailDTDataTable dt, ProviderEDSC.UserImageDTRow usrImagDet)
        {
            ProviderDAC dac = new ProviderDAC();
            usrImagDet.UserID = ProviderID;

            int userImageID = 0;
            int notUsed = 0;
            dac.UpdateUserImageInformation(usrImagDet);

            foreach (var dr in dt)
            {
                dr.UserID = ProviderID;
                dr.UserImageID = usrImagDet.ID;
                dac.CreateUserImage(dr, out  notUsed);
            }
        }
        public int getProviderPrimaryImage(Guid providerID)
        {
            ProviderDAC dac = new ProviderDAC();

            var dt = dac.RetrieveUserImages(providerID);

            foreach (var dr in dt)
            {
                if (dr.isPrimaryImage)
                {
                    return dr.ID;
                }
            }
            return 0;
        }





        public void ChangeActivityEmailAddress(Guid providerID, string oldEmailAddress, string newEmailAddress)
        {
            var dt = new ProviderDAC().RetrieveProviderActivities(providerID,0,"");
            foreach(var dr in dt)
            {
                if (dr.Email == oldEmailAddress)
                    new ProviderDAC().ChangeActivityEmailAddress(dr.ID, newEmailAddress);
            }
        }
    }
}
