using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthyClub.Provider.EDS;
using HealthyClub.EDM;
using HealthyClub.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.IO;
using BCUtility;

namespace HealthyClub.Provider.DA
{
    public class ProviderDAC
    {
        #region Category
        public int DetermineCategoryLevel(ProviderEDSC.CategoryDTRow categoryDR)
        {
            if (categoryDR.IsLevel1ParentIDNull() || categoryDR.Level1ParentID == 0)
                return 0;
            else if (categoryDR.IsLevel2ParentIDNull() || categoryDR.Level2ParentID == 0)
                return 1;
            else
                return 2;
        }

        public void CreateCategory(ProviderEDSC.CategoryDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Category cat = new Category();

            cat.Name = dr.Name;
            cat.Level1ParentID = dr.Level1ParentID;
            cat.Level2ParentID = dr.Level2ParentID;
            cat.CreatedBy = dr.CreatedBy;
            cat.CreatedDateTime = dr.CreatedDateTime;
            cat.ModifiedBy = dr.ModifiedBy;
            cat.ModifiedDateTime = dr.ModifiedDateTime;

            ent.AddToCategory(cat);
            ent.SaveChanges();
        }

        public void UpdateCategory(ProviderEDSC.CategoryDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Category
                        where c.ID == dr.ID
                        select c;

            Category cat = query.FirstOrDefault();

            if (cat != null)
            {
                cat.Name = dr.Name;
                cat.Level1ParentID = dr.Level1ParentID;
                cat.Level2ParentID = dr.Level2ParentID;
                cat.CreatedBy = dr.CreatedBy;
                cat.CreatedDateTime = dr.CreatedDateTime;
                cat.ModifiedBy = dr.ModifiedBy;
                cat.ModifiedDateTime = dr.ModifiedDateTime;
            }

            ent.SaveChanges();
        }

        public void DeleteCategory(int categoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Category
                        where c.ID == categoryID
                        select c;

            Category cat = query.FirstOrDefault();
            if (cat != null)
            {
                ent.DeleteObject(cat);
                ent.SaveChanges();
            }
        }

        public ProviderEDSC.CategoryDTRow RetrieveCategory(int categoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.ID == categoryID
                        select c;

            var dt = new ProviderEDSC.CategoryDTDataTable();
            var dr = new ProviderEDSC.CategoryDTDataTable().NewCategoryDTRow();
            var category = query.SingleOrDefault();

            if (category == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                //ObjectHandler.CopyProperties(category, dr);
                return dr;
            }
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveAllCategories()
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.CategoryDTDataTable dt = new ProviderEDSC.CategoryDTDataTable();

            var query = from c in ent.v_CategoryExplorer
                        select c;

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                // ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt);
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveLv0Categories()
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.CategoryDTDataTable dt = new ProviderEDSC.CategoryDTDataTable();

            var query = from c in ent.v_CategoryExplorer
                        where c.Level == 0
                        select c;

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                // ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt);               
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveLv1Categories(int rootCatID)
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.CategoryDTDataTable dt = new ProviderEDSC.CategoryDTDataTable();

            var query = from c in ent.v_CategoryExplorer
                        where c.Level == 1 && c.Level1ParentID == rootCatID
                        select c;

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                // ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt);
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveSubCategories(int immediateParentCategoryID, int startIndex, int amount, string sortExpression)
        {
            IQueryable<v_CategoryExplorer> query = null;
            HCEntities ent = new HCEntities();
            sortExpression = "c." + sortExpression;
            int parentLevel = 0;

            if (immediateParentCategoryID != 0)
            {
                var parent = RetrieveCategory(immediateParentCategoryID);
                parentLevel = DetermineCategoryLevel(parent);
            }

            switch (parentLevel)
            {
                case 0:
                    query = from c in ent.v_CategoryExplorer
                            where c.Level1ParentID == immediateParentCategoryID && c.Level2ParentID == 0
                            orderby sortExpression
                            select c;
                    break;
                case 1:
                    query = from c in ent.v_CategoryExplorer
                            where c.Level2ParentID == immediateParentCategoryID
                            orderby sortExpression
                            select c;
                    break;
            }

            ProviderEDSC.CategoryDTDataTable dt = new ProviderEDSC.CategoryDTDataTable();

            if (query != null)
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
            }

            return dt;
        }

        public int RetrieveSubCategoriesCount(int immediateParentCategoryID, string sortExpression)
        {
            IQueryable<v_CategoryExplorer> query = null;
            HCEntities ent = new HCEntities();

            int parentLevel = 0;

            if (immediateParentCategoryID != 0)
            {
                var parent = RetrieveCategory(immediateParentCategoryID);
                parentLevel = DetermineCategoryLevel(parent);
            }

            switch (parentLevel)
            {
                case 0:
                    query = from c in ent.v_CategoryExplorer
                            where c.Level1ParentID == immediateParentCategoryID && c.Level2ParentID == 0
                            select c;
                    break;
                case 1:
                    query = from c in ent.v_CategoryExplorer
                            where c.Level2ParentID == immediateParentCategoryID

                            select c;
                    break;
                case 2:
                    query = from c in ent.v_CategoryExplorer
                            where c.ID == immediateParentCategoryID

                            select c;
                    break;
            }

            return query.Count();
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveAllSubCategories(int parentCategoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.Level1ParentID == parentCategoryID ||
                                c.Level2ParentID == parentCategoryID

                        select c;

            var dt = new ProviderEDSC.CategoryDTDataTable();

            ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);

            return dt;
        }

        public ProviderEDSC.CategoryDTDataTable RetrieveCategories()
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.CategoryDTDataTable dt = new ProviderEDSC.CategoryDTDataTable();

            var query = from c in ent.v_CategoryExplorer
                        select c;

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                // ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt);
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public int RetrieveCategoriesCount()
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Category
                        select c;

            return query.Count();
        }

        public string RetrieveLastCategoryID()
        {
            HCEntities ent = new HCEntities();
            var query = from c in ent.Category
                        orderby c.ID descending
                        select c.ID;

            string categoryID = "";
            if (query.Count() != 0)
            {
                categoryID = query.FirstOrDefault().ToString();
            }
            return categoryID;
        }


        #endregion

        #region suburb

        public void DeleteSuburb(int suburbID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Suburb
                        where c.ID == suburbID
                        select c;

            Suburb sub = query.FirstOrDefault();
            if (sub != null)
            {
                ent.DeleteObject(sub);
                ent.SaveChanges();
            }
        }

        public ProviderEDSC.SuburbDTDataTable RetrieveSuburbs(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.SuburbDTDataTable dt = new ProviderEDSC.SuburbDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        orderby sortExpression
                        select q;

            if (query.AsEnumerable().Count() == 0)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public ProviderEDSC.v_SuburbExplorerDTDataTable RetrieveSuburbs()
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.v_SuburbExplorerDTDataTable dt = new ProviderEDSC.v_SuburbExplorerDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        select q;


            if (query.AsEnumerable().Count() == 0)
                return null;
            else
            {
                query = query.OrderBy(row => row.Name);
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public int RetrieveSuburbsCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.SuburbDTDataTable dt = new ProviderEDSC.SuburbDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        select q;

            return query.Count();
        }

        public void CreateSuburb(string Modifier, ProviderEDSC.SuburbDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Suburb sub = new Suburb();
            sub.Name = dr.Name;
            sub.PostCode = dr.PostCode;
            sub.StateID = dr.StateID;

            ent.AddToSuburb(sub);
            ent.SaveChanges();
        }

        public void UpdateSuburb(string Modifier, ProviderEDSC.SuburbDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.Suburb
                        where s.ID == dr.ID
                        select s;

            Suburb sub = query.FirstOrDefault();

            if (sub != null)
            {
                //ObjectHandler.CopyProperties(dr, sub);
                sub.Name = dr.Name;
                sub.PostCode = dr.PostCode;
                sub.StateID = dr.StateID;
            }

            ent.SaveChanges();
        }

        public ProviderEDSC.v_SuburbExplorerDTRow RetrieveSuburbByID(int suburbID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_SuburbExplorer
                        where s.ID == suburbID
                        select s;

            ProviderEDSC.v_SuburbExplorerDTDataTable dt = new ProviderEDSC.v_SuburbExplorerDTDataTable();
            ProviderEDSC.v_SuburbExplorerDTRow dr = new ProviderEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();


            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                //ObjectHandler.CopyProperties(category, dr);
                return dr;
            }
        }

        public ProviderEDSC.v_SuburbExplorerDTRow RetrieveSuburbByPostCode(int postCode)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_SuburbExplorer
                        where s.PostCode == postCode
                        select s;

            ProviderEDSC.v_SuburbExplorerDTDataTable dt = new ProviderEDSC.v_SuburbExplorerDTDataTable();
            ProviderEDSC.v_SuburbExplorerDTRow dr = new ProviderEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();


            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                //ObjectHandler.CopyProperties(category, dr);
                return dr;
            }
        }
        #endregion

        #region state
        public ProviderEDSC.StateDTDataTable RetrieveStates(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            ProviderEDSC.StateDTDataTable dt = new ProviderEDSC.StateDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public ProviderEDSC.StateDTDataTable RetrieveStates()
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            ProviderEDSC.StateDTDataTable dt = new ProviderEDSC.StateDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public int RetrieveStatesCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            return query.Count();
        }

        public ProviderEDSC.StateDTRow RetrieveState(int stateID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == stateID
                        select s;

            ProviderEDSC.StateDTDataTable dt = new ProviderEDSC.StateDTDataTable();
            ProviderEDSC.StateDTRow dr = new ProviderEDSC.StateDTDataTable().NewStateDTRow();


            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                //ObjectHandler.CopyProperties(category, dr);
                return dr;
            }
        }

        public void CreateState(string userName, ProviderEDSC.StateDTRow dr)
        {
            HCEntities ent = new HCEntities();
            State state = new State();
            state.StateName = dr.StateName;
            state.StateDetail = dr.StateDetail;

            ent.AddToState(state);
            ent.SaveChanges();
        }

        public void UpdateState(string userName, ProviderEDSC.StateDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == dr.ID
                        select s;

            State state = query.FirstOrDefault();

            if (state != null)
            {
                //ObjectHandler.CopyProperties(dr, sub);
                state.StateName = dr.StateName;
                state.StateDetail = dr.StateDetail;
            }

            ent.SaveChanges();
        }

        public void DeleteState(int StateID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == StateID
                        select s;

            State sub = query.FirstOrDefault();
            if (sub != null)
            {
                ent.DeleteObject(sub);
                ent.SaveChanges();
            }
        }
        #endregion

        #region Activity

        #region ActivityRegistration


        public void CreateActivityContactDetail(ProviderEDSC.ActivityContactDetailDTRow activityContactDetailDR)
        {
            HCEntities ent = new HCEntities();
            ActivityContactDetail actCon = new ActivityContactDetail();


            actCon.ActivityID = activityContactDetailDR.ActivityID;
            actCon.Title = activityContactDetailDR.Title;
            actCon.Username = activityContactDetailDR.Username;
            actCon.FirstName = activityContactDetailDR.FirstName;
            actCon.MiddleName = activityContactDetailDR.MiddleName;
            actCon.LastName = activityContactDetailDR.LastName;
            actCon.Email = activityContactDetailDR.Email;
            actCon.Address = activityContactDetailDR.Address;
            actCon.SuburbID = activityContactDetailDR.SuburbID;
            actCon.PostCode = activityContactDetailDR.PostCode;
            actCon.PhoneNumber = activityContactDetailDR.PhoneNumber;
            actCon.MobileNumber = activityContactDetailDR.MobileNumber;
            actCon.StateID = activityContactDetailDR.StateID;

            actCon.AltFirstName = activityContactDetailDR.AltFirstName;
            actCon.AltMiddleName = activityContactDetailDR.AltMiddleName;
            actCon.AltLastName = activityContactDetailDR.AltLastName;
            actCon.AltEmail = activityContactDetailDR.AltEmail;
            actCon.AltAddress = activityContactDetailDR.AltAddress;
            actCon.AltSuburbID = activityContactDetailDR.AltSuburbID;
            actCon.AltPostCode = activityContactDetailDR.AltPostCode;
            actCon.AltPhoneNumber = activityContactDetailDR.AltPhoneNumber;
            actCon.AltMobileNumber = activityContactDetailDR.AltMobileNumber;
            actCon.AltStateID = activityContactDetailDR.AltStateID;

            ent.AddToActivityContactDetail(actCon);
            ent.SaveChanges();
        }

        public void CreateActivityGrouping(ProviderEDSC.ActivityGroupingDTRow activityGroupDR)
        {
            HCEntities ent = new HCEntities();

            ActivityGrouping actGroup = new ActivityGrouping();

            actGroup.ActivityID = activityGroupDR.ActivityID;
            actGroup.forMale = activityGroupDR.forMale;
            actGroup.forFemale = activityGroupDR.forFemale;

            actGroup.forChildren = activityGroupDR.forChildren;
            actGroup.AgeFrom = activityGroupDR.AgeFrom;
            actGroup.AgeTo = activityGroupDR.AgeTo;

            ent.AddToActivityGrouping(actGroup);
            ent.SaveChanges();
        }

        public void CreateActivities(ProviderEDSC.ActivityDTRow activityDR, out int activityID)
        {
            HCEntities ent = new HCEntities();
            Activity act = new Activity();
            ObjectHandler.CopyPropertyValues(activityDR, act);

            ent.AddToActivity(act);
            ent.SaveChanges();

            activityID = act.ID;
        }

        public void CreateActivitySchedule(ProviderEDSC.ActivityScheduleDTRow ActScheduleDR)
        {
            HCEntities ent = new HCEntities();

            ActivitySchedule ActSched = new ActivitySchedule();
            ObjectHandler.CopyPropertyValues(ActScheduleDR, ActSched);

            ent.AddToActivitySchedule(ActSched);
            ent.SaveChanges();

        }
        #endregion

        #region ActivityImage

        public void createActivityImageInformation(ProviderEDSC.ActivityImageDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ActivityImage ii = new ActivityImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToActivityImage(ii);
            ent.SaveChanges();
        }

        public void createActivityImageInformation(ProviderEDSC.ActivityImageDTRow dr, out int iiID)
        {
            HCEntities ent = new HCEntities();
            ActivityImage ii = new ActivityImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToActivityImage(ii);
            ent.SaveChanges();
            iiID = ii.ID;
        }

        public void UpdateImageInformation(int activityID, int iiID, ProviderEDSC.ActivityImageDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ii in ent.ActivityImage
                        where ii.ActivityID == dr.ActivityID && ii.ID == dr.ID
                        select ii;

            ActivityImage actImage = query.FirstOrDefault();
            if (actImage != null)
                ObjectHandler.CopyPropertyValues(dr, actImage);

            ent.SaveChanges();
        }

        public void CreateActivityImage(ProviderEDSC.ActivityImageDetailDTRow dr, out int imageID1)
        {
            HCEntities ent = new HCEntities();
            ActivityImageDetail ai = new ActivityImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToActivityImageDetail(ai);
            ent.SaveChanges();
            imageID1 = ai.ID;
        }

        public void CreateActivityImage(ProviderEDSC.ActivityImageDetailDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ActivityImageDetail ai = new ActivityImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToActivityImageDetail(ai);
            ent.SaveChanges();
        }

        public void UpdateActivityImage(ProviderEDSC.ActivityImageDetailDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ActivityImageDetail
                        where p.ActivityID == dr.ActivityID && p.ID == dr.ID
                        select p;

            ActivityImageDetail actImage = query.FirstOrDefault();
            dr.ImageStream = actImage.ImageStream;
            if (actImage != null)
                ObjectHandler.CopyPropertyValues(dr, actImage);

            ent.SaveChanges();
        }

        public void DeleteActivityImage(int activityID, int imageID, out string imageThumbVirtualPath, out string imageVirtualPath)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ActivityImageDetail
                        where p.ActivityID == activityID && p.ID == imageID
                        select p;

            ActivityImageDetail prodImage = query.FirstOrDefault();
            imageVirtualPath = SystemConstants.ActImageDirectory + "/" + activityID + "/" + activityID + "_" + imageID + "_" + prodImage.Filename;
            imageThumbVirtualPath = SystemConstants.ActImageDirectory + "/" + activityID + "/" + SystemConstants.ImageThumbDirectory + activityID + "_" + imageID + "_" + prodImage.Filename;
            if (prodImage != null)
                ent.DeleteObject(prodImage);

            ent.SaveChanges();
        }

        public ProviderEDSC.ActivityImageDTRow RetrieveActivityImageInformation(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImage
                        where i.ActivityID == activityID
                        select i;
            var ii = query.FirstOrDefault();
            if (ii != null)
            {
                ProviderEDSC.ActivityImageDTRow dr = new ProviderEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
                ObjectHandler.CopyPropertyValues(ii, dr);
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.ActivityImageDetailDTDataTable RetrieveActivityImages(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID
                        orderby i.ID
                        select i;

            ProviderEDSC.ActivityImageDetailDTDataTable dt = new ProviderEDSC.ActivityImageDetailDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);

            return dt;
        }

        public int RetrieveActivityImagesCount(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID
                        orderby i.ID
                        select i;

            return query.Count();
        }

        public ProviderEDSC.ActivityImageDetailDTRow RetrievePrimaryProductImage(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.isPrimaryImage == true
                        select i;

            ProviderEDSC.ActivityImageDetailDTRow dr = new ProviderEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            if (query.FirstOrDefault() != null)
            {
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
            }
            return dr;
        }

        public ProviderEDSC.ActivityImageDetailDTRow RetrieveProductMainImage(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.isPrimaryImage == true
                        select i;

            ProviderEDSC.ActivityImageDetailDTRow dr = new ProviderEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            ActivityImageDetail pi = query.FirstOrDefault();
            if (pi != null)
                ObjectHandler.CopyPropertyValues(pi, dr);
            else
            {
                dr.ActivityID = 0;
                dr.ID = 0;
            }
            return dr;
        }

        public ProviderEDSC.v_ActivityImageExplorerDTRow RetrieveActivityImage(int activityID, int imageID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.v_ActivityImageExplorer
                        where i.ActivityID == activityID && i.ImageID == imageID
                        select i;

            ProviderEDSC.v_ActivityImageExplorerDTRow dr = new ProviderEDSC.v_ActivityImageExplorerDTDataTable().Newv_ActivityImageExplorerDTRow();
            v_ActivityImageExplorer ai = query.FirstOrDefault();
            if (ai != null)
                ObjectHandler.CopyPropertyValues(ai, dr);

            return dr;
        }

        public void UpdateActivityPrimaryImage(int activityID, int imageID)
        {
            HCEntities ent = new HCEntities();
            var setMainFalse = from fi in ent.ActivityImageDetail
                               where fi.isPrimaryImage == true && fi.ActivityID == activityID
                               select fi;

            ActivityImageDetail pif = setMainFalse.FirstOrDefault();
            if (pif != null)
            {
                pif.isPrimaryImage = false;
                ent.SaveChanges();
            }

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.ID == imageID
                        select i;

            ActivityImageDetail pit = query.FirstOrDefault();
            if (pit != null)
            {
                pit.isPrimaryImage = true;
                ent.SaveChanges();
            }
        }
        #endregion

        #region ActivitySchedule


        #endregion

        public void DeleteActivity(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == activityID
                        select a;

            Activity act = query.FirstOrDefault();
            if (act != null)
            {
                ent.DeleteObject(act);
                ent.SaveChanges();
            }
        }

        public ProviderEDSC.ActivityScheduleDTDataTable RetrieveActivitySchedules(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.ActivitySchedule
                        where a.ActivityID == ActivityID
                        select a;

            var actSched = query.AsEnumerable();

            if (actSched.Count() != 0)
            {
                var dt = new ProviderEDSC.ActivityScheduleDTDataTable();
                actSched.CopyEnumerableToDataTable(dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public ProviderEDSC.ActivityGroupingDTRow RetrieveActivityGroup(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from g in ent.ActivityGrouping
                        where g.ActivityID == ActivityID
                        select g;

            if (query != null)
            {
                ActivityGrouping group = query.FirstOrDefault();
                var dr = new ProviderEDSC.ActivityGroupingDTDataTable().NewActivityGroupingDTRow();
                ObjectHandler.CopyPropertyValues(group, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateActivities(ProviderEDSC.ActivityDTRow drDetail)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == drDetail.ID
                        select a;

            Activity act = query.FirstOrDefault();
            if (act != null)
            {
                drDetail.ID = act.ID;
                ObjectHandler.CopyPropertyValues(drDetail, act);
                ent.SaveChanges();
            }
        }

        public void UpdateActivityContactDetail(ProviderEDSC.ActivityContactDetailDTRow contactDetails)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.ActivityContactDetail
                        where ac.ActivityID == contactDetails.ActivityID
                        select ac;

            ActivityContactDetail act = query.FirstOrDefault();
            if (act != null)
            {
                contactDetails.ID = act.ID;
                ObjectHandler.CopyPropertyValues(contactDetails, act);
                ent.SaveChanges();
            }
        }

        public void UpdateActivityGrouping(ProviderEDSC.ActivityGroupingDTRow drActGrouping)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.ActivityGrouping
                        where ac.ActivityID == drActGrouping.ActivityID
                        select ac;

            ActivityGrouping act = query.FirstOrDefault();
            if (act != null)
            {
                drActGrouping.ID = act.ID;
                ObjectHandler.CopyPropertyValues(drActGrouping, act);
                ent.SaveChanges();
            }
        }

        public void DeleteActivitySchedules(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from asched in ent.ActivitySchedule
                        where asched.ActivityID == activityID
                        select asched;

            var Slots = query.AsEnumerable();

            foreach (var slot in Slots)
            {
                DeleteActivitySchedule(slot.ID);
            }
        }

        private void DeleteActivitySchedule(int activityScheduleID)
        {
            HCEntities ent = new HCEntities();

            var query = from asched in ent.ActivitySchedule
                        where asched.ID == activityScheduleID
                        select asched;

            ActivitySchedule schedule = query.FirstOrDefault();
            if (schedule != null)
            {
                ent.DeleteObject(schedule);
                ent.SaveChanges();
            }
        }

        public void ChangeStatus(int actID, bool isActive)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == actID
                        select a;
            Activity act = query.FirstOrDefault();

            if (act != null)
            {
                if (isActive)
                    act.Status = (int)SystemConstants.ActivityStatus.Active;
                else
                    act.Status = (int)SystemConstants.ActivityStatus.NotActive;

                ent.SaveChanges();
            }
        }
        public void ChangeStatus(int actID, int Status, string Username)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == actID
                        select a;
            Activity act = query.FirstOrDefault();

            if (act != null)
            {
                act.Status = Status;
                act.ModifiedBy = Username;
                act.ModifiedDateTime = DateTime.Now;
                ent.SaveChanges();
            }
        }

        #endregion



        #region Keyword

        public bool CheckAdvanceSearch()
        {
            HCEntities ent = new HCEntities();
            ProviderEDSC.WebConfigurationDTDataTable dt = new ProviderEDSC.WebConfigurationDTDataTable();
            ProviderEDSC.WebConfigurationDTRow dr = new ProviderEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
            var query = from w in ent.WebConfiguration
                        select w;

            if (query.FirstOrDefault() != null)
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.OverwriteChanges);
                foreach (var drow in dt)
                {
                    dr.AdvancedSearch = drow.AdvancedSearch;
                }
            }
            return dr.AdvancedSearch;
        }

        public ProviderEDSC.v_KeyCollectionViewDTDataTable SearchKeywordCollection(String searchKey)
        {
            String[] Keywords = Array.ConvertAll(searchKey.Split(' '), c => c.Trim());

            HCEntities ent = new HCEntities();

            var dt = new ProviderEDSC.v_KeyCollectionViewDTDataTable();

            var query = (from a in ent.Keyword
                         from w in Keywords
                         where a.Keywords.ToLower().Contains(w.ToLower())
                         select a).Distinct();

            if (query.AsEnumerable() != null)
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.OverwriteChanges);
            }
            else
                return null;
            return dt;
        }
        #endregion

        #region ActivityListing
        /*ActivityView contains attributes used in reports only, ActivityExplorer contains complete 
         *attributes used in activity listing and details. use activity ID to retrieve the activities
         *you want to be show. int[] ActivityID. */

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbySearchPhrase(Guid providerID, string searchKey, int startIndex, int amount, string sortExpression)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query = from a in ent.v_ActivityExplorer
                                                   where a.ProviderID == providerID
                                                   orderby a.ID
                                                   select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                               || a.Keywords.ToLower().Contains(w.ToLower())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_ActivityExplorer> activities = Suggestions.AsEnumerable();

            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPrice)
                activities = activities.OrderByDescending(row => row.IsPaid);

            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPriceDesc)
                activities = activities.OrderBy(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityType);

            activities = activities.Skip(startIndex).Take(amount).AsEnumerable();

            ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
            dt.OrderBy(row => row.Name);
            return dt;
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbySearchPhrase(Guid providerID, string searchKey, string sortExpression)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query = from a in ent.v_ActivityExplorer
                                                   where a.ProviderID == providerID
                                                   orderby a.ID
                                                   select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                               || a.Keywords.ToLower().Contains(w.ToLower())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_ActivityExplorer> activities = Suggestions.AsEnumerable();
            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPrice)
                activities = activities.OrderByDescending(row => row.IsPaid);

            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPriceDesc)
                activities = activities.OrderBy(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityType);
            ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesReportbySearchPhrase(Guid providerID, string searchKey, string sortExpression)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityView> query = from a in ent.v_ActivityView
                                               where a.ProviderID == providerID
                                               orderby a.ID
                                               select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                               || a.Keywords.ToLower().Contains(w.ToLower())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_ActivityView> activities = Suggestions.AsEnumerable();
            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);
            ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public int RetrieveProviderActivitiesbySearchPhraseCount(Guid providerID, string searchKey)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query = from a in ent.v_ActivityExplorer
                                                   where a.ProviderID == providerID
                                                   orderby a.ID
                                                   select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                               || a.Keywords.ToLower().Contains(w.ToLower())
                               select a).Distinct().ToArray();

            return Suggestions.Count();
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesReport(Guid providerID, int categoryID, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityView> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityView
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }

            IEnumerable<v_ActivityView> activities = query.AsEnumerable();
            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);
            ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
            dt.OrderBy(row => row.Name);
            return dt;
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesFilteredReport(Guid ProviderID, string SearchKey, int ageFrom, int ageTo, int postCode, int CategoryID, int startIndex, int amount, string sortExpression)
        {

            HCEntities ent = new HCEntities();
            IEnumerable<v_ActivityView> activitiesReport;

            IQueryable<v_ActivityView> providerCatFiltered;
            IQueryable<v_ActivityView> paramFiltered;
            if (CategoryID == 0)
                providerCatFiltered = from a in ent.v_ActivityView
                                      where a.ProviderID == ProviderID
                                      orderby a.ID
                                      select a;
            else
                providerCatFiltered = from a in ent.v_ActivityView
                                      where (a.ProviderID == ProviderID && (
                                                      a.CategoryID == CategoryID ||
                                                      a.CategoryLevel1ParentID == CategoryID ||
                                                      a.CategoryLevel2ParentID == CategoryID))
                                      select a;


            paramFiltered = from a in providerCatFiltered
                            where a.AgeFrom >= ageFrom && a.AgeTo <= ageTo
                            select a;

            if (postCode != 0)
                paramFiltered = from a in providerCatFiltered
                                where a.SuburbID == postCode
                                select a;

            if (!string.IsNullOrEmpty(SearchKey))
            {
                String[] Keywords = Array.ConvertAll(SearchKey.Split(';'), c => c.Trim());

                var querySearchFiltered = (from a in paramFiltered
                                           from w in Keywords
                                           where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                                           || a.Keywords.ToLower().Contains(w.ToLower())
                                           select a).Distinct().ToArray();
                activitiesReport = querySearchFiltered.AsEnumerable();
            }
            else
            {
                activitiesReport = paramFiltered.AsEnumerable();
            }

            var dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            if (activitiesReport != null)
            {
                ObjectHandler.CopyEnumerableToDataTable(activitiesReport, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else
                return null;
        }

        public int RetrieveProviderActivitiesFilteredReportCount(Guid ProviderID, string SearchKey, int ageFrom, int ageTo, int postCode, int CategoryID)
        {

            HCEntities ent = new HCEntities();
            IEnumerable<v_ActivityView> activitiesReport;

            IQueryable<v_ActivityView> providerCatFiltered;
            IQueryable<v_ActivityView> paramFiltered;
            if (CategoryID == 0)
                providerCatFiltered = from a in ent.v_ActivityView
                                      where a.ProviderID == ProviderID
                                      orderby a.ID
                                      select a;
            else
                providerCatFiltered = from a in ent.v_ActivityView
                                      where (a.ProviderID == ProviderID && (
                                                      a.CategoryID == CategoryID ||
                                                      a.CategoryLevel1ParentID == CategoryID ||
                                                      a.CategoryLevel2ParentID == CategoryID))
                                      select a;


            paramFiltered = from a in providerCatFiltered
                            where a.AgeFrom >= ageFrom && a.AgeTo <= ageTo
                            select a;

            if (postCode != 0)
                paramFiltered = from a in providerCatFiltered
                                where a.SuburbID == postCode
                                select a;

            if (!string.IsNullOrEmpty(SearchKey))
            {
                String[] Keywords = Array.ConvertAll(SearchKey.Split(';'), c => c.Trim());

                var querySearchFiltered = (from a in paramFiltered
                                           from w in Keywords
                                           where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                                           || a.Keywords.ToLower().Contains(w.ToLower())
                                           select a).Distinct().ToArray();
                activitiesReport = querySearchFiltered.AsEnumerable();
            }
            else
            {
                activitiesReport = paramFiltered.AsEnumerable();
            }

            var dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            if (activitiesReport != null)
            {

                return activitiesReport.Count();
            }
            else
                return 0;
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }



            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();

            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPrice)
                activities = activities.OrderByDescending(row => row.IsPaid);

            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPriceDesc)
                activities = activities.OrderBy(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityType);

            activities = activities.Skip(startIndex).Take(amount).AsEnumerable();

            ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(Guid providerID, int categoryID, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }



            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();

            if (sortExpression == SystemConstants.sortName)
                activities = activities.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                activities = activities.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPrice)
                activities = activities.OrderByDescending(row => row.IsPaid);

            else if (sortExpression == SystemConstants.sortNameDesc)
                activities = activities.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPriceDesc)
                activities = activities.OrderBy(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                activities = activities.OrderByDescending(row => row.ActivityType);

            ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public int RetrieveProviderActivitiesCount(Guid providerID, int categoryID)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID
                        select a;
            }

            return query.Count();
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbyCategoryID(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }

            var activities = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (activities != null)
            {
                ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else
                return null;
        }

        public int RetrieveProviderActivitiesbyCategoryIDCount(Guid providerID, int categoryID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID
                        select a;
            }
            return query.Count();
        }

        public ProviderEDSC.v_ActivityExplorerDTRow RetrieveActivityExplorer(int ActivityID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    where a.ID == ActivityID
                    select a;

            if (query.Count() == 0)
                return null;
            var activity = query.FirstOrDefault();

            if (activity != null)
            {
                ProviderEDSC.v_ActivityExplorerDTRow dr = new ProviderEDSC.v_ActivityExplorerDTDataTable().Newv_ActivityExplorerDTRow();
                ObjectHandler.CopyPropertyValues(activity, dr);
                if (string.IsNullOrEmpty(activity.Suburb))
                {
                    dr.Suburb = "";
                }
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.v_ActivityViewDTRow RetrieveActivityView(int ActivityID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;

            query = from a in ent.v_ActivityView
                    where a.ID == ActivityID
                    select a;

            if (query.Count() == 0)
                return null;
            IEnumerable<v_ActivityView> activities = query.AsEnumerable();

            if (activities != null)
            {
                ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
                ProviderEDSC.v_ActivityViewDTRow dr = new ProviderEDSC.v_ActivityViewDTDataTable().Newv_ActivityViewDTRow();

                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveActivityExplorersbyIDs(int[] activityID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    from id in activityID
                    where a.ID.ToString().Contains(activityID.ToString())
                    select a;

            if (query.Count() == 0)
                return null;
            IEnumerable<v_ActivityExplorer> activities = query.Skip(startIndex).Take(amount).AsEnumerable();

            if (activities != null)
            {
                ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }

        public ProviderEDSC.v_ActivityExplorerDTDataTable RetrieveActivityExplorersbyIDs(List<int> activityID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    where activityID.Contains(a.ID)
                    select a;



            if (query.Count() == 0)
                return null;
            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();

            if (activities != null)
            {
                ProviderEDSC.v_ActivityExplorerDTDataTable dt = new ProviderEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }

        public int RetrieveActivityExplorersbyIDsCount(int[] activityID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    from id in activityID
                    where a.ID.ToString().Contains(activityID.ToString())
                    select a;

            return query.Count();
        }

        public ProviderEDSC.ActivityDTRow RetrieveActivity(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == activityID
                        select a;

            if (query.Count() == 0)
                return null;
            IEnumerable<Activity> activities = query.AsEnumerable();

            if (activities != null)
            {
                ProviderEDSC.ActivityDTDataTable dt = new ProviderEDSC.ActivityDTDataTable();
                ProviderEDSC.ActivityDTRow dr = new ProviderEDSC.ActivityDTDataTable().NewActivityDTRow();

                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                return dr;
            }
            else
                return null;
        }

        /*
        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveSearchActivities(string searchKey, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.v_ActivityView
                        where a.Name.Contains(searchKey) || a.CategoryName.Contains(searchKey)
                        || a.Name.Contains(searchKey) || a.CategoryLevel1ParentName.Contains(searchKey)
                        || a.CategoryLevel2ParentName.Contains(searchKey)
                        orderby sortExpression
                        select a;

            IEnumerable<v_ActivityView> activities = query.Skip(startIndex).Take(amount).AsEnumerable();

            ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public int RetrieveSearchActivitiesCount(string searchKey)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.v_ActivityView
                        where a.Name.Contains(searchKey) || a.CategoryName.Contains(searchKey)
                        || a.Name.Contains(searchKey) || a.CategoryLevel1ParentName.Contains(searchKey)
                        || a.CategoryLevel2ParentName.Contains(searchKey)
                        select a;

            return query.Count();
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveSearchProviderActivities(Guid providerID, String searchKey, int startIndex, int amount, string sortExpression)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityView> query = from a in ent.v_ActivityView
                                               where a.ProviderID == providerID
                                               orderby a.ID
                                               select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.ToLower().Contains(w.ToLower()) || a.ShortDescription.ToLower().Contains(w.ToLower())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_ActivityView> activities = Suggestions.Skip(startIndex).Take(amount).AsEnumerable();

            ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return dt;
        }

        public int RetrieveSearchProviderActivitiesCount(Guid providerID, string searchKey)
        {
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();

            //filtering ID  
            IQueryable<v_ActivityView> query = from a in ent.v_ActivityView
                                               where a.ProviderID == providerID
                                               select a;

            var Suggestions = (from a in query
                               from w in Keywords
                               where a.Name.Contains(w)
                               select a).Distinct().ToArray();

            return Suggestions.Count();
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveActivityViews(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityView
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }

            var activities = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (activities != null)
            {
                ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else
                return null;
        }

        public int RetrieveActivityViewsCount(Guid providerID, int categoryID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityView
                        where a.ProviderID == providerID
                        select a;
            }
            return query.Count();
        }

        public ProviderEDSC.v_ActivityViewDTDataTable RetrieveActivityViewsFromActivitiesIDArray(Guid providerID, int[] activitiesID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityView
                        where (a.CategoryID == categoryID ||
                                   a.CategoryLevel1ParentID == categoryID ||
                                   a.CategoryLevel2ParentID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityView
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }

            var activities = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (activities != null)
            {
                ProviderEDSC.v_ActivityViewDTDataTable dt = new ProviderEDSC.v_ActivityViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else
                return null;
        }

        public int RetrieveActivityViewsFromActivitiesIDArrayCount(Guid providerID, int[] activitiesID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_ActivityView
                        where a.ProviderID == providerID
                        select a;
            }
            else
            {
                query = (from a in query
                         from w in activitiesID
                         where a.ID.ToString().Contains(w.ToString())
                         select a).Distinct().ToArray();
            }

            return query.Count();
        }

        public ProviderEDSC.ActivityDTDataTable RetrieveActivities(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<Activity> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.Activity
                        where (a.ProviderID == providerID &&
                                        a.CategoryID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.Activity
                        where (a.CategoryID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.Activity
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }

            var activities = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (activities != null)
            {
                ProviderEDSC.ActivityDTDataTable dt = new ProviderEDSC.ActivityDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else
                return null;
        }

        public int RetrieveActivitiesCount(Guid providerID, int categoryID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<Activity> query = null;
            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.Activity
                        where (a.ProviderID == providerID &&
                                        a.CategoryID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.Activity
                        where (a.CategoryID == categoryID)
                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.Activity
                        where a.ProviderID == providerID
                        orderby sortExpression
                        select a;
            }
            return query.Count();
        }
        */
        #endregion

        #region Menu

        public ProviderEDSC.MenuDTDataTable RetrieveChildMenus(int parentID)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.Menu
                        where m.ParentMenuID == parentID
                        select m;

            IEnumerable<Menu> Menus = query.AsEnumerable();
            if (Menus == null)
                return null;
            else
            {
                ProviderEDSC.MenuDTDataTable dt = new ProviderEDSC.MenuDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Menus, dt, LoadOption.PreserveChanges);
                return dt;
            }

        }
        public ProviderEDSC.v_MenuDTDataTable RetrieveMenuExplorers(int menuType)
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.v_Menu
                        where v.MenuType == menuType
                        orderby v.Sequence
                        select v;


            ProviderEDSC.v_MenuDTDataTable dt = new ProviderEDSC.v_MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }

        public ProviderEDSC.MenuDTRow RetrieveMenu(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.Menu
                        where m.ID == SelectedMenuID
                        select m;

            Menu menu = query.FirstOrDefault();
            ProviderEDSC.MenuDTRow dr = new ProviderEDSC.MenuDTDataTable().NewMenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.v_MenuDTRow RetrieveMenuExplorer(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.v_Menu
                        where m.ID == SelectedMenuID
                        select m;

            v_Menu menu = query.FirstOrDefault();
            ProviderEDSC.v_MenuDTRow dr = new ProviderEDSC.v_MenuDTDataTable().Newv_MenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.MenuDTDataTable RetrieveMenus()
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.Menu
                        orderby v.Sequence
                        select v;


            ProviderEDSC.MenuDTDataTable dt = new ProviderEDSC.MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }
        #endregion

        #region page

        public ProviderEDSC.PageDTRow RetrievePage(int pageID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.ID == pageID
                        select p;

            var page = query.FirstOrDefault();

            if (page != null)
            {
                var dr = new ProviderEDSC.PageDTDataTable().NewPageDTRow(); ;
                ObjectHandler.CopyPropertyValues(page, dr);
                return dr;
            }
            else return null;

        }

        public ProviderEDSC.PageDTDataTable RetrievePages(int PageType, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.PageType == PageType
                        orderby p.Name
                        select p;

            IEnumerable<Page> page = query.Skip(startIndex).Skip(amount).AsEnumerable();
            if (page != null)
            {
                ProviderEDSC.PageDTDataTable dt = new ProviderEDSC.PageDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(page, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int RetrievePagesCount(int PageType, int startIndex, int Amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.PageType == PageType
                        orderby p.Name
                        select p;

            return query.Count();
        }

        #endregion

        public ProviderEDSC.WebConfigurationDTRow RetrieveWebImage()
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.WebConfiguration
                        select i;
            WebConfiguration web = query.FirstOrDefault();
            ProviderEDSC.WebConfigurationDTRow dr = new ProviderEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
            if (web != null)
            {
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;

        }

        public ProviderEDSC.WebConfigurationDTRow RetrieveEmailServerSetting()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                ProviderEDSC.WebConfigurationDTRow dr = new ProviderEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);

                if (dr.IsSMTPAccountNull())
                    return null;
                else return dr;
            }
            else return null;
        }

        public ProviderEDSC.WebConfigurationDTRow RetrieveWebConfiguration()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                var dr = new ProviderEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;
        }

        #region ActImage

        public byte[] RetrieveImageBinary(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.ActivityImageDetail
                        where e.ID == imgID
                        select e;
            ActivityImageDetail aid = query.FirstOrDefault();

            if (aid != null && aid.ImageStream != null)
            {
                byte[] stream = aid.ImageStream;
                return stream;
            }
            else return null;
        }

        public byte[] RetrieveUserImageBinary(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.UserImageDetail
                        where e.ID == imgID
                        select e;
            UserImageDetail aid = query.FirstOrDefault();

            if (aid != null && aid.ImageStream != null)
            {
                byte[] stream = aid.ImageStream;
                return stream;
            }
            else return null;
        }

        public ProviderEDSC.ActivityImageDetailDTRow RetrieveImageDetail(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.ActivityImageDetail
                        where e.ID == imgID
                        select e;

            if (query.FirstOrDefault() != null)
            {
                var dr = new ProviderEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;
        }

        #endregion

        #region UserImage
        public void CreateUserImageInformation(ProviderEDSC.UserImageDTRow dr)
        {
            HCEntities ent = new HCEntities();
            UserImage ii = new UserImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToUserImage(ii);
            ent.SaveChanges();
        }

        public void CreateUserImageInformation(ProviderEDSC.UserImageDTRow dr, out int iiID)
        {
            HCEntities ent = new HCEntities();
            UserImage ii = new UserImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToUserImage(ii);
            ent.SaveChanges();
            iiID = ii.ID;
        }

        public void UpdateUserImageInformation(Guid userID, int iiID, ProviderEDSC.UserImageDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ii in ent.UserImage
                        where ii.UserID == userID && ii.ID == dr.ID
                        select ii;

            UserImage actImage = query.FirstOrDefault();
            if (actImage != null)
                ObjectHandler.CopyPropertyValues(dr, actImage);

            ent.SaveChanges();
        }

        public void CreateUserImage(ProviderEDSC.UserImageDetailDTRow dr, out int imageID1)
        {
            HCEntities ent = new HCEntities();
            UserImageDetail ai = new UserImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToUserImageDetail(ai);
            ent.SaveChanges();
            imageID1 = ai.ID;
        }

        public void UpdateUserImage(ProviderEDSC.UserImageDetailDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.UserImageDetail
                        where p.ID == dr.ID
                        select p;

            UserImageDetail actImage = query.FirstOrDefault();
            dr.Filename = actImage.Filename;
            dr.isPrimaryImage = Convert.ToBoolean(actImage.isPrimaryImage);

            if (actImage != null)
                ObjectHandler.CopyPropertyValues(dr, actImage);

            ent.SaveChanges();
        }

        public void DeleteUserImage(Guid UserID, int imageID, out string imageThumbVirtualPath, out string imageVirtualPath)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.UserImageDetail
                        where p.ID == imageID
                        select p;

            UserImageDetail prodImage = query.FirstOrDefault();
            imageVirtualPath = SystemConstants.UsrImageDirectory + "/" + UserID + "/" + imageID + "_" + prodImage.Filename;
            imageThumbVirtualPath = SystemConstants.UsrImageDirectory + "/" + UserID + "/" + SystemConstants.ImageThumbDirectory + imageID + "_" + prodImage.Filename;
            if (prodImage != null)
                ent.DeleteObject(prodImage);

            ent.SaveChanges();
        }

        public ProviderEDSC.UserImageDTRow RetrieveUserImageInformation(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImage
                        where i.UserID == UserID
                        select i;
            var ii = query.FirstOrDefault();
            if (ii != null)
            {
                ProviderEDSC.UserImageDTRow dr = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
                ObjectHandler.CopyPropertyValues(ii, dr);
                return dr;
            }
            else
                return null;
        }

        public ProviderEDSC.UserImageDetailDTDataTable RetrieveUserImages(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID
                        orderby i.ID
                        select i;

            ProviderEDSC.UserImageDetailDTDataTable dt = new ProviderEDSC.UserImageDetailDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);

            return dt;
        }

        public int RetrieveUserImagesCount(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID
                        orderby i.ID
                        select i;

            return query.Count();
        }

        public ProviderEDSC.UserImageDetailDTRow RetrievePrimaryProductImage(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.isPrimaryImage == true
                        select i;

            ProviderEDSC.UserImageDetailDTRow dr = new ProviderEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
            if (query.FirstOrDefault() != null)
            {
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
            }
            return dr;
        }

        public ProviderEDSC.UserImageDetailDTRow RetrieveProductMainImage(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.isPrimaryImage == true
                        select i;

            ProviderEDSC.UserImageDetailDTRow dr = new ProviderEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
            UserImageDetail pi = query.FirstOrDefault();
            if (pi != null)
                ObjectHandler.CopyPropertyValues(pi, dr);
            else
            {
                dr.UserID = Guid.Empty;
                dr.ID = 0;
            }
            return dr;
        }

        public ProviderEDSC.UserImageDetailDTRow RetrieveUserImage(Guid UserID, int imageID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.ID == imageID
                        select i;

            ProviderEDSC.UserImageDetailDTRow dr = new ProviderEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
            UserImageDetail ai = query.FirstOrDefault();
            if (ai != null)
                ObjectHandler.CopyPropertyValues(ai, dr);

            return dr;
        }

        public void UpdateUserPrimaryImage(Guid UserID, int imageID)
        {
            HCEntities ent = new HCEntities();
            var setMainFalse = from fi in ent.UserImageDetail
                               where fi.isPrimaryImage == true && fi.UserID == UserID
                               select fi;

            UserImageDetail pif = setMainFalse.FirstOrDefault();
            if (pif != null)
            {
                pif.isPrimaryImage = false;
                ent.SaveChanges();
            }

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.ID == imageID
                        select i;

            UserImageDetail pit = query.FirstOrDefault();
            if (pit != null)
            {
                pit.isPrimaryImage = true;
                ent.SaveChanges();
            }
        }

        public bool IsUserImageExist(Guid providerID)
        {
            HCEntities ent = new HCEntities();
            var query = from i in ent.UserImage
                        where i.UserID == providerID
                        select i;

            if (query.FirstOrDefault() != null)
            {
                if (query.FirstOrDefault().ImageAmount != 0)
                    return true;
                else return false;
            }
            else return false;

        }

        #endregion

        #region emailtemplate

        public ProviderEDSC.v_EmailExplorerDTRow RetrieveMailTemplate(int templateType)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.v_EmailExplorer
                        where e.EmailType == templateType
                        select e;

            if (query.FirstOrDefault() != null)
            {
                var dr = new ProviderEDSC.v_EmailExplorerDTDataTable().Newv_EmailExplorerDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;
        }

        #endregion

        #region Users

        public ProviderEDSC.v_UserExplorerDTDataTable RetrieveUserExplorer()
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.v_UserExplorer
                        select u;

            if (query != null)
            {
                var dt = new ProviderEDSC.v_UserExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public bool isEmailAddressExist(string emailaddress)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.v_UserExplorer
                        where u.MemberEmail == emailaddress || u.ProviderEmail == emailaddress
                        select u;

            if (query != null && query.Count() != 0)
                return true;
            else return false;
        }

        #region clubMember
        public Guid RetrieveUserGUIDbyEmailAddress(string EmailAddress)
        {
            if (string.IsNullOrEmpty(EmailAddress))
                return Guid.Empty;
            else
            {
                HCEntities ent = new HCEntities();

                var query = from u in ent.ProviderProfiles
                            where u.Email == EmailAddress
                            select u;

                if (query.FirstOrDefault() != null)
                    return query.FirstOrDefault().UserID;
                else
                {
                    var nextquery = from u in ent.UserProfiles
                                    where u.Email == EmailAddress
                                    select u;

                    if (nextquery.FirstOrDefault() != null)
                        return nextquery.FirstOrDefault().UserID;
                    else return Guid.Empty;
                }
            }
        }

        public Guid RetrieveUserGUID(string username)
        {
            HCEntities ent = new HCEntities();
            var query = from u in ent.Users
                        where u.UserName == username
                        select u;

            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().UserId;
            else return Guid.Empty;

        }

        public Guid RetrieveUserGUID(int userID)
        {
            HCEntities ent = new HCEntities();
            var query = from u in ent.Users
                        where u.ID == userID
                        select u;

            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().UserId;
            else return Guid.Empty;

        }


        public ProviderEDSC.UserProfilesDTRow RetrieveUserProfiles(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.UserProfiles
                        where u.UserID == UserID
                        select u;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                var dr = new ProviderEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();
                ObjectHandler.CopyPropertyValues(user, dr);
                return dr;
            }
            else return null;
        }
        #endregion

        #region Provider

        public void InsertNewProviderProfiles(ProviderEDSC.ProviderProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ProviderProfiles prov = new ProviderProfiles();
            ObjectHandler.CopyPropertyValues(dr, prov);

            ent.AddToProviderProfiles(prov);
            ent.SaveChanges();
        }

        public ProviderEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(Guid providerID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.UserID == providerID
                        select p;
            ProviderProfiles prov = query.FirstOrDefault();
            if (prov != null)
            {
                ProviderEDSC.ProviderProfilesDTRow dr = new ProviderEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();
                ObjectHandler.CopyPropertyValues(prov, dr);
                return dr;
            }
            else return null;
        }

        public ProviderEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(String providerUsername)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.Username == providerUsername
                        select p;

            var dt = new ProviderEDSC.ProviderProfilesDTDataTable();
            var dr = new ProviderEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();

            if (query.SingleOrDefault() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.OverwriteChanges);
                foreach (var drow in dt)
                {
                    dr = drow;
                }
                //ObjectHandler.CopyProperties(category, dr);
                return dr;
            }
        }

        public void UpdateProviderProfiles(ProviderEDSC.ProviderProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.ProviderProfiles
                        where ac.UserID == dr.UserID
                        select ac;

            ProviderProfiles provider = query.FirstOrDefault();
            if (provider != null)
            {
                ObjectHandler.CopyPropertyValues(dr, provider);
                ent.SaveChanges();
            }
        }

        #endregion

        #endregion

        public bool ProviderNameExist(string organisationName)
        {
            HCEntities ent = new HCEntities();

            var query = from o in ent.ProviderProfiles
                        where o.ProviderName == organisationName
                        select o;

            if (query.FirstOrDefault() != null)
                return true;
            else return false;
        }

        public void UpdateUserImageInformation(ProviderEDSC.UserImageDTRow usrImagDet)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.UserImage
                        where ac.ID == usrImagDet.ID
                        select ac;

            UserImage provider = query.FirstOrDefault();
            if (provider != null)
            {
                ObjectHandler.CopyPropertyValues(usrImagDet, provider);
                ent.SaveChanges();
            }
        }

        public bool DeactivateUser(string usr, Guid userID, out string err)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.UserProfiles
                        where ac.UserID == userID && ac.Username == usr
                        select ac;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                if (user.AccountDeletion == true)
                {
                    err = "Account is deactivated";
                    return false;
                }
                else
                {
                    err = "Account is deactivated";
                    user.AccountDeletion = true;
                    ent.SaveChanges();
                    return true;
                }
            }
            else
            {
                var queryP = from ac in ent.ProviderProfiles
                             where ac.UserID == userID && ac.Username == usr
                             select ac;

                ProviderProfiles userp = queryP.FirstOrDefault();
                if (userp != null)
                {
                    if (userp.AccountDeletion == true)
                    {
                        err = "Account is deactivated";
                        return false;
                    }
                    else
                    {
                        err = "Account is deactivated";
                        userp.AccountDeletion = true;
                        ent.SaveChanges();
                        return true;
                    }
                }

                else
                {
                    err = "Unable to find user";
                    return false;
                }
            }
        }

        public byte[] RetrieveAssetBinary(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.WebAssets
                        where e.ID == imgID
                        select e;
            WebAssets aid = query.FirstOrDefault();

            if (aid != null && aid.FileStream != null)
            {
                byte[] stream = aid.FileStream;
                return stream;
            }
            else return null;
        }








        public void ChangeActivityEmailAddress(int activityID, string newEmailAddress)
        {

            HCEntities ent = new HCEntities();

            var query = from a in ent.ActivityContactDetail
                        where a.ActivityID == activityID
                        select a;

            if (query.Count() != 0)
            {
                ActivityContactDetail activities = query.FirstOrDefault();

                if (activities != null)
                {
                    activities.Email = newEmailAddress;                    
                    ent.SaveChanges();
                }

            }

        }
    }
}
