using HealthyClub.Customer.BF;
using HealthyClub.Utility;
using Segmentio;
using Segmentio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                string fName = "Anonymous";
                string lName = "User";
                string rName = "Anonymous";
                string email = "AnonymousUser";
                string userID = "AnonymousUser";

                if (WebSecurity.IsAuthenticated)
                {
                    if (Session[SystemConstants.ses_FName] != null)
                        fName = (String)(Session[SystemConstants.ses_FName]);
                    if (Session[SystemConstants.ses_LName] != null)
                        lName = (String)(Session[SystemConstants.ses_LName]);
                    if (Session[SystemConstants.ses_Role] != null)
                        rName = (String)(Session[SystemConstants.ses_Role]);
                    if (Session[SystemConstants.ses_Email] != null)
                        email = (String)(Session[SystemConstants.ses_Email]);
                    if (Session[SystemConstants.userID] != null)
                        userID = (String)(Session[SystemConstants.ses_UserID].ToString());
                }

                Analytics.Client.Identify(userID, new Traits() {
                    { "name",fName +" "+ lName },
                    { "email", email },
                    { "role", rName },
                    });

            }
        }
    }
}