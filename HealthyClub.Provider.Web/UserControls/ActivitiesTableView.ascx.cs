using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.DA;
using System.Drawing;
using HealthyClub.Utility;
using HealthyClub.Provider.BF;

namespace HealthyClub.Providers.Web.UserControls
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void Refresh()
        {
            ddSort.SelectedValue = SortValue;
            lblKeyword.Visible = false;

            if (SearchKey != null)
            {
                String SearchPhrase = new ProviderBFC().RefineSearchKey(SearchKey);
                SetDataSourceFromSearchKey(SearchPhrase);


                lblAmount.Text = new ProviderDAC().RetrieveProviderActivitiesbySearchPhraseCount(ProviderID, SearchPhrase).ToString();

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

                    lblKeyword.Text = "Search Found " + lblAmount.Text + " Record  with keyword '" + SearchKey + "'";
                else
                    lblKeyword.Text = "there are no records with keyword '" + SearchKey + "'";

            }
            else
            {
                SetDataSourceFromCategoryProvider();
                lblAmount.Text = new ProviderDAC().RetrieveProviderActivitiesbyCategoryIDCount(ProviderID, CategoryID).ToString();

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
        }

        private void SetDataSourceFromSearchKey(String SearchPhrase)
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
            GridViewActivities.PageSize = PageSize;
            GridViewActivities.DataSourceID = "ods";

            SortProducts();
        }

        private void SetDataSourceFromCategoryProvider()
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
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName == "ChangeStatus")
                {
                    ImageButton btnImgStatus = e.CommandSource as ImageButton;
                    GridViewRow row = btnImgStatus.Parent.Parent as GridViewRow;
                    radButtonAct.Checked = radButtonInact.Checked = false;
                    HiddenField hdnActivityID = row.FindControl("hdnActivityID") as HiddenField;

                    var dr = new ProviderDAC().RetrieveActivity(Convert.ToInt32(hdnActivityID.Value));
                    if (dr != null)
                    {
                        hdnCurrentActID.Value = hdnActivityID.Value;
                        if (dr.Status == (int)SystemConstants.ActivityStatus.Active)
                        {
                            radButtonAct.Checked = true;
                        }
                        else if (dr.Status == (int)SystemConstants.ActivityStatus.Expired)
                        {
                            radButtonInact.Checked = true;
                        }
                        else if (dr.Status == (int)SystemConstants.ActivityStatus.NotActive)
                        {
                            radButtonInact.Checked = true;
                        }
                        else if (dr.Status == (int)SystemConstants.ActivityStatus.WillExpire)
                        {
                            radButtonAct.Checked = true;
                        }
                        hdnStatus.Value = dr.Status.ToString();
                        ModalPopupExtender1.Show();
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopUp", "ShowPopUp()", true);
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            int ID;
            string type;
            int startRow = e.NewPageIndex * PageSize;
            int page = e.NewPageIndex;

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
            if (type == "Category")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + ID + "&" + SystemConstants.SortValue + "=" + ddSort.SelectedValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.ViewType + "=" + (int)SystemConstants.ActivityViewType.TableView + "&" + SystemConstants.Page + "=" + (page + 1));
            }

            else if (type == "Search")
            {
                Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.SearchKey + "=" + SearchKey + "&" + SystemConstants.SortValue + "=" + SortValue + "&" + SystemConstants.PageSize + "=" + PageSize + "&" + SystemConstants.PageSize + "=" + (int)SystemConstants.ActivityViewType.TableView + "&" + SystemConstants.Page + "=" + (page + 1));
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
                HiddenField hdnActivityID = e.Row.FindControl("hdnActivityID") as HiddenField;

                HlnkActivitiesName.NavigateUrl = "~/Activity/Default.aspx?" + SystemConstants.qs_ActivitiesID + "=" + hdnActivityID.Value;
                string actName = HlnkActivitiesName.Text;
                actName = actName.Replace(" ", "-");
                HlnkActivitiesName.NavigateUrl = "~/Activity/" + hdnActivityID.Value + "/" + actName;

                //lblPhone.Text = "Tel: " + lblPhone.Text;
                //lblSub.Text = lblSub.Text + ", ";

                HiddenField hdnStatus = e.Row.FindControl("hdnStatus") as HiddenField;
                HiddenField hdnExpiryDate = e.Row.FindControl("hdnExpiryDate") as HiddenField;
                HiddenField hdnType = e.Row.FindControl("hdnType") as HiddenField;

                //Image imgStatus = e.Item.FindControl("imgStatus") as Image;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label lblExpiryDate = e.Row.FindControl("lblExpiryDate") as Label;
                Label lblType = e.Row.FindControl("lblType") as Label;
                lblType.ForeColor = Color.Green;
                LinkButton lnkEditAct = e.Row.FindControl("lnkEditAct") as LinkButton;
                lnkEditAct.PostBackUrl = "~/Activity/EditActivity.aspx?" + SystemConstants.ActivityID + "=" + hdnActivityID.Value;


                if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.NotActive).ToString())
                {
                    lblStatus.Text = "INACTIVE";
                    lblStatus.ForeColor = Color.Green;
                    //imgStatus.ImageUrl = SystemConstants.IconImageUrl + SystemConstants.IconActivityHidden;
                    lblExpiryDate.Text = "";
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Active).ToString())
                {
                    lblStatus.Text = "ACTIVE";
                    lblStatus.ForeColor = Color.Gray;
                    //imgStatus.ImageUrl = SystemConstants.IconImageUrl + SystemConstants.IconActivityActive;
                    lblExpiryDate.Text = "Expires on: " + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.WillExpire).ToString())
                {
                    lblStatus.Text = "EXPIRES SOON";
                    lblStatus.ForeColor = Color.OrangeRed;
                    //imgStatus.ImageUrl = SystemConstants.IconImageUrl + SystemConstants.IconActivityWillExpire;
                    lblExpiryDate.Text = "Expires on: " + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
                {
                    lblStatus.Text = "EXPIRED";
                    lblStatus.ForeColor = Color.Red;
                    //imgStatus.ImageUrl = SystemConstants.IconImageUrl + SystemConstants.IconActivityExpired;
                    lblExpiryDate.Text = "Expired on: " + Convert.ToDateTime(hdnExpiryDate.Value).ToShortDateString();
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
        }

        protected void btnExecPopUp_Click(object sender, EventArgs e)
        {
            if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
            {
                lblAlert.Text = SystemConstants.ActivateExpiredActivity;
                ModalPopupExtender2.Show();
            }
            else
            {
                new ProviderDAC().ChangeStatus(Convert.ToInt32(hdnCurrentActID.Value), radButtonAct.Checked);
                Refresh();
            }
        }


    }
}