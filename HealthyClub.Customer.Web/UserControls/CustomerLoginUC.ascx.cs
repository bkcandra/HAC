using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace HealthyClub.Web.UserControls
{
    public partial class CustomerLoginUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            if (Membership.ValidateUser(txtUsername.Text, txtPassword.Text))
            {
                FormsAuthentication.SetAuthCookie(txtUsername.Text, true);
                Response.Redirect("~");
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registration/");
        }



    }
}