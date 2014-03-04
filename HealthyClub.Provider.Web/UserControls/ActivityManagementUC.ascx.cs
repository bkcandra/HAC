using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System.Drawing;

namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ActivityManagementUC : System.Web.UI.UserControl
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
                else return 1;
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
                else return 10;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.StartRow] != null)
            {
                StartRow = Convert.ToInt32(Request.QueryString[SystemConstants.StartRow]);
            }

            if (Request.QueryString[SystemConstants.Page] != null)
            {
                page = Convert.ToInt32(Request.QueryString[SystemConstants.Page]);
            }

            if (Request.QueryString[SystemConstants.ViewType] != null)
            {
                ViewType = Convert.ToInt32(Request.QueryString[SystemConstants.ViewType]);
                if (ViewType == (int)SystemConstants.ActivityViewType.ListView)
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

            if (Request.QueryString[SystemConstants.CategoryID] != null)
            {
                CategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
            }

            if (Request.QueryString[SystemConstants.SortValue] != null)
            {
                SortValue = Request.QueryString[SystemConstants.SortValue];
            }
            if (Request.QueryString[SystemConstants.SearchKey] != null)
            {
                SearchKey = Request.QueryString[SystemConstants.SearchKey];
            }

            if (Request.QueryString[SystemConstants.PageSize] != null)
            {
                PageSize = Convert.ToInt32(Request.QueryString[SystemConstants.PageSize]);
            }
            Refresh();
        }

        protected void radDetailed_CheckedChanged(object sender, EventArgs e)
        {

                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.ViewType + "=" + (int)SystemConstants.ActivityViewType.ListView);
        }

        protected void radTable_CheckedChanged(object sender, EventArgs e)
        {

            Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.ViewType + "=" + (int)SystemConstants.ActivityViewType.TableView);
        }

        public void Refresh()
        {
            SetActivitiesView();
        }

        private void SetActivitiesView()
        {
            // This code Set Category Navigation
            //BrandNavigationUC1.BrandID = BrandID;
            //CategoryNavigationUC1.CategoryID = CategoryID;

            if (ViewType == (int)SystemConstants.ActivityViewType.ListView)
            {

                var uc = this.LoadControl("~/UserControls/ActivitiesListview.ascx") as ActivitiesListview;
                uc.RefreshActivitiesSection += new ActivitiesListview.SectionHandler(uc_RefreshActivitiesSection);

                uc.CategoryID = CategoryID;
                uc.ProviderID = ProviderID;
                uc.StartRow = StartRow;
                uc.SortValue = SortValue;
                uc.PageSize = PageSize;

                if (SearchKey != null)
                    uc.SearchKey = SearchKey;
                if (divSearchViewContent != null)
                    divSearchViewContent.Controls.Add(uc);
                uc.Refresh();
            }
            else if (ViewType == (int)SystemConstants.ActivityViewType.TableView)
            {

                var uc = this.LoadControl("~/UserControls/ActivitiesTableView.ascx") as ActivitiesTableView;
                uc.CategoryID = CategoryID;
                uc.ProviderID = ProviderID;
                uc.StartRow = StartRow;
                uc.SortValue = SortValue;
                uc.PageSize = PageSize;
                uc.page = page;
                if (SearchKey != null)
                    uc.SearchKey = SearchKey;
                if (divSearchViewContent != null)
                    divSearchViewContent.Controls.Add(uc);
                uc.Refresh();
            }

            /*else if (showActivities == 3)
            {
                var uc = this.LoadControl("~/UserControls/ProductsListView.ascx") as ProductsListView;
                uc.RefreshProductSection += new ProductsListView.SectionHandler(uc_RefreshProductSection);
                uc.CategoryID = CategoryID;
                uc.BrandID = BrandID;
                uc.TagID = TagID;
                uc.StartRow = StartRow;
                uc.SortValue = SortValue;
                uc.PageSize = Pagesize;
                uc.PriceFilterID = PriceFilteringID;
                uc.ProductViewType = SystemConstants.ProductViewType.ProductView;
                uc.DataPagerType = SystemConstants.DataPager.ProductView;

                if (SearchKey != null)
                    uc.SearchKey = SearchKey;
                if (divContent != null)
                    divContent.Controls.Add(uc);
                uc.Refresh();
            }*/
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + txtSearch.Text + "&" + SystemConstants.ViewType + "=" + ViewType);
        }

        void uc_RefreshActivitiesSection(int ID, int startRow, string type, string sortBy, int PageSize, string Searchkey)
        {
            /*if (type == "Brand")
            {
                Response.Redirect("~/ProductPage/Default.aspx?" + SystemConstants.BrandID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + PageSize);
            }
            else if (type == "Category")
            {
                Response.Redirect("~/ProductPage/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + PageSize);
            }
            else if (type == "Tag")
            {
                Response.Redirect("~/ProductPage/Default.aspx?" + SystemConstants.TagID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + PageSize);
            }
            else if (type == "Search")
            {
                Response.Redirect("~/ProductPage/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize);
            }*/


            if (type == "Category")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + PageSize);
            }

            else if (type == "Search")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + PageSize);
            }
        }
    }
}