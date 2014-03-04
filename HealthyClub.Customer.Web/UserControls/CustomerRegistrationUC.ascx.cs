using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Customer.EDS;
using HealthyClub.Customer.DA;
using System.Web.Security;
using HealthyClub.Utility;
using WebMatrix.WebData;
using HealthyClub.Customer.BF;
using BCUtility;
using System.Text.RegularExpressions;

namespace HealthyClub.Web.UserControls
{
    public partial class CustomerRegistrationUC1 : System.Web.UI.UserControl
    {
        bool EnableRecaptcha
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnEnableRecap.Value))
                    return Convert.ToBoolean(hdnEnableRecap.Value);
                else return false;
            }
            set
            {
                hdnEnableRecap.Value = value.ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var web = new CustomerDAC().RetrieveWebConfiguration();
            EnableRecaptcha = web.EnableCaptcha;
            if (EnableRecaptcha)
            {
                RecaptchaControl1.PublicKey = System.Configuration.ConfigurationManager.AppSettings[SystemConstants.reCaptchaPublicKey].ToString();
                RecaptchaControl1.PrivateKey = System.Configuration.ConfigurationManager.AppSettings[SystemConstants.reCaptchaPrivateKey].ToString();
            }
            else
            {
                trCaptcha.Visible = EnableRecaptcha;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (WebSecurity.IsAuthenticated)
                    Response.Redirect("~");
                setRegistrationDDL();
            }
        }

        private void setRegistrationDDL()
        {
            CustomerEDSC.v_SuburbExplorerDTDataTable dt = new CustomerDAC().RetrieveSuburbs();
            CustomerEDSC.StateDTDataTable dtState = new CustomerDAC().RetrieveStates();

            ddlState.Items.Clear();

            ListItem lis = new ListItem("State", "0");
            ddlState.Items.Add(lis);
            foreach (var dr in dtState)
            {
                string name = "";

                if (dr.ID != 0)
                    name = dr.StateName;


                lis = new ListItem(name, dr.ID.ToString());
                ddlState.Items.Add(lis);
            }
        }

        protected void CreatedUser(Guid userID)
        {
            CustomerBFC bfc = new CustomerBFC();
            CustomerDAC dac = new CustomerDAC();

            CustomerEDSC.UserProfilesDTRow dr = GetRegistrationData();


            dr.UserID = userID;
            dac.InsertNewUserProfiles(dr);
            CustomerEDSC.UserRewardDTRow drr = GetRewardData();
            drr.UserID = userID;
            dac.InsertNewRewardUser(drr);

            var drRef = new CustomerEDSC.UserReferenceDTDataTable().NewUserReferenceDTRow();
            drRef.UserID = userID;
            drRef.ReferenceID = bfc.GenerateUserRefID(dr.LastName, dr.FirstName);
            dac.insertNewUserReference(drRef);
        }
        private CustomerEDSC.UserRewardDTRow GetRewardData()
        {

            CustomerEDSC.UserRewardDTRow drr = new CustomerEDSC.UserRewardDTDataTable().NewUserRewardDTRow();
            drr.RewardPoint = 0;
            drr.RedeemedtPoint = 0;
            drr.BonusPoint = 0;

            drr.Status = 1;
            return drr;
        }

        private CustomerEDSC.UserProfilesDTRow GetRegistrationData()
        {
            //Reference wizard's controls
            //CreateUserWizardStep ProviderProfiles = CreateNewMember.FindControl("ProviderProfiles") as CreateUserWizardStep;

            CustomerEDSC.UserProfilesDTRow dr = new CustomerEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();

            dr.Username = Username.Text;
            dr.Title = Convert.ToInt32(ddlTitle.SelectedValue);
            dr.FirstName = txtFirstName.Text;
            dr.MiddleName = "";
            if (radMale.Checked)
                dr.Gender = 1;
            else
                dr.Gender = 2;
            DateTime DOB;
            if (DateTime.TryParse(txtDOB.Text, out DOB))
            {
                dr.DOB = DOB;
            }
            dr.LastName = txtLastName.Text;
            dr.Email = Email.Text.ToLower();
            dr.Address = txtAddress1.Text;
            dr.Suburb = txtSuburb.Text;
            dr.PhoneNumber = txtContactNumber.Text;
            dr.MobileNumber = txtMobileNumber.Text;
            dr.Aggreement = true;
            dr.PostCode = Convert.ToInt32(txtPostCode.Text);
            dr.StateID = Convert.ToInt32(ddlState.SelectedValue);
            dr.AccountDeletion = false;
            if (radiobyEMail.Checked)
            {
                dr.PreferredContact = 1;
            }
            else if (radiobyMail.Checked)
            {
                dr.PreferredContact = 3;
            }
            else if (radiobyPhone.Checked)
            {
                dr.PreferredContact = 2;
            }
            dr.CreatedBy = dr.ModifiedBy = Username.Text;
            dr.CreatedDatetime = dr.ModifiedDatetime = DateTime.Now;
            return dr;
        }

        protected void validateUser(out bool error)
        {
            error = false;
            String ErrorText = "";
            var reg = @"^.*(?=.{6,18})(?=.*\d)(?=.*[a-zA-Z]).*$";
            var match = Regex.Match(Password.Text, reg, RegexOptions.IgnoreCase);

            if (EnableRecaptcha)
            {
                RecaptchaControl1.Validate();
                if (!RecaptchaControl1.IsValid)
                {
                    if (!string.IsNullOrEmpty(lblError.Text))
                        ErrorText += "</br>";
                    ErrorText += "The security code you entered is not correct.";
                    error = true;
                    lblError.Visible = true;
                }
            }


            if (!match.Success)
            {
                if (!string.IsNullOrEmpty(ErrorText))
                    ErrorText += "</br>";
                ErrorText += "Password requirements are not met";
                error = true;
            }
            if (ddlTitle.SelectedValue == "0")
            {
                if (!string.IsNullOrEmpty(lblError.Text))
                    lblError.Text += "</br>";
                lblError.Text += "Title is required.";
                error = true;
                lblError.Visible = true;
            }
            DateTime DOB;
            if (!DateTime.TryParse(txtDOB.Text, out DOB))
            {
                if (!string.IsNullOrEmpty(lblError.Text))
                    lblError.Text += "</br>";
                error = true;
                lblError.Visible = true;
                lblError.Text += "Invalid date of birth.";
            }
            if (ddlState.SelectedValue == "0")
            {
                if (!string.IsNullOrEmpty(lblError.Text))
                    lblError.Text += "</br>";
                lblError.Text += "Address is required.";
                lblError.Visible = true;
                error = true;
            }
            if (WebSecurity.UserExists(Username.Text))
            {
                if (!string.IsNullOrEmpty(ErrorText))
                    ErrorText += "</br>";
                ErrorText += SystemConstants.ErrorUsernameTaken;
                error = true;
            }
            if (new CustomerDAC().isEmailAddressExist(Email.Text.ToLower()))
            {
                if (!string.IsNullOrEmpty(lblError.Text))
                    lblError.Text += "</br>";
                lblError.Text += SystemConstants.ErrorEmailAddressTaken;
                error = true;
                lblError.Visible = true;
            }

            if (string.IsNullOrEmpty(txtSuburb.Text) || txtSuburb.Text == "suburb" || txtAddress1.Text == "Address Line 1" || ddlState.SelectedValue == "0")
            {
                if (!string.IsNullOrEmpty(ErrorText))
                    ErrorText += "</br>";
                ErrorText += "Address is required";
                error = true;
            }


            var emailReg = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            var emailmatch = Regex.Match(Email.Text, emailReg, RegexOptions.IgnoreCase);

            if (!emailmatch.Success)
            {
                if (!string.IsNullOrEmpty(lblError.Text))
                    lblError.Text += "</br>";
                lblError.Text += SystemConstants.ErrorInvalidEmail;
                error = true;
                lblError.Visible = true;
            }
            lblError.Text = ErrorText;
            divError.Visible = lblError.Visible = error;
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Activities/");
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }

        protected void StepNextButton_Click(object sender, EventArgs e)
        {
            bool error = false;
            validateUser(out error);
            if (!error)
            {
                var token = WebSecurity.CreateUserAndAccount(Username.Text, Password.Text, null, true);
                // User cannot login as they need to confirm account first..
                if (!Roles.RoleExists(SystemConstants.CustomerRole))
                    Roles.CreateRole(SystemConstants.CustomerRole);

                Roles.AddUserToRole(Username.Text, SystemConstants.CustomerRole);
                Guid userID = new CustomerDAC().RetrieveUserGUID(Username.Text);

                if (userID != Guid.Empty)
                    CreatedUser(userID);

                SendConfirmationEmail(userID, token);

                Registration.Visible = false;
                completeRegistration.Visible = true;

            }
        }

        private void SendConfirmationEmail(Guid userID, string token)
        {
            var MailConf = new CustomerDAC().RetrieveWebConfiguration();
            var emTemp = new CustomerDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ConfirmationEmail);

            new CustomerBFC().ParseEmail(emTemp, userID, token, (int)SystemConstants.EmailTemplateType.ConfirmationEmail, 0);

            EmailSender.SendEmail(MailConf.SMTPAccount, Email.Text, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
        }

        protected void Username_TextChanged(object sender, EventArgs e)
        {
            lblUsernameError.Visible = WebSecurity.UserExists(Username.Text);
        }

        protected void Email_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                ReqEmailAddress.Visible = true;
                ReqEmailAddress.Validate();
            }
            else
            {
                ReqEmailAddress.Visible = false;
                if (Email.Text.Length >= 5)
                    lblEmailTaken.Visible = new CustomerDAC().isEmailAddressExist(Email.Text.ToLower());


                var emailReg = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                var match = Regex.Match(Email.Text, emailReg, RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    lblInvalidEmail.Visible = true;
                }
                else
                    lblInvalidEmail.Visible = false;
            }
        }


    }
}