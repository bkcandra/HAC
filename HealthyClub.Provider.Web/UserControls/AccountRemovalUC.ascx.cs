using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Provider.Web.UserControls
{
    public partial class AccountRemovalUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthUser())
                {
                    btnDelete.Enabled = txtPassword.Enabled = false;
                    CheckUserStatus();
                }
            }
        }

        private void CheckUserStatus()
        {
            ProviderDAC dac = new ProviderDAC();
            Guid userID = dac.RetrieveUserGUID(WebSecurity.CurrentUserName);
            var drP = dac.RetrieveProviderProfiles(userID);

            if (drP != null)
            {
                if (drP.AccountDeletion)
                {
                    accountCancel.Visible = false;
                    CompleteConfirm.Visible = true;
                }
            }
            else
            {
                Response.Redirect(SystemConstants.CustomerUrl + "Account");
            }
        }

        private bool AuthUser()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
                return false;
            }
            else return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account");
        }

        protected void chkRemoveAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemoveAccount.Checked)
                btnDelete.Enabled = txtPassword.Enabled = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string usr = WebSecurity.CurrentUserName;
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                if (Membership.ValidateUser(usr, txtPassword.Text))
                {
                    ProviderDAC dac = new ProviderDAC();
                    Guid userID = dac.RetrieveUserGUID(usr);
                    string err = "";
                    if (dac.DeactivateUser(usr, userID, out err))
                    {
                        accountCancel.Visible = false;
                        CompleteConfirm.Visible = true;
                    }
                    else
                    {
                        lblError.Visible = true;
                        if (err == "Unable to find user")
                        {
                            lblError.Text = "Unknown error, please retry login";
                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Invalid password";
                }
            }
        }
    }
}