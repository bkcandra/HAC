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
    public partial class CouncilSetupUC : System.Web.UI.UserControl
    {
        public SystemConstants.FormMode Mode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAddEditMode.Value))
                {
                    return (SystemConstants.FormMode)Convert.ToInt32(hdnAddEditMode.Value);
                }
                else
                {
                    return SystemConstants.FormMode.New;
                }
            }
            set
            {
                hdnAddEditMode.Value = Convert.ToInt32(value).ToString();

                if (value == SystemConstants.FormMode.New)
                {
                    lblAddEditTitle.Text = "Add New Council";
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblAddEditTitle.Text = "Edit Council";
                }
                setForm();
                //else
                //    lblAddEditTitle.Text = "Brand Detail";
            }
        }

        protected int CouncilID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public void SetDDL()
        {
            var dt = new AdministrationDAC().RetrieveStates();
            if (dt.Count != 0)
            {
                ddlState.Items.Add(new ListItem("Select Council State", "0"));
                foreach (var dr in dt)
                {
                    ddlState.Items.Add(new ListItem(dr.StateName, dr.ID.ToString()));
                }
            }
            else
            {
                ddlState.Items.Clear();

                ListItem li = new ListItem("Please configure 'State' List", "0");
                ddlState.Items.Add(li);
            }
        }

        public void SetData(AdministrationEDSC.CouncilDTRow dr)
        {
            hdnCouncilID.Value = dr.ID.ToString();
            lblAddEditName.Text = dr.Name;
            txtAddEditName.Text = dr.Name;
            txtDesc.Text = dr.Description;
            lblDesc.Text = dr.Description;
            hdnCreatedDate.Value = dr.CreatedDatetime.ToString();
            hdnCreatedBy.Value = dr.CreatedBy.ToString();
            ddlState.SelectedValue = dr.StateID.ToString();
            lblState.Text = ddlState.SelectedItem.Text;
        }

        protected void page_init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.CouncilID] != null)
                CouncilID = Convert.ToInt32(Request.QueryString[SystemConstants.CouncilID]);
        }

        private void setForm()
        {
            if (Mode == SystemConstants.FormMode.Edit)
            {
                txtAddEditName.Visible = true;
                txtDesc.Visible = true;

                lblAddEditName.Visible = false;
                lblDesc.Visible = false;

                ddlState.Visible = true;
                lblState.Visible = false;

                lnkBackToList.Visible = false;
                lnkOk1.Visible = true;
                lnkCancel1.Visible = true;
                lnkCreateAnother.Visible = false;
                lnkEdit.Visible = false;
            }
            else if (Mode == SystemConstants.FormMode.New)
            {
                txtAddEditName.Visible = true;
                txtDesc.Visible = true;

                lblAddEditName.Visible = false;
                lblDesc.Visible = false;
                ddlState.Visible = true;
                lblState.Visible = false;

                lnkBackToList.Visible = false;
                lnkOk1.Visible = true;
                lnkCancel1.Visible = true;
                lnkCreateAnother.Visible = false;
                lnkEdit.Visible = false;
            }
            else if (Mode == SystemConstants.FormMode.View)
            {
                txtAddEditName.Visible = false;
                txtDesc.Visible = false;

                lblAddEditName.Visible = true;
                lblDesc.Visible = true;
                ddlState.Visible = false;
                lblState.Visible = true;
                lnkBackToList.Visible = true;
                lnkOk1.Visible = false;
                lnkEdit.Visible = true;
                lnkCancel1.Visible = false;
                lnkCreateAnother.Visible = true;
            }
        }

        protected void lnkOk1_Click(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue == "0")
            {
                lblerror.Visible = true;
            }
            else
            {
                AdministrationEDSC.CouncilDTRow dr = new AdministrationEDSC.CouncilDTDataTable().NewCouncilDTRow();
                dr = GetData();

                string userName = SystemConstants.DevUser;
                //Membership.GetUser().UserName;

                AdministrationDAC dac = new AdministrationDAC();

                if (Mode == SystemConstants.FormMode.New)
                {
                    dac.CreateCouncil(userName, dr);
                }
                else if (Mode == SystemConstants.FormMode.Edit)
                {
                    dac.UpdateCouncil(userName, dr);
                }
                Mode = SystemConstants.FormMode.View;
                SetData(dr);
            }

        }

        public AdministrationEDSC.CouncilDTRow GetData()
        {
            AdministrationEDSC.CouncilDTRow dr = new AdministrationEDSC.CouncilDTDataTable().NewCouncilDTRow();

            if (!string.IsNullOrEmpty(hdnCouncilID.Value))
                dr.ID = Convert.ToInt32(hdnCouncilID.Value);
            dr.Name = txtAddEditName.Text;
            dr.Value = txtAddEditName.Text.Replace(" ", "_");
            dr.Description = txtDesc.Text;
            dr.StateID = Convert.ToInt32(ddlState.SelectedValue);
            dr.CreatedBy = hdnCreatedBy.Value;
            dr.CreatedDatetime = Convert.ToDateTime(hdnCreatedDate.Value);
            return dr;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Mode = SystemConstants.FormMode.Edit;

        }

        protected void lnkCreateAnother_Click(object sender, EventArgs e)
        {
            ClearField();
            Mode = SystemConstants.FormMode.New;
        }

        private void ClearField()
        {
            lblAddEditName.Text = "";
            txtAddEditName.Text = "";
            lblDesc.Text = "";
            txtDesc.Text = "";
        }

        protected void lnkBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Council/");
        }

        protected void lnkCancel1_Click(object sender, EventArgs e)
        {
            if (Mode == SystemConstants.FormMode.New)
                Response.Redirect("~/Council/");
            else
                Mode = SystemConstants.FormMode.View;
        }
    }
}