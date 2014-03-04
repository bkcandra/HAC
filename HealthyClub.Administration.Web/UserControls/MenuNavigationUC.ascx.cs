using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.BF;
using HealthyClub.Utility;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class MenuNavigationUC : System.Web.UI.UserControl
    {
        public bool EditMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdAddEditMode.Value))
                    return Convert.ToBoolean(hdAddEditMode.Value);
                else return false;
            }
            set
            {
                hdAddEditMode.Value = value.ToString();
            }
        }

        public UIMode PageSettingMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnModeMember.Value))
                    return (UIMode)Convert.ToInt32(hdnModeMember.Value);
                else
                    return UIMode.View;
            }
            set
            {
                switch (value)
                {
                    case MenuNavigationUC.UIMode.Edit:
                        #region Edit
                        //Edit Mode
                        ddParentMenu.Visible = true;

                        ddTargetType.Visible = true;

                        txtTarget.Enabled = true;
                        ddParentMenu.Enabled = true;
                        ddTarget.Enabled = true;
                        ddTargetType.Enabled = true;

                        txtAddEditDescription.Enabled = true;

                        lnkAddNewMenu.Visible = true;
                        lnkOk.Visible = true;
                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = true;
                        lblWarning.Visible = false;
                        #endregion
                        break;

                    case MenuNavigationUC.UIMode.View:
                        //View mode
                        #region View
                        ddParentMenu.Visible = true;
                        ddTargetType.Visible = true;

                        txtTarget.Enabled = false;
                        ddParentMenu.Enabled = false;
                        ddTarget.Enabled = false;
                        ddTargetType.Enabled = false;
                        txtAddEditDescription.Enabled = false;

                        lnkAddNewMenu.Visible = true;
                        lnkOk.Visible = false;
                        lnkCancel.Visible = false;
                        lnkDelete.Visible = false;
                        lnkEdit.Visible = true;
                        lblWarning.Visible = false;
                        #endregion
                        break;
                    case MenuNavigationUC.UIMode.Create:
                        //Create mode
                        #region Create
                        ddParentMenu.Visible = true;
                        ddTargetType.Visible = true;

                        txtTarget.Enabled = true;
                        ddParentMenu.Enabled = true;
                        ddTarget.Enabled = true;
                        ddTargetType.Enabled = true;
                        txtAddEditDescription.Enabled = true;

                        lnkAddNewMenu.Visible = false;
                        lnkOk.Visible = true;
                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                        lblWarning.Visible = false;
                        #endregion
                        break;
                }



                hdnModeMember.Value = ((int)value).ToString();
            }
        }

        public UIMode PageSettingModeProv
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnModeProvider.Value))
                    return (UIMode)Convert.ToInt32(hdnModeProvider.Value);
                else
                    return UIMode.View;
            }
            set
            {
                switch (value)
                {
                    case MenuNavigationUC.UIMode.Edit:
                        #region Edit
                        //Edit Mode
                        ddProviderMenu.Visible = true;

                        ddTargetTypeProvider.Visible = true;

                        txtTargetProvider.Enabled = true;
                        ddProviderMenu.Enabled = true;
                        ddTargetProvider.Enabled = true;
                        ddTargetTypeProvider.Enabled = true;

                        txtAddEditDescriptionProvider.Enabled = true;

                        lnkAddNewMenuProvider.Visible = true;
                        lnkOkProvider.Visible = true;
                        lnkCancelProvider.Visible = true;
                        lnkEditProvider.Visible = false;
                        lnkDeleteProvider.Visible = true;
                        lblWarning.Visible = false;
                        #endregion
                        break;

                    case MenuNavigationUC.UIMode.View:
                        //View mode
                        #region View
                        ddProviderMenu.Visible = true;
                        ddTargetTypeProvider.Visible = true;

                        txtTargetProvider.Enabled = false;
                        ddProviderMenu.Enabled = false;
                        ddTargetProvider.Enabled = false;
                        ddTargetTypeProvider.Enabled = false;
                        txtAddEditDescriptionProvider.Enabled = false;

                        lnkAddNewMenuProvider.Visible = true;
                        lnkOkProvider.Visible = false;
                        lnkCancelProvider.Visible = false;
                        lnkDeleteProvider.Visible = false;
                        lnkEditProvider.Visible = true;
                        lblWarning.Visible = false;
                        #endregion
                        break;
                    case MenuNavigationUC.UIMode.Create:
                        //Create mode
                        #region Create
                        ddProviderMenu.Visible = true;
                        ddTargetTypeProvider.Visible = true;

                        txtTargetProvider.Enabled = true;
                        ddProviderMenu.Enabled = true;
                        ddTargetProvider.Enabled = true;
                        ddTargetTypeProvider.Enabled = true;
                        txtAddEditDescriptionProvider.Enabled = true;

                        lnkAddNewMenuProvider.Visible = false;
                        lnkOkProvider.Visible = true;
                        lnkCancelProvider.Visible = true;
                        lnkEditProvider.Visible = false;
                        lnkDeleteProvider.Visible = false;
                        lblWarning.Visible = false;
                        #endregion
                        break;
                }



                hdnModeProvider.Value = ((int)value).ToString();
            }
        }

        public enum UIMode { Create, Edit, View }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
                lnkEdit.Visible = false; lnkEditProvider.Visible = false;
            }
        }

        private void Refresh()
        {
            //SetddParentMenu();
            //SetddTargetType();
            //SetTarget();
            //RefreshPopUp();
            TreeViewCustomer.Refresh((int)SystemConstants.MenuType.MemberMenu);
            TreeViewProvider.Refresh((int)SystemConstants.MenuType.ProviderMenu);
            PageSettingMode = UIMode.View;
            PageSettingModeProv = UIMode.View;
        }

        #region CustomerNav
        private void SetddTargetType()
        {
            ddTargetType.Items.Clear();
            string name = SystemConstants.ActivityListByCategory;
            int ID = (int)SystemConstants.MenuTargetType.Category;
            ListItem li = new ListItem(name, ID.ToString());
            ddTargetType.Items.Add(li);

            //name = SystemConstants.ActivityListByProvider;
            //ID = (int)SystemConstants.MenuTargetType.Provider;
            //li = new ListItem(name, ID.ToString());
            //ddTargetType.Items.Add(li);

            name = SystemConstants.MenuTargetType.Activity.ToString();
            ID = (int)SystemConstants.MenuTargetType.Activity;
            li = new ListItem(name, ID.ToString());
            ddTargetType.Items.Add(li);

            name = SystemConstants.page;
            ID = (int)SystemConstants.MenuTargetType.Page;
            li = new ListItem(name, ID.ToString());
            ddTargetType.Items.Add(li);

            name = SystemConstants.externalLink;
            ID = (int)SystemConstants.MenuTargetType.ExternalLink;
            li = new ListItem(name, ID.ToString());
            ddTargetType.Items.Add(li);
        }

        private void SetddParentMenu()
        {
            AdministrationEDSC.v_MenuDTDataTable dt = new AdministrationDAC().RetrieveMenuExplorers((int)SystemConstants.MenuType.MemberMenu);
            ddParentMenu.Items.Clear();
            ListItem li = new ListItem("Root", "0");
            ddParentMenu.Items.Add(li);
            foreach (var dr in dt)
            {
                if (dr.ParentMenuID == 0)
                {
                    string name = "";

                    name += dr.LinkText;

                    li = new ListItem(name, dr.ID.ToString());
                    ddParentMenu.Items.Add(li);
                }
            }
        }

        private void RefreshPopUp()
        {
            ddParentMenu.Visible = false;
            txtAddEditDescription.Visible = false;
            lblmenuLvl.Visible = false;
            ddTargetType.Visible = false;
            ddTarget.Visible = false;
            lnkOk.Visible = false;
            lnkCancel.Visible = false;
            imgUp.Visible = false;
            imgDown.Visible = false;
        }

        protected void lnkAddNewMenu_Click(object sender, EventArgs e)
        {
            PageSettingMode = UIMode.Create;

            SetddParentMenu();
            SetddTargetType();
            SetTarget();
            lblmenuLvl.Text = "1";

            imgUp.Visible = false;
            imgDown.Visible = false;
            lblWarning.Visible = false;
            txtAddEditDescription.Text = "";

            EditMode = false;
            lnkOk.Visible = true;
            lnkCancel.Visible = true;
        }

        private void SetTarget()
        {
            ddTarget.Visible = true;
            txtTarget.Visible = false;
            ddTarget.Items.Clear();
            if (ddTargetType.SelectedValue == ((int)SystemConstants.MenuTargetType.Category).ToString())
            {
                AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveAllCategories();
                ddTarget.Items.Clear();
                foreach (var dr in dt)
                {
                    string name = "";
                    if (dr.Level1ParentID != 0 && !dr.IsLevel1ParentNameNull())
                        name += dr.Level1ParentName + " >> ";

                    if (dr.Level2ParentID != 0 && !dr.IsLevel2ParentNameNull())
                        name += dr.Level2ParentName + " >> ";

                    name += dr.Name;
                    ListItem li = new ListItem(name, dr.ID.ToString());
                    ddTarget.Items.Add(li);
                }
            }
            else if (ddTargetType.SelectedValue == ((int)SystemConstants.MenuTargetType.Provider).ToString())
            {
                AdministrationEDSC.ProviderProfilesDTDataTable dt = new AdministrationDAC().RetrieveAllproviders();
                if (dt.Count == 0)
                {
                    ListItem li = new ListItem("No Data ", "0");
                    ddTarget.Items.Add(li);
                }
                else
                {
                    ddTarget.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = dr.ProviderName;
                        ListItem li = new ListItem(name, dr.UserID.ToString());
                        ddTarget.Items.Add(li);
                    }
                }
            }
            else if (ddTargetType.SelectedValue == "3")
            {
                AdministrationEDSC.ActivityDTDataTable dt = new AdministrationDAC().RetrieveActivities();
                if (dt.Count == 0)
                {

                    ListItem li = new ListItem("No Data ", "0");
                    ddTarget.Items.Add(li);
                }
                else
                {

                    ddTarget.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = "";
                        name += dr.Name;
                        ListItem li = new ListItem(name, dr.ID.ToString());
                        ddTarget.Items.Add(li);
                    }
                }
            }
            else if (ddTargetType.SelectedValue == "4")
            {
                AdministrationEDSC.PageDTDataTable dt = new AdministrationDAC().RetrievePages();
                if (dt.Count == 0)
                {
                    ListItem li = new ListItem("No Page ", "0");
                    ddTarget.Items.Add(li);
                }
                else
                {
                    ddTarget.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = dr.Name;
                        ListItem li = new ListItem(name, dr.ID.ToString());
                        ddTarget.Items.Add(li);
                    }
                }
            }
            else if (ddTargetType.SelectedValue == "6")
            {
                ddTarget.Items.Clear();
                ddTarget.Visible = false;
                txtTarget.Visible = true;

            }
        }

        protected void imgUp_Click(object sender, ImageClickEventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.MenuDTRow dr = new AdministrationEDSC.MenuDTDataTable().NewMenuDTRow();
            dr.ID = Convert.ToInt16(hdnPopUpMenuItemID.Value);

            bool cannotChangePos;

            bfc.SortMenuItem(dr.ID, true, out cannotChangePos);
            if (cannotChangePos)
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Cannot Change Pos";
            }
            else
            {
                lblWarning.Visible = false;
                lblWarning.Text = "";
                TreeViewCustomer.Refresh((int)SystemConstants.MenuType.MemberMenu);
            }
        }

        protected void imgDown_Click(object sender, ImageClickEventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.MenuDTRow dr = new AdministrationEDSC.MenuDTDataTable().NewMenuDTRow();
            dr.ID = Convert.ToInt16(hdnPopUpMenuItemID.Value);
            bool cannotChangePos;

            bfc.SortMenuItem(dr.ID, false, out cannotChangePos);
            if (cannotChangePos)
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Cannot Change Pos";
            }
            else
            {
                lblWarning.Visible = false;
                lblWarning.Text = "";
                TreeViewCustomer.Refresh((int)SystemConstants.MenuType.MemberMenu);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            bfc.DeleteMenuItem(Convert.ToInt32(hdnPopUpMenuItemID.Value));
            Refresh();
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            lblWarning.Visible = false;
            txtAddEditDescription.Text = "";
            lblmenuLvl.Text = "";
            SetTarget();
            EditMode = false;
            Refresh();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (ddTarget.SelectedValue == "0")
            {
                lblWarning.Visible = true;
                lblWarning.Text = "Selected Target Contains No Data";
            }
            else
            {
                lblWarning.Visible = false;

                AdministrationEDSC.v_MenuDTRow dr = GetData();
                lblTextErr.Visible = true;
                Page.Focus();
                if (!string.IsNullOrEmpty(dr.LinkText))
                {
                    lblTextErr.Visible = false;

                    if (EditMode)
                    {
                        AdministrationDAC dac = new AdministrationDAC();

                        dr.ID = Convert.ToInt32(hdnPopUpMenuItemID.Value);
                        dac.UpdateMenu(dr);
                        EditMode = false;
                        Refresh();
                        lblWarning.Visible = true;
                        lblWarning.Text = "Changes Saved";
                    }
                    else if (!EditMode)
                    {
                        AdministrationDAC dac = new AdministrationDAC();

                        dac.CreateMenu(dr);
                        Refresh();
                    }
                }
            }
        }

        private AdministrationEDSC.v_MenuDTRow GetData()
        {
            AdministrationEDSC.v_MenuDTRow dr = new AdministrationEDSC.v_MenuDTDataTable().Newv_MenuDTRow();
            dr.ParentMenuID = Convert.ToInt32(ddParentMenu.SelectedValue);
            dr.LinkType = Convert.ToInt32(ddTargetType.SelectedValue);
            dr.MenuType = (int)SystemConstants.MenuType.MemberMenu;
            if (ddTargetType.SelectedValue == ((int)SystemConstants.MenuTargetType.ExternalLink).ToString())
            {
                dr.LinkText = txtAddEditDescription.Text;
                dr.LinkValue = txtTarget.Text;
            }
            else
            {
                dr.LinkText = txtAddEditDescription.Text;
                dr.LinkValue = ddTarget.SelectedValue;
            }
            return dr;
        }

        protected void MenuTreeView1_OnTreeNodeClicked(int ID)
        {
            var dr = new AdministrationDAC().RetrieveMenuExplorer(ID);
            if (dr == null)
            {
                lblWarning.Visible = false;
                if (ID == 0)
                {
                    txtAddEditDescription.Text = "Home";
                    lblmenuLvl.Text = "1";
                    lblAddEditTitle.Text = "Viewing Home";
                }
                lnkEdit.Visible = false;
                EditMode = false;
                PageSettingMode = UIMode.View;
            }
            else
            {
                lnkEdit.Visible = true;
                EditMode = true;
                SetddParentMenu();
                SetddTargetType();
                SetTarget();

                ddParentMenu.Visible = true;
                txtAddEditDescription.Visible = true;
                lblmenuLvl.Visible = true;
                ddTargetType.Visible = true;
                ddTarget.Visible = true;
                lnkOk.Visible = true;
                lnkCancel.Visible = true;
                imgUp.Visible = false;
                imgDown.Visible = false;
                lnkDelete.Visible = true;

                ddParentMenu.SelectedValue = dr.ParentMenuID.ToString();
                txtAddEditDescription.Text = dr.LinkText;
                lblAddEditTitle.Text = "Edit " + dr.LinkText + " menu";
                txtTarget.Text = dr.LinkValue;
                ddTargetType.SelectedValue = dr.LinkType.ToString();
                SetTarget();
                if (ddTarget.Items.FindByValue(dr.LinkValue.ToString()) != null)
                    ddTarget.SelectedValue = dr.LinkValue.ToString();

                hdnPopUpMenuItemID.Value = dr.ID.ToString();

                if (!string.IsNullOrEmpty(dr.ParentMenuID.ToString()))
                {
                    if (dr.ParentMenuID != 0)
                    {
                        lblmenuLvl.Text = "2";
                    }
                    else if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvl.Text = "1";
                    }
                }
                PageSettingMode = UIMode.View;
            }
        }

        protected void ddParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedMenuID = Convert.ToInt32(ddParentMenu.SelectedValue);
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrieveMenu(SelectedMenuID);

            if (dr != null)
            {
                if (EditMode)
                {
                    if (dr.ParentMenuID != 0)
                    {
                        lblmenuLvl.Text = "2";
                    }
                    else if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvl.Text = "1";
                    }
                }
                else if (!EditMode)
                {
                    if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvl.Text = "2";
                    }
                    else
                    {
                        lblmenuLvl.Text = "1";
                    }
                }
            }
            else lblmenuLvl.Text = "1";

        }

        protected void ddTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTarget();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            PageSettingMode = UIMode.Edit;
        }
        #endregion

        #region ProviderNav

        private void SetddTargetTypeProvider()
        {
            ddTargetTypeProvider.Items.Clear();
            string name = SystemConstants.ActivityListByCategory;
            int ID = (int)SystemConstants.MenuTargetType.Category;
            ListItem li = new ListItem(name, ID.ToString());
            ddTargetTypeProvider.Items.Add(li);

            //name = SystemConstants.ActivityListByProvider;
            //ID = (int)SystemConstants.MenuTargetType.Provider;
            //li = new ListItem(name, ID.ToString());
            //ddTargetType.Items.Add(li);

            name = SystemConstants.MenuTargetType.Activity.ToString();
            ID = (int)SystemConstants.MenuTargetType.Activity;
            li = new ListItem(name, ID.ToString());
            ddTargetTypeProvider.Items.Add(li);

            name = SystemConstants.page;
            ID = (int)SystemConstants.MenuTargetType.Page;
            li = new ListItem(name, ID.ToString());
            ddTargetTypeProvider.Items.Add(li);

            name = SystemConstants.externalLink;
            ID = (int)SystemConstants.MenuTargetType.ExternalLink;
            li = new ListItem(name, ID.ToString());
            ddTargetTypeProvider.Items.Add(li);
        }

        private void SetddParentMenuProvider()
        {
            AdministrationEDSC.v_MenuDTDataTable dt = new AdministrationDAC().RetrieveMenuExplorers((int)SystemConstants.MenuType.ProviderMenu);
            ddProviderMenu.Items.Clear();
            ListItem li = new ListItem("Root", "0");
            ddProviderMenu.Items.Add(li);
            foreach (var dr in dt)
            {
                if (dr.ParentMenuID == 0)
                {
                    string name = "";

                    name += dr.LinkText;

                    li = new ListItem(name, dr.ID.ToString());
                    ddProviderMenu.Items.Add(li);
                }
            }
        }

        private void RefreshPopUpProvider()
        {
            ddProviderMenu.Visible = false;
            txtAddEditDescriptionProvider.Visible = false;
            lblmenuLvlProvider.Visible = false;
            ddTargetTypeProvider.Visible = false;
            ddTargetProvider.Visible = false;
            lnkOkProvider.Visible = false;
            lnkCancelProvider.Visible = false;
            imgUpProv.Visible = false;
            imgDownProv.Visible = false;
        }

        protected void lnkAddNewMenuProvider_Click(object sender, EventArgs e)
        {
            PageSettingModeProv = UIMode.Create;

            SetddParentMenuProvider();
            SetddTargetTypeProvider();
            SetTargetProvider();
            lblmenuLvlProvider.Text = "1";

            imgUpProv.Visible = false;
            imgDownProv.Visible = false;
            lblWarningProvider.Visible = false;
            txtAddEditDescriptionProvider.Text = "";

            EditMode = false;
            lnkOkProvider.Visible = true;
            lnkCancelProvider.Visible = true;
        }

        private void SetTargetProvider()
        {
            ddTargetProvider.Visible = true;
            txtTargetProvider.Visible = false;
            ddTargetProvider.Items.Clear();
            if (ddTargetTypeProvider.SelectedValue == ((int)SystemConstants.MenuTargetType.Category).ToString())
            {
                AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveAllCategories();
                ddTargetProvider.Items.Clear();
                foreach (var dr in dt)
                {
                    string name = "";
                    if (dr.Level1ParentID != 0 && !dr.IsLevel1ParentNameNull())
                        name += dr.Level1ParentName + " >> ";

                    if (dr.Level2ParentID != 0 && !dr.IsLevel2ParentNameNull())
                        name += dr.Level2ParentName + " >> ";

                    name += dr.Name;
                    ListItem li = new ListItem(name, dr.ID.ToString());
                    ddTargetProvider.Items.Add(li);
                }
            }
            else if (ddTargetTypeProvider.SelectedValue == ((int)SystemConstants.MenuTargetType.Provider).ToString())
            {
                AdministrationEDSC.ProviderProfilesDTDataTable dt = new AdministrationDAC().RetrieveAllproviders();
                if (dt.Count == 0)
                {
                    ListItem li = new ListItem("No Data ", "0");
                    ddTargetProvider.Items.Add(li);
                }
                else
                {
                    ddTargetProvider.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = dr.ProviderName;
                        ListItem li = new ListItem(name, dr.UserID.ToString());
                        ddTargetProvider.Items.Add(li);
                    }
                }
            }
            else if (ddTargetTypeProvider.SelectedValue == "3")
            {
                AdministrationEDSC.ActivityDTDataTable dt = new AdministrationDAC().RetrieveActivities();
                if (dt.Count == 0)
                {

                    ListItem li = new ListItem("No Data ", "0");
                    ddTargetProvider.Items.Add(li);
                }
                else
                {

                    ddTargetProvider.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = "";
                        name += dr.Name;
                        ListItem li = new ListItem(name, dr.ID.ToString());
                        ddTargetProvider.Items.Add(li);
                    }
                }
            }
            else if (ddTargetTypeProvider.SelectedValue == "4")
            {
                AdministrationEDSC.PageDTDataTable dt = new AdministrationDAC().RetrievePages();
                if (dt.Count == 0)
                {
                    ListItem li = new ListItem("No Page ", "0");
                    ddTargetProvider.Items.Add(li);
                }
                else
                {
                    ddTargetProvider.Items.Clear();
                    foreach (var dr in dt)
                    {
                        string name = dr.Name;
                        ListItem li = new ListItem(name, dr.ID.ToString());
                        ddTargetProvider.Items.Add(li);
                    }
                }
            }
            else if (ddTargetTypeProvider.SelectedValue == "6")
            {
                ddTargetProvider.Items.Clear();
                ddTargetProvider.Visible = false;
                txtTargetProvider.Visible = true;

            }
        }

        protected void imgUpProv_Click(object sender, ImageClickEventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.MenuDTRow dr = new AdministrationEDSC.MenuDTDataTable().NewMenuDTRow();
            dr.ID = Convert.ToInt16(hdnPopUpMenuItemIDProvider.Value);

            bool cannotChangePos;

            bfc.SortMenuItem(dr.ID, true, out cannotChangePos);
            if (cannotChangePos)
            {
                lblWarningProvider.Visible = true;
                lblWarningProvider.Text = "Cannot Change Pos";
            }
            else
            {
                lblWarningProvider.Visible = false;
                lblWarningProvider.Text = "";
                TreeViewProvider.Refresh((int)SystemConstants.MenuType.ProviderMenu);
            }
        }

        protected void imgDownProv_Click(object sender, ImageClickEventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.MenuDTRow dr = new AdministrationEDSC.MenuDTDataTable().NewMenuDTRow();
            dr.ID = Convert.ToInt16(hdnPopUpMenuItemIDProvider.Value);
            bool cannotChangePos;

            bfc.SortMenuItem(dr.ID, false, out cannotChangePos);
            if (cannotChangePos)
            {
                lblWarningProvider.Visible = true;
                lblWarningProvider.Text = "Cannot Change Pos";
            }
            else
            {
                lblWarningProvider.Visible = false;
                lblWarningProvider.Text = "";
                TreeViewProvider.Refresh((int)SystemConstants.MenuType.ProviderMenu);
            }
        }

        protected void lnkDeleteProvider_Click(object sender, EventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            bfc.DeleteMenuItem(Convert.ToInt32(hdnPopUpMenuItemIDProvider.Value));
            Refresh();
        }

        protected void lnkCancelProvider_Click(object sender, EventArgs e)
        {
            lblWarningProvider.Visible = false;
            txtAddEditDescriptionProvider.Text = "";
            lblmenuLvlProvider.Text = "";
            SetTargetProvider();
            EditMode = false;
            Refresh();
        }

        protected void lnkOkProvider_Click(object sender, EventArgs e)
        {
            if (ddTargetProvider.SelectedValue == "0")
            {
                lblWarningProvider.Visible = true;
                lblWarningProvider.Text = "Selected Target Contains No Data";
            }
            else
            {
                lblWarningProvider.Visible = false;

                AdministrationEDSC.v_MenuDTRow dr = GetDataProvider();

                lblTextErrProvider.Visible = true;
                Page.Focus();
                if (!string.IsNullOrEmpty(dr.LinkText))
                {
                    lblTextErrProvider.Visible = false;

                    if (EditMode)
                    {
                        AdministrationDAC dac = new AdministrationDAC();

                        dr.ID = Convert.ToInt32(hdnPopUpMenuItemIDProvider.Value);
                        dac.UpdateMenu(dr);
                        EditMode = false;
                        Refresh();
                        lblWarningProvider.Visible = true;
                        lblWarningProvider.Text = "Changes Saved";
                    }
                    else if (!EditMode)
                    {
                        AdministrationDAC dac = new AdministrationDAC();

                        dac.CreateMenu(dr);
                        Refresh();
                    }
                }
            }
        }

        private AdministrationEDSC.v_MenuDTRow GetDataProvider()
        {
            AdministrationEDSC.v_MenuDTRow dr = new AdministrationEDSC.v_MenuDTDataTable().Newv_MenuDTRow();

            dr.ParentMenuID = Convert.ToInt32(ddProviderMenu.SelectedValue);
            dr.LinkType = Convert.ToInt32(ddTargetTypeProvider.SelectedValue);
            dr.MenuType = (int)SystemConstants.MenuType.ProviderMenu;
            if (ddTargetTypeProvider.SelectedValue == ((int)SystemConstants.MenuTargetType.ExternalLink).ToString())
            {
                dr.LinkValue = txtTargetProvider.Text;
                dr.LinkText = txtAddEditDescriptionProvider.Text;
            }
            else
            {
                dr.LinkText = ddTarget.SelectedItem.Text;
                dr.LinkValue = ddTargetProvider.SelectedValue;
            }
            return dr;
        }

        protected void TreeViewProvider_OnTreeNodeClicked(int ID)
        {
            var dr = new AdministrationDAC().RetrieveMenuExplorer(ID);
            if (dr == null)
            {
                lblWarningProvider.Visible = false;
                if (ID == 0)
                {
                    txtAddEditDescriptionProvider.Text = "Home";
                    lblmenuLvlProvider.Text = "1";
                    lblAddEditTitleProvider.Text = "Viewing Home";
                }
                lnkEditProvider.Visible = false;
                EditMode = false;
                PageSettingModeProv = UIMode.View;
            }
            else
            {
                lnkEditProvider.Visible = true;
                EditMode = true;
                SetddParentMenuProvider();
                SetddTargetTypeProvider();
                SetTargetProvider();

                ddProviderMenu.Visible = true;
                txtAddEditDescriptionProvider.Visible = true;
                lblmenuLvlProvider.Visible = true;
                ddTargetTypeProvider.Visible = true;
                ddTargetProvider.Visible = true;
                lnkOkProvider.Visible = true;
                lnkCancelProvider.Visible = true;
                imgUpProv.Visible = false;
                imgDownProv.Visible = false;
                lnkDeleteProvider.Visible = true;

                ddProviderMenu.SelectedValue = dr.ParentMenuID.ToString();
                txtAddEditDescriptionProvider.Text = dr.LinkText;
                lblAddEditTitleProvider.Text = "Edit " + dr.LinkText + " menu";
                txtTargetProvider.Text = dr.LinkValue;
                ddTargetTypeProvider.SelectedValue = dr.LinkType.ToString();
                SetTargetProvider();
                if (ddTargetProvider.Items.FindByValue(dr.LinkValue.ToString()) != null)
                    ddTargetProvider.SelectedValue = dr.LinkValue.ToString();

                hdnPopUpMenuItemIDProvider.Value = dr.ID.ToString();

                if (!string.IsNullOrEmpty(dr.ParentMenuID.ToString()))
                {
                    if (dr.ParentMenuID != 0)
                    {
                        lblmenuLvlProvider.Text = "2";
                    }
                    else if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvlProvider.Text = "1";
                    }
                }
                PageSettingModeProv = UIMode.View;
            }
        }

        protected void ddProviderMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedMenuID = Convert.ToInt32(ddProviderMenu.SelectedValue);
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrieveMenu(SelectedMenuID);

            if (dr != null)
            {
                if (EditMode)
                {
                    if (dr.ParentMenuID != 0)
                    {
                        lblmenuLvlProvider.Text = "2";
                    }
                    else if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvlProvider.Text = "1";
                    }
                }
                else if (!EditMode)
                {
                    if (dr.ParentMenuID == 0)
                    {
                        lblmenuLvlProvider.Text = "2";
                    }
                    else
                    {
                        lblmenuLvlProvider.Text = "1";
                    }
                }
            }
            else lblmenuLvlProvider.Text = "1";

        }

        protected void ddTargetTypeProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTargetProvider();
        }

        protected void lnkEditProvider_Click(object sender, EventArgs e)
        {
            PageSettingModeProv = UIMode.Edit;
        }

        #endregion

    }
}