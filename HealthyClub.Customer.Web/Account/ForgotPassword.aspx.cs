using BCUtility;
using HealthyClub.Customer.BF;
using HealthyClub.Customer.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }

        protected void btnGetPasswordToken_Click(object sender, EventArgs e)
        {
            var RegEx = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            var Match = Regex.Match(txtUsername.Text, RegEx, RegexOptions.IgnoreCase);
            if (Match.Success)
            {
                Guid userID = new CustomerDAC().RetrieveUserGUIDbyEmailAddress(txtUsername.Text);
                ProcessPasswordRecovery(userID);
            }
            else
            {
                //Check if username valid
                if (!WebSecurity.UserExists(txtUsername.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "This username is not registered.";
                }
                else
                {
                    CustomerDAC dac = new CustomerDAC();
                    Guid userID = new MembershipHelper().GetProviderUserKey(txtUsername.Text);
                    ProcessPasswordRecovery(userID);
                }
            }
        }

        private void ProcessPasswordRecovery(Guid userID)
        {
            CustomerDAC dac = new CustomerDAC();
            var user = new CustomerDAC().RetrieveUserProfiles(userID);
            if (user != null)
            {
                var MailConf = new CustomerDAC().RetrieveWebConfiguration();
                var emTemp = new CustomerDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                var token = WebSecurity.GeneratePasswordResetToken(user.Username);

                new CustomerBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                EmailSender.SendEmail(MailConf.SMTPAccount, user.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                lblEmail.Text = user.Email;
                CompletePasswordRecovery();
            }
            else
            {
                var userp = dac.RetrieveProviderProfiles(userID);
                if (userp != null)
                {
                    var MailConf = new CustomerDAC().RetrieveWebConfiguration();
                    var emTemp = new CustomerDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                    var token = WebSecurity.GeneratePasswordResetToken(userp.Username);

                    new CustomerBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                    EmailSender.SendEmail(MailConf.SMTPAccount, userp.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                    lblEmail.Text = userp.Email;
                    CompletePasswordRecovery();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = " This email address is not registered.";
                }
            }
        }
        private void CompletePasswordRecovery()
        {
            PasswordRecovery.Visible = false;
            PasswordRecoveryComplete.Visible = true;
        }
    }
}