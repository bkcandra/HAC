using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class SuburbSetup : System.Web.UI.UserControl
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
                    lblAddEditTitle.Text = "Add New Suburb";
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblAddEditTitle.Text = "Edit Suburb";
                }

                if (Mode == SystemConstants.FormMode.Edit)
                {
                    txtAddEditName.Visible = true;
                    txtPostcode.Visible = true;
                    ddlCouncil.Visible = true;

                    lblAddEditName.Visible = false;
                    lblPostcode.Visible = false;
                    lblState.Visible = true;
                    lblCouncil.Visible = false;

                    lnkBackToList.Visible = false;
                    lnkOk1.Visible = true;
                    lnkCancel1.Visible = true;
                    lnkCreateAnother.Visible = false;
                    lnkEdit.Visible = false;

                }
                else if (Mode == SystemConstants.FormMode.New)
                {
                    txtAddEditName.Visible = true;
                    txtPostcode.Visible = true;
                    ddlCouncil.Visible = true;

                    lblAddEditName.Visible = false;
                    lblPostcode.Visible = false;
                    lblState.Visible = true;
                    lblCouncil.Visible = false;

                    lnkBackToList.Visible = false;
                    lnkOk1.Visible = true;
                    lnkCancel1.Visible = true;
                    lnkCreateAnother.Visible = false;
                    lnkEdit.Visible = false;
                }
                else if (Mode == SystemConstants.FormMode.View)
                {
                    txtAddEditName.Visible = false;
                    txtPostcode.Visible = false;
                    ddlCouncil.Visible = false;

                    lblAddEditName.Visible = true;
                    lblPostcode.Visible = true;
                    lblState.Visible = true;
                    lblCouncil.Visible = true;

                    lnkBackToList.Visible = true;
                    lnkOk1.Visible = false;
                    lnkEdit.Visible = true;
                    lnkCancel1.Visible = false;
                    lnkCreateAnother.Visible = true;
                }
                lblerror.Visible = false;
            }
        }

        protected int suburbID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mode == SystemConstants.FormMode.New)
                    SetDDL();
            }
        }

        public void SetData(AdministrationEDSC.v_SuburbExplorerDTRow dr)
        {
            SetDDL();

            hdnSuburbID.Value = dr.ID.ToString();
            lblAddEditName.Text = dr.Name;
            txtAddEditName.Text = dr.Name;
            txtPostcode.Text = dr.PostCode.ToString();
            lblPostcode.Text = dr.PostCode.ToString();
            ddlCouncil.SelectedValue = dr.CouncilID.ToString();
            lblCouncil.Text = ddlCouncil.SelectedItem.Text;
            lblState.Text = dr.StateDetail;
            hdnStateID.Value = dr.StateID.ToString();
        }

        protected void page_init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.SuburbID] != null)
                suburbID = Convert.ToInt32(Request.QueryString[SystemConstants.SuburbID]);
        }

        private void SetDDL()
        {
            AdministrationEDSC.CouncilDTDataTable dt = new AdministrationDAC().RetrieveCouncils();
            if (dt.Count != 0)
            {
                ddlCouncil.Items.Clear();

                ListItem li = new ListItem("Choose one", "0");
                ddlCouncil.Items.Add(li);

                foreach (var dr in dt)
                {
                    li = new ListItem(dr.Name, dr.ID.ToString());
                    ddlCouncil.Items.Add(li);
                }
            }
            else
            {
                ddlCouncil.Items.Clear();

                ListItem li = new ListItem("Please configure 'Council' List", "0");
                ddlCouncil.Items.Add(li);
            }
        }


        protected void lnkOk1_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.SuburbDTRow dr = new AdministrationEDSC.SuburbDTDataTable().NewSuburbDTRow();
            dr = GetData();
            if (ddlCouncil.SelectedValue == "0")
            {
                lblerror.Visible = true;
            }
            else
            {
                string userName = SystemConstants.DevUser;
                //Membership.GetUser().UserName;

                AdministrationDAC dac = new AdministrationDAC();

                if (Mode == SystemConstants.FormMode.New)
                {
                    dac.CreateSuburb(userName, dr);
                }
                else if (Mode == SystemConstants.FormMode.Edit)
                {
                    dac.UpdateSuburb(userName, dr);
                }
                Mode = SystemConstants.FormMode.View;
            }

        }

        public AdministrationEDSC.SuburbDTRow GetData()
        {
            AdministrationEDSC.SuburbDTRow dr = new AdministrationEDSC.SuburbDTDataTable().NewSuburbDTRow();

            if (!string.IsNullOrEmpty(hdnSuburbID.Value))
                dr.ID = Convert.ToInt32(hdnSuburbID.Value);
            dr.Name = txtAddEditName.Text;
            dr.PostCode = Convert.ToInt32(txtPostcode.Text);
            dr.CouncilID = Convert.ToInt32(ddlCouncil.SelectedValue);
            
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

            SetDDL();
        }

        private void ClearField()
        {
            lblAddEditName.Text = "";
            txtAddEditName.Text = "";
            lblPostcode.Text = "";
            txtPostcode.Text = "";
            ddlCouncil.SelectedValue = "0";
        }

        protected void lnkBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Suburb/");
        }

        protected void lnkCancel1_Click(object sender, EventArgs e)
        {
            if (Mode == SystemConstants.FormMode.New)
                Response.Redirect("~/Suburb/");
            else
                Mode = SystemConstants.FormMode.View;
        }

        protected void ddlCouncil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCouncil.SelectedValue == "0")
                lblState.Text = "Select a council";
            else
            {
                var councilDr = new AdministrationDAC().RetrieveCouncilState(Convert.ToInt32(ddlCouncil.SelectedValue));
                lblState.Text = councilDr.StateDetail;
                hdnStateID.Value = councilDr.StateID.ToString();
            }
        }
    }
}