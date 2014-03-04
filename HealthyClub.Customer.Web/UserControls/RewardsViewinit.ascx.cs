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

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class RewardsViewinit : System.Web.UI.UserControl
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

        public void Refresh()
        {
            ddSort.SelectedValue = SortValue;
            
            lblKeyword.Visible = false;

            if (SearchKey != null)
            {
                String SearchPhrase = new CustomerBFC().RefineSearchKeyreward(SearchKey);
                SetDataSourcebySearchKey(SearchPhrase);

                

                lblAmount.Text = new CustomerDAC().RetrieveAdminRewardsbySearchPhraseCount(ProviderID, AgeFrom, AgeTo, RewardType, CategoryID, SearchPhrase).ToString();
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
            if (PageSize == 8)
            {
                ddlPagingTop.SelectedValue = "8";
            }
            else if (PageSize == 16)
            {
                ddlPagingTop.SelectedValue = "16";
            }
           
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortValue = ddSort.SelectedValue;
            Refresh();
        }

        protected void ddlPagingTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPagingTop.SelectedValue == "8")
            {
                PageSize = 8;
            }
            else if (ddlPagingTop.SelectedValue == "16")
            {
                PageSize = 16;
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
                RefreshRewardsSection(ID, startRow, type, ddSort.SelectedValue, PageSize, SearchKey, AgeFrom, AgeTo, RewardType, Filtered);
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

            HyperLink HlnkImg = e.Item.FindControl("HlnkImg") as HyperLink;
            HyperLink HlnkRewardsName = e.Item.FindControl("HlnkRewardsName") as HyperLink;
            HyperLink HlnkClickHere = e.Item.FindControl("HlnkClickHere") as HyperLink;
            HiddenField hdnRewardsID = e.Item.FindControl("hdnRewardsID") as HiddenField;
            HiddenField hdnRewardImage = e.Item.FindControl("hdnRewardImage") as HiddenField;

            System.Web.UI.WebControls.Image imgPreview = e.Item.FindControl("imgPreview") as System.Web.UI.WebControls.Image;

            HlnkRewardsName.NavigateUrl = HlnkImg.NavigateUrl = "~/Rewards/Rewards.aspx?" + SystemConstants.qs_RewardsID + "=" + hdnRewardsID.Value;



            HiddenField hdnExpiryDate = e.Item.FindControl("hdnExpiryDate") as HiddenField;


            //Image imgStatus = e.Item.FindControl("imgStatus") as Image;
            if (!string.IsNullOrEmpty(hdnRewardImage.Value))
            {
                if (Convert.ToBoolean(hdnRewardImage.Value))
                {
                    var dr = new CustomerDAC().RetrieveRewardPrimaryImage(Convert.ToInt32(hdnRewardsID.Value));
                    if (dr != null)
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


    }
}