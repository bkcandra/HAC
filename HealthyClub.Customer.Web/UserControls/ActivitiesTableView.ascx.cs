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

namespace HealthyClub.Web.UserControls
{
    public partial class ActivitiesTableView : System.Web.UI.UserControl
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

        internal void Refresh()
        {
            ddSort.SelectedValue = SortValue;
            lblKeyword.Visible = false;
            String query = "";


            if (SearchKey != null)
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

            if (!string.IsNullOrEmpty(query))
            {
                Stopwatch sw = Stopwatch.StartNew();

                SetDataSourceFromSearchKey(query);


                lblAmount.Text = new CustomerDAC().RetrieveProviderActivitiesbySearchPhraseCount(ProviderID, dtFrom.ToString(), dtTo.ToString(),tmFrom.ToString(),tmTo.ToString(), AgeFrom, AgeTo, SuburbID, CategoryID, query, MonFilter.ToString(), TueFilter.ToString(), WedFilter.ToString(), ThursFilter.ToString(), FriFilter.ToString(), SatFilter.ToString(), SunFilter.ToString()).ToString();

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
                if (lblAmount.Text != "0")
                {
                    //  lblKeyword.Text = "Search Found " + lblAmount.Text + " Record  with keyword '" + SearchKey + "'";
                    // else
                    //{

                    //lblKeyword.Text = "there are no records with keyword '" + SearchKey + "'";
                }
                TimeSpan elapsed = sw.Elapsed;
                hdnTimespan.Value = elapsed.ToString();
            }
            else
            {
                SetDataSourceFromCategoryProvider();
                lblAmount.Text = new CustomerDAC().RetrieveProviderActivitiesCount(ProviderID, dtFrom.ToString(), dtTo.ToString(), tmFrom.ToString(), tmTo.ToString(), AgeFrom, AgeTo, SuburbID, CategoryID, MonFilter.ToString(), TueFilter.ToString(), WedFilter.ToString(), ThursFilter.ToString(), FriFilter.ToString(), SatFilter.ToString(), SunFilter.ToString()).ToString();

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

            }
            if (GridViewActivities.HeaderRow != null)
                GridViewActivities.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void SetDataSourceFromSearchKey(String SearchPhrase)
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
            GridViewActivities.PageSize = PageSize;
            GridViewActivities.DataSourceID = "ods";

            SortProducts();
        }

        private void SetDataSourceFromCategoryProvider()
        {
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("categoryID", CategoryID.ToString());
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
            ods.SelectParameters.Add("stFrom", dtFrom.ToString());
            ods.SelectParameters.Add("stTo", dtTo.ToString());
            ods.SelectParameters.Add("ageFrom", AgeFrom.ToString());
            ods.SelectParameters.Add("ageTo", AgeTo.ToString());
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
            GridViewActivities.PageSize = PageSize;
            GridViewActivities.DataSourceID = "ods";
            SortProducts();
            //DataPager1.SetPageProperties(StartRow, DataPager1.MaximumRows, false);
            //ListViewProducts.DataBind();              

        }

        private void SortProducts()
        {

            if (SortValue == "1")
            {
                GridViewActivities.Sort(SystemConstants.sortLatest, SortDirection.Descending);
            }
            else if (SortValue == "2")
            {
                GridViewActivities.Sort(SystemConstants.sortExpiry, SortDirection.Ascending);
            }
            else if (SortValue == "3")
            {
                GridViewActivities.Sort(SystemConstants.sortExpiry, SortDirection.Descending);
            }
            else if (SortValue == "4")
            {
                GridViewActivities.Sort(SystemConstants.sortName, SortDirection.Ascending);
            }
            else if (SortValue == "5")
            {
                GridViewActivities.Sort(SystemConstants.sortName, SortDirection.Descending);
            }
            else if (SortValue == "6")
            {
                GridViewActivities.Sort(SystemConstants.sortPrice, SortDirection.Ascending);
            }
            else if (SortValue == "7")
            {
                GridViewActivities.Sort(SystemConstants.sortPrice, SortDirection.Descending);
            }
            SetPageSize();

        }

        private void SetPageSize()
        {
            if (PageSize == 10)
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
            GridViewActivities.PageIndex = page - 1;
        }

        protected void ddlPagingTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPagingTop.SelectedValue == "10")
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
            page = 1;
            Refresh();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortValue = ddSort.SelectedValue;
            Refresh();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int ID;
            string type;
            int startRow = e.NewPageIndex * PageSize;
            int page = e.NewPageIndex;

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


            if (type == "Category")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.SortValue + "=" + ddSort.SelectedValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + (int)SystemConstants.ActivityViewType.TableView + "&" + SystemConstants.Page + "=" + (page + 1));
            }
            else if (type == "Search")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.PageSize + "=" + (int)SystemConstants.ActivityViewType.TableView + "&" + SystemConstants.Page + "=" + (page + 1));
            }
            else if (type == "Filtered")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.SuburbID + "=" + SuburbID + "&" + SystemConstants.Filtered + "=" + Filtered);
            }
            else if (type == "FilteredSearch")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.StartRow + "=" + StartRow + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.CategoryID + "=" + CategoryID + "&" + SystemConstants.AgeFrom + "=" + AgeFrom + "&" + SystemConstants.DateFrom + "=" + dtFrom + "&" + SystemConstants.DateTo + "=" + dtTo + "&" + SystemConstants.SuburbID + "=" + SuburbID + "&" + SystemConstants.Filtered + "=" + Filtered);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblNo = e.Row.FindControl("lblNo") as Label;
                //Label lblPhone = e.Row.FindControl("lblPhone") as Label;
                //Label lblSub = e.Row.FindControl("lblSub") as Label;
                //HyperLink HlnkReadMore = e.Row.FindControl("HlnkReadMore") as HyperLink;
                HyperLink HlnkActivitiesName = e.Row.FindControl("HlnkActivitiesName") as HyperLink;
                HyperLink HlnkOrgName = e.Row.FindControl("HlnkOrgName") as HyperLink;
                HiddenField hdnActivityID = e.Row.FindControl("hdnActivityID") as HiddenField;

                HlnkOrgName.NavigateUrl = HlnkActivitiesName.NavigateUrl = "~/Activity/Default.aspx?" + SystemConstants.qs_ActivitiesID + "=" + hdnActivityID.Value;

                //lblPhone.Text = "Tel: " + lblPhone.Text;
                //lblSub.Text = lblSub.Text + ", ";


                HiddenField hdnType = e.Row.FindControl("hdnType") as HiddenField;

                //Image imgStatus = e.Item.FindControl("imgStatus") as Image;
                Label lblType = e.Row.FindControl("lblType") as Label;
                lblType.ForeColor = Color.Green;

                if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Free).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Free).ToString())
                {
                    lblType.Text = "Free Activity";
                    lblType.ForeColor = Color.Green;
                }
                else if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Paid).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Paid).ToString())
                {
                    lblType.Text = "";
                    lblType.ForeColor = Color.Red;

                }
            }
        }
    }
}