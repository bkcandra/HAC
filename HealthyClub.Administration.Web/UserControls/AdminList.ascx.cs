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
using WebMatrix.WebData;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class AdminList : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            SetDataSource();
        }

        private void SetDataSource()
        {
            var dt = new MembershipHelper().GetAllUsersinRole(SystemConstants.AdministratorRole);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton lnkDelete = e.CommandSource as LinkButton;
            GridViewRow row = lnkDelete.Parent.Parent as GridViewRow;


            Label lblUserName = row.FindControl("lblUserName") as Label;

            if (e.CommandName == "DeleteUser")
            {
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(lblUserName.Text);
                Membership.DeleteUser(lblUserName.Text, true);
            }
            Refresh();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}