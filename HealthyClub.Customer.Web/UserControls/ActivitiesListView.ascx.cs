using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using HealthyClub.Utility;
using HealthyClub.Customer.BF;
using HealthyClub.Customer.DA;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using WebMatrix.WebData;
using HealthyClub.Customer.EDS;

namespace HealthyClub.Web.UserControls
{
    public partial class ActivitiesListView : System.Web.UI.UserControl
    {
        public delegate void SectionHandler(int ID, int startRow, string type, string sortValue, int pageSize, string searchKey, DateTime dtFrom, DateTime dtTo, int ageFrom, int ageTo, string SuburbID, bool monFilter, bool tueFilter, bool wedFilter, bool thuFilter, bool friFilter, bool satFilter, bool sunFilter, bool filtered);
        public event SectionHandler RefreshActivitiesSection;

        public int actNo = 0;

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

        public bool MonFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnMonFiltered.Value))
                    return Convert.ToBoolean(hdnMonFiltered.Value);
                else return true;
            }
            set
            {
                hdnMonFiltered.Value = value.ToString();
            }
        }

        public bool TueFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTueFiltered.Value))
                    return Convert.ToBoolean(hdnTueFiltered.Value);
                else return true;
            }
            set
            {
                hdnTueFiltered.Value = value.ToString();
            }
        }

        public bool WedFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnWedFiltered.Value))
                    return Convert.ToBoolean(hdnWedFiltered.Value);
                else return true;
            }
            set
            {
                hdnWedFiltered.Value = value.ToString();
            }
        }

        public bool ThursFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnThuFiltered.Value))
                    return Convert.ToBoolean(hdnThuFiltered.Value);
                else return true;
            }
            set
            {
                hdnThuFiltered.Value = value.ToString();
            }
        }

        public bool FriFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFriFiltered.Value))
                    return Convert.ToBoolean(hdnFriFiltered.Value);
                else return true;
            }
            set
            {
                hdnFriFiltered.Value = value.ToString();
            }
        }

        public bool SatFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSatFiltered.Value))
                    return Convert.ToBoolean(hdnSatFiltered.Value);
                else return true;
            }
            set
            {
                hdnSatFiltered.Value = value.ToString();
            }
        }

        public bool SunFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSunFiltered.Value))
                    return Convert.ToBoolean(hdnSunFiltered.Value);
                else return true;
            }
            set
            {
                hdnSunFiltered.Value = value.ToString();
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
                actNo = StartRow;
            }
        }

        public int PageSize
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageSize.Value))
                    return Convert.ToInt32(hdnPageSize.Value);
                else return 2;
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
                else return "6";
            }
            set
            {
                hdnSortValue.Value = value;

            }
        }

        public string SavedList
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSavedList.Value))
                    return hdnSavedList.Value;
                else return "";
            }
            set
            {
                hdnSavedList.Value = value;

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

        public TimeSpan queryTime
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTimespan.Value))
                    return TimeSpan.Parse(hdnTimespan.Value);
                else return TimeSpan.Zero;
            }
            set
            {
                hdnTimespan.Value = value.ToString();

            }
        }

        public DateTime dtFrom
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

        public DateTime dtTo
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void RetrieveSavedList()
        {
            if (Session[SystemConstants.ses_UserID] != null)
            {
                var dt = new CustomerDAC().retrieveUserActivityList(new Guid(Session[SystemConstants.ses_UserID].ToString()));
                if (dt != null)
                {
                    List<int> savedList = new List<int>(dt.Select(x => x.ListValue));
                    SavedList = string.Join("|", savedList);
                }
            }
        }

        public void Refresh()
        {
            

            if (WebSecurity.IsAuthenticated)
            {
                RetrieveSavedList();
            }
            ddSort.SelectedValue = SortValue;
            lblKeyword.Visible = false;
            String query = "";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (!string.IsNullOrEmpty(SearchKey))
            {
                List<String> parameters = new CustomerBFC().RefineSearchKey(SearchKey);

                foreach (var parameter in parameters)
                {
                    if (!string.IsNullOrEmpty(parameter))
                    {
                        if (parameter.StartsWith(SystemConstants.Query))
                        {
                            query = parameter.Replace(SystemConstants.Query, string.Empty);
                        }
                        else if (parameter.StartsWith(SystemConstants.Location))
                        {
                            String[] locs = parameter.Replace(SystemConstants.Location, string.Empty).ToUpper().Split(';');
                            var subDT = new CustomerDAC().RetrieveSuburbs();

                            var suburbs = subDT.Where(x => locs.Contains(x.Name.ToUpper()));

                            foreach (var sub in suburbs)
                            {
                                if (String.IsNullOrEmpty(SuburbID))
                                    SuburbID = sub.ID.ToString();
                                else
                                {
                                    SuburbID += "|" + sub.ID.ToString();
                                }
                            }
                        }
                        else if (parameter.StartsWith(SystemConstants.Day))
                        {
                            MonFilter = TueFilter = WedFilter = ThursFilter = FriFilter = SatFilter = SunFilter = false;
                            string[] days = parameter.Replace(SystemConstants.Day, string.Empty).Split(';');
                            foreach (var day in days)
                            {
                                if (day.ToUpper().Equals(DayOfWeek.Monday.ToString().ToUpper()))
                                    MonFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Tuesday.ToString().ToUpper()))
                                    TueFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Wednesday.ToString().ToUpper()))
                                    WedFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Thursday.ToString().ToUpper()))
                                    ThursFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Friday.ToString().ToUpper()))
                                    FriFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Saturday.ToString().ToUpper()))
                                    SatFilter = true;
                                if (day.ToUpper().Equals(DayOfWeek.Sunday.ToString().ToUpper()))
                                    SunFilter = true;
                            }
                        }
                        else if (parameter.StartsWith(SystemConstants.Time))
                        {
                            string[] times = parameter.Replace(SystemConstants.Time, string.Empty).Split('-');
                            if (times.Length == 2)
                            {
                                dtFrom = Convert.ToDateTime(SystemConstants.nodate.ToShortDateString() + " " + Convert.ToDateTime(times[0]).ToShortTimeString());
                                dtTo = Convert.ToDateTime(SystemConstants.nodate.ToShortDateString() + " " + Convert.ToDateTime(times[1]).ToShortTimeString());
                            }
                            else if (times.Length == 1)
                            {
                                dtFrom = Convert.ToDateTime(SystemConstants.nodate.ToShortDateString() + " " + Convert.ToDateTime(times[0]).ToShortTimeString());
                            }
                        }
                    }
                }
            }
            TimeSpan time = sw.Elapsed;
            sw.Restart();
            lbltimerefine.Text = time.ToString();

            if (!string.IsNullOrEmpty(query))
            {

                SetDataSourcebySearchKey(query);
                int amount = new CustomerDAC().RetrieveProviderActivitiesbySearchPhraseCount(ProviderID, dtFrom.ToString(), dtTo.ToString(),tmFrom.ToString(),tmTo.ToString(), AgeFrom, AgeTo, SuburbID, CategoryID, query, MonFilter.ToString(), TueFilter.ToString(), WedFilter.ToString(), ThursFilter.ToString(), FriFilter.ToString(), SatFilter.ToString(), SunFilter.ToString());
                lblAmount.Text = amount.ToString();
                if (Convert.ToInt32(lblAmount.Text) <= Convert.ToInt32(PageSize + StartRow))
                {
                    lblEndIndex.Text = lblAmount.Text;
                }
                else
                {
                    lblEndIndex.Text = (StartRow + PageSize).ToString();
                }

                lblStartIndex.Text = (StartRow + 1).ToString();

                if (Convert.ToInt32(lblStartIndex.Text) >= Convert.ToInt32(lblEndIndex.Text))
                {
                    lblStartIndex.Text = lblEndIndex.Text;
                }

                lblEndIndex1.Text = lblEndIndex.Text;
                lblStartIndex1.Text = lblStartIndex.Text;
                lblAmount1.Text = lblAmount.Text;

                lblKeyword.Visible = true;
                if (amount == 0)
                {
                    ItemCountBottom.Visible = false;
                }
                else
                {
                    if (amount <= PageSize)
                    {
                        divPager.Visible = false;
                        ItemCountBottom.Visible = true;
                    }
                    else
                        divPager.Visible = ItemCountBottom.Visible = true;
                }
                TimeSpan time1 = sw.Elapsed;
                lblshowres.Text = hdnTimespan.Value = "Set list by searchprovcat: " + time1.ToString();
            }
            else
            {
                SetDataSourcebyProviderCategory();
                int amount = new CustomerDAC().RetrieveProviderActivitiesCount(ProviderID, dtFrom.ToString(), dtTo.ToString(),tmFrom.ToString(),tmTo.ToString(), AgeFrom, AgeTo, SuburbID, CategoryID, MonFilter.ToString(), TueFilter.ToString(), WedFilter.ToString(), ThursFilter.ToString(), FriFilter.ToString()
                    , SatFilter.ToString(), SunFilter.ToString());
                lblAmount.Text = amount.ToString();

                if (Convert.ToInt32(lblAmount.Text) <= Convert.ToInt32(PageSize + StartRow))
                {
                    lblEndIndex.Text = lblAmount.Text;
                }
                else
                {
                    lblEndIndex.Text = (StartRow + PageSize).ToString();
                }

                lblStartIndex.Text = (StartRow + 1).ToString();

                if (Convert.ToInt32(lblStartIndex.Text) >= Convert.ToInt32(lblEndIndex.Text))
                {
                    lblStartIndex.Text = lblEndIndex.Text;
                }

                lblEndIndex1.Text = lblEndIndex.Text;
                lblStartIndex1.Text = lblStartIndex.Text;
                lblAmount1.Text = lblAmount.Text;


                if (amount == 0)
                {
                    ItemCountBottom.Visible = false;
                }
                else
                {
                    if (amount <= PageSize)
                    {
                        divPager.Visible = false;
                        ItemCountBottom.Visible = true;
                    }
                    else
                        divPager.Visible = ItemCountBottom.Visible = true;
                }
                TimeSpan time1 = sw.Elapsed;
                lblshowres.Text = hdnTimespan.Value ="Set list byprovcat: "+ time1.ToString();
            }
        }

        private void SetDataSourcebySearchKey(String SearchPhrase)
        {
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("searchKey", SearchPhrase);
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
            ods.SelectParameters.Add("ageFrom", AgeFrom.ToString());
            ods.SelectParameters.Add("ageTo", AgeTo.ToString());
            ods.SelectParameters.Add("stFrom", dtFrom.ToString());
            ods.SelectParameters.Add("stTo", dtTo.ToString());
            ods.SelectParameters.Add("tmFrom", tmFrom.ToString());
            ods.SelectParameters.Add("tmTo", tmTo.ToString());
            ods.SelectParameters.Add("suburbID", SuburbID.ToString());
            ods.SelectParameters.Add("categoryID", CategoryID.ToString());

            ods.SelectParameters.Add("MonFilter", MonFilter.ToString());
            ods.SelectParameters.Add("TueFilter", TueFilter.ToString());
            ods.SelectParameters.Add("WedFilter", WedFilter.ToString());
            ods.SelectParameters.Add("ThursFilter", ThursFilter.ToString());
            ods.SelectParameters.Add("FriFilter", FriFilter.ToString());
            ods.SelectParameters.Add("SatFilter", SatFilter.ToString());
            ods.SelectParameters.Add("SunFilter", SunFilter.ToString());

            ods.SelectMethod = "RetrieveProviderActivitiesbySearchPhrase";
            ods.SelectCountMethod = "RetrieveProviderActivitiesbySearchPhraseCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";

            ListViewActivities.DataSourceID = "ods";

            SortProducts();
        }

        private void SetDataSourcebyProviderCategory()
        {
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("categoryID", CategoryID.ToString());
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
            ods.SelectParameters.Add("ageFrom", AgeFrom.ToString());
            ods.SelectParameters.Add("ageTo", AgeTo.ToString());
            ods.SelectParameters.Add("stFrom", dtFrom.ToString());
            ods.SelectParameters.Add("stTo", dtTo.ToString());
            ods.SelectParameters.Add("tmFrom", tmFrom.ToString());
            ods.SelectParameters.Add("tmTo", tmTo.ToString());
            ods.SelectParameters.Add("suburbID", SuburbID.ToString());

            ods.SelectParameters.Add("MonFilter", MonFilter.ToString());
            ods.SelectParameters.Add("TueFilter", TueFilter.ToString());
            ods.SelectParameters.Add("WedFilter", WedFilter.ToString());
            ods.SelectParameters.Add("ThursFilter", ThursFilter.ToString());
            ods.SelectParameters.Add("FriFilter", FriFilter.ToString());
            ods.SelectParameters.Add("SatFilter", SatFilter.ToString());
            ods.SelectParameters.Add("SunFilter", SunFilter.ToString());

            ods.SelectMethod = "RetrieveProviderActivities";
            ods.SelectCountMethod = "RetrieveProviderActivitiesCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";

            ListViewActivities.DataSourceID = "ods";
            SortProducts();
        }

        private void SortProducts()
        {
            if (SortValue == "1")
            {
                ListViewActivities.Sort(SystemConstants.sortLatest, SortDirection.Descending);
            }
            else if (SortValue == "2")
            {
                ListViewActivities.Sort(SystemConstants.sortExpiry, SortDirection.Ascending);
            }
            else if (SortValue == "3")
            {
                ListViewActivities.Sort(SystemConstants.sortExpiry, SortDirection.Descending);
            }
            else if (SortValue == "4")
            {
                ListViewActivities.Sort(SystemConstants.sortName, SortDirection.Ascending);
            }
            else if (SortValue == "5")
            {
                ListViewActivities.Sort(SystemConstants.sortName, SortDirection.Descending);
            }
            else if (SortValue == "6")
            {
                ListViewActivities.Sort(SystemConstants.sortPrice, SortDirection.Ascending);
            }
            else if (SortValue == "7")
            {
                ListViewActivities.Sort(SystemConstants.sortPrice, SortDirection.Descending);
            }
            DataPager1.SetPageProperties(StartRow, PageSize, false);
            SetPageSize();

        }

        private void SetPageSize()
        {
            if (PageSize == 5)
            {
                ddlPagingTop.SelectedValue = "5";
            }
            else if (PageSize == 10)
            {
                ddlPagingTop.SelectedValue = "10";
            }
            else if (PageSize == 20)
            {
                ddlPagingTop.SelectedValue = "20";
            }
            else if (PageSize == 50)
            {
                ddlPagingTop.SelectedValue = "50";

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortValue = ddSort.SelectedValue;
            Refresh();
        }

        protected void ddlPagingTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPagingTop.SelectedValue == "5")
            {
                PageSize = 5;
            }
            else if (ddlPagingTop.SelectedValue == "10")
            {
                PageSize = 10;
            }
            else if (ddlPagingTop.SelectedValue == "20")
            {
                PageSize = 20;
            }
            else if (ddlPagingTop.SelectedValue == "50")
            {
                PageSize = 50;
            }


            if (DataPager1.StartRowIndex == 0)
                DataPager1.Fields[0].Visible = false;

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
                    type = "Filtered";
                }
            }

            if (RefreshActivitiesSection != null)
            {
                RefreshActivitiesSection(ID, 0, type, ddSort.SelectedValue, PageSize, SearchKey, dtFrom, dtTo, AgeFrom, AgeTo, SuburbID, MonFilter, TueFilter, WedFilter, ThursFilter, FriFilter, SatFilter, SunFilter, Filtered);
            }
        }

        protected void ListViewActivities_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals(string.Empty))
            {
                if (e.CommandName == "ToggleSave")
                {
                    HiddenField hdnisSaved = e.Item.FindControl("hdnisSaved") as HiddenField;
                    HiddenField hdnActivityID = e.Item.FindControl("hdnActivityID") as HiddenField;
                    LinkButton lnkSaved = e.Item.FindControl("lnkSaved") as LinkButton;
                    Label lblSaved = lnkSaved.FindControl("lblSaved") as Label;
                    Guid userID = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);

                    if (Convert.ToBoolean(hdnisSaved.Value))
                    {
                        lnkSaved.Attributes.CssStyle.Clear();
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-star");
                        lnkSaved.Text = "Save Activity";
                        new CustomerDAC().RemoveFromSavedList(WebSecurity.CurrentUserName, userID, Convert.ToInt32(hdnActivityID.Value));


                    }
                    else
                    {
                        lnkSaved.Attributes.CssStyle.Clear();
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-starred");
                        lnkSaved.Text = "Saved";
                        var dr = new CustomerEDSC.UserSavedListDTDataTable().NewUserSavedListDTRow();
                        dr.ID = 0;
                        dr.ListType = (int)SystemConstants.SavedListType.Activity;
                        dr.ListValue = Convert.ToInt32(hdnActivityID.Value);
                        dr.OwnerGuid = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);
                        dr.CreatedBy = WebSecurity.CurrentUserName;
                        dr.CreatedDatetime = DateTime.Now;
                        new CustomerDAC().AddToSavedList(dr);
                    }
                }
            }

        }

        protected void ListViewActivities_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            if (DataPager1.StartRowIndex == 0)
                DataPager1.Fields[0].Visible = false;

            int ID;
            string type;
            int startRow = e.StartRowIndex;
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

            if (RefreshActivitiesSection != null)
            {
                RefreshActivitiesSection(ID, startRow, type, ddSort.SelectedValue, PageSize, SearchKey, dtFrom, dtTo, AgeFrom, AgeTo, SuburbID, MonFilter, TueFilter, WedFilter, ThursFilter, FriFilter, SatFilter, SunFilter, Filtered);
            }
        }

        protected void ListViewActivities_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            actNo++;
            Label lblNo = e.Item.FindControl("lblNo") as Label;
            Label lblPhone = e.Item.FindControl("lblPhone") as Label;
            Label lblSub = e.Item.FindControl("lblSub") as Label;
            Label lblAddress = e.Item.FindControl("lblAddress") as Label;
            Label lblState = e.Item.FindControl("lblState") as Label;
            Label lblPostCode = e.Item.FindControl("lblPostCode") as Label;
            Label lblShortDescription = e.Item.FindControl("lblShortDescription") as Label;
            Label lblSaved = e.Item.FindControl("lblSaved") as Label;

            HyperLink HlnkReadMore = e.Item.FindControl("HlnkReadMore") as HyperLink;
            HyperLink HlnkActivitiesName = e.Item.FindControl("HlnkActivitiesName") as HyperLink;

            HiddenField hdnProviderID = e.Item.FindControl("hdnProviderID") as HiddenField;
            HiddenField hdnActivityID = e.Item.FindControl("hdnActivityID") as HiddenField;
            HiddenField hdnisSaved = e.Item.FindControl("hdnisSaved") as HiddenField;

            LinkButton lnkSaved = e.Item.FindControl("lnkSaved") as LinkButton;

            System.Web.UI.WebControls.Image imgPreview = e.Item.FindControl("imgPreview") as System.Web.UI.WebControls.Image;

            string actName = HlnkActivitiesName.Text;
            actName = actName.Replace(" ", "-");
            actName = actName.Replace("/", "-or-");
            if (actName.EndsWith("."))
                actName.TrimEnd('.');
            HlnkActivitiesName.NavigateUrl = HlnkReadMore.NavigateUrl = "~/Activity/" + hdnActivityID.Value + "/" + actName;

            //lblPhone.Text = "Tel: " + lblPhone.Text;
            HtmlGenericControl divDescription = e.Item.FindControl("divDescription") as HtmlGenericControl;
            if (Regex.IsMatch(lblShortDescription.Text, @"([a-zA-Z]){20,}"))
                divDescription.Attributes.Add("class", "breaking");

            TimeViewer ScheduleViewerUC1 = e.Item.FindControl("ScheduleViewerUC") as TimeViewer;

            ScheduleViewerUC1.ActivityID = Convert.ToInt32(hdnActivityID.Value);
            ScheduleViewerUC1.timetableFormat = (int)SystemConstants.TimetableFormat.Seasonal;

            if (string.IsNullOrEmpty(lblSub.Text))
            {
                lblAddress.Visible = false;
                lblSub.Visible = false;
                lblState.Visible = false;
                lblPostCode.Visible = false;
            }
            lblSub.Text = lblSub.Text + ", ";
            //lblNo.Text = actNo.ToString() + ".";

            HiddenField hdnExpiryDate = e.Item.FindControl("hdnExpiryDate") as HiddenField;
            HiddenField hdnType = e.Item.FindControl("hdnType") as HiddenField;

            System.Web.UI.WebControls.Image imgStatus = e.Item.FindControl("imgStatus") as System.Web.UI.WebControls.Image;
            System.Web.UI.WebControls.Image imgCostIcon = e.Item.FindControl("imgCostIcon") as System.Web.UI.WebControls.Image;

            //Label lblType = e.Item.FindControl("lblType") as Label;
            //lblType.ForeColor = Color.Green;

            if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Free).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Free).ToString())
            {
                //lblType.Text = "Free Activity";
                //lblType.ForeColor = Color.Green; ;
                imgCostIcon.ImageUrl = "~/Content/StyleImages/free.png";
                imgCostIcon.ToolTip = "This activity is free";
            }
            else if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Paid).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Paid).ToString())
            {
                imgCostIcon.ToolTip = "This activity has a fee";
                imgCostIcon.ImageUrl = "~/Content/StyleImages/Paid.png";
            }
            var dr = new CustomerDAC().RetrieveActivityPrimaryImage(Convert.ToInt32(hdnActivityID.Value));
            if (dr != null && dr.ImageStream != null)
            {
                //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);                 Convert byte directly, while its easier, its not suppose to be
                imgPreview.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ThumbImageID + "=" + dr.ID;
            }
            else
            {

                if (new CustomerDAC().IsUserImageExist(new Guid(hdnProviderID.Value)))
                {
                    int ImageID = new CustomerBFC().getProviderPrimaryImage(new Guid(hdnProviderID.Value));
                    if (ImageID != 0)
                        imgPreview.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_UserImageID + "=" + ImageID;
                    else
                    {
                        imgPreview.Visible = false;
                    }
                }
            }
            if (WebSecurity.IsAuthenticated)
            {
                if (SavedList.Equals(string.Empty))
                {
                    hdnisSaved.Value = false.ToString();
                    lblSaved.Text = "Save Activity";
                    lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-star");
                }
                else
                {
                    List<string> SavedactList = SavedList.Split('|').ToList();

                    if (SavedactList.Contains(hdnActivityID.Value))
                    {
                        hdnisSaved.Value = true.ToString();
                        lblSaved.Text = "Saved";
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-starred");
                    }
                    else
                    {
                        hdnisSaved.Value = false.ToString();
                        lblSaved.Text = "Save Activity";
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-star btn-star");
                    }
                }
                lnkSaved.Attributes.Add("OnClick", "ToggleSave(" + hdnActivityID.Value + "," + lnkSaved.ClientID + "," + hdnisSaved.Value + ");");
                RetrieveSavedList();
            }

            else
            {
                lnkSaved.Visible = false;
            }
        }

    }
}