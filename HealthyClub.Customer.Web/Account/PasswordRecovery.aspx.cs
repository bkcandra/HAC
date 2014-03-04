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
    public partial class PasswordRecovery : System.Web.UI.Page
    {

        public string Token
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnToken.Value))
                    return hdnToken.Value;
                else return "";
            }
            set
            {
                hdnToken.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString[SystemConstants.token] != null)
                {
                    int userID = WebSecurity.GetUserIdFromPasswordResetToken(Request.QueryString[SystemConstants.token]);
                    if (userID != -1 && userID != 0)
                    {
                        Token = Request.QueryString[SystemConstants.token];
                        invalidToken.Visible = false;
                        Recovery.Visible = true;
                        CompleteRecovery.Visible = false;
                    }
                    else
                        lblError.Text = "Invalid recovery code, You can resend your password recovery code by using the forgot password link on the login page.";
                }
                else
                {
                    Response.Redirect("~");
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

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (ValidatePassword())
            {
                var success = WebSecurity.ResetPassword(Token, txtVerifyNewPassword.Text);
                if (success)
                {
                    MembershipHelper.ResetPasswordToken(WebSecurity.GetUserIdFromPasswordResetToken(Token));

                    invalidToken.Visible = false;
                    Recovery.Visible = false;
                    CompleteRecovery.Visible = true;
                }

                else
                {
                    invalidToken.Visible = true;
                    Recovery.Visible = CompleteRecovery.Visible = false;
                }
            }
            else
            {
                invalidToken.Visible = true;
                lblError.Text = "Password Requirements not met";
                lblError.Visible = true;
            }
        }

        private bool ValidatePassword()
        {
            var reg = @"^.*(?=.{6,18})(?=.*\d)(?=.*[a-zA-Z]).*$";
            var match = Regex.Match(txtVerifyNewPassword.Text, reg, RegexOptions.IgnoreCase);

            if (!match.Success)
                return false;
            else
                return true;
        }


    }
}