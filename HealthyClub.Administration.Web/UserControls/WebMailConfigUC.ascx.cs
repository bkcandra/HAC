using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using System.Web.Security;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using BCUtility;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class WebMailConfigUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Refresh();
        }

        public UIMode Mode
        {
            set
            {
                switch (value)
                {
                    case WebMailConfigUC.UIMode.Edit:
                        #region Edit
                        //Edit Mode
                        txtPassword.Visible = true;
                        txtSender.Visible = true;
                        txtSMTPAddress.Visible = true;
                        txtSMTPPort.Visible = true;
                        txtUsername.Visible = true;

                        lblPassword.Visible = false;
                        lblSender.Visible = false;
                        lblSMTPAddress.Visible = false;
                        lblSMTPPort.Visible = false;
                        lblUsername.Visible = false;

                        chkSsl.Enabled = true;
                        chkIIS.Enabled = true;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        lnkTest.Visible = false;
                        #endregion
                        break;

                    case WebMailConfigUC.UIMode.View:
                        //View mode
                        #region View
                        txtPassword.Visible = false;
                        txtSender.Visible = false;
                        txtSMTPAddress.Visible = false;
                        txtSMTPPort.Visible = false;
                        txtUsername.Visible = false;

                        lblPassword.Visible = true;
                        lblSender.Visible = true;
                        lblSMTPAddress.Visible = true;
                        lblSMTPPort.Visible = true;
                        lblUsername.Visible = true;

                        chkSsl.Enabled = false;
                        chkIIS.Enabled = false;

                        lnkCancel.Visible = false;
                        lnkEdit.Visible = true;
                        lnkSave.Visible = false;
                        lnkTest.Visible = true;
                        #endregion
                        break;
                    case WebMailConfigUC.UIMode.Create:
                        //Create mode
                        #region Create
                        txtPassword.Visible = true;
                        txtSender.Visible = true;
                        txtSMTPAddress.Visible = true;
                        txtSMTPPort.Visible = true;
                        txtUsername.Visible = true;

                        lblSender.Visible = false;
                        lblSMTPAddress.Visible = false;
                        lblSMTPPort.Visible = false;
                        lblUsername.Visible = false;
                        chkSsl.Enabled = true;
                        chkIIS.Enabled = true;
                        lblPassword.Visible = false;

                        lnkCancel.Visible = true;
                        lnkEdit.Visible = false;
                        lnkSave.Visible = true;
                        lnkTest.Visible = false;
                        #endregion
                        break;
                }

                hdnMode.Value = ((int)value).ToString();
            }
            get
            {
                if (!string.IsNullOrEmpty(hdnMode.Value))
                    return (UIMode)Convert.ToInt32(hdnMode.Value);
                else
                    return UIMode.View;
            }
        }

        private void Refresh()
        {
            Mode = UIMode.View;
            SetEmailer();

        }

        private void SetEmailer()
        {
            var dr = new AdministrationDAC().RetrieveEmailer();
            if (dr != null)
            {
                SetEmailerInformation(dr);
            }
            else
            {
                Mode = UIMode.Create;
            }

        }

        private void SetEmailerInformation(EDS.AdministrationEDSC.WebConfigurationDTRow dr)
        {
            txtSender.Text = dr.SMTPAccount;
            txtUsername.Text = dr.SMTPUserName;

            for (int i = 1; i <= dr.SMTPPassword.Length; i++)
            {
                lblPassword.Text += "*";
            }

            txtPassword.Text = dr.SMTPPassword;
            txtSMTPAddress.Text = dr.SMTPHost;
            txtSMTPPort.Text = dr.SMTPPort.ToString();

            lblSender.Text = dr.SMTPAccount;
            lblUsername.Text = dr.SMTPUserName;

            lblSMTPAddress.Text = dr.SMTPHost;
            lblSMTPPort.Text = dr.SMTPPort.ToString();
            
            chkIIS.Checked = dr.SMTPIIS;
            chkSsl.Checked = dr.SMTPSSL;
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            AdministrationDAC dac = new AdministrationDAC();
            if (Mode == UIMode.Create)
            {
                var dr = GetData();
                dac.EditEmailer(dr);
                Refresh();

            }
            else if (Mode == UIMode.Edit)
            {
                var dr = GetData();
                dac.EditEmailer(dr);
                Refresh();
            }
            lblNotif.Visible = true;
            lblNotif.Text = "Mailer Setting Saved";
        }

        private AdministrationEDSC.WebConfigurationDTRow GetData()
        {
            AdministrationEDSC.WebConfigurationDTRow dr = new AdministrationEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();
            dr.SMTPAccount = txtSender.Text;
            dr.SMTPPassword = txtPassword.Text;
            dr.SMTPUserName = txtUsername.Text;
            dr.SMTPHost = txtSMTPAddress.Text;
            dr.SMTPPort = Convert.ToInt32(txtSMTPPort.Text);
            dr.SMTPSSL = chkSsl.Checked;
            dr.SMTPIIS = chkIIS.Checked;
            return dr;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Mode = UIMode.View;
            Refresh();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Mode = UIMode.Edit;
        }

        public enum UIMode { View, Edit, Create }

        protected void lnkTest_Click(object sender, EventArgs e)
        {
            lblNotif.Visible = true;
            lblNotif.Text = "Testing Sending Email...";

            var dr = new AdministrationDAC().RetrieveEmailer();
            try
            {
                string subject = "Healthy Club Email Notification System Tester";
                string emailBody = "Please Do not Reply This Email. <br> This email was sent from Club Management Notification System to test the setting of the email server. If you receive this email, it means that email setting in Administration module is correct.";

                EmailSender.SendTestEmail(txtSender.Text, dr.SMTPAccount, subject, emailBody, dr.SMTPHost, dr.SMTPPort, dr.SMTPUserName, dr.SMTPPassword, dr.SMTPSSL,dr.SMTPIIS);
                lblError.Visible = false;
                lblNotif.Visible = true;
                lblNotif.Text = "Success sending email to " + dr.SMTPAccount + ".";
            }

            catch (Exception ex)
            {
                lblNotif.Text = "Failed Sending Email...";
                lblError.Visible = true;
                lblError.Text = "ERROR: " + ex.Message.ToString();
            }

        }

    }
}