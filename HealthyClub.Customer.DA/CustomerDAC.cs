using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthyClub.EDM;
using HealthyClub.Customer.EDS;
using HealthyClub.Utility;
using System.Data;
using System.Transactions;
using BCUtility;
using nullpointer.Metaphone;
using System.Text.RegularExpressions;

namespace HealthyClub.Customer.DA
{
    public class CustomerDAC
    {
        #region Category
        public int DetermineCategoryLevel(CustomerEDSC.v_CategoryExplorerDTRow categoryDR)
        {
            if (categoryDR.IsLevel1ParentIDNull() || categoryDR.Level1ParentID == 0)
                return 0;
            else if (categoryDR.IsLevel2ParentIDNull() || categoryDR.Level2ParentID == 0)
                return 1;
            else
                return 2;

        }

        public void CreateCategory(CustomerEDSC.CategoryDTRow dr)
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

        public void UpdateCategory(CustomerEDSC.CategoryDTRow dr)
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

        public CustomerEDSC.v_CategoryExplorerDTRow RetrieveCategory(int categoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.ID == categoryID
                        select c;

            var category = query.SingleOrDefault();

            if (category == null)
                return null;
            else
            {
                var dr = new CustomerEDSC.v_CategoryExplorerDTDataTable().Newv_CategoryExplorerDTRow();
                ObjectHandler.CopyPropertyValues(category, dr);
                return dr;
            }
        }

        public CustomerEDSC.CategoryDTDataTable RetrieveAllCategories()
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.CategoryDTDataTable dt = new CustomerEDSC.CategoryDTDataTable();

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

        public CustomerEDSC.CategoryDTDataTable RetrieveLv0Categories()
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.CategoryDTDataTable dt = new CustomerEDSC.CategoryDTDataTable();

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

        public CustomerEDSC.CategoryDTDataTable RetrieveLv1Categories(int rootCatID)
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.CategoryDTDataTable dt = new CustomerEDSC.CategoryDTDataTable();

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

        public CustomerEDSC.CategoryDTDataTable RetrieveSubCategories(int immediateParentCategoryID, int startIndex, int amount, string sortExpression)
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

            CustomerEDSC.CategoryDTDataTable dt = new CustomerEDSC.CategoryDTDataTable();

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

        public CustomerEDSC.CategoryDTDataTable RetrieveAllSubCategories(int parentCategoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.Level1ParentID == parentCategoryID ||
                                c.Level2ParentID == parentCategoryID

                        select c;

            var dt = new CustomerEDSC.CategoryDTDataTable();

            ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);

            return dt;
        }

        public CustomerEDSC.CategoryDTDataTable RetrieveCategories()
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.CategoryDTDataTable dt = new CustomerEDSC.CategoryDTDataTable();

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

        public CustomerEDSC.v_CategoryExplorerDTDataTable RetrieveCategories(int startingRef)
        {
            HCEntities ent = new HCEntities();

            CustomerEDSC.v_CategoryExplorerDTRow startPoint = RetrieveCategory(startingRef);

            IQueryable<v_CategoryExplorer> query = null;

            if (startPoint == null)
            {
                query = from b in ent.v_CategoryExplorer
                        orderby b.Level2ParentName, b.Level1ParentName, b.Name
                        select b;
            }
            else
            {
                query = from b in ent.v_CategoryExplorer
                        where
                         (startPoint.Level == 0 && b.Level1ParentID == startingRef) ||
                         (startPoint.Level == 1 && b.Level1ParentID == startPoint.Level1ParentID && b.Level2ParentID == startingRef)
                        orderby b.Level2ParentName, b.Level1ParentName, b.Name
                        select b;
            }

            var dt = new CustomerEDSC.v_CategoryExplorerDTDataTable();

            ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
            return dt;
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

        public CustomerEDSC.SuburbDTDataTable RetrieveSuburbs(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.SuburbDTDataTable dt = new CustomerEDSC.SuburbDTDataTable();

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

        public CustomerEDSC.v_SuburbExplorerDTDataTable RetrieveSuburbs()
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.v_SuburbExplorerDTDataTable dt = new CustomerEDSC.v_SuburbExplorerDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        select q;
            query = query.OrderBy(row => row.Name);

            if (query.AsEnumerable().Count() == 0)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public int RetrieveSuburbsCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.SuburbDTDataTable dt = new CustomerEDSC.SuburbDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        select q;

            return query.Count();
        }

        public CustomerEDSC.SuburbDTDataTable RetrieveSuburbs(List<int> subIDint)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.Suburb
                        from l in subIDint
                        where s.ID == l
                        select s;

            if (query.AsEnumerable() != null)
            {
                var suburbs = query.AsEnumerable();
                var dt = new CustomerEDSC.SuburbDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(suburbs, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else
                return null;
        }

        public void CreateSuburb(string Modifier, CustomerEDSC.SuburbDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Suburb sub = new Suburb();
            sub.Name = dr.Name;
            sub.PostCode = dr.PostCode;
            sub.StateID = dr.StateID;

            ent.AddToSuburb(sub);
            ent.SaveChanges();
        }

        public void UpdateSuburb(string Modifier, CustomerEDSC.SuburbDTRow dr)
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

        public CustomerEDSC.v_SuburbExplorerDTRow RetrieveSuburbByID(int suburbID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_SuburbExplorer
                        where s.ID == suburbID
                        select s;

            CustomerEDSC.v_SuburbExplorerDTDataTable dt = new CustomerEDSC.v_SuburbExplorerDTDataTable();
            CustomerEDSC.v_SuburbExplorerDTRow dr = new CustomerEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();


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

        public CustomerEDSC.v_SuburbExplorerDTRow RetrieveSuburbByPostCode(int postCode)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_SuburbExplorer
                        where s.PostCode == postCode
                        select s;

            CustomerEDSC.v_SuburbExplorerDTDataTable dt = new CustomerEDSC.v_SuburbExplorerDTDataTable();
            CustomerEDSC.v_SuburbExplorerDTRow dr = new CustomerEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();


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
        public CustomerEDSC.StateDTDataTable RetrieveStates(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            CustomerEDSC.StateDTDataTable dt = new CustomerEDSC.StateDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public CustomerEDSC.StateDTDataTable RetrieveStates()
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            CustomerEDSC.StateDTDataTable dt = new CustomerEDSC.StateDTDataTable();

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

        public CustomerEDSC.StateDTRow RetrieveState(int stateID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == stateID
                        select s;

            CustomerEDSC.StateDTDataTable dt = new CustomerEDSC.StateDTDataTable();
            CustomerEDSC.StateDTRow dr = new CustomerEDSC.StateDTDataTable().NewStateDTRow();


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

        public void CreateState(string userName, CustomerEDSC.StateDTRow dr)
        {
            HCEntities ent = new HCEntities();
            State state = new State();
            state.StateName = dr.StateName;
            state.StateDetail = dr.StateDetail;

            ent.AddToState(state);
            ent.SaveChanges();
        }

        public void UpdateState(string userName, CustomerEDSC.StateDTRow dr)
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

        #region ActivityRegistration

        public void CreateActivityContactDetail(CustomerEDSC.ActivityContactDetailDTRow activityContactDetailDR)
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


        public void CreateActivityGrouping(CustomerEDSC.ActivityGroupingDTRow activityGroupDR)
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

        public void CreateActivities(CustomerEDSC.ActivityDTRow activityDR, out int activityID)
        {
            HCEntities ent = new HCEntities();
            Activity act = new Activity();

            act.ActivityCode = activityDR.ActivityCode;
            act.ProviderID = activityDR.ProviderID;
            act.Name = activityDR.Name;
            act.ShortDescription = activityDR.ShortDescription;
            act.FullDescription = activityDR.FullDescription;
            act.CategoryID = activityDR.CategoryID;
            act.Price = activityDR.Price;
            act.ExpiryDate = activityDR.ExpiryDate;
            act.ActivityType = activityDR.ActivityType;
            act.eligibilityDescription = activityDR.eligibilityDescription;
            act.Website = activityDR.Website;
            act.Status = activityDR.Status;

            act.CreatedBy = activityDR.CreatedBy;
            act.CreatedDateTime = activityDR.CreatedDateTime;
            act.ModifiedBy = activityDR.ModifiedBy;
            act.ModifiedDateTime = activityDR.ModifiedDateTime;

            ent.AddToActivity(act);
            ent.SaveChanges();

            activityID = act.ID;
        }

        public void CreateActivitySchedule(CustomerEDSC.ActivityScheduleDTRow ActScheduleDR)
        {
            HCEntities ent = new HCEntities();

            ActivitySchedule ActSched = new ActivitySchedule();
            ObjectHandler.CopyPropertyValues(ActScheduleDR, ActSched);

            ent.AddToActivitySchedule(ActSched);
            ent.SaveChanges();

        }
        #endregion

        #region Keyword

        public bool CheckAdvanceSearch()
        {
            HCEntities ent = new HCEntities();
            CustomerEDSC.WebConfigurationDTDataTable dt = new CustomerEDSC.WebConfigurationDTDataTable();
            CustomerEDSC.WebConfigurationDTRow dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
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

        public CustomerEDSC.v_KeyCollectionViewDTDataTable SearchKeywordCollection(String searchKey)
        {
            String[] Keywords = Array.ConvertAll(searchKey.Split(' '), c => c.Trim());

            HCEntities ent = new HCEntities();

            var dt = new CustomerEDSC.v_KeyCollectionViewDTDataTable();

            var query = (from a in ent.Keyword
                         from w in Keywords
                         where a.Keywords.ToUpper().Contains(w.ToUpper())
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

        #region ImageViewer

        public CustomerEDSC.ActivityImageDetailDTRow RetrieveProductImage(int activityID, int imageID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.ID == imageID
                        select i;

            CustomerEDSC.ActivityImageDetailDTRow dr = new CustomerEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            ActivityImageDetail pi = query.FirstOrDefault();
            if (pi != null)
                ObjectHandler.CopyPropertyValues(pi, dr);

            return dr;
        }

        public CustomerEDSC.WebConfigurationDTRow RetrieveWebImage()
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.WebConfiguration
                        select i;
            WebConfiguration web = query.FirstOrDefault();
            CustomerEDSC.WebConfigurationDTRow dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
            if (web != null)
            {
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;

        }

        public CustomerEDSC.ActivityImageDetailDTRow RetrieveActivityPrimaryImage(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.isPrimaryImage == true
                        select i;

            CustomerEDSC.ActivityImageDetailDTRow dr = new CustomerEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            ActivityImageDetail pi = query.FirstOrDefault();
            if (pi != null)
                ObjectHandler.CopyPropertyValues(pi, dr);
            else
            {
                //dr.ProductID = 0;
                //dr.ImageID = 0;
                return null;
            }
            return dr;
        }

        public CustomerEDSC.ActivityImageDetailDTDataTable RetrieveActivityImages(int activityID)
        {
            HCEntities ent = new HCEntities();
            var dt = new CustomerEDSC.ActivityImageDetailDTDataTable();

            var query = from actIm in ent.ActivityImageDetail
                        where actIm.ActivityID == activityID
                        select actIm;

            if (query.AsEnumerable().Count() == 0)
            {
                return null;
            }
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }
        /*
        public bool CheckImageBanner()
        {
            HCEntities ent = new HCEntities();
            var query = from w in ent.WebConfiguration
                        select w;
            if (query.FirstOrDefault() != null)
            {
                CustomerEDSC.WebConfigurationDTRow dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                if (!dr.IsBannerImageNameNull())
                    return true;
                else return false;
            }
            else return false;
        }

        public bool CheckImageLogo()
        {
            HCEntities ent = new HCEntities();
            var query = from w in ent.WebConfiguration
                        select w;

            if (query.FirstOrDefault() != null)
            {
                CustomerEDSC.WebConfigurationDTRow dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                if (!dr.IsLogoImageNameNull())
                    return true;
                else return false;
            }
            else return false;

        }
        */
        #endregion

        #region ActivityListing
        /*ActivityView contains value used in reports only, ActivityExplorer contains complete 
         *value used in activity listing and details. use activity ID to retrieve the activities
         *you want to be show. int[] ActivityID. */

        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbySearchPhrase(Guid providerID, string stFrom, string stTo, string tmFrom, string tmTo, string searchKey, int ageFrom, int ageTo, string suburbID, int categoryID, int startIndex, int amount, string sortExpression, string MonFilter, string TueFilter, string WedFilter, string ThursFilter, string FriFilter, string SatFilter, string SunFilter)
        {

            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query;
            //filtering ProviderID  
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID &&
                        (a.Status == (int)SystemConstants.ActivityStatus.Active ||
                        a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby a.ID
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.Status == (int)SystemConstants.ActivityStatus.Active ||
                        a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby a.ID
                        select a;
            }
            //we are now creating a dictionary for matching query

            Dictionary<string, HashSet<int>> ActDictionary = new Dictionary<string, HashSet<int>>();
            foreach (var activity in query)
            {
                activity.Name = Regex.Replace(activity.Name, @"[!@#-;,:$%_]", "");
                activity.ShortDescription = Regex.Replace(activity.ShortDescription, @"[!@#-;,:$%_]", "");

                string[] actTitle = activity.Name.Trim().Split();
                foreach (var word in actTitle)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
                string[] keywords = activity.Keywords.Trim().Split(';');
                foreach (var word in keywords)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
                string[] actDesc = activity.ShortDescription.Trim().Split();
                foreach (var word in actDesc)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
            }


            HashSet<string> keywordsMPs = new HashSet<string>();
            foreach (var word in Keywords)
            {
                DoubleMetaphone mp = new DoubleMetaphone(word);
                if (mp.PrimaryKey != null)
                    keywordsMPs.Add(mp.PrimaryKey);
                if (mp.AlternateKey != null)
                    keywordsMPs.Add(mp.AlternateKey);
            }
            HashSet<int> matchesAct = new HashSet<int>();
            foreach (var keywordsMP in keywordsMPs)
            {
                HashSet<int> matches = new HashSet<int>();
                ActDictionary.TryGetValue(keywordsMP, out matches);
                if (matches != null)
                {
                    matchesAct.UnionWith(matches);
                }
            }
            IEnumerable<v_ActivityExplorer> Suggestions = query.Where(x => matchesAct.Contains(x.ID));

            //START FILTERING ACTIVITY BY CATEGORY
            if (categoryID != 0)
            {
                Suggestions = from a in Suggestions
                              where a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID
                              select a;
            }

            //START FILTERING ACTIVITY BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                Suggestions = from a in Suggestions
                              where a.AgeFrom <= ageFrom && a.AgeTo >= ageTo
                              select a;
            }

            //START FILTERING ACTIVITY BY LOCATION
            if (!string.IsNullOrEmpty(suburbID) && suburbID != "0")
            {

                string[] suburbs = suburbID.Split('|');
                List<int> suburbsInt = new List<int>();
                foreach (var suburb in suburbs)
                {
                    var suburbInt = Convert.ToInt32(suburb);
                    suburbsInt.Add(suburbInt);
                }
                Suggestions = from a in Suggestions
                              from s in suburbsInt
                              where a.SuburbID == s
                              select a;
            }


            if (sortExpression == SystemConstants.sortName)
                Suggestions = Suggestions.OrderBy(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatest)
                Suggestions = Suggestions.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiry)
                Suggestions = Suggestions.OrderBy(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPrice)
                Suggestions = Suggestions.OrderByDescending(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortNameDesc)
                Suggestions = Suggestions.OrderByDescending(row => row.Name);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                Suggestions = Suggestions.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                Suggestions = Suggestions.OrderByDescending(row => row.ModifiedDateTime);
            else if (sortExpression == SystemConstants.sortPriceDesc)
                Suggestions = Suggestions.OrderBy(row => row.IsPaid);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                Suggestions = Suggestions.OrderByDescending(row => row.ActivityType);

            //START FILTERING ACTIVITY BY MATCHING DATE & DAY & TIME
            bool mon = Convert.ToBoolean(MonFilter); bool fri = Convert.ToBoolean(FriFilter);
            bool tue = Convert.ToBoolean(TueFilter); bool sat = Convert.ToBoolean(SatFilter);
            bool wed = Convert.ToBoolean(WedFilter); bool sun = Convert.ToBoolean(SunFilter);
            bool thu = Convert.ToBoolean(ThursFilter);
            DateTime dtFrom = Convert.ToDateTime(stFrom);
            DateTime dtTo = Convert.ToDateTime(stTo);
            TimeSpan timeFrom = Convert.ToDateTime(tmFrom).TimeOfDay;
            TimeSpan timeTo = Convert.ToDateTime(tmTo).TimeOfDay;

            if ((dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate) || (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay) || (!mon || !tue || !wed || !thu || !fri || !sat || !sun))
            {
                HashSet<int> actScheds = new HashSet<int>(ent.ActivitySchedule.Select(x => x.ActivityID));
                HashSet<int> acts = new HashSet<int>(Suggestions.Select(x => x.ID));
                HashSet<int> ActsWISched = new HashSet<int>(acts.Where(x => actScheds.Contains(x)));
                HashSet<int> ActsWOSched = new HashSet<int>(acts.Where(x => !actScheds.Contains(x)));
                CustomerEDSC.ActivityScheduleDTDataTable Schedsdt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(ActsWISched);

                if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDate(Schedsdt, dtFrom, dtTo);
                }

                if (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesTime(Schedsdt, timeFrom, timeTo);
                }


                if (!mon || !tue || !wed || !thu || !fri || !sat || !sun)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDay(Schedsdt, mon, tue, wed, thu, fri, sat, sun);
                }



                if (ActsWISched != null)
                {
                    var filteredSuggestions = Suggestions.Where(x => ActsWISched.Contains(x.ActivityID));
                    if (ActsWOSched != null)
                        Suggestions = Suggestions.Where(x => ActsWOSched.Contains(x.ActivityID));

                    if (sortExpression == SystemConstants.sortName)
                        filteredSuggestions = filteredSuggestions.OrderBy(row => row.Name);
                    else if (sortExpression == SystemConstants.sortLatest)
                        filteredSuggestions = filteredSuggestions.OrderBy(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortExpiry)
                        filteredSuggestions = filteredSuggestions.OrderBy(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortPrice)
                        filteredSuggestions = filteredSuggestions.OrderByDescending(row => row.IsPaid);
                    else if (sortExpression == SystemConstants.sortNameDesc)
                        filteredSuggestions = filteredSuggestions.OrderByDescending(row => row.Name);
                    else if (sortExpression == SystemConstants.sortLatestDesc)
                        filteredSuggestions = filteredSuggestions.OrderByDescending(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortExpiryDesc)
                        filteredSuggestions = filteredSuggestions.OrderByDescending(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortPriceDesc)
                        filteredSuggestions = filteredSuggestions.OrderBy(row => row.IsPaid);
                    else if (sortExpression == SystemConstants.sortExpiryDesc)
                        filteredSuggestions = filteredSuggestions.OrderByDescending(row => row.ActivityType);

                    Suggestions = filteredSuggestions.Select(x => x).Concat(Suggestions.Select(y => y));
                }

            }
            Suggestions = Suggestions.Skip(startIndex).Take(amount).AsEnumerable();

            if (Suggestions != null && Suggestions.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Suggestions, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else return null;

        }

        //public CustomerEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesReportbySearchPhrase(Guid providerID, string stFrom, string stTo, string searchKey, string sortExpression)
        //{
        //    //splitting keywords as keyword can be a multiple words
        //    String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

        //    HCEntities ent = new HCEntities();
        //    IQueryable<v_ActivityView> query;
        //    //filtering ProviderID  
        //    if (providerID != Guid.Empty)
        //    {
        //        query = from a in ent.v_ActivityView
        //                where a.ProviderID == providerID &&
        //                (a.Status == (int)SystemConstants.ActivityStatus.Active ||
        //                a.Status == (int)SystemConstants.ActivityStatus.WillExpire)
        //                orderby a.ID
        //                select a;
        //    }
        //    else
        //    {
        //        query = from a in ent.v_ActivityView
        //                where (a.Status == (int)SystemConstants.ActivityStatus.Active ||
        //                a.Status == (int)SystemConstants.ActivityStatus.WillExpire)
        //                orderby a.ID
        //                select a;
        //    }


        //    var Suggestions = (from a in query
        //                       from w in Keywords
        //                       where a.Name.ToUpper().Contains(w.ToUpper()) || a.ShortDescription.ToUpper().Contains(w.ToUpper())
        //                       || a.Keywords.ToUpper().Contains(w.ToUpper()) && (a.Status == (int)SystemConstants.ActivityStatus.Active
        //                           || a.Status == (int)SystemConstants.ActivityStatus.WillExpire)
        //                       orderby sortExpression
        //                       select a).Distinct().ToArray();

        //    IEnumerable<v_ActivityView> activities = Suggestions.AsEnumerable();

        //    if (sortExpression == SystemConstants.sortName)
        //        activities = activities.OrderBy(row => row.Name);
        //    else if (sortExpression == SystemConstants.sortLatest)
        //        activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
        //    else if (sortExpression == SystemConstants.sortExpiry)
        //        activities = activities.OrderBy(row => row.ActivityModifiedDateTime);
        //    else if (sortExpression == SystemConstants.sortNameDesc)
        //        activities = activities.OrderByDescending(row => row.Name);
        //    else if (sortExpression == SystemConstants.sortLatestDesc)
        //        activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);
        //    else if (sortExpression == SystemConstants.sortExpiryDesc)
        //        activities = activities.OrderByDescending(row => row.ActivityModifiedDateTime);

        //    CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
        //    ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

        //    DateTime dtFrom = Convert.ToDateTime(stFrom);
        //    DateTime dtTo = Convert.ToDateTime(stTo);

        //    if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
        //    {
        //        HashSet<int> actIDID = new HashSet<int>();
        //        foreach (var dr in dt)
        //            actIDID.Add(dr.ActivityID);

        //        if (actIDID.Count != 0)
        //        {
        //            HashSet<int> FilteredID = FilterActivitiesDate(actIDID, dtFrom, dtTo);
        //            dt = RetrieveActivityViewsbyIDs(FilteredID, sortExpression);
        //            return dt;
        //        }
        //    }
        //    return dt;
        //}

        public int RetrieveProviderActivitiesbySearchPhraseCount(Guid providerID, string stFrom, string stTo, string tmFrom, string tmTo, int ageFrom, int ageTo, string suburbID, int categoryID, string searchKey, string MonFilter, string TueFilter, string WedFilter, string ThursFilter, string FriFilter, string SatFilter, string SunFilter)
        {

            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query;
            //filtering ProviderID  
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_ActivityExplorer
                        where a.ProviderID == providerID &&
                        (a.Status == (int)SystemConstants.ActivityStatus.Active ||
                        a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby a.ID
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.Status == (int)SystemConstants.ActivityStatus.Active ||
                        a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby a.ID
                        select a;
            }
            //we are now creating a dictionary for matching query

            Dictionary<string, HashSet<int>> ActDictionary = new Dictionary<string, HashSet<int>>();
            foreach (var activity in query)
            {
                activity.Name = Regex.Replace(activity.Name, @"[!@#-;,:$%_]", "");
                activity.ShortDescription = Regex.Replace(activity.ShortDescription, @"[!@#-;,:$%_]", "");

                string[] actTitle = activity.Name.Trim().Split();
                foreach (var word in actTitle)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
                string[] keywords = activity.Keywords.Trim().Split(';');
                foreach (var word in keywords)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
                string[] actDesc = activity.ShortDescription.Trim().Split();
                foreach (var word in actDesc)
                {
                    DoubleMetaphone mp = new DoubleMetaphone(word);
                    HashSet<int> savedAct = new HashSet<int>();
                    ActDictionary.TryGetValue(mp.PrimaryKey, out savedAct);

                    if (savedAct == null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary[mp.PrimaryKey] = savedAct;
                    }
                    savedAct.Add(activity.ID);

                    if (mp.AlternateKey != null)
                    {
                        savedAct = new HashSet<int>();
                        ActDictionary.TryGetValue(mp.AlternateKey, out savedAct);

                        if (savedAct == null)
                        {
                            savedAct = new HashSet<int>();
                            ActDictionary[mp.AlternateKey] = savedAct;
                        }
                        savedAct.Add(activity.ID);
                    }
                }
            }


            HashSet<string> keywordsMPs = new HashSet<string>();
            foreach (var word in Keywords)
            {
                DoubleMetaphone mp = new DoubleMetaphone(word);
                if (mp.PrimaryKey != null)
                    keywordsMPs.Add(mp.PrimaryKey);
                if (mp.AlternateKey != null)
                    keywordsMPs.Add(mp.AlternateKey);
            }
            HashSet<int> matchesAct = new HashSet<int>();
            foreach (var keywordsMP in keywordsMPs)
            {
                HashSet<int> matches = new HashSet<int>();
                ActDictionary.TryGetValue(keywordsMP, out matches);
                if (matches != null)
                {
                    matchesAct.UnionWith(matches);
                }
            }
            IEnumerable<v_ActivityExplorer> Suggestions = query.Where(x => matchesAct.Contains(x.ID));

            //START FILTERING ACTIVITY BY CATEGORY
            if (categoryID != 0)
            {
                Suggestions = from a in Suggestions
                              where a.CategoryID == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID
                              select a;
            }

            //START FILTERING ACTIVITY BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                Suggestions = from a in Suggestions
                              where a.AgeFrom <= ageFrom && a.AgeTo >= ageTo
                              select a;
            }

            //START FILTERING ACTIVITY BY LOCATION
            if (!string.IsNullOrEmpty(suburbID) && suburbID != "0")
            {

                string[] suburbs = suburbID.Split('|');
                List<int> suburbsInt = new List<int>();
                foreach (var suburb in suburbs)
                {
                    var suburbInt = Convert.ToInt32(suburb);
                    suburbsInt.Add(suburbInt);
                }
                Suggestions = from a in Suggestions
                              from s in suburbsInt
                              where a.SuburbID == s
                              select a;
            }

            //START FILTERING ACTIVITY BY MATCHING DATE & DAY & TIME
            bool mon = Convert.ToBoolean(MonFilter); bool fri = Convert.ToBoolean(FriFilter);
            bool tue = Convert.ToBoolean(TueFilter); bool sat = Convert.ToBoolean(SatFilter);
            bool wed = Convert.ToBoolean(WedFilter); bool sun = Convert.ToBoolean(SunFilter);
            bool thu = Convert.ToBoolean(ThursFilter);
            DateTime dtFrom = Convert.ToDateTime(stFrom);
            DateTime dtTo = Convert.ToDateTime(stTo);
            TimeSpan timeFrom = Convert.ToDateTime(tmFrom).TimeOfDay;
            TimeSpan timeTo = Convert.ToDateTime(tmTo).TimeOfDay;

            if ((dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate) || (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay) || (!mon || !tue || !wed || !thu || !fri || !sat || !sun))
            {
                HashSet<int> actScheds = new HashSet<int>(ent.ActivitySchedule.Select(x => x.ActivityID));
                HashSet<int> acts = new HashSet<int>(Suggestions.Select(x => x.ID));
                HashSet<int> ActsWISched = new HashSet<int>(acts.Where(x => actScheds.Contains(x)));
                HashSet<int> ActsWOSched = new HashSet<int>(acts.Where(x => !actScheds.Contains(x)));
                CustomerEDSC.ActivityScheduleDTDataTable Schedsdt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(ActsWISched);

                if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDate(Schedsdt, dtFrom, dtTo);
                }

                if (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesTime(Schedsdt, timeFrom, timeTo);
                }


                if (!mon || !tue || !wed || !thu || !fri || !sat || !sun)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDay(Schedsdt, mon, tue, wed, thu, fri, sat, sun);
                }


                if (ActsWISched != null)
                {
                    var filteredSuggestions = Suggestions.Where(x => ActsWISched.Contains(x.ActivityID));
                    if (ActsWOSched != null)
                        Suggestions = Suggestions.Where(x => ActsWOSched.Contains(x.ActivityID));

                    Suggestions = filteredSuggestions.Select(x => x).Concat(Suggestions.Select(y => y));
                }

            }
            Suggestions = Suggestions.AsEnumerable();

            if (Suggestions != null && Suggestions.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Suggestions, dt, LoadOption.OverwriteChanges);
                return dt.Count;
            }
            else return 0;

        }

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesReport(Guid providerID, string stFrom, string stTo, int categoryID, string sortExpression)
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

            if (activities != null)
            {
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

                CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

                return dt;
            }

            else
                return null;


        }

        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(Guid providerID, string stFrom, string stTo, string tmFrom, string tmTo, int categoryID, int ageFrom, int ageTo, string suburbID, int startIndex, int amount, string sortExpression, string MonFilter, string TueFilter, string WedFilter, string ThursFilter, string FriFilter, string SatFilter, string SunFilter)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.SecondaryCategoryID1 == categoryID ||
                                        a.SecondaryCategoryID2 == categoryID ||
                                        a.SecondaryCategoryID3 == categoryID ||
                                        a.SecondaryCategoryID4 == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                                   && (a.Status == (int)SystemConstants.ActivityStatus.Active
                                   || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby sortExpression
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.SecondaryCategoryID1 == categoryID ||
                                   a.SecondaryCategoryID2 == categoryID ||
                                   a.SecondaryCategoryID3 == categoryID ||
                                   a.SecondaryCategoryID4 == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID)
                                   && (a.Status == (int)SystemConstants.ActivityStatus.Active
                                   || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true

                        orderby sortExpression
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.Status == (int)SystemConstants.ActivityStatus.Active
                        || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        orderby sortExpression
                        select a;
            }

            //START FILTERING ACTIVITY BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                query = from a in query
                        where a.AgeFrom <= ageFrom && a.AgeTo >= ageTo
                        orderby sortExpression
                        select a;
            }

            //START FILTERING ACTIVITY BY LOCATION
            if (!string.IsNullOrEmpty(suburbID) && suburbID != "0")
            {

                string[] suburbs = suburbID.Split('|');
                List<int> suburbsInt = new List<int>();
                foreach (var suburb in suburbs)
                {
                    var suburbInt = Convert.ToInt32(suburb);
                    suburbsInt.Add(suburbInt);
                }
                query = from a in query
                        from s in suburbsInt
                        where a.SuburbID == s
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

            //START FILTERING ACTIVITY BY MATCHING DATE & DAY
            bool mon = Convert.ToBoolean(MonFilter); bool fri = Convert.ToBoolean(FriFilter);
            bool tue = Convert.ToBoolean(TueFilter); bool sat = Convert.ToBoolean(SatFilter);
            bool wed = Convert.ToBoolean(WedFilter); bool sun = Convert.ToBoolean(SunFilter);
            bool thu = Convert.ToBoolean(ThursFilter);
            DateTime dtFrom = Convert.ToDateTime(stFrom);
            DateTime dtTo = Convert.ToDateTime(stTo);
            TimeSpan timeFrom = Convert.ToDateTime(tmFrom).TimeOfDay;
            TimeSpan timeTo = Convert.ToDateTime(tmTo).TimeOfDay;

            if ((dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate) || (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay) || (!mon || !tue || !wed || !thu || !fri || !sat || !sun))
            {
                HashSet<int> actScheds = new HashSet<int>(ent.ActivitySchedule.Select(x => x.ActivityID));
                HashSet<int> acts = new HashSet<int>(activities.Select(x => x.ID));
                HashSet<int> ActsWISched = new HashSet<int>(acts.Where(x => actScheds.Contains(x)));
                HashSet<int> ActsWOSched = new HashSet<int>(acts.Where(x => !actScheds.Contains(x)));
                CustomerEDSC.ActivityScheduleDTDataTable Schedsdt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(ActsWISched);

                if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDate(Schedsdt, dtFrom, dtTo);
                    else return null;
                }
                if (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesTime(Schedsdt, timeFrom, timeTo);
                }

                if (!mon || !tue || !wed || !thu || !fri || !sat || !sun)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDay(Schedsdt, mon, tue, wed, thu, fri, sat, sun);
                    else return null;
                }

                if (ActsWISched != null)
                {
                    var filteredActivities = activities.Where(x => ActsWISched.Contains(x.ActivityID));
                    if (ActsWOSched != null)
                        activities = activities.Where(x => ActsWOSched.Contains(x.ActivityID));
                    if (sortExpression == SystemConstants.sortName)
                        filteredActivities = filteredActivities.OrderBy(row => row.Name);
                    else if (sortExpression == SystemConstants.sortLatest)
                        filteredActivities = filteredActivities.OrderBy(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortExpiry)
                        filteredActivities = filteredActivities.OrderBy(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortPrice)
                        filteredActivities = filteredActivities.OrderByDescending(row => row.IsPaid);
                    else if (sortExpression == SystemConstants.sortNameDesc)
                        filteredActivities = filteredActivities.OrderByDescending(row => row.Name);
                    else if (sortExpression == SystemConstants.sortLatestDesc)
                        filteredActivities = filteredActivities.OrderByDescending(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortExpiryDesc)
                        filteredActivities = filteredActivities.OrderByDescending(row => row.ModifiedDateTime);
                    else if (sortExpression == SystemConstants.sortPriceDesc)
                        filteredActivities = filteredActivities.OrderBy(row => row.IsPaid);
                    else if (sortExpression == SystemConstants.sortExpiryDesc)
                        filteredActivities = filteredActivities.OrderByDescending(row => row.ActivityType);

                    activities = filteredActivities.Select(x => x).Concat(activities.Select(y => y));
                }
            }
            activities = activities.Skip(startIndex).Take(amount).AsEnumerable();

            if (activities != null && activities.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else return null;
        }


        //public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(Guid providerID, int categoryID, string sortExpression)
        //{
        //    HCEntities ent = new HCEntities();

        //    //filtering ProviderID  
        //    IQueryable<v_ActivityExplorer> query;

        //    if (providerID != Guid.Empty && categoryID != 0)
        //    {
        //        query = from a in ent.v_ActivityExplorer
        //                where (a.ProviderID == providerID && (
        //                                a.CategoryID == categoryID ||
        //                                a.CategoryLevel1ParentID == categoryID ||
        //                                a.CategoryLevel2ParentID == categoryID))
        //                           && (a.Status == (int)SystemConstants.ActivityStatus.Active
        //                           || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
        //                orderby sortExpression
        //                select a;
        //    }
        //    else if (providerID == Guid.Empty && categoryID != 0)
        //    {
        //        query = from a in ent.v_ActivityExplorer
        //                where (a.CategoryID == categoryID ||
        //                           a.CategoryLevel1ParentID == categoryID ||
        //                           a.CategoryLevel2ParentID == categoryID)
        //                           && (a.Status == (int)SystemConstants.ActivityStatus.Active
        //                           || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true

        //                orderby sortExpression
        //                select a;
        //    }
        //    else
        //    {
        //        query = from a in ent.v_ActivityExplorer
        //                where (a.Status == (int)SystemConstants.ActivityStatus.Active
        //                || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
        //                orderby sortExpression
        //                select a;
        //    }

        //    IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();
        //    if (activities != null)
        //    {
        //        if (sortExpression == SystemConstants.sortName)
        //            activities = activities.OrderBy(row => row.Name);
        //        else if (sortExpression == SystemConstants.sortLatest)
        //            activities = activities.OrderBy(row => row.ModifiedDateTime);
        //        else if (sortExpression == SystemConstants.sortExpiry)
        //            activities = activities.OrderBy(row => row.ModifiedDateTime);
        //        else if (sortExpression == SystemConstants.sortNameDesc)
        //            activities = activities.OrderByDescending(row => row.Name);
        //        else if (sortExpression == SystemConstants.sortLatestDesc)
        //            activities = activities.OrderByDescending(row => row.ModifiedDateTime);
        //        else if (sortExpression == SystemConstants.sortExpiryDesc)
        //            activities = activities.OrderByDescending(row => row.ModifiedDateTime);
        //        CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
        //        ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

        //        return dt;
        //    }
        //    else return null;
        //}

        public int RetrieveProviderActivitiesCount(Guid providerID, string stFrom, string stTo, string tmFrom, string tmTo, int ageFrom, int ageTo, string suburbID, int categoryID, string MonFilter, string TueFilter, string WedFilter, string ThursFilter, string FriFilter, string SatFilter, string SunFilter)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_ActivityExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID ||
                                        a.SecondaryCategoryID1 == categoryID ||
                                        a.SecondaryCategoryID2 == categoryID ||
                                        a.SecondaryCategoryID3 == categoryID ||
                                        a.SecondaryCategoryID4 == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID))
                                   && (a.Status == (int)SystemConstants.ActivityStatus.Active
                                   || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        select a;
            }
            else if (providerID == Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.CategoryID == categoryID ||
                                   a.SecondaryCategoryID1 == categoryID ||
                                   a.SecondaryCategoryID2 == categoryID ||
                                   a.SecondaryCategoryID3 == categoryID ||
                                   a.SecondaryCategoryID4 == categoryID ||
                                        a.CategoryLevel1ParentID == categoryID ||
                                        a.CategoryLevel2ParentID == categoryID)
                                   && (a.Status == (int)SystemConstants.ActivityStatus.Active
                                   || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        select a;
            }
            else
            {
                query = from a in ent.v_ActivityExplorer
                        where (a.Status == (int)SystemConstants.ActivityStatus.Active
                        || a.Status == (int)SystemConstants.ActivityStatus.WillExpire) && a.isApproved == true
                        select a;
            }

            //START FILTERING ACTIVITY BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                query = from a in query
                        where a.AgeFrom <= ageFrom && a.AgeTo >= ageTo
                        select a;
            }

            //START FILTERING ACTIVITY BY LOCATION
            if (!string.IsNullOrEmpty(suburbID) && suburbID != "0")
            {

                string[] suburbs = suburbID.Split('|');
                List<int> suburbsInt = new List<int>();
                foreach (var suburb in suburbs)
                {
                    var suburbInt = Convert.ToInt32(suburb);
                    suburbsInt.Add(suburbInt);
                }
                query = from a in query
                        from s in suburbsInt
                        where a.SuburbID == s
                        select a;
            }

            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();


            //START FILTERING ACTIVITY BY MATCHING DATE & DAY
            bool mon = Convert.ToBoolean(MonFilter); bool fri = Convert.ToBoolean(FriFilter);
            bool tue = Convert.ToBoolean(TueFilter); bool sat = Convert.ToBoolean(SatFilter);
            bool wed = Convert.ToBoolean(WedFilter); bool sun = Convert.ToBoolean(SunFilter);
            bool thu = Convert.ToBoolean(ThursFilter);
            DateTime dtFrom = Convert.ToDateTime(stFrom);
            DateTime dtTo = Convert.ToDateTime(stTo);
            TimeSpan timeFrom = Convert.ToDateTime(tmFrom).TimeOfDay;
            TimeSpan timeTo = Convert.ToDateTime(tmTo).TimeOfDay;

            if ((dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate) || (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay) || (!mon || !tue || !wed || !thu || !fri || !sat || !sun))
            {
                HashSet<int> actScheds = new HashSet<int>(ent.ActivitySchedule.Select(x => x.ActivityID));
                HashSet<int> acts = new HashSet<int>(activities.Select(x => x.ID));
                HashSet<int> ActsWISched = new HashSet<int>(acts.Where(x => actScheds.Contains(x)));
                HashSet<int> ActsWOSched = new HashSet<int>(acts.Where(x => !actScheds.Contains(x)));
                CustomerEDSC.ActivityScheduleDTDataTable Schedsdt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(ActsWISched);

                if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDate(Schedsdt, dtFrom, dtTo);
                }

                if (!mon || !tue || !wed || !thu || !fri || !sat || !sun)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesDay(Schedsdt, mon, tue, wed, thu, fri, sat, sun);
                }
                if (timeFrom != SystemConstants.nodate.TimeOfDay && timeTo != SystemConstants.nodate.TimeOfDay)
                {
                    if (ActsWISched.Count != 0)
                        ActsWISched = FilterActivitiesTime(Schedsdt, timeFrom, timeTo);
                }


                if (ActsWISched != null)
                {
                    var filteredActivities = activities.Where(x => ActsWISched.Contains(x.ActivityID));
                    if (ActsWOSched != null)
                        activities = activities.Where(x => ActsWOSched.Contains(x.ActivityID));
                    activities = filteredActivities.Select(x => x).Concat(activities.Select(y => y));
                }
            }
            activities = activities.AsEnumerable();

            if (activities != null && activities.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt.Count;
            }
            else return 0;
        }

        public CustomerEDSC.v_ActivityExplorerDTRow RetrieveActivityExplorer(int ActivityID)
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
                CustomerEDSC.v_ActivityExplorerDTRow dr = new CustomerEDSC.v_ActivityExplorerDTDataTable().Newv_ActivityExplorerDTRow();
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

        public CustomerEDSC.v_ActivityViewDTRow RetrieveActivityView(int ActivityID)
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
                CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
                CustomerEDSC.v_ActivityViewDTRow dr = new CustomerEDSC.v_ActivityViewDTDataTable().Newv_ActivityViewDTRow();

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

        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveActivityExplorersbyIDs(int[] activityID, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    from id in activityID
                    where a.ID.ToString().Contains(activityID.ToString())
                    select a;

            if (query.Count() == 0)
                return null;
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



            if (activities != null && activities.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }

        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveActivityExplorersbyIDs(HashSet<int> activityID, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    where activityID.Contains(a.ID)
                    select a;
            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();

            if (activities != null && activities.Count() != 0)
            {
                if (sortExpression == SystemConstants.sortName)
                    activities = activities.OrderBy(row => row.Name);
                else if (sortExpression == SystemConstants.sortLatest)
                    activities = activities.OrderBy(row => row.ModifiedDateTime);
                else if (sortExpression == SystemConstants.sortExpiry)
                    activities = activities.OrderBy(row => row.ModifiedDateTime);
                else if (sortExpression == SystemConstants.sortNameDesc)
                    activities = activities.OrderByDescending(row => row.Name);
                else if (sortExpression == SystemConstants.sortLatestDesc)
                    activities = activities.OrderByDescending(row => row.ModifiedDateTime);
                else if (sortExpression == SystemConstants.sortExpiryDesc)
                    activities = activities.OrderByDescending(row => row.ModifiedDateTime);

                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }
        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveActivityExplorersbyIDs(HashSet<int> activityID, int startIndex, int Amount)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityExplorer> query = null;

            query = from a in ent.v_ActivityExplorer
                    where activityID.Contains(a.ID)
                    select a;
            IEnumerable<v_ActivityExplorer> activities = query.AsEnumerable();

            if (activities != null && activities.Count() != 0)
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveActivityViewsbyIDs(HashSet<int> activityID, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_ActivityView> query = null;

            query = from a in ent.v_ActivityView
                    where activityID.Contains(a.ID)
                    select a;



            if (query.Count() == 0)
                return null;
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

            if (activities != null)
            {
                CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;

        }

        public int RetrieveActivityExplorersbyIDsCount(HashSet<int> activityID)
        {
            if (activityID == null)
                return 0;
            else
            {
                HCEntities ent = new HCEntities();
                IQueryable<v_ActivityExplorer> query = null;

                query = from a in ent.v_ActivityExplorer
                        where activityID.Contains(a.ID)
                        select a;

                return query.Count();
            }
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

        public CustomerEDSC.ActivityDTRow RetrieveActivity(int activityID)
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
                CustomerEDSC.ActivityDTDataTable dt = new CustomerEDSC.ActivityDTDataTable();
                CustomerEDSC.ActivityDTRow dr = new CustomerEDSC.ActivityDTDataTable().NewActivityDTRow();

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
        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveSearchActivities(string searchKey, int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.v_ActivityView
                        where a.Name.Contains(searchKey) || a.CategoryName.Contains(searchKey)
                        || a.Name.Contains(searchKey) || a.CategoryLevel1ParentName.Contains(searchKey)
                        || a.CategoryLevel2ParentName.Contains(searchKey)
                        orderby sortExpression
                        select a;

            IEnumerable<v_ActivityView> activities = query.Skip(startIndex).Take(amount).AsEnumerable();

            CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
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

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveSearchProviderActivities(Guid providerID, String searchKey, int startIndex, int amount, string sortExpression)
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
                               where a.Name.ToUpper().Contains(w.ToUpper()) || a.ShortDescription.ToUpper().Contains(w.ToUpper())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_ActivityView> activities = Suggestions.Skip(startIndex).Take(amount).AsEnumerable();

            CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
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

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveActivityViews(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
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
                CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
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

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveActivityViewsFromActivitiesIDArray(Guid providerID, int[] activitiesID, int startIndex, int amount, string sortExpression)
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
                CustomerEDSC.v_ActivityViewDTDataTable dt = new CustomerEDSC.v_ActivityViewDTDataTable();
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

        public CustomerEDSC.ActivityDTDataTable RetrieveActivities(Guid providerID, int categoryID, int startIndex, int amount, string sortExpression)
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
                CustomerEDSC.ActivityDTDataTable dt = new CustomerEDSC.ActivityDTDataTable();
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

        public CustomerEDSC.MenuDTDataTable RetrieveChildMenus(int parentID)
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
                CustomerEDSC.MenuDTDataTable dt = new CustomerEDSC.MenuDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Menus, dt, LoadOption.PreserveChanges);
                return dt;
            }

        }
        public CustomerEDSC.v_MenuDTDataTable RetrieveMenuExplorers(int menuType)
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.v_Menu
                        where v.MenuType == menuType
                        orderby v.Sequence
                        select v;


            CustomerEDSC.v_MenuDTDataTable dt = new CustomerEDSC.v_MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }
        public CustomerEDSC.MenuDTRow RetrieveMenu(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.Menu
                        where m.ID == SelectedMenuID
                        select m;

            Menu menu = query.FirstOrDefault();
            CustomerEDSC.MenuDTRow dr = new CustomerEDSC.MenuDTDataTable().NewMenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public CustomerEDSC.v_MenuDTRow RetrieveMenuExplorer(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.v_Menu
                        where m.ID == SelectedMenuID
                        select m;

            v_Menu menu = query.FirstOrDefault();
            CustomerEDSC.v_MenuDTRow dr = new CustomerEDSC.v_MenuDTDataTable().Newv_MenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public CustomerEDSC.MenuDTDataTable RetrieveMenus()
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.Menu
                        orderby v.Sequence
                        select v;


            CustomerEDSC.MenuDTDataTable dt = new CustomerEDSC.MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }
        #endregion

        #region page

        public CustomerEDSC.PageDTRow RetrievePage(string PageName)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.Name == PageName
                        select p;

            Page page = query.FirstOrDefault();

            if (page != null)
            {
                var dr = new CustomerEDSC.PageDTDataTable().NewPageDTRow(); ;
                ObjectHandler.CopyPropertyValues(page, dr);
                return dr;
            }
            else return null;
        }

        public CustomerEDSC.PageDTRow RetrievePage(int pageID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.ID == pageID
                        select p;

            var page = query.FirstOrDefault();

            if (page != null)
            {
                var dr = new CustomerEDSC.PageDTDataTable().NewPageDTRow(); ;
                ObjectHandler.CopyPropertyValues(page, dr);
                return dr;
            }
            else return null;

        }

        public CustomerEDSC.PageDTDataTable RetrievePages(int PageType, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.PageType == PageType
                        orderby p.Name
                        select p;

            IEnumerable<Page> page = query.Skip(startIndex).Skip(amount).AsEnumerable();
            if (page != null)
            {
                CustomerEDSC.PageDTDataTable dt = new CustomerEDSC.PageDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(page, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public CustomerEDSC.PageDTDataTable RetrievePages()
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        orderby p.Name
                        select p;

            IEnumerable<Page> page = query.AsEnumerable();
            if (page != null)
            {
                CustomerEDSC.PageDTDataTable dt = new CustomerEDSC.PageDTDataTable();
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

        #region user-Provider

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

        public CustomerEDSC.UserProfilesDTRow RetrieveMember(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.UserProfiles
                        where u.UserID == UserID
                        select u;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                var dr = new CustomerEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();
                ObjectHandler.CopyPropertyValues(user, dr);
                return dr;
            }
            else return null;
        }

        public CustomerEDSC.UserProfilesDTRow RetrieveUserProfiles(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.UserProfiles
                        where u.UserID == UserID
                        select u;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                var dr = new CustomerEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();
                ObjectHandler.CopyPropertyValues(user, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateProviderProviles(CustomerEDSC.ProviderProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.UserProfiles
                        where ac.UserID == dr.UserID
                        select ac;

            UserProfiles provider = query.FirstOrDefault();
            if (provider != null)
            {
                ObjectHandler.CopyPropertyValues(dr, provider);
                ent.SaveChanges();
            }
        }

        public CustomerEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(Guid providerID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.UserID == providerID
                        select p;
            ProviderProfiles prov = query.FirstOrDefault();
            if (prov != null)
            {
                CustomerEDSC.ProviderProfilesDTRow dr = new CustomerEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();
                ObjectHandler.CopyPropertyValues(prov, dr);
                return dr;
            }
            else return null;
        }

        public CustomerEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(String providerUsername)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.Username == providerUsername
                        select p;

            var dt = new CustomerEDSC.ProviderProfilesDTDataTable();
            var dr = new CustomerEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();

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

        public void InsertNewUserProfiles(CustomerEDSC.UserProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();
            UserProfiles user = new UserProfiles();
            ObjectHandler.CopyPropertyValues(dr, user);

            ent.AddToUserProfiles(user);
            ent.SaveChanges();
        }

        public void UpdateUserProfiles(CustomerEDSC.UserProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.UserProfiles
                        where ac.UserID == dr.UserID
                        select ac;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(dr, user);
                ent.SaveChanges();
            }
        }
        #endregion

        #region Activity
        public CustomerEDSC.ActivityGroupingDTRow RetrieveActivityGroup(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from g in ent.ActivityGrouping
                        where g.ActivityID == ActivityID
                        select g;

            if (query != null)
            {
                ActivityGrouping group = query.FirstOrDefault();
                var dr = new CustomerEDSC.ActivityGroupingDTDataTable().NewActivityGroupingDTRow();
                ObjectHandler.CopyPropertyValues(group, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateActivity(CustomerEDSC.ActivityDTRow drDetail)
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

        public void UpdateActivityContactDetail(CustomerEDSC.ActivityContactDetailDTRow contactDetails)
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

        public void UpdateActivityGrouping(CustomerEDSC.ActivityGroupingDTRow drActGrouping)
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
            using (TransactionScope trans = new TransactionScope())
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
                trans.Complete();
            }
        }

        private void DeleteActivitySchedule(int activityScheduleID)
        {
            HCEntities ent = new HCEntities();

            var query = from asched in ent.ActivitySchedule
                        where asched.ID == activityScheduleID
                        select asched;

            ActivitySchedule slot = query.FirstOrDefault();
            if (slot != null)
            {
                ent.DeleteObject(slot);
                ent.SaveChanges();
            }
        }

        public CustomerEDSC.v_ActivityViewDTDataTable RetrieveProviderActivitiesFilteredReport(Guid ProviderID, string SearchKey, int ageFrom, int ageTo, int postCode, int CategoryID)
        {
            String[] Keywords = Array.ConvertAll(SearchKey.Split(';'), c => c.Trim());

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
                            where a.AgeFrom <= ageFrom && a.AgeTo >= ageTo
                            select a;

            if (postCode != 0)
                paramFiltered = from a in providerCatFiltered
                                where a.SuburbID == postCode
                                select a;


            if (!string.IsNullOrEmpty(SearchKey))
            {
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

            var dt = new CustomerEDSC.v_ActivityViewDTDataTable();
            if (activitiesReport != null)
            {
                ObjectHandler.CopyEnumerableToDataTable(activitiesReport, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else
                return null;
        }

        public CustomerEDSC.ActivityScheduleDTDataTable RetrieveActivitiesSchedulesbyIDs(HashSet<int> activityID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<ActivitySchedule> query = null;

            query = from a in ent.ActivitySchedule
                    select a;
            query = query.Where(a => activityID.Contains(a.ActivityID));


            if (query.Count() == 0)
                return null;
            IEnumerable<ActivitySchedule> actsched = query.AsEnumerable();

            if (actsched != null)
            {
                CustomerEDSC.ActivityScheduleDTDataTable dt = new CustomerEDSC.ActivityScheduleDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(actsched, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;
        }

        private HashSet<int> FilterActivitiesDay(CustomerEDSC.ActivityScheduleDTDataTable dt, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun)
        {
            HashSet<int> actIDs = new HashSet<int>();

            var scheds = from s in dt
                         where (s.OnMonday == mon && mon != false) || (s.OnTuesday == tue && tue != false) || (s.OnWednesday == wed && wed != false)
                         || (s.OnThursday == thu && thu != false) || (s.OnFriday == fri && fri != false) || (s.OnSaturday == sat && sat != false) || (s.OnSunday == sun && sun != false)
                         select s.ActivityID;
            actIDs = new HashSet<int>(scheds.AsEnumerable());
            return actIDs;
        }

        private HashSet<int> FilterActivitiesDate(CustomerEDSC.ActivityScheduleDTDataTable dt, DateTime dtFrom, DateTime dtTo)
        {

            //CustomerEDSC.ActivityScheduleDTDataTable dt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(actIDs);

            var query = from a in dt
                        where (dtFrom.DayOfYear >= a.ActivityStartDatetime.DayOfYear || dtTo.DayOfYear <= a.ActivityExpiryDate.DayOfYear)
                        select a;

            if (dtFrom.TimeOfDay != SystemConstants.nodate.TimeOfDay || dtTo.TimeOfDay != SystemConstants.nodate.TimeOfDay)
            {
                query = from a in query
                        where (dtFrom.TimeOfDay <= a.ActivityStartDatetime.TimeOfDay && dtTo.TimeOfDay >= a.ActivityEndDatetime.TimeOfDay)
                        select a;
            }
            if (query.AsEnumerable() != null)
            {
                HashSet<int> actIDs = new HashSet<int>(query.AsEnumerable().Select(x => x.ActivityID));
                return actIDs;
            }
            else return null;
        }
        private HashSet<int> FilterActivitiesTime(CustomerEDSC.ActivityScheduleDTDataTable dt, TimeSpan tmFrom, TimeSpan tmTo)
        {

            //CustomerEDSC.ActivityScheduleDTDataTable dt = new CustomerDAC().RetrieveActivitiesSchedulesbyIDs(actIDs);

            if (tmFrom != SystemConstants.nodate.TimeOfDay || tmTo != SystemConstants.nodate.TimeOfDay)
            {
                var query = from a in dt
                            where (tmFrom <= a.ActivityStartDatetime.TimeOfDay && tmFrom <= a.ActivityEndDatetime.TimeOfDay) || (tmTo <= a.ActivityStartDatetime.TimeOfDay && tmTo <= a.ActivityEndDatetime.TimeOfDay)
                            select a;
                if (query.AsEnumerable() != null)
                {
                    HashSet<int> actIDs = new HashSet<int>(query.AsEnumerable().Select(x => x.ActivityID));
                    return actIDs;
                }
                else return null;
            }
            else return null;

        }

        public CustomerEDSC.UserImageDetailDTRow RetrieveProviderPrimaryImage(Guid providerID)
        {
            HCEntities ent = new HCEntities();

            var query = from pi in ent.UserImageDetail
                        where pi.UserID == providerID && pi.isPrimaryImage == true
                        select pi;

            UserImageDetail detail = query.FirstOrDefault();
            if (detail != null)
            {
                var dr = new CustomerEDSC().UserImageDetailDT.NewUserImageDetailDTRow();
                ObjectHandler.CopyPropertyValues(detail, dr);
                return dr;
            }
            else return null;
        }

        public CustomerEDSC.ActivityScheduleDTDataTable RetrieveActivitySchedules(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.ActivitySchedule
                        where a.ActivityID == ActivityID
                        select a;

            var actSched = query.AsEnumerable();

            if (actSched.Count() != 0)
            {
                var dt = new CustomerEDSC.ActivityScheduleDTDataTable();
                actSched.CopyEnumerableToDataTable(dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        #endregion

        #region emailtemplate

        public CustomerEDSC.v_EmailExplorerDTRow RetrieveMailTemplate(int templateType)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.v_EmailExplorer
                        where e.EmailType == templateType
                        select e;

            if (query.FirstOrDefault() != null)
            {
                var dr = new CustomerEDSC.v_EmailExplorerDTDataTable().Newv_EmailExplorerDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;
        }

        #endregion

        #region Users
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

        public Guid RetrieveUserGUIDbyEmailAddress(string EmailAddress)
        {
            if (string.IsNullOrEmpty(EmailAddress))
                return Guid.Empty;
            else
            {
                HCEntities ent = new HCEntities();

                var query = from u in ent.UserProfiles
                            where u.Email == EmailAddress
                            select u;

                if (query.FirstOrDefault() != null)
                    return query.FirstOrDefault().UserID;
                else
                {
                    var nextquery = from u in ent.ProviderProfiles
                                    where u.Email == EmailAddress
                                    select u;

                    if (nextquery.FirstOrDefault() != null)
                        return nextquery.FirstOrDefault().UserID;
                    else return Guid.Empty;
                }
            }
        }

        public CustomerEDSC.UserProfilesDTDataTable RetrieveUserProfiles()
        {
            HCEntities ent = new HCEntities();

            IQueryable<UserProfiles> query = from p in ent.UserProfiles
                                             select p;
            var Users = query.AsEnumerable();
            if (Users != null)
            {
                var dt = new CustomerEDSC.UserProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Users, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
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
        #endregion

        #region UserImage
        public void CreateUserImageInformation(CustomerEDSC.UserImageDTRow dr)
        {
            HCEntities ent = new HCEntities();
            UserImage ii = new UserImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToUserImage(ii);
            ent.SaveChanges();
        }

        public void CreateUserImageInformation(CustomerEDSC.UserImageDTRow dr, out int iiID)
        {
            HCEntities ent = new HCEntities();
            UserImage ii = new UserImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToUserImage(ii);
            ent.SaveChanges();
            iiID = ii.ID;
        }

        public void UpdateUserImageInformation(Guid userID, int iiID, CustomerEDSC.UserImageDTRow dr)
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

        public void CreateUserImage(CustomerEDSC.UserImageDetailDTRow dr, out int imageID1)
        {
            HCEntities ent = new HCEntities();
            UserImageDetail ai = new UserImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToUserImageDetail(ai);
            ent.SaveChanges();
            imageID1 = ai.ID;
        }

        public void UpdateUserImage(CustomerEDSC.UserImageDetailDTRow dr)
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

        public CustomerEDSC.UserImageDTRow RetrieveUserImageInformation(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImage
                        where i.UserID == UserID
                        select i;
            var ii = query.FirstOrDefault();
            if (ii != null)
            {
                CustomerEDSC.UserImageDTRow dr = new CustomerEDSC.UserImageDTDataTable().NewUserImageDTRow();
                ObjectHandler.CopyPropertyValues(ii, dr);
                return dr;
            }
            else
                return null;
        }

        public CustomerEDSC.UserImageDetailDTDataTable RetrieveUserImages(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID
                        orderby i.ID
                        select i;

            CustomerEDSC.UserImageDetailDTDataTable dt = new CustomerEDSC.UserImageDetailDTDataTable();
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

        public CustomerEDSC.UserImageDetailDTRow RetrievePrimaryProductImage(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.isPrimaryImage == true
                        select i;

            CustomerEDSC.UserImageDetailDTRow dr = new CustomerEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
            if (query.FirstOrDefault() != null)
            {
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
            }
            return dr;
        }

        public CustomerEDSC.UserImageDetailDTRow RetrieveProductMainImage(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.isPrimaryImage == true
                        select i;

            CustomerEDSC.UserImageDetailDTRow dr = new CustomerEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
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

        public CustomerEDSC.UserImageDetailDTRow RetrieveUserImage(Guid UserID, int imageID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.UserImageDetail
                        where i.UserID == UserID && i.ID == imageID
                        select i;

            CustomerEDSC.UserImageDetailDTRow dr = new CustomerEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
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

        public CustomerEDSC.ActivityImageDetailDTRow RetrieveImageDetail(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.ActivityImageDetail
                        where e.ID == imgID
                        select e;

            if (query.FirstOrDefault() != null)
            {
                var dr = new CustomerEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;
        }

        #endregion

        #region webconfig

        public CustomerEDSC.WebConfigurationDTRow RetrieveWebConfiguration()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                var dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;
        }

        public CustomerEDSC.WebConfigurationDTRow RetrieveEmailServerSetting()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                CustomerEDSC.WebConfigurationDTRow dr = new CustomerEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);

                if (dr.IsSMTPAccountNull())
                    return null;
                else return dr;
            }
            else return null;
        }

        #endregion

        #region savedList

        public void RemoveFromSavedList(string username, Guid userID, int actID)
        {

            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID && a.ListValue == actID
                        select a;

            UserSavedList list = query.FirstOrDefault();
            if (list != null)
            {
                ent.DeleteObject(list);
                ent.SaveChanges();
            }

        }

        public void AddToSavedList(CustomerEDSC.UserSavedListDTRow list)
        {
            HCEntities ent = new HCEntities();
            UserSavedList savedList = new UserSavedList();
            ObjectHandler.CopyPropertyValues(list, savedList);
            ent.AddToUserSavedList(savedList);
            ent.SaveChanges();
        }

        public CustomerEDSC.UserSavedListDTDataTable retrieveUserSavedList(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID
                        select a;

            var list = query.AsEnumerable();
            if (list != null)
            {
                var dt = new CustomerEDSC.UserSavedListDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(list, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int retrieveUserSavedListCount(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID
                        select a;

            return query.Count();
        }

        public CustomerEDSC.UserSavedListDTDataTable retrieveUserActivityList(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID && a.ListType == (int)SystemConstants.SavedListType.Activity
                        select a;

            var list = query.AsEnumerable();
            if (list != null)
            {
                var dt = new CustomerEDSC.UserSavedListDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(list, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int retrieveUserActivityListCount(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID && a.ListType == (int)SystemConstants.SavedListType.Activity
                        select a;

            return query.Count();
        }

        public void InsertNewRewardUser(CustomerEDSC.UserRewardDTRow drr)
        {
            HCEntities ent = new HCEntities();
            UserReward user = new UserReward();
            ObjectHandler.CopyPropertyValues(drr, user);

            ent.AddToUserReward(user);
            ent.SaveChanges();
        }

        public CustomerEDSC.UserSavedListDTDataTable retrieveUserRewardList(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID && a.ListType == (int)SystemConstants.SavedListType.Reward
                        select a;

            var list = query.AsEnumerable();
            if (list != null)
            {
                var dt = new CustomerEDSC.UserSavedListDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(list, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int retrieveUserRewardListCount(Guid userID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.UserSavedList
                        where a.OwnerGuid == userID && a.ListType == (int)SystemConstants.SavedListType.Reward
                        select a;

            return query.Count();
        }

        #endregion

        #region RewardListing

        public CustomerEDSC.v_RewardExplorerDTDataTable RetrieveRewardCart(string RewardID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query = null;

            //START FILTERING REWARDS BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardID) && RewardID != "0")
            {

                string[] rewards = RewardID.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                query = from a in ent.v_RewardExplorer
                        from s in rewardsInt
                        where a.ID == s
                        select a;
            }
            IEnumerable<v_RewardExplorer> allrewards = query.AsEnumerable();
            if (allrewards != null)
            {
                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;
        }

        public void InsertNewVoucherDetail(CustomerEDSC.VoucherDetailsDTRow dr)
        {
            HCEntities ent = new HCEntities();
            VoucherDetails user = new VoucherDetails();
            ObjectHandler.CopyPropertyValues(dr, user);

            ent.AddToVoucherDetails(user);
            ent.SaveChanges();
        }


        public int RetrieveRewardCartCount(string RewardID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query = null;

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardID) && RewardID != "0")
            {

                string[] rewards = RewardID.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                query = from a in ent.v_RewardExplorer
                        from s in rewardsInt
                        where a.ID == s
                        select a;
            }
            IEnumerable<v_RewardExplorer> allrewards = query.AsEnumerable();
            if (allrewards != null)
            {
                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);
                return dt.Count;
            }
            else
                return 0;
        }


        public CustomerEDSC.v_UserAttendanceViewDTDataTable RetrieveActAttendance(Guid UserID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_UserAttendanceView> query = null;

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (UserID != Guid.Empty)
            {

                query = from a in ent.v_UserAttendanceView
                        where a.UserID == UserID
                        select a;
            }
            IEnumerable<v_UserAttendanceView> allUsers = query.AsEnumerable();

            if (allUsers != null)
            {
                allUsers = allUsers.OrderByDescending(row => row.CreatedDateTime);
                CustomerEDSC.v_UserAttendanceViewDTDataTable dt = new CustomerEDSC.v_UserAttendanceViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allUsers, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;
        }



        public CustomerEDSC.v_VoucherExplorerDTDataTable RetrieveVouchers(string VoucherID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_VoucherExplorer> query = null;

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(VoucherID) && VoucherID != "0")
            {

                string[] Vouchers = VoucherID.Split('|');
                List<string> Vouchersstring = new List<string>();
                foreach (var Voucher in Vouchers)
                {
                    var Voucherstring = Convert.ToString(Voucher);
                    Vouchersstring.Add(Voucherstring);
                }
                query = from a in ent.v_VoucherExplorer
                        from s in Vouchersstring
                        where a.VoucherCode == s
                        select a;
            }
            var allVouchers = query.AsEnumerable();
            if (allVouchers != null)
            {
                CustomerEDSC.v_VoucherExplorerDTDataTable dt = new CustomerEDSC.v_VoucherExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allVouchers, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;
        }


        public int RetrieveVoucherCount(string VoucherID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_VoucherExplorer> query = null;

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(VoucherID) && VoucherID != "0")
            {

                string[] Vouchers = VoucherID.Split('|');
                List<string> Vouchersstring = new List<string>();
                foreach (var Voucher in Vouchers)
                {
                    var Voucherstring = Convert.ToString(Voucher);
                    Vouchersstring.Add(Voucherstring);
                }
                query = from a in ent.v_VoucherExplorer
                        from s in Vouchersstring
                        where a.VoucherCode == s
                        select a;
            }
            IEnumerable<v_VoucherExplorer> allVouchers = query.AsEnumerable();
            if (allVouchers != null)
            {
                CustomerEDSC.v_VoucherExplorerDTDataTable dt = new CustomerEDSC.v_VoucherExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allVouchers, dt, LoadOption.OverwriteChanges);
                return dt.Count;
            }
            else
                return 0;
        }




        public int RetrieveActAttCount(Guid UserID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_UserAttendanceView> query = null;

            //START FILTERING ACTIVITY BY UserTYPE
            if (UserID != Guid.Empty)
            {


                query = from a in ent.v_UserAttendanceView
                        where a.UserID == UserID
                        select a;
            }
            IEnumerable<v_UserAttendanceView> allUsers = query.AsEnumerable();
            if (allUsers != null)
            {
                CustomerEDSC.v_UserAttendanceViewDTDataTable dt = new CustomerEDSC.v_UserAttendanceViewDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allUsers, dt, LoadOption.OverwriteChanges);
                return dt.Count;
            }
            else
                return 0;
        }

        public Guid getSponsorID(int rewardid)
        {
            HCEntities ent = new HCEntities();
            var query = from a in ent.v_RewardExplorer
                        where a.RewardID == rewardid
                        select a;
            if (query.FirstOrDefault() != null)
                return query.FirstOrDefault().ProviderID;
            else return Guid.Empty;

        }

        public CustomerEDSC.v_RewardExplorerDTDataTable RetrieveAdminRewardsbySearchPhrase(Guid providerID, string searchKey, int ageFrom, int ageTo, string RewardType, int categoryID, int startIndex, int amount, string sortExpression)
        {

            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query;
            //filtering ProviderID  
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_RewardExplorer
                        where a.ProviderID == providerID
                        orderby a.ID
                        select a;
            }
            else
            {
                query = from a in ent.v_RewardExplorer
                        orderby a.ID
                        select a;
            }

            IEnumerable<v_RewardExplorer> Suggestions = (from a in query
                                                         from w in Keywords
                                                         where a.RewardsName.ToLower().Contains(w.ToLower()) || a.RewardDescription.ToLower().Contains(w.ToLower())
                                                         || a.Keywords.ToLower().Contains(w.ToLower())
                                                         select a).Distinct().ToArray();


            //START FILTERING ACTIVITY BY CATEGORY
            if (categoryID != 0)
            {
                Suggestions = from a in Suggestions
                              where a.CategoryID == categoryID
                              select a;
            }


            //START FILTERING REWARDS BY POINTS
            if (ageFrom != 0 && ageTo != 0)
            {
                Suggestions = from a in Suggestions
                              where a.RequiredRewardPoint >= ageFrom && a.RequiredRewardPoint <= ageTo
                              select a;
            }

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardType) && RewardType != "0")
            {

                string[] rewards = RewardType.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                Suggestions = from a in Suggestions
                              from s in rewardsInt
                              where a.RewardSource == s
                              select a;
            }

            IEnumerable<v_RewardExplorer> allrewards = Suggestions.Skip(startIndex).Take(amount).AsEnumerable();

            if (sortExpression == SystemConstants.sortName)
                allrewards = allrewards.OrderBy(row => row.RewardsName);

            else if (sortExpression == SystemConstants.sortExpiry)
                allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);

            else if (sortExpression == SystemConstants.sortNameDesc)
                allrewards = allrewards.OrderByDescending(row => row.RewardsName);
            else if (sortExpression == SystemConstants.sortLatestDesc)
                allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
            else if (sortExpression == SystemConstants.sortExpiryDesc)
                allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
            else if (sortExpression == SystemConstants.sortPoints)
                allrewards = allrewards.OrderBy(row => row.RequiredRewardPoint);
            else if (sortExpression == SystemConstants.sortPointsDesc)
                allrewards = allrewards.OrderByDescending(row => row.RequiredRewardPoint);
            else if (sortExpression == SystemConstants.sortType)
                allrewards = allrewards.OrderByDescending(row => row.RewardType);

            CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);


            return dt;
        }

        public CustomerEDSC.v_RewardExplorerDTDataTable RetrieveAdminRewardsbySearchPhrase(Guid providerID, string searchKey, string sortExpression)
        {
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query;
            //filtering ProviderID  
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_RewardExplorer
                        where a.ProviderID == providerID
                        orderby a.ID
                        select a;
            }
            else
            {
                query = from a in ent.v_RewardExplorer
                        orderby a.ID
                        select a;
            }


            var Suggestions = (from a in query
                               from w in Keywords
                               where a.RewardsName.ToLower().Contains(w.ToLower()) || a.RewardDescription.ToLower().Contains(w.ToLower())
                               || a.Keywords.ToLower().Contains(w.ToLower())
                               orderby sortExpression
                               select a).Distinct().ToArray();

            IEnumerable<v_RewardExplorer> allrewards = Suggestions.AsEnumerable();
            if (allrewards != null)
            {

                if (sortExpression == SystemConstants.sortName)
                    allrewards = allrewards.OrderBy(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatest)
                    allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiry)
                    allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);


                else if (sortExpression == SystemConstants.sortNameDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatestDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiryDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiryDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardType);
                else if (sortExpression == SystemConstants.sortPoints)
                    allrewards = allrewards.OrderBy(row => row.RequiredRewardPoint);
                else if (sortExpression == SystemConstants.sortPointsDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RequiredRewardPoint);

                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else return null;
        }


        public int RetrieveAdminRewardsbySearchPhraseCount(Guid providerID, int ageFrom, int ageTo, string RewardType, int categoryID, string searchKey)
        {
            //splitting keywords as keyword can be a multiple words
            String[] Keywords = Array.ConvertAll(searchKey.Split(';'), c => c.Trim());

            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query;
            //filtering ProviderID  
            if (providerID != Guid.Empty)
            {
                query = from a in ent.v_RewardExplorer
                        where a.ProviderID == providerID
                        orderby a.ID
                        select a;
            }
            else
            {
                query = from a in ent.v_RewardExplorer
                        orderby a.ID
                        select a;
            }

            IEnumerable<v_RewardExplorer> Suggestions = (from a in query
                                                         from w in Keywords
                                                         where a.RewardsName.ToLower().Contains(w.ToLower()) || a.RewardDescription.ToLower().Contains(w.ToLower())
                                                         || a.Keywords.ToLower().Contains(w.ToLower())
                                                         select a).Distinct().ToArray();


            //START FILTERING ACTIVITY BY CATEGORY
            if (categoryID != 0)
            {
                Suggestions = from a in Suggestions
                              where a.CategoryID == categoryID
                              select a;
            }


            //START FILTERING REWARDS BY POINTS
            if (ageFrom != 0 && ageTo != 0)
            {
                Suggestions = from a in Suggestions
                              where a.RequiredRewardPoint >= ageFrom && a.RequiredRewardPoint <= ageTo
                              select a;
            }

            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardType) && RewardType != "0")
            {

                string[] rewards = RewardType.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                Suggestions = from a in Suggestions
                              from s in rewardsInt
                              where a.RewardSource == s
                              select a;
            }

            IEnumerable<v_RewardExplorer> activities = Suggestions.AsEnumerable();
            CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
            ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);

            return Suggestions.Count();
        }


        public CustomerEDSC.v_RewardExplorerDTDataTable RetrieveAdminRewards(int categoryID, Guid providerID, int ageFrom, int ageTo, string RewardType, string sortExpression, int startIndex, int amount)
        {

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_RewardExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_RewardExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID))
                        orderby sortExpression
                        select a;
            }

            else
            {
                query = from a in ent.v_RewardExplorer

                        orderby sortExpression
                        select a;
            }

            //START FILTERING ACTIVITY BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                query = from a in query
                        where a.RequiredRewardPoint >= ageFrom && a.RequiredRewardPoint <= ageTo
                        orderby sortExpression
                        select a;
            }



            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardType) && RewardType != "0")
            {

                string[] rewards = RewardType.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                query = from a in query
                        from s in rewardsInt
                        where a.RewardSource == s
                        select a;
            }
            IEnumerable<v_RewardExplorer> allrewards = query.AsEnumerable();

            if (allrewards != null)
            {

                if (sortExpression == SystemConstants.sortName)
                    allrewards = allrewards.OrderBy(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatest)
                    allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiry)
                    allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortPoints)
                    allrewards = allrewards.OrderBy(row => row.RequiredRewardPoint);
                else if (sortExpression == SystemConstants.sortPointsDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RequiredRewardPoint);

                else if (sortExpression == SystemConstants.sortNameDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatestDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiryDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);

                allrewards = allrewards.Skip(startIndex).Take(amount);

                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else return null;
        }

        public CustomerEDSC.v_RewardExplorerDTDataTable RetrieveAdminRewards(Guid providerID, int categoryID, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            IQueryable<v_RewardExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_RewardExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID))
                        orderby sortExpression
                        select a;
            }

            else
            {
                query = from a in ent.v_RewardExplorer
                        orderby sortExpression
                        select a;
            }

            IEnumerable<v_RewardExplorer> allrewards = query.AsEnumerable();
            if (allrewards != null)
            {
                if (sortExpression == SystemConstants.sortName)
                    allrewards = allrewards.OrderBy(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatest)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiry)
                    allrewards = allrewards.OrderBy(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortNameDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardsName);
                else if (sortExpression == SystemConstants.sortLatestDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortExpiryDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RewardExpiryDate);
                else if (sortExpression == SystemConstants.sortPoints)
                    allrewards = allrewards.OrderBy(row => row.RequiredRewardPoint);
                else if (sortExpression == SystemConstants.sortPointsDesc)
                    allrewards = allrewards.OrderByDescending(row => row.RequiredRewardPoint);
                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allrewards, dt, LoadOption.OverwriteChanges);

                return dt;
            }
            else return null;

        }

        public int RetrieveAdminRewardsCount(Guid providerID, int ageFrom, int ageTo, string RewardType, int categoryID)
        {

            HCEntities ent = new HCEntities();

            //filtering ProviderID  
            // GET ALL activity in category
            IQueryable<v_RewardExplorer> query;

            if (providerID != Guid.Empty && categoryID != 0)
            {
                query = from a in ent.v_RewardExplorer
                        where (a.ProviderID == providerID && (
                                        a.CategoryID == categoryID))
                        select a;
            }

            else
            {
                query = from a in ent.v_RewardExplorer

                        select a;
            }


            //START FILTERING Reward BY AGE
            if (ageFrom != 0 && ageTo != 0)
            {
                query = from a in query
                        where a.RequiredRewardPoint >= ageFrom && a.RequiredRewardPoint <= ageTo
                        select a;
            }
            //START FILTERING ACTIVITY BY REWARDTYPE
            if (!string.IsNullOrEmpty(RewardType) && RewardType != "0")
            {

                string[] rewards = RewardType.Split('|');
                List<int> rewardsInt = new List<int>();
                foreach (var reward in rewards)
                {
                    var rewardInt = Convert.ToInt32(reward);
                    rewardsInt.Add(rewardInt);
                }
                query = from a in query
                        from s in rewardsInt
                        where a.RewardSource == s
                        select a;
            }


            IEnumerable<v_RewardExplorer> activities = query.AsEnumerable();

            if (activities != null)
            {
                CustomerEDSC.v_RewardExplorerDTDataTable dt = new CustomerEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(activities, dt, LoadOption.OverwriteChanges);


                return query.Count();
            }
            else return 0;
        }

        public CustomerEDSC.v_RewardExplorerDTRow RetrieveRewardInfo(int RewardID)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_RewardExplorer> query = null;
            query = from i in ent.v_RewardExplorer
                    where i.ID == RewardID
                    select i;
            if (query.Count() == 0)
            {
                return null;
            }
            var Reward = query.FirstOrDefault();
            if (Reward != null)
            {
                CustomerEDSC.v_RewardExplorerDTRow dr = new CustomerEDSC.v_RewardExplorerDTDataTable().Newv_RewardExplorerDTRow();
                ObjectHandler.CopyPropertyValues(Reward, dr);
                return dr;
            }
            else

                return null;


        }

        public CustomerEDSC.v_RewardUserExplorerDTRow RetrieveUserRewardDetails(string Uname)
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_RewardUserExplorer> query = null;
            query = from i in ent.v_RewardUserExplorer
                    where i.FirstName == Uname
                    select i;
            if (query.Count() == 0)
            {
                return null;
            }
            var Reward = query.FirstOrDefault();
            if (Reward != null)
            {
                CustomerEDSC.v_RewardUserExplorerDTRow dr = new CustomerEDSC.v_RewardUserExplorerDTDataTable().Newv_RewardUserExplorerDTRow();
                ObjectHandler.CopyPropertyValues(Reward, dr);
                return dr;
            }
            else

                return null;


        }


        public CustomerEDSC.RewardImageDTRow RetrieveRewardPrimaryImage(int RewardID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.RewardImage
                        where i.RewardID == RewardID
                        select i;

            CustomerEDSC.RewardImageDTRow dr = new CustomerEDSC.RewardImageDTDataTable().NewRewardImageDTRow();
            RewardImage pi = query.FirstOrDefault();
            if (pi != null)
                ObjectHandler.CopyPropertyValues(pi, dr);
            else
            {
                //dr.ProductID = 0;
                //dr.ImageID = 0;
                return null;
            }
            return dr;
        }

        public byte[] RetrieveRewardBinary(int imgID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.RewardImage
                        where e.ID == imgID
                        select e;
            RewardImage aid = query.FirstOrDefault();

            if (aid != null && aid.ImageStream != null)
            {
                byte[] stream = aid.ImageStream;
                return stream;
            }
            else return null;
        }


        #endregion


        public CustomerEDSC.UserReferenceDTDataTable RetrieveUserReferences()
        {
            HCEntities ent = new HCEntities();

            var query = from r in ent.UserReference
                        select r;

            if (query.AsEnumerable() != null)
            {
                var dt = new CustomerEDSC.UserReferenceDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public void insertNewUserReference(CustomerEDSC.UserReferenceDTRow drRef)
        {
            HCEntities ent = new HCEntities();
            UserReference user = new UserReference();
            ObjectHandler.CopyPropertyValues(drRef, user);

            ent.AddToUserReference(user);
            ent.SaveChanges();
        }
    }
}
