using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class CouncilUC : System.Web.UI.UserControl
    {
        public string SortExpression
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSortParameter.Value))
                    return hdnSortParameter.Value;
                else
                    return "";
            }
            set
            {
                hdnSortParameter.Value = value;
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
            if (!IsPostBack)
            {
            }
            SetDataSource();
            GridView1.DataBind();
        }

        private void SetDataSource()
        {
            GridView1.DataSource = ods;
            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("sortExpression", SortExpression);
            ods.EnablePaging = GridView1.AllowPaging;
            ods.SelectMethod = "RetrieveCouncils";
            ods.SelectCountMethod = "RetrieveCouncilsCount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.MaximumRowsParameterName = "amount";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RenameCouncil")
            {
                LinkButton lnkRename = e.CommandSource as LinkButton;
                GridViewRow row = lnkRename.Parent.Parent as GridViewRow;
                HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                LinkButton lnkName = row.FindControl("lnkName") as LinkButton;
                if (e.CommandName == "RenameCouncil")
                {
                    Response.Redirect("~/Council/CouncilSetup.aspx?" + SystemConstants.CouncilID + "=" +
                    hdnID.Value);
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.CouncilDTDataTable dt = GetSelected();
            foreach (AdministrationEDSC.CouncilDTRow dr in dt)
            {
                bool complete = bfc.DeleteCouncil(dr.ID);
                if (complete)
                {
                    divSuccess.Visible = complete;
                    divError.Visible = !complete;
                    lblSuccess.Text = "Selected Council has been successfully deleted";
                }
                else
                {
                    divSuccess.Visible = complete;
                    divError.Visible = !complete;
                    lblError.Text = "Cannot delete selected Council, one or more suburb(s) are linked to this Council. Consider to delete or modify the Suburb(s) and try again.";
                }
            }
            Refresh();
        }

        private AdministrationEDSC.CouncilDTDataTable GetSelected()
        {
            AdministrationEDSC.CouncilDTDataTable dt = new AdministrationEDSC.CouncilDTDataTable();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox1 = row.FindControl("chkboxSelected") as CheckBox;
                    HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                    var dr = dt.NewCouncilDTRow();
                    if (CheckBox1.Checked)
                    {
                        dr.ID = Convert.ToInt32(hdnID.Value);
                        dt.AddCouncilDTRow(dr);
                    }
                }
            }
            return dt;
        }

        protected void lnkNew_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Council/CouncilSetup.aspx?" + SystemConstants.CouncilID + "=0");
        }
    }
}