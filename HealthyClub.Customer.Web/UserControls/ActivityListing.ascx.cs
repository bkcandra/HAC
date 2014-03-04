using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;

namespace HealthyClub.Web.UserControls
{
    public partial class ActivityListing : System.Web.UI.UserControl
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

        public ListItemCollection filteredDays { get; set; }

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
                hdnSearchKey.Value = ActivityListingNavigation.SearchKey = value;
            }
        }

        public string SuburbID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSuburbID.Value))
                    return hdnSuburbID.Value;
                else return "0";
            }
            set
            {
                hdnSuburbID.Value = value.ToString();
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

        public DateTime DTFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnDateFrom.Value))
                    return Convert.ToDateTime(hdnDateFrom.Value);
                else return SystemConstants.nodate;
            }
            set
            {
                hdnDateFrom.Value = value.ToString();
            }
        }

        public DateTime DTTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnDateTo.Value))
                    return Convert.ToDateTime(hdnDateTo.Value);
                else return SystemConstants.nodate;
            }
            set
            {
                hdnDateTo.Value = value.ToString();
            }
        }

        public TimeSpan tmFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTmFrom.Value))
                    return Convert.ToDateTime(hdnTmFrom.Value).TimeOfDay;
                else return SystemConstants.nodate.TimeOfDay;
            }
            set
            {
                hdnTmFrom.Value = value.ToString();
            }
        }

        public TimeSpan tmTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTmTo.Value))
                    return Convert.ToDateTime(hdnTmTo.Value).TimeOfDay;
                else return SystemConstants.nodate.TimeOfDay;
            }
            set
            {
                hdnTmTo.Value = value.ToString();
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
                ActivityListingNavigation.Filtered = value;
                hdnFiltered.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[SystemConstants.StartRow] != null)
                    StartRow = Convert.ToInt32(Request.QueryString[SystemConstants.StartRow]);
                if (Request.QueryString[SystemConstants.FirstStart] != null)
                    FirstStart = Convert.ToBoolean(Request.QueryString[SystemConstants.FirstStart]);
                if (Request.QueryString[SystemConstants.Page] != null)
                    page = Convert.ToInt32(Request.QueryString[SystemConstants.Page]);
                if (Request.QueryString[SystemConstants.ViewType] != null)
                    ViewType = Convert.ToInt32(Request.QueryString[SystemConstants.ViewType]);
                if (Request.QueryString[SystemConstants.CategoryID] != null)
                    CategoryID = ActivityListingSidebar1.CategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
                if (Request.QueryString[SystemConstants.SortValue] != null)
                    SortValue = Request.QueryString[SystemConstants.SortValue];
                if (Request.QueryString[SystemConstants.SearchKey] != null)
                    SearchKey = Request.QueryString[SystemConstants.SearchKey];
                if (Request.QueryString[SystemConstants.Filtered] != null)
                    Filtered = ActivityListingSidebar1.Filtered = Convert.ToBoolean(Request.QueryString[SystemConstants.Filtered]);
                if (Request.QueryString[SystemConstants.PageSize] != null)
                    PageSize = Convert.ToInt32(Request.QueryString[SystemConstants.PageSize]);
                if (Request.QueryString[SystemConstants.DateFrom] != null)
                    DTFrom = ActivityListingSidebar1.dtFrom = Convert.ToDateTime(Request.QueryString[SystemConstants.DateFrom]);
                if (Request.QueryString[SystemConstants.DateTo] != null)
                    DTTo = ActivityListingSidebar1.dtTo = Convert.ToDateTime(Request.QueryString[SystemConstants.DateTo]);
                if (Request.QueryString[SystemConstants.DateFrom] != null)
                    tmFrom = ActivityListingSidebar1.tmFrom = Convert.ToDateTime(Request.QueryString[SystemConstants.TimeFrom]).TimeOfDay;
                if (Request.QueryString[SystemConstants.DateTo] != null)
                    tmTo = ActivityListingSidebar1.tmTo = Convert.ToDateTime(Request.QueryString[SystemConstants.TimeTo]).TimeOfDay;
                if (Request.QueryString[SystemConstants.AgeFrom] != null)
                    AgeFrom = ActivityListingSidebar1.ageFrom = Convert.ToInt32(Request.QueryString[SystemConstants.AgeFrom]);
                if (Request.QueryString[SystemConstants.AgeTo] != null)
                    AgeTo = ActivityListingSidebar1.ageTo = Convert.ToInt32(Request.QueryString[SystemConstants.AgeTo]);
                if (Request.QueryString[SystemConstants.SuburbID] != null)
                    SuburbID = ActivityListingSidebar1.SuburbID = Request.QueryString[SystemConstants.SuburbID];

                //Catch Day filter
                filteredDays = new ListItemCollection();
                if (Request.QueryString[SystemConstants.MondayisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.MondayisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Monday.ToString(), DayOfWeek.Monday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.TuesdayisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.TuesdayisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Tuesday.ToString(), DayOfWeek.Tuesday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.WedisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.WedisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Wednesday.ToString(), DayOfWeek.Wednesday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.ThurisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.ThurisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Thursday.ToString(), DayOfWeek.Thursday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.FriisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.FriisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Friday.ToString(), DayOfWeek.Friday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.SatisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.SatisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Saturday.ToString(), DayOfWeek.Saturday.ToString());
                        filteredDays.Add(day);
                    }
                }
                if (Request.QueryString[SystemConstants.SunisFiltered] != null)
                {
                    if (Convert.ToInt32(Request.QueryString[SystemConstants.SunisFiltered]) == 1)
                    {
                        ListItem day = new ListItem(DayOfWeek.Sunday.ToString(), DayOfWeek.Sunday.ToString());
                        filteredDays.Add(day);
                    }
                }
                ActivityListingSidebar1.filteredDays = filteredDays;
                SetActivitiesView();
            }
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
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "Search")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "Filtered")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.DateFrom + "=" + DTFrom + "&" + SystemConstants.DateTo + "=" + DTTo + "&" + SystemConstants.SuburbID + "=" + SuburbID + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "FilteredSearch")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.DateFrom + "=" + DTFrom + "&" + SystemConstants.DateTo + "=" + DTTo + "&" + SystemConstants.SuburbID + "=" + SuburbID + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
            else if (type == "FilteredCategory")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.DateFrom + "=" + DTFrom + "&" + SystemConstants.DateTo + "=" + DTTo + "&" + SystemConstants.SuburbID + "=" + SuburbID + "&" + SystemConstants.Filtered + "=" + Filtered + "&" + SystemConstants.ViewType + "=" + ViewType);
        }

        public void SetActivitiesView()
        {
            ActivitiesListView.CategoryID = CategoryID;
            ActivitiesListView.ProviderID = ProviderID;
            ActivitiesListView.StartRow = StartRow;
            ActivitiesListView.SortValue = SortValue;
            ActivitiesListView.PageSize = PageSize;
            ActivitiesListView.SuburbID = SuburbID;
            ActivitiesListView.AgeFrom = AgeFrom;
            ActivitiesListView.AgeTo = AgeTo;
            ActivitiesListView.dtFrom = DTFrom;
            ActivitiesListView.dtTo = DTTo;
            ActivitiesListView.tmFrom = tmFrom;
            ActivitiesListView.tmTo = tmTo;
            ActivitiesListView.Filtered = Filtered;

            if (filteredDays.Count != 0)
                ActivitiesListView.MonFilter = ActivitiesListView.TueFilter = ActivitiesListView.WedFilter = ActivitiesListView.ThursFilter = ActivitiesListView.FriFilter = ActivitiesListView.SatFilter = ActivitiesListView.SunFilter = false;
            foreach (ListItem day in filteredDays)
            {
                if (day.Text == DayOfWeek.Monday.ToString())
                    ActivitiesListView.MonFilter = true;
                else if (day.Text == DayOfWeek.Tuesday.ToString())
                    ActivitiesListView.TueFilter = true;
                else if (day.Text == DayOfWeek.Wednesday.ToString())
                    ActivitiesListView.WedFilter = true;
                else if (day.Text == DayOfWeek.Thursday.ToString())
                    ActivitiesListView.ThursFilter = true;
                else if (day.Text == DayOfWeek.Friday.ToString())
                    ActivitiesListView.FriFilter = true;
                else if (day.Text == DayOfWeek.Saturday.ToString())
                    ActivitiesListView.SatFilter = true;
                else if (day.Text == DayOfWeek.Sunday.ToString())
                    ActivitiesListView.SunFilter = true;
            }
            if (SearchKey != null)
                ActivitiesListView.SearchKey = SearchKey;
            ActivitiesListView.Refresh();


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + txtSearch.Text + "&" + SystemConstants.ViewType + "=" + ViewType);
        }



        protected void ApplySidebarFilter(int categoryID, int ageFrom, int ageTo, string suburbID, DateTime dtFrom, DateTime dtTo, TimeSpan tmFrom, TimeSpan tmTo, ListItemCollection filteredDays, bool filtered)
        {
            String daysFilter = "";
            foreach (ListItem day in filteredDays)
            {
                if (day.Text == DayOfWeek.Monday.ToString())
                    daysFilter += "&" + SystemConstants.MondayisFiltered + "=1";
                else if (day.Text == DayOfWeek.Tuesday.ToString())
                    daysFilter += "&" + SystemConstants.TuesdayisFiltered + "=1";
                else if (day.Text == DayOfWeek.Wednesday.ToString())
                    daysFilter += "&" + SystemConstants.WedisFiltered + "=1";
                else if (day.Text == DayOfWeek.Thursday.ToString())
                    daysFilter += "&" + SystemConstants.ThurisFiltered + "=1";
                else if (day.Text == DayOfWeek.Friday.ToString())
                    daysFilter += "&" + SystemConstants.FriisFiltered + "=1";
                else if (day.Text == DayOfWeek.Saturday.ToString())
                    daysFilter += "&" + SystemConstants.SatisFiltered + "=1";
                else if (day.Text == DayOfWeek.Sunday.ToString())
                    daysFilter += "&" + SystemConstants.SunisFiltered + "=1";
                else if (day.Text == SystemConstants.AnyisFiltered)
                    daysFilter += "&" + SystemConstants.AnyisFiltered + "=1";
            }
            //Reset The search key
            //Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + categoryID + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.AgeTo + "=" + ageTo + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.SuburbID + "=" + suburbID + daysFilter + "&" + SystemConstants.Filtered + "=" + filtered);



            if (string.IsNullOrEmpty(SearchKey))
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + categoryID + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.AgeTo + "=" + ageTo + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.TimeFrom + "=" + tmFrom + "&" + SystemConstants.TimeTo + "=" + tmTo + "&" + SystemConstants.SuburbID + "=" + suburbID + daysFilter + "&" + SystemConstants.Filtered + "=" + filtered);
            else
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + categoryID + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.AgeTo + "=" + ageTo + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.TimeFrom + "=" + tmFrom + "&" + SystemConstants.TimeTo + "=" + tmTo + "&" + SystemConstants.SuburbID + "=" + suburbID + daysFilter + "&" + SystemConstants.Filtered + "=" + filtered);

        }

        protected void CloseListingNav(int navType)
        {
            if (navType == (int)SystemConstants.ListingNavigationType.category)
            {
                Filtered = false;
                if (!string.IsNullOrEmpty(SearchKey))
                    Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey);
                else if (string.IsNullOrEmpty(SearchKey))
                {
                    Response.Redirect("~/Activities");
                }
            }
            else if (navType == (int)SystemConstants.ListingNavigationType.filter)
            {
                Filtered = false;
                if (!string.IsNullOrEmpty(SearchKey))
                    Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey);
                else if (string.IsNullOrEmpty(SearchKey))
                {
                    Response.Redirect("~/Activities");
                }
            }
            else if (navType == (int)SystemConstants.ListingNavigationType.search)
            {
                Response.Redirect("~/Activities");
            }
            SetActivitiesView();
        }

        protected void RefreshActivitiesSection(int ID, int startRow, string type, string sortBy, int pageSize, string searchKey, DateTime dtFrom, DateTime dtTo, int ageFrom, int ageTo, string suburbID, bool monFilter, bool tueFilter, bool wedFilter, bool thuFilter, bool friFilter, bool satFilter, bool sunFilter, bool filtered)
        {
            String daysFilter = "";
            
                if (monFilter)
                    daysFilter += "&" + SystemConstants.MondayisFiltered + "=1";
                else if (tueFilter)
                    daysFilter += "&" + SystemConstants.TuesdayisFiltered + "=1";
                else if (wedFilter)
                    daysFilter += "&" + SystemConstants.WedisFiltered + "=1";
                else if (thuFilter)
                    daysFilter += "&" + SystemConstants.ThurisFiltered + "=1";
                else if (friFilter)
                    daysFilter += "&" + SystemConstants.FriisFiltered + "=1";
                else if (satFilter)
                    daysFilter += "&" + SystemConstants.SatisFiltered + "=1";
                else if (sunFilter)
                    daysFilter += "&" + SystemConstants.SunisFiltered + "=1";
            

            if (type == "Category")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.Filtered + "=" + filtered + "&" + SystemConstants.PageSize + "=" + pageSize);
            else if (type == "Search")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + startRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.Filtered + "=" + filtered + "&" + SystemConstants.PageSize + "=" + pageSize);
            else if (type == "Filtered")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + pageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.SuburbID + "=" + suburbID + daysFilter + "&" + SystemConstants.Filtered + "=" + filtered);
            else if (type == "FilteredSearch")
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.ViewType + "=" + ViewType + "&" + SystemConstants.SortValue + "=" + sortBy + "&" + SystemConstants.PageSize + "=" + pageSize + "&" + SystemConstants.AgeFrom + "=" + ageFrom + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.SuburbID + "=" + suburbID + daysFilter + "&" + SystemConstants.Filtered + "=" + filtered);
        }
    }
}