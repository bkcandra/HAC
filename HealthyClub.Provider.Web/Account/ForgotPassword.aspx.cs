using BCUtility;
using HealthyClub.Provider.BF;
using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Provider.Web.Account
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
                ProviderDAC dac = new ProviderDAC();
                Guid userID = dac.RetrieveUserGUIDbyEmailAddress(txtUsername.Text);

                var user = dac.RetrieveUserProfiles(userID);
                if (user != null)
                {
                    var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                    var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                    var token = WebSecurity.GeneratePasswordResetToken(user.Username);

                    new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                    EmailSender.SendEmail(MailConf.SMTPAccount, user.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                    
                    lblEmail.Text = user.Email;
                    CompletePasswordRecovery();
                }
                else
                {
                    var userp = dac.RetrieveProviderProfiles(userID);
                    if (userp != null)
                    {
                        var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                        var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                        var token = WebSecurity.GeneratePasswordResetToken(userp.Username);

                        new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                        EmailSender.SendEmail(MailConf.SMTPAccount, userp.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                        
                        lblEmail.Text = userp.Email;
                        CompletePasswordRecovery();
                    }

                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Could not find matched email address";
                    }
                }
            }
            else
            {
                //Check if username valid
                if (!WebSecurity.UserExists(txtUsername.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "A password can only be sent to registered users. This username is not registered.";
                }
                else
                {
                    ProviderDAC dac = new ProviderDAC();
                    Guid userID = dac.RetrieveUserGUID(txtUsername.Text);

                    var user = dac.RetrieveUserProfiles(userID);
                    if (user != null)
                    {
                        var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                        var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                        var token = WebSecurity.GeneratePasswordResetToken(user.Username);

                        new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                        EmailSender.SendEmail(MailConf.SMTPAccount, user.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                        lblEmail.Text = user.Email;
                        CompletePasswordRecovery();
                    }
                    else
                    {
                        var userp = dac.RetrieveProviderProfiles(userID);
                        if (userp != null)
                        {
                            var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                            var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ForgotPassword);
                            var token = WebSecurity.GeneratePasswordResetToken(userp.Username);

                            new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ForgotPassword, 0);
                            EmailSender.SendEmail(MailConf.SMTPAccount, userp.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                            lblEmail.Text = userp.Email;
                            CompletePasswordRecovery();
                        }

                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "Could not process request please try again later.";
                        }
                    }

                    
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