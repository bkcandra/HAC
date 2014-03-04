using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System.Web.Security;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class MailTemplateSettingUC : System.Web.UI.UserControl
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
                    case MailTemplateSettingUC.UIMode.Edit:
                        //Edit Mode
                        #region Edit
                        ddEmailTemplate1.Enabled = true;
                        ddEmailTemplate2.Enabled = true;
                        ddEmailTemplate3.Enabled = true;
                        ddEmailTemplate4.Enabled = true;
                        ddEmailTemplate5.Enabled = true;
                        ddEmailTemplate6.Enabled = true;
                        ddEmailTemplate7.Enabled = true;
                        ddEmailTemplate8.Enabled = true;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        #endregion
                        break;

                    case MailTemplateSettingUC.UIMode.View:
                        //View mode
                        #region View
                        ddEmailTemplate1.Enabled = false;
                        ddEmailTemplate2.Enabled = false;
                        ddEmailTemplate3.Enabled = false;
                        ddEmailTemplate4.Enabled = false;
                        ddEmailTemplate5.Enabled = false;
                        ddEmailTemplate6.Enabled = false;
                        ddEmailTemplate7.Enabled = false;
                        ddEmailTemplate8.Enabled = false;

                        lnkCancel.Visible = false;
                        lnkEdit.Visible = true;
                        lnkSave.Visible = false;
                        #endregion
                        break;
                    case MailTemplateSettingUC.UIMode.Create:
                        //Create mode
                        #region Create
                        ddEmailTemplate1.Enabled = true;
                        ddEmailTemplate2.Enabled = true;
                        ddEmailTemplate3.Enabled = true;
                        ddEmailTemplate4.Enabled = true;
                        ddEmailTemplate5.Enabled = true;
                        ddEmailTemplate6.Enabled = true;
                        ddEmailTemplate7.Enabled = true;
                        ddEmailTemplate8.Enabled = true;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        #endregion
                        break;
                }

                hdnMode.Value = ((int)value).ToString();
            }
        }

        public enum UIMode { Create, Edit, View }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            Mode = UIMode.View;
            SetDDLEmail();
            SetEmailSetting();
        }

        private void SetDDLEmail()
        {
            AdministrationDAC dac = new AdministrationDAC();
            var dt1 = dac.RetrieveEmailTemplates();

            foreach (var dr in dt1)
            {
                ListItem li1 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate1.Items.Add(li1);

                ListItem li2 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate2.Items.Add(li2);

                ListItem li3 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate3.Items.Add(li3);

                ListItem li4 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate4.Items.Add(li4);

                ListItem li5 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate5.Items.Add(li5);

                ListItem li6 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate6.Items.Add(li6);

                ListItem li7 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate7.Items.Add(li7);

                ListItem li8 = new ListItem(dr.EmailName, dr.ID.ToString());
                ddEmailTemplate8.Items.Add(li8);
            }

        }

        private void SetEmailSetting()
        {
            var dt = new AdministrationDAC().RetrieveEmailSettings();
            if (dt != null)
                foreach (var dr in dt)
                {
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.WelcomeEmail)
                        ddEmailTemplate1.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                        ddEmailTemplate2.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.ProviderWelcomeEmail)
                        ddEmailTemplate3.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.ForgotPassword)
                        ddEmailTemplate4.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.Expired2week)
                        ddEmailTemplate5.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.Expired1week)
                        ddEmailTemplate6.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.Expired)
                        ddEmailTemplate7.SelectedValue = dr.EmailTemplateID.ToString();
                    if (dr.EmailType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                        ddEmailTemplate8.SelectedValue = dr.EmailTemplateID.ToString();
                }
        }

        private AdministrationEDSC.EmailSettingDTDataTable GetEmailSetting()
        {
            var dt = new AdministrationEDSC.EmailSettingDTDataTable();

            var dr1 = dt.NewEmailSettingDTRow();
            dr1.Name = "Welcome Email";
            dr1.Description = lblSc1.Text;
            dr1.EmailTemplateID = Convert.ToInt32(ddEmailTemplate1.SelectedValue);
            dr1.EmailType = (int)SystemConstants.EmailTemplateType.WelcomeEmail;
            dr1.ModifiedDatetime = DateTime.Now;
            dr1.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr1);

            var dr2 = dt.NewEmailSettingDTRow();
            dr2.Name = "Confirmation Email";
            dr2.Description = lblSc2.Text;
            dr2.EmailTemplateID = Convert.ToInt32(ddEmailTemplate2.SelectedValue);
            dr2.EmailType = (int)SystemConstants.EmailTemplateType.ConfirmationEmail;
            dr2.ModifiedDatetime = DateTime.Now;
            dr2.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr2);

            var dr3 = dt.NewEmailSettingDTRow();
            dr3.Name = "Provider Welcome Email";
            dr3.Description = lblSc3.Text;
            dr3.EmailTemplateID = Convert.ToInt32(ddEmailTemplate3.SelectedValue);
            dr3.EmailType = (int)SystemConstants.EmailTemplateType.ProviderWelcomeEmail;
            dr3.ModifiedDatetime = DateTime.Now;
            dr3.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr3);

            var dr4 = dt.NewEmailSettingDTRow();
            dr4.Name = "Forgot Password Email";
            dr4.Description = lblSc4.Text;
            dr4.EmailTemplateID = Convert.ToInt32(ddEmailTemplate4.SelectedValue);
            dr4.EmailType = (int)SystemConstants.EmailTemplateType.ForgotPassword;
            dr4.ModifiedDatetime = DateTime.Now;
            dr4.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr4);

            var dr5 = dt.NewEmailSettingDTRow();
            dr5.Name = "Expire 2 week";
            dr5.Description = lblSc5.Text;
            dr5.EmailTemplateID = Convert.ToInt32(ddEmailTemplate5.SelectedValue);
            dr5.EmailType = (int)SystemConstants.EmailTemplateType.Expired2week;
            dr5.ModifiedDatetime = DateTime.Now;
            dr5.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr5);

            var dr6 = dt.NewEmailSettingDTRow();
            dr6.Name = "Expire 1 week";
            dr6.Description = lblSc6.Text;
            dr6.EmailTemplateID = Convert.ToInt32(ddEmailTemplate6.SelectedValue);
            dr6.EmailType = (int)SystemConstants.EmailTemplateType.Expired1week;
            dr6.ModifiedDatetime = DateTime.Now;
            dr6.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr6);

            var dr7 = dt.NewEmailSettingDTRow();
            dr7.Name = "Expired Email";
            dr7.Description = lblSc7.Text;
            dr7.EmailTemplateID = Convert.ToInt32(ddEmailTemplate7.SelectedValue);
            dr7.EmailType = (int)SystemConstants.EmailTemplateType.Expired;
            dr7.ModifiedDatetime = DateTime.Now;
            dr7.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr7);


            var dr8 = dt.NewEmailSettingDTRow();
            dr8.Name = "Provider Confirmation Email";
            dr8.Description = lblSc8.Text;
            dr8.EmailTemplateID = Convert.ToInt32(ddEmailTemplate8.SelectedValue);
            dr8.EmailType = (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail;
            dr8.ModifiedDatetime = DateTime.Now;
            dr8.ModifiedBy = Membership.GetUser().UserName;
            dt.AddEmailSettingDTRow(dr8);
            return dt;
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var dt = GetEmailSetting();

            new AdministrationDAC().SaveEmailSettings(dt);
            lblStatus.Text = "Saved - " + DateTime.Now;
            lblStatus.Visible = true;
            Mode = UIMode.View;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Mode = UIMode.Edit;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            SetEmailSetting();
            Mode = UIMode.View;
        }
    }
}