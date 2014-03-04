using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class MailTemplateDetailUC : System.Web.UI.UserControl
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
                    case MailTemplateDetailUC.UIMode.Edit:
                        #region Edit
                        //Edit Mode
                        txtCC.Visible = true;

                        txtName.Visible = true;
                        txtSubject.Visible = true;
                        lblCC.Visible = false;

                        lblName.Visible = false;
                        lblSubject.Visible = false;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;

                        CKEditorControl1.Visible = true;
                        divPageEditor.Visible = true;
                        divContentPreview.Visible = false;
                        divContentPreviewHead.Visible = false;

                        CKEditorControl1.ToolbarStartupExpanded = true;

                        #endregion
                        break;

                    case MailTemplateDetailUC.UIMode.View:
                        //View mode
                        #region View
                        txtCC.Visible = false;
                        txtName.Visible = false;
                        txtSubject.Visible = false;
                        CKEditorControl1.Visible = false;
                        lblCC.Visible = true;
                        lblName.Visible = true;
                        lblSubject.Visible = true;

                        lnkCancel.Visible = false;
                        lnkEdit.Visible = true;
                        lnkSave.Visible = false;

                        divPageEditor.Visible = false;
                        divContentPreview.Visible = true;
                        divContentPreviewHead.Visible = true;
                        #endregion
                        break;
                    case MailTemplateDetailUC.UIMode.Create:
                        //Create mode
                        #region Create
                        txtCC.Visible = true;

                        txtName.Visible = true;
                        txtSubject.Visible = true;
                        lblCC.Visible = false;

                        lblName.Visible = false;
                        lblSubject.Visible = false;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;

                        CKEditorControl1.Visible = true;
                        divPageEditor.Visible = true;
                        divContentPreview.Visible = false;
                        divContentPreviewHead.Visible = false;

                        CKEditorControl1.ToolbarStartupExpanded = true;
                        #endregion
                        break;
                }

                hdnMode.Value = ((int)value).ToString();
            }
        }

        public enum UIMode { Create, Edit, View }

        public int TemplateID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTemplateID.Value))
                    return Convert.ToInt32(hdnTemplateID.Value);
                else return 0;
            }
            set
            {
                hdnTemplateID.Value = value.ToString();
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

        public int EmailType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnEmailType.Value))
                    return Convert.ToInt32(hdnEmailType.Value);
                else return 0;
            }
            set
            {
                hdnEmailType.Value = value.ToString();
                if (EmailType == (int)SystemConstants.ItemType.Standard)
                    lblPageSetup.Text = "Template Setup";
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
            if (Request.QueryString[SystemConstants.TemplateID] != null)
            {
                TemplateID = Convert.ToInt32(Request.QueryString[SystemConstants.TemplateID]);
            }
            if (Request.QueryString[SystemConstants.EmailType] != null)
            {
                EmailType = Convert.ToInt32(Request.QueryString[SystemConstants.EmailType]);
            }
            if (Request.QueryString[SystemConstants.EditMode] != null)
            {
                EditMode = Convert.ToBoolean(Request.QueryString[SystemConstants.EditMode]);
            }

        }

        private void SetData(AdministrationEDSC.EmailTemplateDTRow dr)
        {
            EmailType = dr.EmailType;
            txtName.Text = dr.EmailName;
            lblName.Text = dr.EmailName;
            txtCC.Text = dr.Emailcc;
            lblCC.Text = dr.Emailcc;
            txtSubject.Text = dr.EmailSubject;
            lblSubject.Text = dr.EmailSubject;

            CKEditorControl1.Text = dr.EmailBody;
            divContentPreview.InnerHtml = dr.EmailBody;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            if (Mode == UIMode.Create)
                Response.Redirect("~/Mail/");

            else if (Mode == UIMode.Edit)
                Refresh();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var dr = GetData();
            if (Mode == UIMode.Create)
            {
                new AdministrationDAC().CreateEmailTemplate(dr);
                TemplateID = dr.ID;
            }
            else if (Mode == UIMode.Edit)
                new AdministrationDAC().UpdateEmailTemplate(dr);
            Refresh();
        }

        private AdministrationEDSC.EmailTemplateDTRow GetData()
        {
            AdministrationEDSC.EmailTemplateDTRow dr = new AdministrationEDSC.EmailTemplateDTDataTable().NewEmailTemplateDTRow();

            dr.EmailType = 1;//(int)SystemConstants.EmailTemplateType.Standard; << Email type may be needed in the future dev
            dr.ID = TemplateID;

            if (!string.IsNullOrEmpty(txtName.Text))
                dr.EmailName = txtName.Text;
            else dr.EmailName = "StandardPage";

            if (!string.IsNullOrEmpty(txtSubject.Text))
                dr.EmailSubject = txtSubject.Text;
            else
                dr.EmailSubject = "Standard Subject";

            dr.EmailIsHTML = true;
            if (!string.IsNullOrEmpty(txtCC.Text))
                dr.Emailcc = txtCC.Text;
            else
                dr.Emailcc = "";




            if (!string.IsNullOrEmpty(CKEditorControl1.Text))
                dr.EmailBody = CKEditorControl1.Text;
            else dr.EmailBody = "";
            return dr;

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Refresh();
            Mode = UIMode.Edit;
        }

        private void Refresh()
        {
            if (EmailType != (int)SystemConstants.ItemType.Standard)
            {

            }
            else if (EmailType == (int)SystemConstants.ItemType.Sidebar)
            {

                trDescription.Visible = false;
                txtCC.Text = "Sidebar Page";
            }

            if (TemplateID == 0)
            {
                Mode = UIMode.Create;
            }

            else if (TemplateID != 0)
            {
                SetPageInfo();
                if (EditMode)
                {
                    Mode = UIMode.Edit;
                    EditMode = false;
                }
            }


        }

        private void SetPageInfo()
        {
            AdministrationDAC dac = new AdministrationDAC();
            var dr = dac.RetrieveEmailTemplate(TemplateID);
            SetData(dr);
            Mode = UIMode.View;
        }



    }
}