using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.BF;
using System.Text;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class PageSetupUC : System.Web.UI.UserControl
    {
        public UIMode Mode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnMode.Value))
                    return (UIMode)Convert.ToInt32(hdnMode.Value);
                else
                    return UIMode.View;
            }
            set
            {
                switch (value)
                {
                    case PageSetupUC.UIMode.Edit:
                        #region Edit
                        //Edit Mode
                        txtDescription.Visible = true;
                        txtKeyword.Visible = true;
                        txtName.Visible = true;
                        txtTitle.Visible = true;
                        lblDescription.Visible = false;
                        lblKeyword.Visible = false;
                        lblName.Visible = false;
                        lblTitle.Visible = false;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        lnkDuplicate.Visible = false;

                        divCopyTable.Visible = false;

                        divPageEditor.Visible = true;
                        divContentPreview.Visible = false;
                        divContentPreviewHead.Visible = false;

                        CKEditorControl1.ToolbarStartupExpanded = true;

                        #endregion
                        break;

                    case PageSetupUC.UIMode.View:
                        //View mode
                        #region View
                        txtDescription.Visible = false;
                        txtKeyword.Visible = false;
                        txtName.Visible = false;
                        txtTitle.Visible = false;

                        lblDescription.Visible = true;
                        lblKeyword.Visible = true;
                        lblName.Visible = true;
                        lblTitle.Visible = true;

                        lnkCancel.Visible = false;
                        lnkEdit.Visible = true;
                        lnkSave.Visible = false;
                        lnkDuplicate.Visible = true;

                        divCopyTable.Visible = false;

                        divPageEditor.Visible = false;
                        divContentPreview.Visible = true;
                        divContentPreviewHead.Visible = true;
                        #endregion
                        break;
                    case PageSetupUC.UIMode.Create:
                        //Create mode
                        #region Create
                        txtDescription.Visible = true;
                        txtKeyword.Visible = true;
                        txtName.Visible = true;
                        txtTitle.Visible = true;
                        lblDescription.Visible = false;
                        lblKeyword.Visible = false;
                        lblName.Visible = false;
                        lblTitle.Visible = false;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        lnkDuplicate.Visible = false;

                        divCopyTable.Visible = true;

                        divPageEditor.Visible = true;
                        divContentPreview.Visible = false;
                        divContentPreviewHead.Visible = false;
                        #endregion
                        break;
                }

                hdnMode.Value = ((int)value).ToString();
            }
        }

        public enum UIMode { Create, Edit, View }

        public int PageID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageID.Value))
                    return Convert.ToInt32(hdnPageID.Value);
                else return 0;
            }
            set
            {
                hdnPageID.Value = value.ToString();
            }
        }

        public bool EditMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnEditMode.Value))
                    return Convert.ToBoolean(hdnEditMode.Value);
                else return false;
            }
            set
            {
                hdnEditMode.Value = value.ToString();
            }
        }

        public int PageType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageType.Value))
                    return Convert.ToInt32(hdnPageType.Value);
                else return 0;
            }
            set
            {
                hdnPageType.Value = value.ToString();
                if (PageType == (int)SystemConstants.ItemType.Standard)
                    lblPageSetup.Text = "Page Setup";
                /*else if (PageType == (int)SystemConstants.ItemType.Tab)
                    lblPageSetup.Text = "Tabbed Page Setup";
                else if (PageType == (int)SystemConstants.ItemType.Sidebar)
                    lblPageSetup.Text = "Sidebar Setup";*/
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.PageID] != null)
            {
                PageID = Convert.ToInt32(Request.QueryString[SystemConstants.PageID]);
            }
            if (Request.QueryString[SystemConstants.PageType] != null)
            {
                PageType = Convert.ToInt32(Request.QueryString[SystemConstants.PageType]);
            }
            if (Request.QueryString[SystemConstants.EditMode] != null)
            {
                EditMode = Convert.ToBoolean(Request.QueryString[SystemConstants.EditMode]);
            }

        }

        protected void lnkCopyFrom_Click(object sender, EventArgs e)
        {
            int newDynamicPageID = 0;
            int oldDynamicPageID = Convert.ToInt32(ddDynamicPage.SelectedValue);
            new AdministrationBFC().DuplicatePage(oldDynamicPageID, out newDynamicPageID);
            Response.Redirect("~/Pages/PageSetup.aspx?" + SystemConstants.PageID + "=" + newDynamicPageID + "&" + SystemConstants.EditMode + "=" + true);
        }

        private void SetData(AdministrationEDSC.PageDTRow dr)
        {
            PageType = dr.PageType;
            txtName.Text = dr.Name;
            lblName.Text = dr.Name;
            txtTitle.Text = dr.Title;
            lblTitle.Text = dr.Title;
            txtDescription.Text = dr.MetaDescription;
            lblDescription.Text = dr.MetaDescription;
            txtKeyword.Text = dr.MetaTag;
            lblKeyword.Text = dr.MetaTag;
            CKEditorControl1.Text = dr.PageContent;
            divContentPreview.InnerHtml = dr.PageContent;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            if (Mode == UIMode.Create)
                Response.Redirect("~/Pages");

            else if (Mode == UIMode.Edit)
                Refresh();


        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            var dr = GetData();
            
                if (Mode == UIMode.Create)
                {
                    if (Validate(dr))
                    {
                    new AdministrationDAC().CreatePage(dr);
                    PageID = dr.ID;
                    }
                }
                else if (Mode == UIMode.Edit)
                    new AdministrationDAC().UpdatePage(dr);
                Refresh();
            
        }

        private bool Validate(AdministrationEDSC.PageDTRow dr)
        {
            bool isvalid = true;
            StringBuilder sbError = new StringBuilder();
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtKeyword.Text) || string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtDescription.Text))
            {
                isvalid = false;

                sbError.AppendLine("One or more page information is empty.<br />");
            }
            if (!isUnique(dr.Name))
            {
                isvalid = false;
                sbError.AppendLine("Page name must be unique.<br />");
            }


            divError.Visible = divSuccess.Visible != !isvalid;
            if (!isvalid)
                lblError.Text = sbError.ToString();
            else
                lblSuccess.Text = "Page saved";

            return isvalid;
        }
        private bool isUnique(string name)
        {
            if (new AdministrationDAC().isPageExist(name))
                return false;
            else return true;


        }


        private AdministrationEDSC.PageDTRow GetData()
        {
            AdministrationEDSC.PageDTRow dr = new AdministrationEDSC.PageDTDataTable().NewPageDTRow();

            dr.PageType = (int)SystemConstants.ItemType.Standard;
            dr.ID = PageID;

            if (!string.IsNullOrEmpty(txtName.Text))
                dr.Name = txtName.Text;
            else dr.Name = "StandardPage";

            if (!string.IsNullOrEmpty(txtTitle.Text))
                dr.Title = txtTitle.Text;
            else
                dr.Title = "StandardPage";


            if (!string.IsNullOrEmpty(txtDescription.Text))
                dr.MetaDescription = txtDescription.Text;
            else
                dr.MetaDescription = "";

            if (!string.IsNullOrEmpty(txtKeyword.Text))
                dr.MetaTag = txtKeyword.Text;
            else
                dr.MetaTag = "";

            if (PageType == (int)SystemConstants.ItemType.Standard)
                dr.MetaTag = txtKeyword.Text;
            else
                dr.MetaTag = "";
            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                dr.PageContent = CKEditorControl1.Text;
            else dr.PageContent = "";
            return dr;

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Refresh();
            Mode = UIMode.Edit;
        }

        private void Refresh()
        {
            if (PageType != (int)SystemConstants.ItemType.Standard)
            {

            }
            else if (PageType == (int)SystemConstants.ItemType.Sidebar)
            {
                trkeyword.Visible = false;
                trDescription.Visible = false;
                txtKeyword.Text = "Sidebar Page";
                txtDescription.Text = "Sidebar Page";
            }

            if (PageID == 0)
            {
                Mode = UIMode.Create;
                setddPage();
            }

            else if (PageID != 0)
            {
                SetPageInfo();
                if (EditMode)
                {
                    Mode = UIMode.Edit;
                    EditMode = false;
                }
            }


        }

        private void setddPage()
        {
            var dt = new AdministrationDAC().RetrievePages((int)SystemConstants.ItemType.Standard);
            ddDynamicPage.Items.Clear();

            if (dt != null)
            {
                foreach (var dr in dt)
                {
                    ListItem dede = new ListItem(dr.Name, dr.ID.ToString());
                    ddDynamicPage.Items.Add(dede);
                }
            }
            else
            {
                ListItem dede = new ListItem("No Page", "0");
                ddDynamicPage.Items.Add(dede);
            }
        }

        private void SetPageInfo()
        {
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrievePage(PageID);
            SetData(dr);
            Mode = UIMode.View;
        }

        protected void lnkDuplicate_Click(object sender, EventArgs e)
        {
            int newPageID = 0;
            new AdministrationBFC().DuplicatePage(PageID, out newPageID);
            Response.Redirect("~/Pages/PageSetup.aspx?" + SystemConstants.PageID + "=" + newPageID + "&" + SystemConstants.EditMode + "=" + true);
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            lblNameError.Visible = false;
            if (!isUnique(txtName.Text))
            {
                lblNameError.Text = "This page name has been taken.";
                lblNameError.Visible = true;
            }
        }

    }
}