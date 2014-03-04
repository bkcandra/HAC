using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using HealthyClub.Administration.EDS;
using System.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using HealthyClub.Utility;
using System.Collections;
using HealthyClub.EDM;
using BCUtility;



namespace HealthyClub.Administration.DA
{
    public class AdministrationDAC
    {
        #region Category
        public int DetermineCategoryLevel(AdministrationEDSC.CategoryDTRow categoryDR)
        {
            if (categoryDR.IsLevel1ParentIDNull() || categoryDR.Level1ParentID == 0)
                return 0;
            else if (categoryDR.IsLevel2ParentIDNull() || categoryDR.Level2ParentID == 0)
                return 1;
            else
                return 2;
        }

        public void CreateCategory(AdministrationEDSC.CategoryDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Category cat = new Category();
            ObjectHandler.CopyPropertyValues(dr, cat);
            ent.AddToCategory(cat);
            ent.SaveChanges();
        }

        public void UpdateCategory(AdministrationEDSC.CategoryDTRow dr)
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

        public void UpdateCategoryName(AdministrationEDSC.CategoryDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Category
                        where c.ID == dr.ID
                        select c;

            Category cat = query.FirstOrDefault();

            if (cat != null)
            {
                cat.Name = dr.Name;
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

        public AdministrationEDSC.CategoryDTRow RetrieveCategory(int categoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.ID == categoryID
                        select c;

            var dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
            var category = query.SingleOrDefault();

            if (category == null) return null;
            else
            {
                ObjectHandler.CopyPropertyValues(category, dr);
                return dr;
            }
        }

        public AdministrationEDSC.CategoryDTDataTable RetrieveAllCategories()
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();

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

        public AdministrationEDSC.CategoryDTDataTable RetrieveSubCategories(int immediateParentCategoryID, int startIndex, int amount, string sortExpression)
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

            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();

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

        public AdministrationEDSC.CategoryDTDataTable RetrieveAllSubCategories(int parentCategoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.v_CategoryExplorer
                        where c.Level1ParentID == parentCategoryID ||
                                c.Level2ParentID == parentCategoryID

                        select c;

            var dt = new AdministrationEDSC.CategoryDTDataTable();

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

        public AdministrationEDSC.CategoryDTDataTable RetrieveLv0Categories()
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();

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

        public AdministrationEDSC.CategoryDTDataTable RetrieveLv1Categories(int rootCatID)
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();

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

        public int RetrieveCategoryLevel(int CurrentCategoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.v_CategoryExplorer
                        where e.ID == CurrentCategoryID
                        select e;
            var dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
            var category = query.SingleOrDefault();

            if (category == null)
                return SystemConstants.intError;
            else
            {
                return category.Level;
            }


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

        public AdministrationEDSC.SuburbDTDataTable RetrieveSuburbs(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.SuburbDTDataTable dt = new AdministrationEDSC.SuburbDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        orderby sortExpression
                        select q;

            if (query.AsEnumerable().Count() == 0)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query.Skip(startIndex).Take(amount).AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
        }

        public int RetrieveSuburbsCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.SuburbDTDataTable dt = new AdministrationEDSC.SuburbDTDataTable();

            var query = from q in ent.v_SuburbExplorer
                        select q;

            return query.Count();
        }

        public void CreateSuburb(string Modifier, AdministrationEDSC.SuburbDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Suburb sub = new Suburb();
            sub.Name = dr.Name;
            sub.PostCode = dr.PostCode;
            sub.StateID = dr.StateID;

            ent.AddToSuburb(sub);
            ent.SaveChanges();
        }

        public void UpdateSuburb(string Modifier, AdministrationEDSC.SuburbDTRow dr)
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

        public AdministrationEDSC.v_SuburbExplorerDTRow RetrieveSuburb(int suburbID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_SuburbExplorer
                        where s.ID == suburbID
                        select s;

            AdministrationEDSC.v_SuburbExplorerDTDataTable dt = new AdministrationEDSC.v_SuburbExplorerDTDataTable();
            AdministrationEDSC.v_SuburbExplorerDTRow dr = new AdministrationEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();


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

        public AdministrationEDSC.v_SuburbExplorerDTDataTable RetrieveSuburbs()
        {
            HCEntities ent = new HCEntities();
            AdministrationEDSC.v_SuburbExplorerDTDataTable dt = new AdministrationEDSC.v_SuburbExplorerDTDataTable();

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

        #endregion

        #region state
        public AdministrationEDSC.StateDTDataTable RetrieveStates()
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            AdministrationEDSC.StateDTDataTable dt = new AdministrationEDSC.StateDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public AdministrationEDSC.StateDTDataTable RetrieveStates(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        select s;

            AdministrationEDSC.StateDTDataTable dt = new AdministrationEDSC.StateDTDataTable();

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

        public AdministrationEDSC.StateDTRow RetrieveState(int stateID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == stateID
                        select s;

            AdministrationEDSC.StateDTDataTable dt = new AdministrationEDSC.StateDTDataTable();
            AdministrationEDSC.StateDTRow dr = new AdministrationEDSC.StateDTDataTable().NewStateDTRow();


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

        public void CreateState(string userName, AdministrationEDSC.StateDTRow dr)
        {
            HCEntities ent = new HCEntities();
            State state = new State();
            state.StateName = dr.StateName;
            state.StateDetail = dr.StateDetail;

            ent.AddToState(state);
            ent.SaveChanges();
        }

        public void UpdateState(string userName, AdministrationEDSC.StateDTRow dr)
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

        #region Member

        public AdministrationEDSC.UserProfilesDTRow RetrieveUserProfiles(Guid UserID)
        {
            HCEntities ent = new HCEntities();

            var query = from u in ent.UserProfiles
                        where u.UserID == UserID
                        select u;

            UserProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                var dr = new AdministrationEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();
                ObjectHandler.CopyPropertyValues(user, dr);
                return dr;
            }
            else return null;
        }

        public AdministrationEDSC.UserProfilesDTDataTable RetrieveCustomerList(string SearchString, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            IQueryable<UserProfiles> query = from c in ent.UserProfiles
                                             orderby c.FirstName
                                             select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = from c in ent.UserProfiles
                        where c.FirstName.Contains(SearchString) || c.Email.Contains(SearchString)
                        orderby c.FirstName
                        select c;
            }

            var customers = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (customers != null)
            {
                var dt = new AdministrationEDSC.UserProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(customers, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;

        }

        public int RetrieveCustomerListCount(string SearchString)
        {
            HCEntities ent = new HCEntities();

            IQueryable<UserProfiles> query = from c in ent.UserProfiles
                                             orderby c.FirstName
                                             select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = from c in ent.UserProfiles
                        where c.FirstName.Contains(SearchString) || c.Email.Contains(SearchString)
                        orderby c.FirstName
                        select c;
            }

            return query.Count();
        }

        public AdministrationEDSC.UserProfilesDTDataTable RetrieveUserProfiles()
        {
            HCEntities ent = new HCEntities();

            IQueryable<UserProfiles> query = from p in ent.UserProfiles
                                             select p;
            var Users = query.AsEnumerable();
            if (Users != null)
            {
                var dt = new AdministrationEDSC.UserProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Users, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public void UpdateUserProfiles(AdministrationEDSC.UserProfilesDTRow dr)
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

        #region Provider
        public AdministrationEDSC.ProviderProfilesDTDataTable RetrieveProviders()
        {
            HCEntities ent = new HCEntities();

            var query = ent.ProviderProfiles.AsEnumerable();
            if (query != null)
            {
                var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.ProviderProfilesDTDataTable RetrieveProviderList(string SearchString, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            IQueryable<ProviderProfiles> query = from p in ent.ProviderProfiles
                                                 orderby p.FirstName
                                                 select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = from p in ent.ProviderProfiles
                        where p.FirstName.Contains(SearchString) || p.Email.Contains(SearchString)
                        orderby p.FirstName
                        select p;
            }

            var customers = query.Skip(startIndex).Take(amount).AsEnumerable();
            if (customers != null)
            {
                var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(customers, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;

        }

        public int RetrieveProviderListCount(string SearchString)
        {
            HCEntities ent = new HCEntities();

            IQueryable<ProviderProfiles> query = from p in ent.ProviderProfiles
                                                 orderby p.FirstName
                                                 select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                query = from p in ent.ProviderProfiles
                        where p.FirstName.Contains(SearchString) || p.Email.Contains(SearchString)
                        orderby p.FirstName
                        select p;
            }

            return query.Count();
        }

        public AdministrationEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(Guid providerID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.UserID == providerID
                        select p;
            ProviderProfiles prov = query.FirstOrDefault();
            if (prov != null)
            {
                AdministrationEDSC.ProviderProfilesDTRow dr = new AdministrationEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();
                ObjectHandler.CopyPropertyValues(prov, dr);
                return dr;
            }
            else return null;
        }

        public AdministrationEDSC.ProviderProfilesDTRow RetrieveProviderProfiles(String providerUsername)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        where p.Username == providerUsername
                        select p;

            var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();
            var dr = new AdministrationEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();

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

        public AdministrationEDSC.ProviderProfilesDTDataTable RetrieveProviderProfiles()
        {
            HCEntities ent = new HCEntities();

            IQueryable<ProviderProfiles> query = from p in ent.ProviderProfiles
                                                 select p;
            var providers = query.AsEnumerable();
            if (providers != null)
            {
                var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(providers, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public void UpdateProviderProfiles(AdministrationEDSC.ProviderProfilesDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.ProviderProfiles
                        where ac.UserID == dr.UserID
                        select ac;

            ProviderProfiles user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(dr, user);
                ent.SaveChanges();
            }
        }
        #endregion

        #region KeywordsManagement
        public AdministrationEDSC.v_KeyCollectionViewDTDataTable RetrieveKeyCollections(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_KeyCollectionView
                        select s;

            AdministrationEDSC.v_KeyCollectionViewDTDataTable dt = new AdministrationEDSC.v_KeyCollectionViewDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public AdministrationEDSC.v_KeyCollectionViewDTRow RetrieveKeyword(int CollectionID)
        {
            HCEntities ent = new HCEntities();

            var query = from kc in ent.v_KeyCollectionView
                        where kc.ID == CollectionID
                        select kc;

            var dt = new AdministrationEDSC.v_KeyCollectionViewDTDataTable();
            var dr = new AdministrationEDSC.v_KeyCollectionViewDTDataTable().Newv_KeyCollectionViewDTRow();
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
                return dr;
            }
        }

        public int RetrieveKeyCollectionsCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.v_KeyCollectionView
                        select s;

            return query.Count();
        }

        /*public AdministrationEDSC.StateDTRow RetrieveKeywords(int keyCollectionID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.State
                        where s.ID == stateID
                        select s;

            AdministrationEDSC.StateDTDataTable dt = new AdministrationEDSC.StateDTDataTable();
            AdministrationEDSC.StateDTRow dr = new AdministrationEDSC.StateDTDataTable().NewStateDTRow();


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
        }*/

        public void DeleteKeyCollection(int keyColletionID)
        {
            HCEntities ent = new HCEntities();

            var query = from keyColl in ent.KeyCollection
                        where keyColl.ID == keyColletionID
                        select keyColl;

            KeyCollection key = query.FirstOrDefault();
            if (key != null)
            {
                ent.DeleteObject(key);
                ent.SaveChanges();
            }
        }

        public void CreateKeyCollection(AdministrationEDSC.KeyCollectionDTRow drKeyProperties, out int KeyColID)
        {
            HCEntities ent = new HCEntities();
            KeyCollection key = new KeyCollection();

            key.Name = drKeyProperties.Name;
            key.Description = drKeyProperties.Description;

            ent.AddToKeyCollection(key);
            ent.SaveChanges();
            KeyColID = key.ID;
        }

        public void createKeywords(AdministrationEDSC.KeywordDTRow drKeywords, int KeyColID)
        {
            HCEntities ent = new HCEntities();
            Keyword keyword = new Keyword();

            keyword.KeyCollectionID = KeyColID;
            keyword.Keywords = drKeywords.Keywords;

            ent.AddToKeyword(keyword);
            ent.SaveChanges();
        }

        public void UpdateKeyCollection(AdministrationEDSC.KeyCollectionDTRow drKeyProperties)
        {
            HCEntities ent = new HCEntities();

            var query = from kc in ent.KeyCollection
                        where kc.ID == drKeyProperties.ID
                        select kc;

            KeyCollection keyCollection = query.FirstOrDefault();

            if (keyCollection != null)
            {
                //ObjectHandler.CopyProperties(dr, sub);
                keyCollection.Name = drKeyProperties.Name;
                keyCollection.Description = drKeyProperties.Description;
            }

            ent.SaveChanges();
        }

        public void UpdateKeywords(AdministrationEDSC.KeywordDTRow drKeywords)
        {
            HCEntities ent = new HCEntities();

            var query = from k in ent.Keyword
                        where k.ID == drKeywords.KeyCollectionID
                        select k;

            Keyword key = query.FirstOrDefault();

            if (key != null)
            {
                //ObjectHandler.CopyProperties(dr, sub);
                key.Keywords = drKeywords.Keywords;
            }

            ent.SaveChanges();
        }
        #endregion

        #region page

        public AdministrationEDSC.PageDTDataTable RetrievePages(int PageType, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.PageType == PageType
                        orderby p.Name
                        select p;

            IEnumerable<Page> page = query.Skip(startIndex).Skip(amount).AsEnumerable();
            if (page != null)
            {
                AdministrationEDSC.PageDTDataTable dt = new AdministrationEDSC.PageDTDataTable();
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



        public bool isPageExist(string name)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.Name.ToUpper().Equals(name.ToUpper())
                        select p;

            var pages = query.FirstOrDefault();

            if (pages != null)
            {
                return true;
            }
            else return false;
        }

        public AdministrationEDSC.PageDTDataTable RetrievePages(int pageType)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.PageType == pageType
                        select p;

            var pages = query.AsEnumerable();

            if (pages != null)
            {
                var dt = new AdministrationEDSC.PageDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(pages, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.PageDTRow RetrievePage(int pageID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.ID == pageID
                        select p;

            var page = query.FirstOrDefault();

            if (page != null)
            {
                var dr = new AdministrationEDSC.PageDTDataTable().NewPageDTRow(); ;
                ObjectHandler.CopyPropertyValues(page, dr);
                return dr;
            }
            else return null;

        }

        public void DeletePage(int PageID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.ID == PageID
                        select p;

            Page page = query.FirstOrDefault();
            if (page != null)
            {
                ent.DeleteObject(page);
                ent.SaveChanges();
            }
        }

        public void CreatePage(AdministrationEDSC.PageDTRow dr)
        {
            HCEntities ent = new HCEntities();

            Page page = new Page();
            ObjectHandler.CopyPropertyValues(dr, page);
            ent.AddToPage(page);
            ent.SaveChanges();
            dr.ID = page.ID;
        }

        public void UpdatePage(AdministrationEDSC.PageDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        where p.ID == dr.ID
                        select p;

            var page = query.FirstOrDefault();

            if (page != null)
            {
                ObjectHandler.CopyPropertyValues(dr, page);
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.PageDTDataTable RetrievePages()
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Page
                        select p;

            var pages = query.AsEnumerable();

            if (pages != null)
            {
                var dt = new AdministrationEDSC.PageDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        #endregion

        #region MailTemplate

        public AdministrationEDSC.EmailTemplateDTDataTable RetrieveMailTemplates(int EmailType, int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        where p.EmailType == EmailType
                        orderby p.EmailName
                        select p;

            IEnumerable<EmailTemplate> emails = query.Skip(startIndex).Skip(amount).AsEnumerable();
            if (emails != null)
            {
                AdministrationEDSC.EmailTemplateDTDataTable dt = new AdministrationEDSC.EmailTemplateDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(emails, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int RetrieveMailTemplatesCount(int EmailType, int startIndex, int Amount)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        where p.EmailType == EmailType
                        orderby p.EmailName
                        select p;

            return query.Count();
        }

        public AdministrationEDSC.EmailTemplateDTDataTable RetrieveEmailTemplates()
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        select p;

            var templates = query.AsEnumerable();

            if (templates != null)
            {
                var dt = new AdministrationEDSC.EmailTemplateDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(templates, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.EmailTemplateDTDataTable RetrieveEmailTemplates(int EmailType)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        where p.EmailType == EmailType
                        select p;

            var templates = query.AsEnumerable();

            if (templates != null)
            {
                var dt = new AdministrationEDSC.EmailTemplateDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(templates, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.EmailTemplateDTRow RetrieveEmailTemplate(int EmailTemplateID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        where p.ID == EmailTemplateID
                        select p;

            var template = query.FirstOrDefault();

            if (template != null)
            {
                var dr = new AdministrationEDSC.EmailTemplateDTDataTable().NewEmailTemplateDTRow(); ;
                ObjectHandler.CopyPropertyValues(template, dr);
                return dr;
            }
            else return null;

        }

        public void DeleteEmailTemplate(int EmailTemplateID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.EmailTemplate
                        where p.ID == EmailTemplateID
                        select p;

            EmailTemplate template = query.FirstOrDefault();
            if (template != null)
            {
                ent.DeleteObject(template);
                ent.SaveChanges();
            }
        }

        public void CreateEmailTemplate(AdministrationEDSC.EmailTemplateDTRow dr)
        {
            HCEntities ent = new HCEntities();

            EmailTemplate email = new EmailTemplate();
            ObjectHandler.CopyPropertyValues(dr, email);
            ent.AddToEmailTemplate(email);
            ent.SaveChanges();
            dr.ID = email.ID;
        }

        public void UpdateEmailTemplate(AdministrationEDSC.EmailTemplateDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.EmailTemplate
                        where e.ID == dr.ID
                        select e;

            var email = query.FirstOrDefault();

            if (email != null)
            {
                ObjectHandler.CopyPropertyValues(dr, email);
                ent.SaveChanges();
            }
        }

        #endregion

        #region Activities
        public AdministrationEDSC.v_ActivityExplorerDTDataTable RetrieveActivitiesExplorer()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_ActivityExplorer
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.v_ActivityExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.v_VoucherExplorerDTDataTable RetrieveallVouchers()
        {
            HCEntities ent = new HCEntities();
            IQueryable<v_VoucherExplorer> query = null;

            query = from a in ent.v_VoucherExplorer
                    select a;

            var allVouchers = query.AsEnumerable();
            if (allVouchers != null)
            {
                AdministrationEDSC.v_VoucherExplorerDTDataTable dt = new AdministrationEDSC.v_VoucherExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(allVouchers, dt, LoadOption.OverwriteChanges);
                return dt;
            }
            else
                return null;
        }


        public int RetrieveActivitiesExplorerCount()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_ActivityExplorer
                        select q;

            return query.Count();


        }

        public AdministrationEDSC.ActivityDTDataTable RetrieveActivities()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.ActivityDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.ActivityDTDataTable RetrievePendingActivities()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        where q.isApproved == false
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.ActivityDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int RetrievePendingActivitiesCount()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        where q.isApproved == false
                        select q;
            return query.Count();
        }

        public int RetrieveProviderActivitiesCount(Guid ProviderID)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        where q.ProviderID == ProviderID
                        select q;

            return query.Count();


        }

        public int RetrieveApprovedActivitiesCount()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        where q.isApproved == true
                        select q;

            return query.Count();


        }

        public int RetrieveActivitiesCount()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Activity
                        select q;

            return query.Count();
        }
        public int RetrieveActivitiesCount(int status, bool approved)
        {
            HCEntities ent = new HCEntities();
            IQueryable<Activity> query;
            if (status == (int)SystemConstants.ActivityStatus.Deleting || status == (int)SystemConstants.ActivityStatus.Expired)
            {
                query = from q in ent.Activity
                        where q.Status == status
                        select q;
            }
            else
            {
                query = from q in ent.Activity
                        where q.Status == status && q.isApproved == approved
                        select q;
            }


            return query.Count();


        }

        #region Activity Schedule

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

        #endregion

        #endregion

        #region Menu

        private int GetMaxMenuSequence(int? menuID, int? parentMenuItemID)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.Menu
                        where m.ID == menuID && m.ParentMenuID == parentMenuItemID
                        select m.Sequence;

            return Convert.ToInt32((query.Max()));
        }

        public void CreateMenu(AdministrationEDSC.MenuDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Menu menu = new Menu();
            int linkID;
            CreateLink(dr, out linkID);

            menu.ParentMenuID = dr.ParentMenuID;
            menu.LinkID = linkID;
            menu.Sequence = GetMaxMenuSequence(menu.ID, menu.ParentMenuID) + 1;

            ent.AddToMenu(menu);
            ent.SaveChanges();

            dr.ID = menu.ID;
        }

        public void UpdateMenu(AdministrationEDSC.v_MenuDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.Menu
                        where m.ID == dr.ID
                        select m;

            Menu menu = query.FirstOrDefault();

            if (menu != null)
            {
                //ObjectHandler.CopyProperties(dr, menu);
                UpdateLink(dr, Convert.ToInt32(menu.LinkID));
                menu.ParentMenuID = dr.ParentMenuID;
                //menu.Sequence = dr.Sequence;
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.MenuDTDataTable RetrieveChildMenuItems(int parentID)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.Menu
                        where m.ParentMenuID == parentID
                        select m;

            IEnumerable<Menu> menuItems = query.AsEnumerable();
            if (menuItems == null)
                return null;
            else
            {
                AdministrationEDSC.MenuDTDataTable dt = new AdministrationEDSC.MenuDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(menuItems, dt, LoadOption.PreserveChanges);
                return dt;
            }

        }

        public void CreateMenu(AdministrationEDSC.v_MenuDTRow dr)
        {
            HCEntities ent = new HCEntities();
            Menu menu = new Menu();
            int linkID;
            CreateLink(dr, out linkID);

            menu.ParentMenuID = dr.ParentMenuID;
            menu.MenuType = dr.MenuType;
            menu.LinkID = linkID;
            menu.Sequence = GetMaxMenuSequence(menu.ID, menu.ParentMenuID) + 1;

            ent.AddToMenu(menu);
            ent.SaveChanges();

            dr.ID = menu.ID;
        }

        public AdministrationEDSC.MenuDTDataTable RetrieveChildMenus(int parentID)
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
                AdministrationEDSC.MenuDTDataTable dt = new AdministrationEDSC.MenuDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(Menus, dt, LoadOption.PreserveChanges);
                return dt;
            }

        }

        public AdministrationEDSC.MenuDTRow RetrieveMenu(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.Menu
                        where m.ID == SelectedMenuID
                        select m;

            Menu menu = query.FirstOrDefault();
            AdministrationEDSC.MenuDTRow dr = new AdministrationEDSC.MenuDTDataTable().NewMenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public AdministrationEDSC.v_MenuDTRow RetrieveMenuExplorer(int SelectedMenuID)
        {
            HCEntities ent = new HCEntities();
            var query = from m in ent.v_Menu
                        where m.ID == SelectedMenuID
                        select m;

            v_Menu menu = query.FirstOrDefault();
            AdministrationEDSC.v_MenuDTRow dr = new AdministrationEDSC.v_MenuDTDataTable().Newv_MenuDTRow();
            if (menu != null)
            {
                ObjectHandler.CopyPropertyValues(menu, dr);
                return dr;
            }
            else
                return null;
        }

        public AdministrationEDSC.MenuDTDataTable RetrieveMenus()
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.Menu
                        orderby v.Sequence
                        select v;


            AdministrationEDSC.MenuDTDataTable dt = new AdministrationEDSC.MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }

        public void UpdateMenu(AdministrationEDSC.MenuDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from m in ent.Menu
                        where m.ID == dr.ID
                        select m;

            Menu menu = query.FirstOrDefault();

            if (menu != null)
            {
                //ObjectHandler.CopyProperties(dr, menu);
                UpdateLink(RetrieveMenuExplorer(dr.ID), Convert.ToInt32(menu.LinkID));
                menu.ParentMenuID = dr.ParentMenuID;
                menu.Sequence = dr.Sequence;
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.v_MenuDTDataTable RetrieveMenuExplorers(int menuType)
        {
            HCEntities ent = new HCEntities();

            var query = from v in ent.v_Menu
                        where v.MenuType == menuType
                        orderby v.Sequence
                        select v;


            AdministrationEDSC.v_MenuDTDataTable dt = new AdministrationEDSC.v_MenuDTDataTable();
            var Menu = query.AsEnumerable();

            ObjectHandler.CopyEnumerableToDataTable(Menu, dt, LoadOption.PreserveChanges);
            return dt;
        }

        public void DeleteMenu(int menuID, out int linkID)
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.Menu
                        where p.ID == menuID
                        select p;

            Menu menu = query.FirstOrDefault();

            if (menu != null)
            {
                linkID = Convert.ToInt32(menu.LinkID);
                ent.DeleteObject(menu);
                ent.SaveChanges();
            }
            linkID = 0;
        }

        public void DeleteLink(int linkID)
        {
            HCEntities ent = new HCEntities();

            var query = from l in ent.Link
                        where l.ID == linkID
                        select l;

            Link obj = query.FirstOrDefault();

            if (obj != null)
            {
                ent.Link.DeleteObject(obj);
                ent.SaveChanges();
            }
        }

        private void UpdateLink(AdministrationEDSC.v_MenuDTRow dr, int LinkID)
        {
            HCEntities ent = new HCEntities();
            var query = from l in ent.Link
                        where l.ID == LinkID
                        select l;
            Link link = query.FirstOrDefault();

            link.LinkText = dr.LinkText;
            link.LinkType = dr.LinkType;
            link.LinkValue = dr.LinkValue;

            ent.SaveChanges();
        }

        public AdministrationEDSC.ProviderProfilesDTDataTable RetrieveAllproviders()
        {
            HCEntities ent = new HCEntities();

            var query = from p in ent.ProviderProfiles
                        select p;

            var provs = query.AsEnumerable();
            if (provs != null)
            {
                var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();
                ObjectHandler.CopyPropertyValues(provs, dt);
                return dt;
            }
            else return null;
        }

        private void CreateLink(AdministrationEDSC.MenuDTRow dr, out int linkID)
        {
            HCEntities ent = new HCEntities();
            Link link = new Link();

            ObjectHandler.CopyPropertyValues(dr, link);
            ent.AddToLink(link);
            ent.SaveChanges();
            linkID = link.ID;
        }

        private void CreateLink(AdministrationEDSC.v_MenuDTRow dr, out int linkID)
        {
            HCEntities ent = new HCEntities();

            Link link = new Link();

            link.LinkText = dr.LinkText;
            link.LinkType = dr.LinkType;
            link.LinkValue = dr.LinkValue;

            ent.AddToLink(link);
            ent.SaveChanges();
            linkID = link.ID;
        }


        #endregion

        #region Activity

        public AdministrationEDSC.v_ActivityExplorerDTRow RetrieveActivityExplorer(int ActivityID)
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
                AdministrationEDSC.v_ActivityExplorerDTRow dr = new AdministrationEDSC.v_ActivityExplorerDTDataTable().Newv_ActivityExplorerDTRow();
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

        public AdministrationEDSC.ActivityDTRow RetrieveActivity(int activityID)
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
                AdministrationEDSC.ActivityDTDataTable dt = new AdministrationEDSC.ActivityDTDataTable();
                AdministrationEDSC.ActivityDTRow dr = new AdministrationEDSC.ActivityDTDataTable().NewActivityDTRow();

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

        public AdministrationEDSC.ActivityGroupingDTRow RetrieveActivityGroup(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from g in ent.ActivityGrouping
                        where g.ActivityID == ActivityID
                        select g;

            if (query != null)
            {
                ActivityGrouping group = query.FirstOrDefault();
                var dr = new AdministrationEDSC.ActivityGroupingDTDataTable().NewActivityGroupingDTRow();
                ObjectHandler.CopyPropertyValues(group, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateActivity(AdministrationEDSC.ActivityDTRow drDetail)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == drDetail.ID
                        select a;

            Activity act = query.FirstOrDefault();
            if (act != null)
            {
                ObjectHandler.CopyPropertyValues(drDetail, act);
                ent.SaveChanges();
            }
        }

        public void UpdateActivityContactDetail(AdministrationEDSC.ActivityContactDetailDTRow contactDetails)
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

        public void UpdateActivityGrouping(AdministrationEDSC.ActivityGroupingDTRow drActGrouping)
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

        public void CreateActivitySchedule(AdministrationEDSC.ActivityScheduleDTRow ActScheduleDR)
        {
            HCEntities ent = new HCEntities();

            ActivitySchedule ActSched = new ActivitySchedule();
            ObjectHandler.CopyPropertyValues(ActScheduleDR, ActSched);

            ent.AddToActivitySchedule(ActSched);
            ent.SaveChanges();

        }

        public int RetrieveActivitiesInCategoryCount(int CategoryID)
        {
            HCEntities ent = new HCEntities();

            var query = from act in ent.Activity
                        where act.CategoryID == CategoryID || act.SecondaryCategoryID1 == CategoryID || act.SecondaryCategoryID2 == CategoryID ||
                        act.SecondaryCategoryID3 == CategoryID || act.SecondaryCategoryID4 == CategoryID
                        select act;
            return query.Count();
        }

        #region ActivitySchedule

        public AdministrationEDSC.ActivityScheduleDTDataTable RetrieveActivitySchedules(int ActivityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.ActivitySchedule
                        where a.ActivityID == ActivityID
                        select a;

            var actSched = query.AsEnumerable();

            if (actSched.Count() != 0)
            {
                var dt = new AdministrationEDSC.ActivityScheduleDTDataTable();
                actSched.CopyEnumerableToDataTable(dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public void UpdateActivitySchedule(AdministrationEDSC.ActivityScheduleDTRow drSched)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.ActivitySchedule
                        where a.ID == drSched.ID
                        select a;

            ActivitySchedule act = query.FirstOrDefault();
            if (act != null)
            {
                ObjectHandler.CopyPropertyValues(drSched, act);
                ent.SaveChanges();
            }
        }
        #endregion

        #endregion

        #region ActivityImage

        public void createActivityImageInformation(AdministrationEDSC.ActivityImageDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ActivityImage ii = new ActivityImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToActivityImage(ii);
            ent.SaveChanges();
        }

        public void createActivityImageInformation(AdministrationEDSC.ActivityImageDTRow dr, out int iiID)
        {
            HCEntities ent = new HCEntities();
            ActivityImage ii = new ActivityImage();
            ObjectHandler.CopyPropertyValues(dr, ii);

            ent.AddToActivityImage(ii);
            ent.SaveChanges();
            iiID = ii.ID;
        }

        public void UpdateImageInformation(int activityID, int iiID, AdministrationEDSC.ActivityImageDTRow dr)
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

        public void CreateActivityImage(AdministrationEDSC.ActivityImageDetailDTRow dr, out int imageID1)
        {
            HCEntities ent = new HCEntities();
            ActivityImageDetail ai = new ActivityImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToActivityImageDetail(ai);
            ent.SaveChanges();
            imageID1 = ai.ID;
        }

        public void CreateActivityImage(AdministrationEDSC.ActivityImageDetailDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ActivityImageDetail ai = new ActivityImageDetail();
            ObjectHandler.CopyPropertyValues(dr, ai);
            ent.AddToActivityImageDetail(ai);
            ent.SaveChanges();
        }

        public void UpdateActivityImage(AdministrationEDSC.ActivityImageDetailDTRow dr)
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

        public AdministrationEDSC.ActivityImageDTRow RetrieveActivityImageInformation(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImage
                        where i.ActivityID == activityID
                        select i;
            var ii = query.FirstOrDefault();
            if (ii != null)
            {
                AdministrationEDSC.ActivityImageDTRow dr = new AdministrationEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
                ObjectHandler.CopyPropertyValues(ii, dr);
                return dr;
            }
            else
                return null;
        }

        public AdministrationEDSC.ActivityImageDetailDTDataTable RetrieveActivityImages(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID
                        orderby i.ID
                        select i;

            AdministrationEDSC.ActivityImageDetailDTDataTable dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
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

        public AdministrationEDSC.ActivityImageDetailDTRow RetrievePrimaryProductImage(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.isPrimaryImage == true
                        select i;

            AdministrationEDSC.ActivityImageDetailDTRow dr = new AdministrationEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            if (query.FirstOrDefault() != null)
            {
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
            }
            return dr;
        }

        public AdministrationEDSC.ActivityImageDetailDTRow RetrieveProductMainImage(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.ActivityImageDetail
                        where i.ActivityID == activityID && i.isPrimaryImage == true
                        select i;

            AdministrationEDSC.ActivityImageDetailDTRow dr = new AdministrationEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
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

        public AdministrationEDSC.v_ActivityImageExplorerDTRow RetrieveActivityImage(int activityID, int imageID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.v_ActivityImageExplorer
                        where i.ActivityID == activityID && i.ImageID == imageID
                        select i;

            AdministrationEDSC.v_ActivityImageExplorerDTRow dr = new AdministrationEDSC.v_ActivityImageExplorerDTDataTable().Newv_ActivityImageExplorerDTRow();
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

        #region log

        public void createActivityLogGroup(AdministrationEDSC.ActivitiesLogGroupDTRow actLogGroup, out int ActivityLogGroupID)
        {
            HCEntities ent = new HCEntities();
            ActivitiesLogGroup actloggr = new ActivitiesLogGroup();
            ObjectHandler.CopyPropertyValues(actLogGroup, actloggr);
            ent.AddToActivitiesLogGroup(actloggr);
            ent.SaveChanges();
            ActivityLogGroupID = actloggr.ID;
        }

        public AdministrationEDSC.ActivitiesLogGroupDTRow RetrievePastActivityLogGroup(int activityID, int LastNotificationType, DateTime ExpiryDate)
        {
            HCEntities ent = new HCEntities();
            var query = from alg in ent.ActivitiesLogGroup
                        where alg.ActivityID == activityID && alg.ExpiryDate == ExpiryDate
                        && alg.LastNotificationType == LastNotificationType
                        select alg;

            var actlg = query.FirstOrDefault();
            if (actlg != null)
            {
                var dr = new AdministrationEDSC.ActivitiesLogGroupDTDataTable().NewActivitiesLogGroupDTRow();
                ObjectHandler.CopyPropertyValues(actlg, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateActivityLogGroup(int activityLogGroupID, AdministrationEDSC.ActivitiesLogGroupDTRow actLogGroup)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.ActivitiesLogGroup
                        where c.ID == activityLogGroupID
                        select c;

            ActivitiesLogGroup web = query.FirstOrDefault();

            if (web != null)
            {
                ObjectHandler.CopyPropertyValues(actLogGroup, web);
                ent.SaveChanges();
            }
        }

        public void UpdateActivityogGroup(AdministrationEDSC.ActivitiesLogGroupDTRow actLogGroup, int actLogGroupID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.ActivitiesLogGroup
                        where c.ID == actLogGroupID
                        select c;

            ActivitiesLogGroup web = query.FirstOrDefault();

            if (web != null)
            {
                ObjectHandler.CopyPropertyValues(actLogGroup, web);
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.ActivitiesLogGroupDTDataTable RetrieveActivityLogGroups()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.ActivitiesLogGroup
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.ActivitiesLogGroupDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);

                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.ActivitiesLogDTDataTable RetrieveActivitiesLogActions(int ActivitiesLogGroupID)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.ActivitiesLog
                        where q.ActivityLogGroupID == ActivitiesLogGroupID
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.ActivitiesLogDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);

                return dt;
            }
            else return null;
        }

        public void UpdateActivityLogNote(int activityLogID, string noteMessage)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.ActivitiesLog
                        where q.ID == activityLogID
                        select q;

            var act = query.FirstOrDefault();
            if (act != null)
            {
                act.Note = noteMessage;
                ent.SaveChanges();
            }
        }

        public string RetrieveActivityLogNote(int activityLogID)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.ActivitiesLog
                        where q.ID == activityLogID
                        select q;

            var act = query.FirstOrDefault();
            if (act != null)
            {
                return act.Note;
            }
            else return null;
        }

        public AdministrationEDSC.ActivitiesLogGroupDTRow RetrieveActivitiesLogGroup(int activityID, DateTime expiryDate)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.ActivitiesLogGroup
                        where q.ActivityID == activityID && q.ExpiryDate == expiryDate.Date
                        select q;

            var act = query.FirstOrDefault();
            if (act != null)
            {
                var dr = new AdministrationEDSC.ActivitiesLogGroupDTDataTable().NewActivitiesLogGroupDTRow();
                BCUtility.ObjectHandler.CopyPropertyValues(act, dr);
                return dr;
            }
            else return null;
        }

        public void SaveWebLogAction(AdministrationEDSC.WeblLogActionDTRow dr, out int LogActionID)
        {
            HCEntities ent = new HCEntities();
            WeblLogAction logAct = new WeblLogAction();

            ObjectHandler.CopyPropertyValues(dr, logAct);
            ent.AddToWeblLogAction(logAct);
            ent.SaveChanges();
            LogActionID = logAct.ID;
        }

        public void ChangeStatus(int actID, int activityStatus)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == actID
                        select a;
            Activity act = query.FirstOrDefault();

            if (act != null)
            {
                act.Status = activityStatus;
                ent.SaveChanges();
            }
        }

        public void SaveLog(AdministrationEDSC.WebLogDTRow drLog, out int WebLogID)
        {
            HCEntities ent = new HCEntities();
            WebLog log = new WebLog();

            ObjectHandler.CopyPropertyValues(drLog, log);
            ent.AddToWebLog(log);
            ent.SaveChanges();
            WebLogID = log.ID;
        }

        public AdministrationEDSC.WebLogDTDataTable RetrieveLogs()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.WebLog
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.WebLogDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);

                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.WeblLogActionDTDataTable RetrieveLogActions(int webLogID)
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.WeblLogAction
                        where q.WebLogID == webLogID
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.WeblLogActionDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);


                return dt;
            }
            else return null;
        }

        public void SaveActivityLog(AdministrationEDSC.ActivitiesLogDTRow dr)
        {
            HCEntities ent = new HCEntities();
            ActivitiesLog actLog = new ActivitiesLog();

            ObjectHandler.CopyPropertyValues(dr, actLog);
            ent.AddToActivitiesLog(actLog);
            ent.SaveChanges();
        }

        #endregion

        public AdministrationEDSC.WebConfigurationDTRow RetrieveWebImage()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.WebConfiguration
                        select q;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                var dr = new AdministrationEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;
        }

        public void UpdateWebConfigurationColor(AdministrationEDSC.WebConfigurationDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.WebConfiguration
                        select c;

            WebConfiguration web = query.FirstOrDefault();

            if (web != null)
            {
                ObjectHandler.CopyPropertyValues(dr, web);
                ent.SaveChanges();
            }
        }

        #region Emailer

        public AdministrationEDSC.WebConfigurationDTRow RetrieveEmailer()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                AdministrationEDSC.WebConfigurationDTRow dr = new AdministrationEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);
                if (dr.IsSMTPAccountNull())
                    return null;
                else return dr;
            }
            else return null;
        }

        public void EditEmailer(AdministrationEDSC.WebConfigurationDTRow dr)
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                web.SMTPAccount = dr.SMTPAccount;
                web.SMTPHost = dr.SMTPHost;
                web.SMTPUserName = dr.SMTPUserName;
                web.SMTPPassword = dr.SMTPPassword;
                web.SMTPPort = dr.SMTPPort;
                web.SMTPSSL = dr.SMTPSSL;
                web.SMTPIIS = dr.SMTPIIS;
                ent.SaveChanges();
            }
            else if (web == null && web.SMTPAccount == null)
            {
                CreateEmailer(dr);
            }

        }

        public void CreateEmailer(AdministrationEDSC.WebConfigurationDTRow dr)
        {
            HCEntities ent = new HCEntities();
            WebConfiguration web = new WebConfiguration();

            web.SMTPAccount = dr.SMTPAccount;
            web.SMTPHost = dr.SMTPHost;
            web.SMTPUserName = dr.SMTPUserName;
            web.SMTPPassword = dr.SMTPPassword;
            web.SMTPPort = dr.SMTPPort;
            web.SMTPSSL = dr.SMTPSSL;

            ent.AddToWebConfiguration(web);
            ent.SaveChanges();
        }

        public void SaveEmailSettings(AdministrationEDSC.EmailSettingDTDataTable dt)
        {
            ClearEmailSettings();
            HCEntities ent = new HCEntities();
            foreach (var dr in dt)
            {
                EmailSetting email = new EmailSetting();
                ObjectHandler.CopyPropertyValues(dr, email);
                ent.AddToEmailSetting(email);
            }

            ent.SaveChanges();

        }

        public void ClearEmailSettings()
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.EmailSetting
                        select e;

            var settings = query.AsEnumerable();
            foreach (EmailSetting setting in settings)
            {
                ent.DeleteObject(setting);
            }
            ent.SaveChanges();
        }

        public AdministrationEDSC.EmailSettingDTDataTable RetrieveEmailSettings()
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.EmailSetting
                        select e;

            var settings = query.AsEnumerable();
            if (settings != null)
            {
                var dt = new AdministrationEDSC.EmailSettingDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(settings, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else
                return null;

        }

        #endregion

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

        #region asset

        public void DeleteAsset(int assetID)
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebAssets
                        where w.ID == assetID
                        select w;

            WebAssets cat = query.FirstOrDefault();
            if (cat != null)
            {
                ent.DeleteObject(cat);
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.WebAssetsDTDataTable RetrieveWebAssets(int startIndex, int amount)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.WebAssets
                        select e;

            var assets = query.AsEnumerable();
            if (assets != null)
            {
                var dt = new AdministrationEDSC.WebAssetsDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(assets, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int RetrieveWebAssetsCount()
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.WebAssets
                        select e;

            return query.Count();
        }

        public void CreateAssetInformation(AdministrationEDSC.WebAssetsDTRow dr)
        {
            HCEntities ent = new HCEntities();
            WebAssets cat = new WebAssets();

            ObjectHandler.CopyPropertyValues(dr, cat);

            ent.AddToWebAssets(cat);
            ent.SaveChanges();
        }

        #endregion

        public AdministrationEDSC.WebConfigurationDTRow RetrieveWebConfiguration()
        {
            HCEntities ent = new HCEntities();

            var query = from w in ent.WebConfiguration
                        select w;

            WebConfiguration web = query.FirstOrDefault();
            if (web != null)
            {
                var dr = new AdministrationEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
                ObjectHandler.CopyPropertyValues(web, dr);
                return dr;
            }
            else return null;
        }

        public AdministrationEDSC.v_EmailExplorerDTRow RetrieveMailTemplate(int templateType)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.v_EmailExplorer
                        where e.EmailType == templateType
                        select e;

            if (query.FirstOrDefault() != null)
            {
                var dr = new AdministrationEDSC.v_EmailExplorerDTDataTable().Newv_EmailExplorerDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;
        }
        #region emailtemplate



        #endregion

        /*
        public AdministrationEDSC.ActivitiesLogDTDataTable RetrieveActivityLog(int ActivityID, int NotificationNumber, DateTime ExpiryDate)
        {
            HCEntities ent = new HCEntities();
            var expiry = ExpiryDate.ToShortDateString();

            var query = from al in ent.v_ActivitiesLogExplorer
                        where al.ActivityID == ActivityID && al.Value == expiry
                        select al;

            var actLog = query.AsEnumerable();
            if (actLog.Count() != 0)
            {
                var dt = new AdministrationEDSC.ActivitiesLogDTDataTable();
                BCUtility.ObjectHandler.CopyEnumerableToDataTable(actLog, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }
        */

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

        #region Activity

        public void ConfirmActivity(int activityID)
        {
            HCEntities ent = new HCEntities();

            var query = from a in ent.Activity
                        where a.ID == activityID
                        select a;

            Activity act = query.FirstOrDefault();
            if (act != null)
            {
                act.isApproved = true;
                act.ModifiedBy = "Admin";
                act.ModifiedDateTime = DateTime.Now;
                ent.SaveChanges();
            }
        }

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

        #endregion

        public AdministrationEDSC.ActivityDTDataTable RetrieveActivitiesbyIDs(List<int> selectedDT)
        {
            HCEntities ent = new HCEntities();

            HashSet<int> selectedActivityID = new HashSet<int>(selectedDT.Select(x => x));
            var query = ent.Activity.Where(x => selectedActivityID.Contains(x.ID));

            if (query.Count() != 0)
            {
                var dt = new AdministrationEDSC.ActivityDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }


        public string RetrieveActivityNamebyID(int actid)
        {
            HCEntities ent = new HCEntities();

            var query = from e in ent.Activity
                        where e.ID == actid
                        select e;
            var act = query.FirstOrDefault();
            if (act != null)
            {

                return act.Name;
            }
            else return null;
        }


        public AdministrationEDSC.ActivityScheduleDTDataTable RetrieveActivitySchedulesbyIDs(List<int> selectedDT)
        {
            HCEntities ent = new HCEntities();

            HashSet<int> selectedActivityID = new HashSet<int>(selectedDT.Select(x => x));
            var query = ent.ActivitySchedule.Where(x => selectedActivityID.Contains(x.ActivityID));

            if (query.Count() != 0)
            {
                var dt = new AdministrationEDSC.ActivityScheduleDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }



        public List<int> RetrieveExpiredActivityIDs()
        {
            HCEntities ent = new HCEntities();

            IQueryable<Activity> query = ent.Activity.Where(x => x.Status.Equals((int)SystemConstants.ActivityStatus.Expired));

            if (query.AsEnumerable() != null && query.Count() > 0)
            {
                return new List<int>(query.Select(x => x.ID));
            }
            else return null;
        }

        public AdministrationEDSC.ActivityDTDataTable RetrieveExpiredActivities()
        {
            HCEntities ent = new HCEntities();

            IQueryable<Activity> query = ent.Activity.Where(x => x.Status.Equals((int)SystemConstants.ActivityStatus.Expired));

            if (query.AsEnumerable() != null && query.Count() > 0)
            {
                AdministrationEDSC.ActivityDTDataTable dt = new AdministrationEDSC.ActivityDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        #region Rewards

        public string getSponsorName(Guid spnID)
        {
            HCEntities ent = new HCEntities();
            var query = from a in ent.Sponsor
                        where a.ID == spnID
                        select a;
            var act = query.FirstOrDefault();
            if (act != null)
            {
                return act.Name;
            }
            else return null;

        }

        public void DeleteSponsor(Guid sponsorid)
        {
            HCEntities ent = new HCEntities();

            var query2 = from a in ent.Sponsor
                         where a.ID == sponsorid
                         select a;

            Sponsor rwd2 = query2.FirstOrDefault();
            if (rwd2 != null)
            {
                ent.DeleteObject(rwd2);
                ent.SaveChanges();
            }

        }

        public AdministrationEDSC.SponsorDTRow RetrieveSponsorDetails(Guid SponsorID)
        {
            HCEntities ent = new HCEntities();
            var query = from a in ent.Sponsor
                        where a.ID == SponsorID
                        select a;
            if (query.Count() == 0)
            {
                return null;
            }
            var Sponsor = query.FirstOrDefault();
            if (Sponsor != null)
            {
                AdministrationEDSC.SponsorDTRow dr = new AdministrationEDSC.SponsorDTDataTable().NewSponsorDTRow();
                ObjectHandler.CopyPropertyValues(Sponsor, dr);
                return dr;
            }
            else

                return null;



        }

        public AdministrationEDSC.SponsorDTDataTable RetrieveSponsorsExplorer()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Sponsor
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.SponsorDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }



        public AdministrationEDSC.v_RewardExplorerDTDataTable RetrieveRewardsExplorer()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        select q;

            var act = query.AsEnumerable();
            if (act != null)
            {
                var dt = new AdministrationEDSC.v_RewardExplorerDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(act, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;
        }

        public int RetrieveRewardsExplorerCount()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        select q;

            return query.Count();


        }

        public void SaveReward(AdministrationEDSC.RewardDTRow drReward, out int RewardID)
        {
            HCEntities ent = new HCEntities();
            Reward rew = new Reward();
            ObjectHandler.CopyPropertyValues(drReward, rew);
            ent.AddToReward(rew);
            ent.SaveChanges();
            RewardID = rew.ID;
        }

        public void UpdateSponsor(AdministrationEDSC.SponsorDTRow sr)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.Sponsor
                        where ac.ID == sr.ID
                        select ac;

            Sponsor user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(sr, user);
                ent.SaveChanges();
            }

        }

        public void UpdateReward(AdministrationEDSC.RewardDTRow drReward)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.Reward
                        where ac.ID == drReward.ID
                        select ac;

            Reward user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(drReward, user);
                ent.SaveChanges();
            }
        }

        public int getDetailsID(int RewardID)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.RewardsDetails
                        where ac.RewardID == RewardID
                        select ac;
            var act = query.FirstOrDefault();
            if (act != null)
            {
                return act.ID;
            }
            else return 0;

        }

        public int getImageID(int RewardID)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.RewardImage
                        where ac.RewardID == RewardID
                        select ac;
            var act = query.FirstOrDefault();
            if (act != null)
            {
                return act.ID;
            }
            else return 0;

        }
        public void SaveSponsorDetail(AdministrationEDSC.SponsorDTRow drSpnDet)
        {
            HCEntities ent = new HCEntities();
            Sponsor det = new Sponsor();
            ObjectHandler.CopyPropertyValues(drSpnDet, det);
            ent.AddToSponsor(det);
            ent.SaveChanges();
        }


        public void SaveRewardImage(AdministrationEDSC.RewardImageDTRow drRwrdImage)
        {
            HCEntities ent = new HCEntities();
            RewardImage img = new RewardImage();
            ObjectHandler.CopyPropertyValues(drRwrdImage, img);
            ent.AddToRewardImage(img);
            ent.SaveChanges();

        }

        public void UpdateRewardImage(AdministrationEDSC.RewardImageDTRow drReward)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.RewardImage
                        where ac.RewardID == drReward.RewardID
                        select ac;

            RewardImage user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(drReward, user);
                ent.SaveChanges();
            }
        }

        public void SaveRewardDetail(AdministrationEDSC.RewardsDetailsDTRow drRwrdDet)
        {
            HCEntities ent = new HCEntities();
            RewardsDetails det = new RewardsDetails();
            ObjectHandler.CopyPropertyValues(drRwrdDet, det);
            ent.AddToRewardsDetails(det);
            ent.SaveChanges();
        }

        public void UpdateRewardDetail(AdministrationEDSC.RewardsDetailsDTRow drReward)
        {
            HCEntities ent = new HCEntities();

            var query = from ac in ent.RewardsDetails
                        where ac.RewardID == drReward.RewardID
                        select ac;

            RewardsDetails user = query.FirstOrDefault();
            if (user != null)
            {
                ObjectHandler.CopyPropertyValues(drReward, user);
                ent.SaveChanges();
            }
        }

        public void DeleteReward(int RewardID)
        {
            HCEntities ent = new HCEntities();
            var query2 = from a in ent.RewardImage
                         where a.RewardID == RewardID
                         select a;

            RewardImage rwd2 = query2.FirstOrDefault();
            if (rwd2 != null)
            {
                ent.DeleteObject(rwd2);
                ent.SaveChanges();
            }

            var query1 = from a in ent.RewardsDetails
                         where a.RewardID == RewardID
                         select a;

            RewardsDetails rwd1 = query1.FirstOrDefault();
            if (rwd1 != null)
            {
                ent.DeleteObject(rwd1);
                ent.SaveChanges();
            }


            var query = from a in ent.Reward
                        where a.ID == RewardID
                        select a;

            Reward rwd = query.FirstOrDefault();
            if (rwd != null)
            {
                ent.DeleteObject(rwd);
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.v_RewardExplorerDTRow RetrieveRewardInfo(int RewardID)
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
                AdministrationEDSC.v_RewardExplorerDTRow dr = new AdministrationEDSC.v_RewardExplorerDTDataTable().Newv_RewardExplorerDTRow();
                ObjectHandler.CopyPropertyValues(Reward, dr);
                return dr;
            }
            else

                return null;


        }

        public AdministrationEDSC.RewardsTypeDTDataTable RetrieveRewardTypes()
        {
            HCEntities ent = new HCEntities();

            var query = from rt in ent.RewardsType
                        select rt;

            if (query.AsEnumerable() != null)
            {
                var dt = new AdministrationEDSC.RewardsTypeDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(query.AsEnumerable(), dt, LoadOption.PreserveChanges);
                return dt;
            }
            else return null;

        }

        public AdministrationEDSC.RewardsTypeDTRow RetrieveRewardType(int Rewardtype)
        {
            HCEntities ent = new HCEntities();

            var query = from rt in ent.RewardsType
                        where rt.Type == Rewardtype
                        select rt;

            if (query.FirstOrDefault() != null)
            {
                var dr = new AdministrationEDSC.RewardsTypeDTDataTable().NewRewardsTypeDTRow();
                ObjectHandler.CopyPropertyValues(query.FirstOrDefault(), dr);
                return dr;
            }
            else return null;

        }

        public AdministrationEDSC.RewardImageDTRow RetrieveRewardPrimaryImage(int RewardID)
        {
            HCEntities ent = new HCEntities();

            var query = from i in ent.RewardImage
                        where i.RewardID == RewardID
                        select i;

            AdministrationEDSC.RewardImageDTRow dr = new AdministrationEDSC.RewardImageDTDataTable().NewRewardImageDTRow();
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

        public int RetrieveActiveRewards()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.UsageTimes > q.NofTimeUsed
                        select q;

            return query.Count();


        }

        public int RetrieveInactiveRewards()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.UsageTimes <= q.NofTimeUsed
                        select q;

            return query.Count();


        }

        public int RetrieveExpiredRewards()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.RewardExpiryDate < DateTime.Now
                        select q;

            return query.Count();


        }

        public int RetrieveGifts()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.RewardType == (int)SystemConstants.RewardType.Gift
                        select q;

            return query.Count();


        }


        public int RetrieveOffers()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.RewardType == (int)SystemConstants.RewardType.Offer
                        select q;

            return query.Count();


        }


        public int RetrieveDiscounts()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.RewardType == (int)SystemConstants.RewardType.Discount
                        select q;

            return query.Count();


        }


        public int RetrieveOthers()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_RewardExplorer
                        where q.RewardType == (int)SystemConstants.RewardType.Other
                        select q;

            return query.Count();


        }

        public int Retrieveneverred()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.Reward
                        where q.NofTimeUsed == 0
                        select q;
            return query.Count();


        }

        public int RetrieveTotalRedempted()
        {
            HCEntities ent = new HCEntities();

            var query = from q in ent.v_VoucherExplorer
                        select q;
            return query.Count();


        }
        #endregion

        #region Council

        public void CreateCouncil(string userName, AdministrationEDSC.CouncilDTRow dr)
        {
            dr.CreatedBy = dr.ModifiedBy = userName;
            dr.CreatedDatetime = dr.ModifiedDatetime = DateTime.Now;

            HCEntities ent = new HCEntities();
            Council Council = new Council();
            ObjectHandler.CopyPropertyValues(dr, Council);
            ent.AddToCouncil(Council);
            ent.SaveChanges();
        }

        public void UpdateCouncil(string userName, AdministrationEDSC.CouncilDTRow dr)
        {
            dr.ModifiedDatetime = DateTime.Now;
            dr.ModifiedBy = userName;

            HCEntities ent = new HCEntities();

            var query = from c in ent.Council
                        where c.ID == dr.ID
                        select c;

            Council council = query.FirstOrDefault();

            if (council != null)
            {
                ObjectHandler.CopyPropertyValues(dr, council);
                ent.SaveChanges();
            }


        }

        public void DeleteCouncil(int CouncilID)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.Council
                        where s.ID == CouncilID
                        select s;

            Council council = query.FirstOrDefault();
            if (council != null)
            {
                ent.DeleteObject(council);
                ent.SaveChanges();
            }
        }

        public AdministrationEDSC.CouncilDTRow RetrieveCouncil(int councilID)
        {
            HCEntities ent = new HCEntities();

            var query = from c in ent.Council
                        where c.ID == councilID
                        select c;

            var subs = query.FirstOrDefault();
            if (subs != null)
            {
                var dr = new AdministrationEDSC.CouncilDTDataTable().NewCouncilDTRow();
                ObjectHandler.CopyPropertyValues(subs, dr);
                return dr;
            }
            else
                return null;
        }

        public AdministrationEDSC.CouncilDTDataTable RetrieveCouncils()
        {
            HCEntities ent = new HCEntities();

            var query = from sub in ent.Council
                        select sub;

            var subs = query.AsEnumerable();
            if (subs != null)
            {
                var dt = new AdministrationEDSC.CouncilDTDataTable();
                ObjectHandler.CopyEnumerableToDataTable(subs, dt, LoadOption.PreserveChanges);
                return dt;
            }
            else
                return null;


        }

        public AdministrationEDSC.CouncilDTDataTable RetrieveCouncils(int startIndex, int amount, string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.Council
                        select s;

            AdministrationEDSC.CouncilDTDataTable dt = new AdministrationEDSC.CouncilDTDataTable();

            if (query.AsEnumerable() == null)
                return null;
            else
            {
                ObjectHandler.CopyEnumerableToDataTable(query, dt, LoadOption.PreserveChanges);

                //ObjectHandler.CopyProperties(category, dr);
                return dt;
            }
        }

        public int RetrieveCouncilsCount(string sortExpression)
        {
            HCEntities ent = new HCEntities();

            var query = from s in ent.Council
                        select s;

            return query.Count();
        }

        public AdministrationEDSC.v_SuburbExplorerDTRow RetrieveSuburbCouncil(int suburbID)
        {
            HCEntities ent = new HCEntities();

            var query = from coun in ent.v_SuburbExplorer
                        where coun.ID == suburbID
                        select coun;

            var subs = query.FirstOrDefault();
            if (subs != null)
            {
                var dr = new AdministrationEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();
                ObjectHandler.CopyPropertyValues(subs, dr);
                return dr;
            }
            else
                return null;


        }

        public AdministrationEDSC.v_CouncilExplorerDTRow RetrieveCouncilState(int councilID)
        {
            HCEntities ent = new HCEntities();

            var query = from sub in ent.v_CouncilExplorer
                        where sub.ID == councilID
                        select sub;

            var subs = query.FirstOrDefault();
            if (subs != null)
            {
                var dt = new AdministrationEDSC.v_CouncilExplorerDTDataTable().Newv_CouncilExplorerDTRow();
                ObjectHandler.CopyPropertyValues(subs, dt);
                return dt;
            }
            else
                return null;


        }

        public int RetrieveCouncilSuburbsCount(int councilID)
        {
            HCEntities ent = new HCEntities();

            var query = from sub in ent.Suburb
                        where sub.CouncilID == councilID
                        select sub;
            return query.Count();
        }

        #endregion










    }

}
