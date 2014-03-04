using HealthyClub.Customer.DA;
using HealthyClub.Customer.BF;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class RewardsView : System.Web.UI.UserControl
    {
        public delegate void SectionHandler(int ID, int startRow, string type, string sortValue, int pageSize, string searchKey, int ageFrom, int ageTo, string RewardType, bool filtered);
        public event SectionHandler RefreshRewardsSection;


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



        public string RewardType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnRewardType.Value))
                    return hdnRewardType.Value;
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
            getHyperLink();
            lblerror.Visible = false;

        }

        public void getHyperLink()
        {
            if (Request.QueryString[SystemConstants.SearchKey] != null)
            {
                hlnkHome.Visible = image1.Visible = hlnkRewardsShop.Visible = image2.Visible = lblsearch.Visible= true;
                lblsearch.Text = "Search '" + SearchKey + "'";
             
            }
            if (Request.QueryString[SystemConstants.RewType] != null)
            {
                hlnkHome.Visible = image1.Visible = hlnkRewardsShop.Visible = image2.Visible = lblsearch.Visible = true;
                if (AgeFrom != 0 && AgeTo != 99)
                {
                    lblsearch.Text = "Point Search From " + AgeFrom + " To" + AgeTo;
                
                }
                else if (RewardType == "0|1")
                {
                    lblsearch.Text = "Reward Type 'Internal'";

                }
                else if(RewardType == "0|2")
                {
                    lblsearch.Text = "Reward Type 'External'";
                }
            }
           
        }
        public void Refresh()
        {
            ddSort.SelectedValue = SortValue;
            lblKeyword.Visible = false;
            if (PageSize == 8 || PageSize == 16)
                PageSize = 10;
          /*  if (SearchKey != null)
            {
                String SearchPhrases = new CustomerBFC().RefineSearchKeyreward(SearchKey);
                string query = "";
                foreach (var phrase in SearchPhrases)
                {
                    if (query.StartsWith(SystemConstants.Query))
                    {
                        query = phrase;
                        query = query.Replace(SystemConstants.Query, string.Empty);
                    }
                }
                SetDataSourcebySearchKey(query);*/
            if (SearchKey != null)
            {
                String SearchPhrase = new CustomerBFC().RefineSearchKeyreward(SearchKey);
                SetDataSourcebySearchKey(SearchPhrase);


                int amount = new CustomerDAC().RetrieveAdminRewardsbySearchPhraseCount(ProviderID, AgeFrom, AgeTo, RewardType, CategoryID, SearchPhrase);
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
                if (lblAmount.Text == "0")
                {
                    //lblKeyword.Text = "Search Found " + lblAmount.Text + " Record  with keyword '" + SearchKey + "'";
                    //else
                    //{
                    divPager.Visible = false;
                    //lblKeyword.Text = "there are no records with keyword '" + SearchKey + "'";
                }
            }
            else
            {
                SetDataSourcebyProviderCategory();
                int amount = new CustomerDAC().RetrieveAdminRewardsCount(ProviderID, AgeFrom, AgeTo, RewardType, CategoryID);
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

        }
        private void SetDataSourcebySearchKey(String SearchPhrase)
        {
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            
            ods.SelectParameters.Add("providerID", ProviderID.ToString());
            ods.SelectParameters.Add("searchKey", SearchPhrase);
            ods.SelectParameters.Add("ageFrom", AgeFrom.ToString());
            ods.SelectParameters.Add("ageTo", AgeTo.ToString());

            ods.SelectParameters.Add("RewardType", RewardType.ToString());
            ods.SelectParameters.Add("categoryID", CategoryID.ToString());

            ods.SelectMethod = "RetrieveAdminRewardsbySearchPhrase";
            ods.SelectCountMethod = "RetrieveAdminRewardsbySearchPhraseCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";

            ListViewRewards.DataSourceID = "ods";

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

            ods.SelectParameters.Add("RewardType", RewardType.ToString());

            ods.SelectMethod = "RetrieveAdminRewards";
            ods.SelectCountMethod = "RetrieveAdminRewardsCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";

            ListViewRewards.DataSourceID = "ods";
            SortProducts();
        }


        private void SortProducts()
        {

            if (SortValue == "1")
            {
                ListViewRewards.Sort(SystemConstants.sortLatest, SortDirection.Descending);
            }
            else if (SortValue == "2")
            {
                ListViewRewards.Sort(SystemConstants.sortExpiry, SortDirection.Ascending);
            }
            else if (SortValue == "3")
            {
                ListViewRewards.Sort(SystemConstants.sortExpiry, SortDirection.Descending);
            }
            else if (SortValue == "4")
            {
                ListViewRewards.Sort(SystemConstants.sortName, SortDirection.Ascending);
            }
            else if (SortValue == "5")
            {
                ListViewRewards.Sort(SystemConstants.sortName, SortDirection.Descending);
            }
            else if (SortValue == "6")
            {
                ListViewRewards.Sort(SystemConstants.sortPoints, SortDirection.Ascending);
            }
            else if (SortValue == "7")
            {
                ListViewRewards.Sort(SystemConstants.sortPointsDesc, SortDirection.Descending);
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
            int startRow = DataPager1.StartRowIndex;
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

            if (RefreshRewardsSection != null)
            {
                RefreshRewardsSection(ID, 0, type, ddSort.SelectedValue, PageSize, SearchKey, AgeFrom, AgeTo, RewardType, Filtered);
            }
        }

        protected void ListViewRewards_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void ListViewRewards_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
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

            if (RefreshRewardsSection != null)
            {
                RefreshRewardsSection(ID, startRow, type, ddSort.SelectedValue, PageSize, SearchKey, AgeFrom, AgeTo, RewardType, Filtered);
            }
        }



        protected void ListViewRewards_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            actNo++;
            Label lblNo = e.Item.FindControl("lblNo") as Label;
            HyperLink HlnkReadMore = e.Item.FindControl("HlnkReadMore") as HyperLink;
            HyperLink HlnkRewardsName = e.Item.FindControl("HlnkRewardsName") as HyperLink;
            HyperLink HlnkClickHere = e.Item.FindControl("HlnkClickHere") as HyperLink;
            HiddenField hdnRewardsID = e.Item.FindControl("hdnRewardsID") as HiddenField;
            HiddenField hdnRewardImage = e.Item.FindControl("hdnRewardImage") as HiddenField;
            
            System.Web.UI.WebControls.Image imgPreview = e.Item.FindControl("imgPreview") as System.Web.UI.WebControls.Image;
            HlnkRewardsName.NavigateUrl = HlnkReadMore.NavigateUrl = "~/Rewards/Rewards.aspx?" + SystemConstants.qs_RewardsID + "=" + hdnRewardsID.Value;
            HiddenField hdnExpiryDate = e.Item.FindControl("hdnExpiryDate") as HiddenField;
            Button Addtocart = e.Item.FindControl("Addtocart") as Button;
            Button Checkout = e.Item.FindControl("AddtocartNcheckout") as Button;
            Label points = e.Item.FindControl("lblpoints") as Label;
            Addtocart.ID = hdnRewardsID.Value;
            Addtocart.CommandName = points.Text;
            Checkout.CommandName = hdnRewardsID.Value;
            Checkout.CommandArgument = points.Text;
            //Image imgStatus = e.Item.FindControl("imgStatus") as Image;


            if (!string.IsNullOrEmpty(hdnRewardImage.Value))
            {
                if (Convert.ToBoolean(hdnRewardImage.Value))
                {
                    var dr = new CustomerDAC().RetrieveRewardPrimaryImage(Convert.ToInt32(hdnRewardsID.Value));
                    if (dr != null || dr.ImageStream != null)
                        //Convert byte directly, while its easier, its not suppose to be 
                        //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);
                        imgPreview.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_RewardThumbImageID + "=" + dr.ID;
                }
                else
                    imgPreview.ImageUrl = "~/Images/gift.jpg";
            }
            else
                imgPreview.ImageUrl = "~/Images/gift.jpg";
        }

        public void AddToCart_Click(object sender, EventArgs e)
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Session != null)
                {

                    Button ClickedButton = (Button)sender;
                    int RewardPts = Convert.ToInt32(ClickedButton.CommandName);
                    bool check = checkRewards(RewardPts);
                    if (check == true)
                    {
                        int RewardID = Convert.ToInt32(ClickedButton.ID);
                        RewardCart.Instance.AddItem(RewardID);
                        Response.Redirect(Request.RawUrl);
                    }
                    else

                        lblerror.Visible = true;

                }

            }
            else
                Response.Redirect("Performance.aspx");
        }
        protected void Checkout_Click(object sender, EventArgs e)
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Session != null)
                {
                    Button ClickedButton = (Button)sender;
                    int RewardPts = Convert.ToInt32(ClickedButton.CommandArgument);
                    bool check = checkRewards(RewardPts);
                    if (check == true)
                    {
                        int RewardID = Convert.ToInt32(ClickedButton.CommandName);
                        RewardCart.Instance.AddItem(RewardID);
                        Response.Redirect("Redeem.aspx");
                    }
                    else
                        lblerror.Visible = true;
                }

            }
            else
                Response.Redirect("Performance.aspx");
        }


        protected bool checkRewards(int pts)
        {
            int currrwd = Convert.ToInt32(Session[SystemConstants.ses_Rwdpts]);
            int subtotal = RewardCart.Instance.GetSubTotal() + pts;
            if (currrwd >= subtotal)
                return true;
            return false;


        }

    }
}