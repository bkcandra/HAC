using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Provider.DA;
using System.Drawing;
using HealthyClub.Provider.BF;
using WebMatrix.WebData;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ActivitiesListview : System.Web.UI.UserControl
    {
        public delegate void SectionHandler(int ID, int startRow, string type, string sortValue, int pageSize, string searchKey);
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

        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        public void Refresh()
        {
            ddSort.SelectedValue = SortValue;
            lblKeyword.Visible = false;
            int amount = 0;
            if (SearchKey != null)
            {
                String SearchPhrase = new ProviderBFC().RefineSearchKey(SearchKey);
                SetDataSourcebySearchKey(SearchPhrase);
                amount = new ProviderDAC().RetrieveProviderActivitiesbySearchPhraseCount(ProviderID, SearchPhrase);
                lblAmount1.Text = lblAmount.Text = amount.ToString();

                if (Convert.ToInt32(lblAmount.Text) <= Convert.ToInt32(PageSize + StartRow))
                    lblEndIndex.Text = lblAmount.Text;
                else
                    lblEndIndex.Text = (StartRow + PageSize).ToString();

                lblStartIndex.Text = (StartRow + 1).ToString();

                if (Convert.ToInt32(lblStartIndex.Text) >= Convert.ToInt32(lblEndIndex.Text))

                    lblStartIndex.Text = lblEndIndex.Text;


                lblEndIndex1.Text = lblEndIndex.Text;
                lblStartIndex1.Text = lblStartIndex.Text;

                lblKeyword.Visible = true;
                if (lblAmount.Text != "0")

                    lblKeyword.Text = "Search Found " + lblAmount.Text + " Record  with keyword '" + SearchKey + "'";
                else
                    lblKeyword.Text = "there are no records with keyword '" + SearchKey + "'";
            }
            else
            {
                SetDataSourcebyProviderCategory();
                amount = new ProviderDAC().RetrieveProviderActivitiesbyCategoryIDCount(ProviderID, CategoryID);
                lblAmount1.Text = lblAmount.Text = amount.ToString();

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
            }
            if (amount == 0 || amount <= PageSize)
            {
                divPager.Visible = false;
                tabShowAct.Visible = false;
            }
            else
            {
                divPager.Visible = true;
                tabShowAct.Visible = true;
            }
        }

        private void SetDataSourcebySearchKey(String SearchPhrase)
        {
            ods.TypeName = typeof(ProviderDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("searchKey", SearchPhrase);
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
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
            ods.TypeName = typeof(ProviderDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("categoryID", CategoryID.ToString());
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
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
            // Refresh();
            Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.ViewType + "=" + (int)SystemConstants.ActivityViewType.ListView + "&" + SystemConstants.PageSize + "=" + PageSize);
        }

        protected void ListViewActivities_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LinkButton lnkDeleteAct = e.CommandSource as LinkButton;
            ListViewItem item = lnkDeleteAct.Parent as ListViewItem;

            if (e.CommandName == "DeleteAct")
            {
                string Username = WebSecurity.CurrentUserName;
                if (string.IsNullOrEmpty(Username))
                    Username = "ERR_GETUSR";
                HiddenField hdnActivityID = item.FindControl("hdnActivityID") as HiddenField;
                int actID = Convert.ToInt32(hdnActivityID.Value);
                if (actID != 0 || actID != -1)
                {
                    //new ProviderDAC().DeleteActivity(actID);
                    new ProviderDAC().ChangeStatus(actID, (int)SystemConstants.ActivityStatus.Deleting, Username);
                }
            }

            //SortValue = ddSort.SelectedValue;
            Refresh();
        }

        protected void ListViewActivities_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            if (DataPager1.StartRowIndex == 0)
                DataPager1.Fields[0].Visible = false;

            int ID;
            string type;
            int startRow = e.StartRowIndex;

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
            if (RefreshActivitiesSection != null)
            {
                RefreshActivitiesSection(ID, startRow, type, ddSort.SelectedValue, PageSize, SearchKey);
            }
            Refresh();

        }

        protected void ListViewActivities_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            actNo++;
            Label lblPhone = e.Item.FindControl("lblPhone") as Label;
            Label lblSub = e.Item.FindControl("lblSub") as Label;
            Label lblAddress = e.Item.FindControl("lblAddress") as Label;
            Label lblState = e.Item.FindControl("lblState") as Label;
            Label lblPostCode = e.Item.FindControl("lblPostCode") as Label;
            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            Label lblExpiryDate = e.Item.FindControl("lblExpiryDate") as Label;
            Label lblType = e.Item.FindControl("lblType") as Label;
            Label lblShortDescription = e.Item.FindControl("lblShortDescription") as Label;
            HtmlGenericControl divDescription = e.Item.FindControl("divDescription") as HtmlGenericControl;
            LinkButton lnkEditAct = e.Item.FindControl("lnkEditAct") as LinkButton;

            HyperLink HlnkReadMore = e.Item.FindControl("HlnkReadMore") as HyperLink;
            HyperLink HlnkActivitiesName = e.Item.FindControl("HlnkActivitiesName") as HyperLink;
            HiddenField hdnActivityID = e.Item.FindControl("hdnActivityID") as HiddenField;
            HiddenField hdnStatus = e.Item.FindControl("hdnStatus") as HiddenField;
            HiddenField hdnisApproved = e.Item.FindControl("hdnisApproved") as HiddenField;
            HiddenField hdnExpiryDate = e.Item.FindControl("hdnExpiryDate") as HiddenField;
            HiddenField hdnType = e.Item.FindControl("hdnType") as HiddenField;
            HiddenField hdnModified = e.Item.FindControl("hdnModified") as HiddenField;

            System.Web.UI.WebControls.Image imgStatus = e.Item.FindControl("imgStatus") as System.Web.UI.WebControls.Image;

            lnkEditAct.PostBackUrl = "~/Activity/EditActivity.aspx?" + SystemConstants.ActivityID + "=" + hdnActivityID.Value;

            string actName = HlnkActivitiesName.Text;
            actName = actName.Replace(" ", "-");
            HlnkActivitiesName.NavigateUrl = HlnkReadMore.NavigateUrl = "~/Activity/" + hdnActivityID.Value + "/" + actName;

            //lblPhone.Text = "Tel: " + lblPhone.Text;

            if (Regex.IsMatch(lblShortDescription.Text, @"([a-zA-Z]){20,}"))
                divDescription.Attributes.Add("class", "breaking");

            ScheduleViewerUC ScheduleViewerUC1 = e.Item.FindControl("ScheduleViewerUC") as ScheduleViewerUC;

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

            lblType.ForeColor = Color.Green;
            imgStatus.ToolTip = "This activity is ";

            //Lets handle to icon first
            if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Deleting).ToString())
            {
                imgStatus.ImageUrl = SystemConstants.IconImageUrl + "grey.png";
                imgStatus.ToolTip = "This activity will be deleted";
            }
            else
            {
                if (Convert.ToBoolean(hdnisApproved.Value))
                {
                    imgStatus.ToolTip += "Approved ";

                    if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.NotActive).ToString())
                    {
                        imgStatus.ImageUrl = SystemConstants.IconImageUrl + "grey.png";
                        imgStatus.ToolTip += "";
                    }
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Active).ToString())
                    {
                        imgStatus.ImageUrl = SystemConstants.IconImageUrl + "green.png";
                        imgStatus.ToolTip += "";
                    }
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.WillExpire).ToString())
                    {
                        imgStatus.ImageUrl = SystemConstants.IconImageUrl + "amber.png";
                        imgStatus.ToolTip += " and expiring.";
                    }
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
                    {
                        imgStatus.ImageUrl = SystemConstants.IconImageUrl + "red.png";
                        imgStatus.ToolTip += " and expired.";
                    }

                }
                else
                {
                    imgStatus.ToolTip += "awaiting approval ";
                    imgStatus.ImageUrl = SystemConstants.IconImageUrl + "amber.png";
                    if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.NotActive).ToString())
                        imgStatus.ToolTip += "";
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Active).ToString())
                        imgStatus.ToolTip += "";
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.WillExpire).ToString())
                        imgStatus.ToolTip += " and expiring.";
                    else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
                    {
                        imgStatus.ImageUrl = SystemConstants.IconImageUrl + "red.png";
                        imgStatus.ToolTip += " and expired.";
                    }
                }
            }
            if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.NotActive).ToString())
            {
                lblStatus.Text = "INACTIVE";
                lblStatus.ForeColor = Color.Gray;
                lblExpiryDate.Text = "Activity inactive";

            }
            else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Active).ToString())
            {
                lblStatus.Text = "ACTIVE";
                lblStatus.ForeColor = Color.Green;
                lblExpiryDate.Text = "Expires on:<br/>" + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
            }
            else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.WillExpire).ToString())
            {
                lblStatus.Text = "EXPIRES SOON";
                lblStatus.ForeColor = Color.OrangeRed;
                lblExpiryDate.Text = "Expires on:<br/>" + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
            }
            else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
            {
                lblStatus.Text = "EXPIRED";
                lblStatus.ForeColor = Color.Red;
                lblExpiryDate.Text = "Expired on:<br/>" + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
            }
            else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Deleting).ToString())
            {
                lblStatus.Text = "DELETING";
                lblStatus.ForeColor = Color.Gray;
                DateTime deleted = Convert.ToDateTime(hdnModified.Value);
                deleted = deleted.AddDays(3);
                lblExpiryDate.Text = "Deleted on:<br/>" + deleted.ToShortDateString();
            }



            if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Free).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Free).ToString())
            {
                lblType.Text = "FREE ACTIVITY";
                lblType.ForeColor = Color.Green;
            }
            else if (hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Private_Paid).ToString() || hdnType.Value == ((int)SystemConstants.ActivityFeeCategory.Public_Paid).ToString())
            {
                lblType.Text = "PAID ACTIVITY";
                lblType.ForeColor = Color.Blue;
            }
        }

        protected void ListViewActivities_ItemDeleted(object sender, ListViewDeletedEventArgs e)
        {

        }



        protected void ListViewActivities_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
    }
}