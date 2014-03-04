using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityRegistrationCategory : System.Web.UI.UserControl
    {
        public Boolean EditMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnEdit.Value))
                    return Convert.ToBoolean(hdnEdit.Value);
                else return false;
            }
            set
            {
                hdnEdit.Value = value.ToString();
            }
        }

        public int ActivityID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnActivityID.Value))
                    return Convert.ToInt32(hdnActivityID.Value);
                else return 0;
            }
            set
            {
                hdnActivityID.Value = value.ToString();
            }
        }
        /*
        public int SelectedID1
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSelectedCat1.Value))
                    return Convert.ToInt32(hdnSelectedCat1.Value);
                else return 0;
            }
            set
            {
                hdnSelectedCat1.Value = value.ToString();
            }
        }

        public int SelectedID2
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSelectedCat2.Value))
                    return Convert.ToInt32(hdnSelectedCat2.Value);
                else return 0;
            }
            set
            {
                hdnSelectedCat2.Value = value.ToString();
            }
        }

        public int SelectedID5
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSelectedCat5.Value))
                    return Convert.ToInt32(hdnSelectedCat5.Value);
                else return 0;
            }
            set
            {
                hdnSelectedCat5.Value = value.ToString();
            }
        }
        */
        public int SelectedCount
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSelectedCount.Value))
                    return Convert.ToInt32(hdnSelectedCount.Value);
                else return 0;
            }
            set
            {
                hdnSelectedCount.Value = value.ToString();
                if (SelectedCount == 0)
                    lblAddCat.Text = "Add category";
                else
                {
                    lblAddCat.Text = "Add another category";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();

            }
        }

        private void Refresh()
        {
            if (!EditMode)
                InitiateCategory();
        }

        public void setSelectedCategory(List<ListItem> selectedCats)
        {
            ListviewSelectedCat.DataSource = selectedCats;
            ListviewSelectedCat.DataBind();
            SelectedCount = selectedCats.Count;
        }

        public List<ListItem> addSelectedCategory()
        {
            List<ListItem> SelectedCategory = GetSelectedCategory();
            SelectedCount = 0;

            List<ListItem> newSelectedCategory = new List<ListItem>();
            lblError.Visible = false;

            foreach (var item in listViewCatRoot.Items)
            {
                HiddenField hdnSelectedID = item.FindControl("hdnCategoryID") as HiddenField;
                CheckBox chkIsChecked = item.FindControl("chkRootIsChecked") as CheckBox;
                LinkButton lnkCategoryName = item.FindControl("lnkCategoryName") as LinkButton;
                if (chkIsChecked != null)
                {
                    if (chkIsChecked.Checked)
                    {
                        if (SelectedCount != 5)
                        {
                            ListItem a = new ListItem(lnkCategoryName.Text, hdnSelectedID.Value);
                            newSelectedCategory.Add(a);
                            SelectedCount++;
                        }
                        else
                        {
                            lblError.Text = SystemConstants.err_MaximumCategorySelected;
                            lblError.Visible = true;
                        }
                    }
                }
            }

            foreach (var item in listViewCatLvl1.Items)
            {
                HiddenField hdnSelectedID = item.FindControl("hdnCategoryID") as HiddenField;
                CheckBox chkLvl1IsChecked = item.FindControl("chkLvl1IsChecked") as CheckBox;
                Label lnkCategoryName = item.FindControl("lnkCategoryName") as Label;
                if (chkLvl1IsChecked != null)
                {

                    if (chkLvl1IsChecked.Checked)
                    {
                        if (SelectedCount != 5)
                        {
                            ListItem a = new ListItem(lnkCategoryName.Text, hdnSelectedID.Value);
                            newSelectedCategory.Add(a);
                            SelectedCount++;
                        }
                        else
                        {
                            lblError.Text = SystemConstants.err_MaximumCategorySelected;
                            lblError.Visible = true;
                        }
                    }
                }
            }

            foreach (var item in listViewCatLvl2.Items)
            {
                HiddenField hdnSelectedID = item.FindControl("hdnCategoryID") as HiddenField;
                CheckBox chkLvl2IsChecked = item.FindControl("chkLvl2IsChecked") as CheckBox;
                Label lnkCategoryName = item.FindControl("lnkCategoryName") as Label;
                if (chkLvl2IsChecked != null)
                {
                    if (chkLvl2IsChecked.Checked)
                    {
                        if (SelectedCount != 5)
                        {
                            ListItem a = new ListItem(lnkCategoryName.Text, hdnSelectedID.Value);
                            newSelectedCategory.Add(a);
                            SelectedCount++;
                        }
                        else
                        {
                            lblError.Text = SystemConstants.err_MaximumCategorySelected;
                            lblError.Visible = true;
                        }
                    }
                }
            }

            if (SelectedCategory.Count != 0)
            {
                IEnumerable<ListItem> query = from newcat in newSelectedCategory
                                              where !SelectedCategory.Any(x => x.Value == newcat.Value)
                                              select newcat;

                if (query != null)
                {
                    foreach (ListItem cat in query)
                    {
                        if (SelectedCategory.Count != 5)
                            SelectedCategory.Add(cat);
                        else
                        {
                            lblError.Text = SystemConstants.err_MaximumCategorySelected;
                            lblError.Visible = true;
                        }
                    }
                }
                return SelectedCategory;
            }
            else
            {
                return newSelectedCategory;
            }
        }

        private void InitiateCategory()
        {
            listViewCatRoot.Items.Clear();
            listViewCatLvl1.Items.Clear();
            listViewCatLvl2.Items.Clear();
            divSecCat.Visible = false;
            divL2SecCat.Visible = false;
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveLv0Categories();
            SortListItem(dt);
            listViewCatRoot.DataSource = dt;
            listViewCatRoot.DataBind();

        }

        private void SortListItem(AdministrationEDSC.CategoryDTDataTable dt)
        {

            string catName = "";
            int catVal = 0;
            var drRemove = dt.NewCategoryDTRow();
            foreach (var dr in dt)
            {

                if (dr.Name == "Other" || dr.Name == "other")
                {

                    catName = dr.Name;
                    catVal = dr.ID;
                    drRemove = dr;

                }
            }

            if (!string.IsNullOrEmpty(catName))
            {
                dt.RemoveCategoryDTRow(drRemove);
                var drLast = dt.NewCategoryDTRow();
                drLast.Name = catName;
                drLast.ID = catVal;
                dt.AddCategoryDTRow(drLast);
            }
        }



        private void CheckCategorylvl1(int RootCat)
        {
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveLv1Categories(RootCat);
            SortListItem(dt);
            listViewCatLvl1.Visible = false;
            if (dt.Count() != 0)
            {
                listViewCatLvl1.Items.Clear();
                listViewCatLvl1.DataSource = dt;
                listViewCatLvl1.DataBind();
                listViewCatLvl1.Visible = true;
                divSecCat.Visible = true;

            }
            else
            {
                listViewCatLvl1.Items.Clear();
                listViewCatLvl1.Visible = false;
                divSecCat.Visible = false;
            }
        }

        private void CheckCategorylvl2(int Lvl1Cat)
        {
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveLv1Categories(Lvl1Cat);
            SortListItem(dt);
            listViewCatLvl2.Items.Clear();
            if (dt.Count() != 0)
            {
                listViewCatLvl2.Items.Clear();
                listViewCatLvl2.DataSource = dt;
                listViewCatLvl2.DataBind();
                listViewCatLvl2.Visible = true;
                divL2SecCat.Visible = true;
            }
            else
            {
                listViewCatLvl2.Items.Clear();
                listViewCatLvl2.Visible = false;
                divL2SecCat.Visible = false;
            }
        }

        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            setSelectedCategory(addSelectedCategory());
            InitiateCategory();
        }

        protected void listViewCatRoot_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName == "CheckLvl1Cat")
                {
                    HiddenField hdnSelectedID = e.Item.FindControl("hdnCategoryID") as HiddenField;
                    if (!string.IsNullOrEmpty(hdnSelectedID.Value))
                        CheckCategorylvl1(Convert.ToInt32(hdnSelectedID.Value));
                }
            }

        }

        protected void listViewCatLvl1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName == "CheckLvl2Cat")
                {
                    HiddenField hdnSelectedID = e.Item.FindControl("hdnCategoryID") as HiddenField;
                    if (!string.IsNullOrEmpty(hdnSelectedID.Value))
                        CheckCategorylvl2(Convert.ToInt32(hdnSelectedID.Value));
                }
            }
        }

        protected void listViewCatLvl2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName == "CheckTrue")
                {
                    CheckBox chkIsChecked = e.Item.FindControl("chkIsChecked") as CheckBox;
                    chkIsChecked.Checked = true;
                }
            }
        }

        protected void ListviewSelectedCat_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                if (e.CommandName == "RemoveCat")
                {

                    HiddenField hdnCategoryID = e.Item.FindControl("hdnCategoryID") as HiddenField;
                    UpdateSelectedCat(Convert.ToInt32(hdnCategoryID.Value));

                }
            }
        }

        private void UpdateSelectedCat(int removedCatID)
        {
            List<ListItem> SelectedCategory = new List<ListItem>();
            lblError.Visible = false;
            SelectedCount = 0;
            foreach (var item in ListviewSelectedCat.Items)
            {
                HiddenField hdnSelectedID = item.FindControl("hdnCategoryID") as HiddenField;
                Label lblCategory = item.FindControl("lblCategory") as Label;
                if (removedCatID.ToString() != hdnSelectedID.Value)
                {
                    ListItem a = new ListItem(lblCategory.Text, hdnSelectedID.Value);
                    SelectedCategory.Add(a);
                    SelectedCount++;
                }
            }

            ListviewSelectedCat.DataSource = SelectedCategory;
            ListviewSelectedCat.DataBind();
        }

        public List<ListItem> GetSelectedCategory()
        {
            List<ListItem> SelectedCategory = new List<ListItem>();
            lblError.Visible = false;

            foreach (var item in ListviewSelectedCat.Items)
            {
                HiddenField hdnSelectedID = item.FindControl("hdnCategoryID") as HiddenField;
                Label lblCategory = item.FindControl("lblCategory") as Label;

                ListItem a = new ListItem(lblCategory.Text, hdnSelectedID.Value);

                SelectedCategory.Add(a);
                SelectedCount++;
            }
            return SelectedCategory;
        }

        protected void listViewCatRoot_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void listViewCatLvl1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    CheckBox chkLvl1IsChecked = e.Item.FindControl("chkLvl1IsChecked") as CheckBox;
            //    HiddenField hdnCategoryID = e.Item.FindControl("hdnCategoryID") as HiddenField;

            //    var selectedCats = GetSelectedCategory();
            //    foreach (var cat in selectedCats)
            //    {
            //        if (cat.Value == hdnCategoryID.Value)
            //            chkLvl1IsChecked.Checked = true;
            //    }

            //}
        }

        protected void listViewCatLvl2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    CheckBox chkLvl2IsChecked = e.Item.FindControl("chkLvl2IsChecked") as CheckBox;
            //    HiddenField hdnCategoryID = e.Item.FindControl("hdnCategoryID") as HiddenField;

            //    var selectedCats = GetSelectedCategory();
            //    foreach (var cat in selectedCats)
            //    {
            //        if (cat.Value == hdnCategoryID.Value)
            //            chkLvl2IsChecked.Checked = true;
            //    }

            //}
        }

        protected void chkIsChecked_CheckedChanged(object sender, EventArgs e)
        {
            int hiddenCat = 0;
            foreach (var listItem in listViewCatRoot.Items)
            {
                if (listItem.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox chkRootIsChecked = listItem.FindControl("chkRootIsChecked") as CheckBox;
                    if (chkRootIsChecked.Checked)
                    {
                        HiddenField hdnSelectedID = listItem.FindControl("hdnCategoryID") as HiddenField;
                        if (!string.IsNullOrEmpty(hdnSelectedID.Value))
                        {
                            CheckCategorylvl1(Convert.ToInt32(hdnSelectedID.Value));
                        }
                    }
                    else
                    {
                        Control divCat = listItem.FindControl("divCat") as Control;
                        divCat.Visible = false;
                        hiddenCat++;
                    }
                }
            }
            if (listViewCatRoot.Items.Count - hiddenCat == 0)
            {
                InitiateCategory();
            }
        }

        protected void chkIsLvl1Checked_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}