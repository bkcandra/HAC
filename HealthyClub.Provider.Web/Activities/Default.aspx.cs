using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;
using HealthyClub.Provider.DA;

namespace HealthyClub.Providers.Web.Activities
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckSignIn();
            }
        }

        private void CheckSignIn()
        {
            if (WebSecurity.IsAuthenticated)
            {
                int userID = WebSecurity.CurrentUserId;
                if (userID != -1)
                {
                    ActivityManagementUC1.ProviderID = new MembershipHelper().GetProviderUserKey(userID);
                }
            }            
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}