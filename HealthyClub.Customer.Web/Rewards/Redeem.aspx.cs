using HealthyClub.Customer.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.Rewards
{
    public partial class Redeem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Redeem your rewards";
            if (!WebSecurity.IsAuthenticated)
            {

                
                    Response.Redirect("Performance.aspx");
                
                
            }

        }
    }
}