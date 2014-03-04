using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using System.Web.Security;

namespace HealthyClub.Administration.Web.Category
{
    public partial class CategoriesSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[SystemConstants.CategoryID] != null)
                {

                    int categoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);


                    var dac = new AdministrationDAC();

                    AdministrationEDSC.CategoryDTRow dr = null;

                    if (categoryID != 0)
                    {
                        dr = dac.RetrieveCategory(categoryID);

                        CategoriesSetupUC1.Mode = SystemConstants.FormMode.Edit;
                    }
                    else
                    {
                        dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
                        CategoriesSetupUC1.Mode = SystemConstants.FormMode.New;
                    }

                    CategoriesSetupUC1.SetData(dr);
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