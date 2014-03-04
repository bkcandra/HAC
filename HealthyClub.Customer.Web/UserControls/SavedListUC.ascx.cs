using HealthyClub.Customer.BF;
using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using HealthyClub.Utility;
using HealthyClub.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class SavedListUC : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void RetrieveSavedList()
        {
            var dt = new CustomerDAC().retrieveUserActivityList(new Guid(Session[SystemConstants.ses_UserID].ToString()));
            if (dt != null)
            {
                List<int> savedList = new List<int>(dt.Select(x => x.ListValue));
                SavedList = string.Join("|", savedList);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        public void Refresh()
        {
            if (WebSecurity.IsAuthenticated)
            {
                RetrieveSavedList();
            }

            lblKeyword.Visible = false;
            int amount = 0;
            if (!SavedList.Equals(string.Empty))
            {
                string[] savedActsArr = SavedList.Split('|');

                HashSet<int> savedActs = new HashSet<int>(savedActsArr.Select(x => Convert.ToInt32(x)));
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = new CustomerDAC().RetrieveActivityExplorersbyIDs(savedActs, "");
                ListViewActivities.DataSource = dt;
                ListViewActivities.DataBind();
                SortProducts();

                amount = new CustomerDAC().RetrieveActivityExplorersbyIDsCount(savedActs);
                
            }
            else
            {
                CustomerEDSC.v_ActivityExplorerDTDataTable dt = null;
                ListViewActivities.DataSource = dt;
                ListViewActivities.DataBind();
            }

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
                RefreshActivitiesSection(ID, 0, type, "", PageSize, SearchKey, dtFrom, dtTo, AgeFrom, AgeTo, SuburbID, MonFilter, TueFilter, WedFilter, ThursFilter, FriFilter, SatFilter, SunFilter, Filtered);
            }
        }

        protected void ListViewActivities_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals(string.Empty))
            {
                if (e.CommandName == "ToggleSave")
                {
                    LinkButton source = e.CommandSource as LinkButton;
                    ListViewItem row = source.Parent.Parent as ListViewItem;
                    HiddenField hdnisSaved = row.FindControl("hdnisSaved") as HiddenField;
                    HiddenField hdnActivityID = row.FindControl("hdnActivityID") as HiddenField;
                    LinkButton lnkSaved = row.FindControl("lnkSaved") as LinkButton;

                    Guid userID = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);

                    if (Convert.ToBoolean(hdnisSaved.Value))
                    {
                        lnkSaved.Attributes.CssStyle.Clear();
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-star");
                        new CustomerDAC().RemoveFromSavedList(WebSecurity.CurrentUserName, userID, Convert.ToInt32(hdnActivityID.Value));
                    }
                    else
                    {
                        lnkSaved.Attributes.CssStyle.Clear();
                        lnkSaved.Attributes.Add("Class", "btn-icon btn-white btn-radius btn-starred");
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
            Refresh();

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
                RefreshActivitiesSection(ID, startRow, type, "", PageSize, SearchKey, dtFrom, dtTo, AgeFrom, AgeTo, SuburbID, MonFilter, TueFilter, WedFilter, ThursFilter, FriFilter, SatFilter, SunFilter, Filtered);
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

        protected void ListViewActivities_Sorting(object sender, ListViewSortEventArgs e)
        {

        }

    }
}