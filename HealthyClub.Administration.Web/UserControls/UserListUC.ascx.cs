using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.BF;
using HealthyClub.Utility;
using System.Web.Security;
using WebMatrix.WebData;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class UserListUC : System.Web.UI.UserControl
    {
        public string SearchString
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSearchString.Value))
                    return hdnSearchString.Value;
                else return "";
            }
            set
            {
                hdnSearchString.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            SearchString = "";
            SetDataSource();
        }

        private void SetDataSource()
        {
            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("SearchString", SearchString);
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SelectMethod = "RetrieveCustomerList";
            ods.SelectCountMethod = "RetrieveCustomerListCount";
            ods.EnablePaging = true;

            gridview1.DataBind();

        }

        private AdministrationEDSC.UserProfilesDTDataTable GetSelected()
        {
            var dt = new AdministrationEDSC.UserProfilesDTDataTable();

            foreach (GridViewRow row in gridview1.Rows)
            {
                CheckBox chkSelected = row.FindControl("chkSelected") as CheckBox;
                

                if (chkSelected.Checked)
                {
                    Label lblEmail = row.FindControl("lblEmail") as Label;
                    LinkButton lblUsername = row.FindControl("lnkUserName") as LinkButton;
                    var dr = dt.NewUserProfilesDTRow();
                    dr.Username = lblUsername.Text;
                    dr.UserID = Guid.Empty;
                    dt.AddUserProfilesDTRow(dr);
                }
            }
            return dt;
        }

        private void DeleteSelected()
        {
            foreach (GridViewRow row in gridview1.Rows)
            {
                CheckBox chkSelected = row.FindControl("chkSelected") as CheckBox;

                if (chkSelected.Checked)
                {
                    Label lblEmail = row.FindControl("lblEmail") as Label;
                    LinkButton lblUsername = row.FindControl("lnkUserName") as LinkButton; 

                    ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(lblUsername.Text);                    
                    Membership.DeleteUser(lblUsername.Text, true);
                }
            }
            
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
            Refresh();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SearchString = txtSearch.Text;
            SetDataSource();
        }

        protected void gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                var source = e.CommandSource as Control;
                GridViewRow row = source.Parent.Parent as GridViewRow;

                HiddenField hdnId = row.FindControl("hdnId") as HiddenField;
                Response.Redirect("~/User/Member.aspx?" + SystemConstants.UserID + "=" + hdnId.Value);
            }

        }

        protected void gridview1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDataSource();
        }
    }
}