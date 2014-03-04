using System;
using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using HealthyClub.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.Rewards
{
    public partial class UserAsses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Rewards Dashboard";
            if (WebSecurity.IsAuthenticated)
            {
                String loginName;
                if (Session != null)
                {
                    string fName = (String)(Session[SystemConstants.ses_FName]);
                    
                    loginName = fName;
                    HeadLoginName.Text = loginName;


                }
                else
                    WebSecurity.Logout();
            }
        }
    }
}