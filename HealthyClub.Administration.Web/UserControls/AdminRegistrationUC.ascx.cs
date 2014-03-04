using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class AdminRegistrationUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            bool isPasswordWeak = true;
            CheckPasswordStrength(out isPasswordWeak);
            if (!isPasswordWeak)
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword2.Text) && !string.IsNullOrEmpty(txtUsername.Text))
                {
                    var token = WebSecurity.CreateUserAndAccount(txtUsername.Text, txtPassword.Text, null, false);
                    WebSecurity.Login(txtUsername.Text, txtPassword.Text);
                    if (!Roles.RoleExists(SystemConstants.AdministratorRole))
                        Roles.CreateRole(SystemConstants.AdministratorRole);

                    Roles.AddUserToRole(txtUsername.Text, SystemConstants.AdministratorRole);

                    Response.Redirect("~");
                }
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private void CheckPasswordStrength(out bool isPasswordWeak)
        {
            string Password = txtPassword.Text;
            if (Password.Length >= SystemConstants.MaxRequiredPasswordLength)
            {
                isPasswordWeak = true;
                lblError.Text = "Maximum password length exceeded, Maximum Password Length is " + SystemConstants.MaxRequiredPasswordLength + " Characters";
            }
            else if (Password.Length <= SystemConstants.MinRequiredPasswordLength)
            {
                isPasswordWeak = true;
                lblError.Text = "Password is too weak, Minimum Password Length is " + SystemConstants.MinRequiredPasswordLength + " Characters";
            }

            else { isPasswordWeak = false; }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~");
        }
    }
}