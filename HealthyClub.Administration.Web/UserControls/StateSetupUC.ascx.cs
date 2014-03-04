using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class StateSetupUC : System.Web.UI.UserControl
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
                    lblAddEditTitle.Text = "Add New State";
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblAddEditTitle.Text = "Edit State";
                }
                setForm();
                //else
                //    lblAddEditTitle.Text = "Brand Detail";
            }
        }

        protected int suburbID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public void SetData(AdministrationEDSC.StateDTRow dr)
        {
            hdnStateID.Value = dr.ID.ToString();
            lblAddEditName.Text = dr.StateName;
            txtAddEditName.Text = dr.StateName;
            txtFullName.Text = dr.StateDetail;
            lblFullName.Text = dr.StateDetail;
        }

        protected void page_init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.SuburbID] != null)
                suburbID = Convert.ToInt32(Request.QueryString[SystemConstants.SuburbID]);
        }

        private void setForm()
        {
            if (Mode == SystemConstants.FormMode.Edit)
            {
                txtAddEditName.Visible = true;
                txtFullName.Visible = true;

                lblAddEditName.Visible = false;
                lblFullName.Visible = false;

                lnkBackToList.Visible = false;
                lnkOk1.Visible = true;
                lnkCancel1.Visible = true;
                lnkCreateAnother.Visible = false;
                lnkEdit.Visible = false;
            }
            else if (Mode == SystemConstants.FormMode.New)
            {
                txtAddEditName.Visible = true;
                txtFullName.Visible = true;

                lblAddEditName.Visible = false;
                lblFullName.Visible = false;

                lnkBackToList.Visible = false;
                lnkOk1.Visible = true;
                lnkCancel1.Visible = true;
                lnkCreateAnother.Visible = false;
                lnkEdit.Visible = false;
            }
            else if (Mode == SystemConstants.FormMode.View)
            {
                txtAddEditName.Visible = false;
                txtFullName.Visible = false;

                lblAddEditName.Visible = true;
                lblFullName.Visible = true;

                lnkBackToList.Visible = true;
                lnkOk1.Visible = false;
                lnkEdit.Visible = true;
                lnkCancel1.Visible = false;
                lnkCreateAnother.Visible = true;
            }
        }

        protected void lnkOk1_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.StateDTRow dr = new AdministrationEDSC.StateDTDataTable().NewStateDTRow();
            dr = GetData();

            string userName = SystemConstants.DevUser;
            //Membership.GetUser().UserName;

            AdministrationDAC dac = new AdministrationDAC();

            if (Mode == SystemConstants.FormMode.New)
            {
                dac.CreateState(userName, dr);
            }
            else if (Mode == SystemConstants.FormMode.Edit)
            {
                dac.UpdateState(userName, dr);
            }
            Mode = SystemConstants.FormMode.View;
            SetData(dr);
        }

        public AdministrationEDSC.StateDTRow GetData()
        {
            AdministrationEDSC.StateDTRow dr = new AdministrationEDSC.StateDTDataTable().NewStateDTRow();

            if (!string.IsNullOrEmpty(hdnStateID.Value))
                dr.ID = Convert.ToInt32(hdnStateID.Value);
            dr.StateName = txtAddEditName.Text;
            dr.StateDetail = txtFullName.Text;
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
            lblFullName.Text = "";
            txtFullName.Text = "";
        }

        protected void lnkBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/State/");
        }

        protected void lnkCancel1_Click(object sender, EventArgs e)
        {
            if (Mode == SystemConstants.FormMode.New)
                Response.Redirect("~/State/");
            else
                Mode = SystemConstants.FormMode.View;
        }
    }
}