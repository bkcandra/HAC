using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class AdminLoginUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (Roles.IsUserInRole(LoginUser.UserName, SystemConstants.AdministratorRole))
                e.Authenticated = true;
            else
            {
                e.Authenticated = false;
                LoginUser.FailureText = "Incorrect Username or Password for Administrator Account";
            }
        }
    }
}