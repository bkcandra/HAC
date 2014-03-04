using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using System.Web.Security;

namespace HealthyClub.Administration.Web.State
{
    public partial class StateSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[SystemConstants.StateID] != null)
                {
                    int stateID = Convert.ToInt32(Request.QueryString[SystemConstants.StateID]);
                    var dac = new AdministrationDAC();
                    AdministrationEDSC.StateDTRow dr = null;

                    if (stateID != 0)
                    {
                        dr = dac.RetrieveState(stateID);
                        StateSetupUC1.Mode = SystemConstants.FormMode.Edit;
                        StateSetupUC1.SetData(dr);
                    }
                    else
                    {
                        dr = new AdministrationEDSC.StateDTDataTable().NewStateDTRow();
                        StateSetupUC1.Mode = SystemConstants.FormMode.New;
                    }
                }
                CheckSignIn();
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