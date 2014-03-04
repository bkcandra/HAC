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
    public partial class ConfirmAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[SystemConstants.token] != null)
                {
                    bool Confirmed = WebSecurity.ConfirmAccount(Request.QueryString[SystemConstants.token]);
                    if (Confirmed)
                    {

                        Confirm.Visible = false;
                        CompleteConfirm.Visible = true;
                    }
                    else
                        lblError.Text = "Invalid confirmation code, Please enter our username to resend confirmation email";
                }
            }
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }

        protected void btnSendConfirmation_Click(object sender, EventArgs e)
        {
            var RegEx = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            var Match = Regex.Match(txtUsername.Text, RegEx, RegexOptions.IgnoreCase);
            if (Match.Success)
            {
                Guid userID = new ProviderDAC().RetrieveUserGUIDbyEmailAddress(txtUsername.Text);
                var user = new ProviderDAC().RetrieveUserProfiles(userID);
                if (user != null)
                {
                    var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                    var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail);
                    var token = new MembershipHelper().GetConfirmationCode(userID);

                    new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail, 0);

                    EmailSender.SendEmail(MailConf.SMTPAccount, user.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                    lblError.Text = "Confirmation email resent. Please check your email";
                }
                else
                {
                    var userP = new ProviderDAC().RetrieveProviderProfiles(userID);
                    if (userP != null)
                    {
                        var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                        var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail);
                        var token = new MembershipHelper().GetConfirmationCode(userID);

                        new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail, 0);

                        EmailSender.SendEmail(MailConf.SMTPAccount, userP.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                        lblError.Text = "Confirmation email resent. Please check your email";
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "User information not found.";
                    }
                }
            }
            else
            {
                int ID = WebSecurity.GetUserId(txtUsername.Text);
                lblError.Visible = false;

                if (ID == 0)
                    lblError.Text = "Invalid Username.";
                else
                {
                    Guid userID = new ProviderDAC().RetrieveUserGUID(ID);
                    var user = new ProviderDAC().RetrieveUserProfiles(userID);
                    if (user != null)
                    {
                        var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                        var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail);
                        var token = new MembershipHelper().GetConfirmationCode(userID);

                        new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail, 0);

                        EmailSender.SendEmail(MailConf.SMTPAccount, user.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                        lblError.Text = "Confirmation email resent. Please check your email";
                    }
                    else
                    {
                        var userP = new ProviderDAC().RetrieveProviderProfiles(userID);
                        if (userP != null)
                        {
                            var MailConf = new ProviderDAC().RetrieveWebConfiguration();
                            var emTemp = new ProviderDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail);
                            var token = new MembershipHelper().GetConfirmationCode(userID);

                            new ProviderBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail, 0);

                            EmailSender.SendEmail(MailConf.SMTPAccount, userP.Email, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
                            lblError.Text = "Confirmation email resent. Please check your email";
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.Text = "User information not found.";
                        }                        
                    }
                }
            }
        }

        protected void lnkEnterCode_Click(object sender, EventArgs e)
        {
            divEnterCode.Visible = true;
        }

        protected void btnConfirmCode_Click(object sender, EventArgs e)
        {
            bool Confirmed = WebSecurity.ConfirmAccount(txtConfirmationCode.Text);
            if (Confirmed)
            {
                Confirm.Visible = false;
                CompleteConfirm.Visible = true;
            }
            else
                lblError.Text = "Invalid confirmation code, Please enter our username to resend confirmation email";
        }


    }
}