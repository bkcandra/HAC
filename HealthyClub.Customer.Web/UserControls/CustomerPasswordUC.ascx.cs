using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Web.UserControls
{
    public partial class CustomerPasswordUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ContinueButton_Click(object sender, EventArgs e)
        {

        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login");
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            CompleteConfirm.Visible = true;
            ChangeUserPassword.Visible = false;
        }
    }
}