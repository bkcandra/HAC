using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Customer.DA;

namespace HealthyClub.Customer.Web.Rewards
{
    public partial class Default : System.Web.UI.Page
    {
        public string LinkHome
        {
            get;
            set;

        }
        public string LinkHow
        {
            get;
            set;
        }
        public string LinkTerms
        {
            get;
            set;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Page.RouteData.Values[SystemConstants.PageName] != null)
                {
                    string PageName = Page.RouteData.Values[SystemConstants.PageName].ToString();
                    if (PageName == "RewardsHome")
                    {
                        LinkHome = "CurrentItem";
                    }
                    else if (PageName == "How It Works")
                    {
                        LinkHow = "CurrentItem";
                    }
                    else if (PageName == "Rewards Terms and Conditions")
                    {
                        LinkTerms = "CurrentItem";
                    }                    
                }

            }
        }

    }
}