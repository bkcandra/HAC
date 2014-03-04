using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using HealthyClub.Utility;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class StateUC : System.Web.UI.UserControl
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
            ods.SelectMethod = "RetrieveStates";
            ods.SelectCountMethod = "RetrieveStatesCount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.MaximumRowsParameterName = "amount";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RenameState")
            {
                LinkButton lnkRename = e.CommandSource as LinkButton;
                GridViewRow row = lnkRename.Parent.Parent as GridViewRow;
                HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                LinkButton lnkName = row.FindControl("lnkName") as LinkButton;
                if (e.CommandName == "RenameState")
                {
                    Response.Redirect("~/State/StateSetup.aspx?" + SystemConstants.StateID + "=" +
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
            AdministrationDAC dac = new AdministrationDAC();
            AdministrationEDSC.StateDTDataTable dt = GetSelected();
            foreach (AdministrationEDSC.StateDTRow dr in dt)
            {
                dac.DeleteState(dr.ID);
            }
            Refresh();
        }

        private AdministrationEDSC.StateDTDataTable GetSelected()
        {
            AdministrationEDSC.StateDTDataTable dt = new AdministrationEDSC.StateDTDataTable();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox1 = row.FindControl("chkboxSelected") as CheckBox;
                    HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                    var dr = dt.NewStateDTRow();
                    if (CheckBox1.Checked)
                    {
                        dr.ID = Convert.ToInt32(hdnID.Value);
                        dt.AddStateDTRow(dr);
                    }
                }
            }
            return dt;
        }

        protected void lnkNew_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/State/StateSetup.aspx?" + SystemConstants.StateID + "=0");
        }
    }
}