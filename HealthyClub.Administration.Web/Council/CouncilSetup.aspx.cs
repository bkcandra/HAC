using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.Council
{
    public partial class CouncilSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckSignIn();
                if (Request.QueryString[SystemConstants.CouncilID] != null)
                {
                    int councilID = Convert.ToInt32(Request.QueryString[SystemConstants.CouncilID]);
                    var dac = new AdministrationDAC();
                    AdministrationEDSC.CouncilDTRow dr = null;

                    if (councilID != 0)
                    {
                        dr = dac.RetrieveCouncil(councilID);
                        CouncilSetupUC.Mode = SystemConstants.FormMode.Edit;
                        CouncilSetupUC.SetDDL();
                        CouncilSetupUC.SetData(dr);
                    }
                    else
                    {                        
                        CouncilSetupUC.Mode = SystemConstants.FormMode.New;
                    }
                }
               
            }

        }

        private void CheckSignIn()
        {
            if (Membership.GetUser() == null)
            {
                Response.Redirect("~/Account/login.aspx");
            }
        }
    }
}