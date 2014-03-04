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

namespace HealthyClub.Administration.Web.Suburb
{
    public partial class SuburbSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[SystemConstants.SuburbID] != null)
                {
                    int suburbID = Convert.ToInt32(Request.QueryString[SystemConstants.SuburbID]);
                    var dac = new AdministrationDAC();
                    AdministrationEDSC.v_SuburbExplorerDTRow dr = null;

                    if (suburbID != 0)
                    {
                        dr = dac.RetrieveSuburb(suburbID);
                        SuburbSetupUC1.Mode = SystemConstants.FormMode.Edit;
                        SuburbSetupUC1.SetData(dr);
                    }
                    else
                    {
                        dr = new AdministrationEDSC.v_SuburbExplorerDTDataTable().Newv_SuburbExplorerDTRow();
                        SuburbSetupUC1.Mode = SystemConstants.FormMode.New;
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