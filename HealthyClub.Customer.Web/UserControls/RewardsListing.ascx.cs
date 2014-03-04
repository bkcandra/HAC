using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Customer.Web.UserControls;

namespace HealthyClub.Web.UserControls
{
    public partial class RewardsListing : System.Web.UI.UserControl
    {

        public int CategoryID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnCategoryID.Value))
                    return Convert.ToInt32(hdnCategoryID.Value);
                else return 0;
            }
            set
            {
                hdnCategoryID.Value = value.ToString();
            }
        }

        public bool AdvSearch
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAdvSearch.Value))
                    return Convert.ToBoolean(hdnAdvSearch.Value);
                else return true;
            }
            set
            {
                hdnAdvSearch.Value = value.ToString();
            }
        }

        public bool FirstStart
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFirstStart.Value))
                    return Convert.ToBoolean(hdnFirstStart.Value);
                else return false;
            }
            set
            {
                hdnFirstStart.Value = value.ToString();
            }
        }

        public int page
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPage.Value))
                    return Convert.ToInt32(hdnPage.Value);
                else return 1;
            }
            set
            {
                hdnPage.Value = value.ToString();
            }
        }

        public Guid ProviderID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnProviderID.Value))
                    return new Guid(hdnProviderID.Value);
                else return Guid.Empty;
            }
            set
            {
                hdnProviderID.Value = value.ToString();
            }
        }

        public int StartRow
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnStartRow.Value))
                    return Convert.ToInt32(hdnStartRow.Value);
                else return 0;
            }
            set
            {
                hdnStartRow.Value = value.ToString();
            }
        }

        public int ViewType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnViewType.Value))
                    return Convert.ToInt32(hdnViewType.Value);
                else return 2;
            }
            set
            {
                hdnViewType.Value = value.ToString();
            }
        }

        public int PageSize
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageSize.Value))
                    return Convert.ToInt32(hdnPageSize.Value);
                else return 8;
            }
            set
            {
                hdnPageSize.Value = value.ToString();
            }
        }

        public string SortValue
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSortValue.Value))
                    return hdnSortValue.Value;
                else return "1";
            }
            set
            {
                hdnSortValue.Value = value;

            }
        }

        public string SearchKey
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSearchKey.Value))
                    return hdnSearchKey.Value;
                else return null;
            }
            set
            {
                hdnSearchKey.Value = value;

            }
        }


        public string RewardType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnRewardType.Value))
                    return hdnRewardType.Value.ToString();
                else return "0";
            }
            set
            {
                hdnRewardType.Value = value.ToString();
            }
        }

        public int AgeFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAgeFrom.Value))
                    return Convert.ToInt32(hdnAgeFrom.Value);
                else return 0;
            }
            set
            {
                hdnAgeFrom.Value = value.ToString();
            }
        }

        public int AgeTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAgeTo.Value))
                    return Convert.ToInt32(hdnAgeTo.Value);
                else return 99;
            }
            set
            {
                hdnAgeTo.Value = value.ToString();
            }
        }
        public bool Filtered
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFiltered.Value))
                    return Convert.ToBoolean(hdnFiltered.Value);
                else return false;
            }
            set
            {
                hdnFiltered.Value = value.ToString();
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Request.QueryString[SystemConstants.StartRow] != null)
                StartRow = Convert.ToInt32(Request.QueryString[SystemConstants.StartRow]);
            if (Request.QueryString[SystemConstants.FirstStart] != null)
                FirstStart = Convert.ToBoolean(Request.QueryString[SystemConstants.FirstStart]);
            if (Request.QueryString[SystemConstants.Page] != null)
                page = Convert.ToInt32(Request.QueryString[SystemConstants.Page]);
            if (Request.QueryString[SystemConstants.ViewType] != null)
            {
                ViewType = Convert.ToInt32(Request.QueryString[SystemConstants.ViewType]);
                if (ViewType == (int)SystemConstants.RewardViewType.ListView)
                {
                    radDetailed.Checked = true;
                    radTable.Checked = false;
                }
                else
                {
                    radTable.Checked = true;
                    radDetailed.Checked = false;
                }
            }
            if (Request.QueryString[SystemConstants.RewType] != null)
                RewardType = Request.QueryString[SystemConstants.RewType];
            if (Request.QueryString[SystemConstants.CategoryID] != null)
                CategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
            if (Request.QueryString[SystemConstants.SortValue] != null)
                SortValue = Request.QueryString[SystemConstants.SortValue];
            if (Request.QueryString[SystemConstants.SearchKey] != null)
                SearchKey = Request.QueryString[SystemConstants.SearchKey];
            if (Request.QueryString[SystemConstants.Filtered] != null)
                Filtered = Convert.ToBoolean(Request.QueryString[SystemConstants.Filtered]);
            if (Request.QueryString[SystemConstants.PageSize] != null)
                PageSize = Convert.ToInt32(Request.QueryString[SystemConstants.PageSize]);
            if (Request.QueryString[SystemConstants.AgeFrom] != null)
                AgeFrom = Convert.ToInt32(Request.QueryString[SystemConstants.AgeFrom]);
            if (Request.QueryString[SystemConstants.AgeTo] != null)
                AgeTo = Convert.ToInt32(Request.QueryString[SystemConstants.AgeTo]);

            setRewardsView();
        }


        protected void radDetailed_CheckedChanged(object sender, EventArgs e)
        {
            ViewType = (int)SystemConstants.ActivityViewType.ListView;
            ChangeView();
        }

        protected void radTable_CheckedChanged(object sender, EventArgs e)
        {
            ViewType = (int)SystemConstants.ActivityViewType.TableView;
            ChangeView();

        }

        private void ChangeView()
        {
            int ID;
            string type;
            if (!Filtered)
                if (SearchKey != null)
                {
                    ID = 0;
                    type = "Search";
                }
                else
                {
                    ID = CategoryID;
                    type = "Category";
                }
            else
            {
                if (SearchKey != null)
                {
                    ID = 0;
                    type = "FilteredSearch";
                }
                else
                {
                    ID = CategoryID;
                    type = "FilteredCategory";
                }
            }
            if (type == "Category")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "Search")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "Filtered")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "FilteredSearch")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "FilteredCategory")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
        }


        public void setRewardsView()
        {

            divSearchViewContent.Controls.Clear();

            if (FirstStart == true)
            {
                
                
                FirstStart = false;
                var uc = this.LoadControl("~/UserControls/StartPage.ascx") as StartPage;
                if (divSearchViewContent != null)
                    divSearchViewContent.Controls.Add(uc);
               
            }
            else
            {
                if (ViewType == (int)SystemConstants.RewardViewType.TileView)
                {
                    radTable.Checked = true;
                    var uc = this.LoadControl("~/UserControls/RewardsViewinit.ascx") as RewardsViewinit;
                    uc.RefreshRewardsSection += new RewardsViewinit.SectionHandler(uc_RefreshRewardsSection);

                    uc.CategoryID = CategoryID;
                    uc.ProviderID = ProviderID;
                    uc.StartRow = StartRow;
                    uc.SortValue = SortValue;
                    uc.PageSize = PageSize;
                    uc.RewardType = RewardType;
                    uc.AgeFrom = AgeFrom;
                    uc.AgeTo = AgeTo;
                    uc.Filtered = Filtered;

                    if (SearchKey != null)
                        uc.SearchKey = SearchKey;
                    if (divSearchViewContent != null)
                        divSearchViewContent.Controls.Add(uc);
                    uc.Refresh();
                }
                else if (ViewType == (int)SystemConstants.RewardViewType.ListView)
                {
                    radDetailed.Checked = true;
                    var uc = this.LoadControl("~/UserControls/RewardsView.ascx") as RewardsView;
                    uc.RefreshRewardsSection += new RewardsView.SectionHandler(uc_RefreshRewardsSection);

                    uc.CategoryID = CategoryID;
                    uc.ProviderID = ProviderID;
                    uc.StartRow = StartRow;
                    uc.SortValue = SortValue;
                    uc.PageSize = PageSize;
                    uc.RewardType = RewardType;
                    uc.AgeFrom = AgeFrom;
                    uc.AgeTo = AgeTo;
                    uc.Filtered = Filtered;

                    if (SearchKey != null)
                        uc.SearchKey = SearchKey;
                    if (divSearchViewContent != null)
                        divSearchViewContent.Controls.Add(uc);
                    uc.Refresh();
                
                
                }
            }
        }
        
     

        void uc_RefreshRewardsSection(int ID, int startRow, string type, string sortBy, int pageSize, string searchKey, int ageFrom, int ageTo, string RewardType, bool filtered)
        {
            if (type == "Category")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.Filtered + "=" + filtered + "&" + SystemConstants.PageSize + "=" + pageSize);
            else if (type == "Search")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.Filtered + "=" + filtered + "&" + SystemConstants.PageSize + "=" + pageSize);
            else if (type == "Filtered")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + pageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + filtered);
            else if (type == "FilteredSearch")
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + pageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + filtered);
        }

        protected void PushSidebarFilter(int categoryID, int ageFrom, int ageTo, string RewardType, bool filtered)
        {
            if (string.IsNullOrEmpty(SearchKey))
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.CategoryID + "=" + categoryID + "&" + SystemConstants.ViewType + "=" + 1 + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.AgeTo + "=" + ageTo + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + filtered);
            else
                Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + categoryID + "&" + SystemConstants.ViewType + "=" + 1 + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.AgeTo + "=" + ageTo + "&" + SystemConstants.RewType + "=" + RewardType + "&" + SystemConstants.Filtered + "=" + filtered);

        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {

            Response.Redirect("~/Rewards/Rewardshop.aspx?" + SystemConstants.SearchKey + "=" + txtSearch.Text + "&" + SystemConstants.ViewType + "=" + 1);
        }


    }

}
