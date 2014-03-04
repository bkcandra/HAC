﻿using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ProviderAccount : System.Web.UI.UserControl
    {
        public Guid ProviderID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnProviderID.Value))
                    return new Guid(hdnProviderID.Value);
                else return Guid.Empty;
            }
            set
            {
                hdnProviderID.Value = value.ToString();
            }
        }

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

                if (value == SystemConstants.FormMode.View)
                {
                    lblCompany.Visible = true;
                    lblBranch.Visible = true;
                    txtCompany.Visible = false;
                    txtBranch.Visible = false;

                    lblName.Visible = true;
                    lblAddress1.Visible = true;
                    lblAddress2.Visible = true;
                    lblContactNumber.Visible = true;
                    lblMobile.Visible = true;
                    lblPrefered.Visible = true;

                    ddlTitle.Visible = false;
                    txtFirstName.Visible = false;
                    txtMiddleName.Visible = false;
                    txtLastName.Visible = false;
                    txtAddress1.Visible = false;
                    txtSuburb.Visible = false;
                    ddlState.Visible = false;
                    txtPostCode.Visible = false;
                    txtContactNumber.Visible = false;
                    txtMobile.Visible = false;
                    radiobyEMail.Visible = false;
                    radiobyMail.Visible = false;
                    radiobyPhone.Visible = false;
                    EditSuburb.Visible = false;

                    btnCancel.Visible = false;
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblCompany.Visible = false;
                    lblBranch.Visible = false;
                    txtCompany.Visible = true;
                    txtBranch.Visible = true;

                    lblName.Visible = false;
                    lblAddress1.Visible = false;
                    lblAddress2.Visible = false;
                    lblContactNumber.Visible = false;
                    lblMobile.Visible = false;
                    lblPrefered.Visible = false;

                    ddlTitle.Visible = true;
                    txtFirstName.Visible = true;
                    txtMiddleName.Visible = true;
                    txtLastName.Visible = true;
                    txtAddress1.Visible = true;
                    txtSuburb.Visible = true;
                    ddlState.Visible = true;
                    txtPostCode.Visible = true;
                    txtContactNumber.Visible = true;
                    txtMobile.Visible = true;
                    radiobyEMail.Visible = true;
                    radiobyMail.Visible = true;
                    radiobyPhone.Visible = true;
                    EditSuburb.Visible = true;

                    btnCancel.Visible = true;
                    btnEdit.Visible = false;
                    btnSave.Visible = true;
                }
                //else
                //    lblAddEditTitle.Text = "Brand Detail";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.ProviderID] != null)
            {
                ProviderID = new Guid(Request.QueryString[SystemConstants.ProviderID].ToString());
            }
            else
            {
                divProvider.Visible = false;
                divNoProvider.Visible = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (!IsPostBack)
                if (authUser())
                {
                    Mode = SystemConstants.FormMode.View;
                    SetDDL();
                    SetProviderInformation();
                }
        }

        private void SetDDL()
        {
            AdministrationEDSC.StateDTDataTable dtState = new AdministrationDAC().RetrieveStates();

            ddlState.Items.Clear();

            ListItem lis = new ListItem("State", "0");
            ddlState.Items.Add(lis);
            foreach (var dr in dtState)
            {
                string name = "";

                if (dr.ID != 0)
                    name = dr.StateName;


                lis = new ListItem(name, dr.ID.ToString());
                ddlState.Items.Add(lis);
            }

            AdministrationEDSC.StateDTDataTable dtState2 = new AdministrationDAC().RetrieveStates();

            ddlState.Items.Clear();

            ListItem lis2 = new ListItem("State", "0");
            ddlState.Items.Add(lis2);
            foreach (var drstate2 in dtState2)
            {
                string name = "";

                if (drstate2.ID != 0)
                    name = drstate2.StateName;

                lis2 = new ListItem(name, drstate2.ID.ToString());
                ddlState.Items.Add(lis2);
            }
        }

        private void SetProviderInformation()
        {
            var dr = new AdministrationDAC().RetrieveProviderProfiles(ProviderID);
            if (dr != null)
            {
                txtCompany.Text = dr.ProviderName;
                lblCompany.Text = dr.ProviderName;
                if (!string.IsNullOrEmpty(dr.ProviderBranch))
                {
                    txtBranch.Text = dr.ProviderBranch;
                    lblBranch.Text = dr.ProviderBranch;
                }
                if (!string.IsNullOrEmpty(dr.SecondarySuburb))
                {
                    string[] secondarySub = dr.SecondarySuburb.Split('|');
                    {
                        foreach (string sub in secondarySub)
                        {
                            lblBranch.Text += "</br>" + sub;
                        }
                    }
                    SetProviderSuburb(secondarySub);
                }


                ddlTitle.SelectedValue = dr.Title.ToString();
                lblName.Text = ddlTitle.SelectedItem.Text + " " + dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;

                txtFirstName.Text = dr.FirstName;
                txtMiddleName.Text = dr.MiddleName;
                txtLastName.Text = dr.LastName;
                txtAddress1.Text = dr.Address;
                lblAddress1.Text = dr.Address;

                txtSuburb.Text = dr.Suburb;
                lblAddress2.Text = txtSuburb.Text + ", " + ddlState.SelectedItem.Text + " " + dr.PostCode.ToString();

                ddlState.SelectedValue = dr.StateID.ToString();

                txtPostCode.Text = dr.PostCode.ToString();
                txtContactNumber.Text = dr.PhoneNumber;
                txtMobile.Text = dr.MobileNumber;
                lblContactNumber.Text = dr.PhoneNumber;
                lblMobile.Text = dr.MobileNumber;

                hdnUsername.Value = dr.Username;
                hdnEmailAddress.Value = dr.Email;
                hdnAgreement.Value = dr.Aggreement.ToString();

                hdnCreatedDatetime.Value = dr.CreatedDatetime.ToString();
                hdnCreatedBy.Value = dr.CreatedBy;
                hdnModifiedDatetime.Value = dr.ModifiedDatetime.ToString();
                hdnModifiedBy.Value = dr.ModifiedBy;

                if (dr.PreferredContact == (int)SystemConstants.PreferedContact.Email)
                {
                    radiobyEMail.Checked = true;
                    lblPrefered.Text = "Email";
                }
                else if (dr.PreferredContact == (int)SystemConstants.PreferedContact.Brochure)
                {
                    radiobyMail.Checked = true;
                    lblPrefered.Text = "Mail";
                }
                else if (dr.PreferredContact == (int)SystemConstants.PreferedContact.Phone)
                {
                    radiobyPhone.Checked = true;
                    lblPrefered.Text = "Phone";
                }
            }
            else
            {
                divNoProvider.Visible = true;
                divProvider.Visible = false;
            }

        }

        private bool authUser()
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (ProviderID != Guid.Empty)
                    return true;
                else
                {
                    divNoProvider.Visible = true;
                    divProvider.Visible = false;
                    return false;
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!isError())
            {
                var dr = GetRegistrationData();
                new AdministrationDAC().UpdateProviderProfiles(dr);
                SetProviderInformation();

                lblError.Text = "Account Information Modified on " + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                lblError.Visible = true;
                Mode = SystemConstants.FormMode.View;
            }
        }

        private AdministrationEDSC.ProviderProfilesDTRow GetRegistrationData()
        {
            //Reference wizard's controls
            //CreateUserWizardStep ProviderProfiles = CreateNewProvider.FindControl("ProviderProfiles") as CreateUserWizardStep;
            AdministrationEDSC.ProviderProfilesDTRow dr = new AdministrationEDSC.ProviderProfilesDTDataTable().NewProviderProfilesDTRow();

            dr.UserID = ProviderID;
            dr.ProviderBranch = txtBranch.Text;
            dr.Username = hdnUsername.Value;
            dr.Title = Convert.ToInt32(ddlTitle.SelectedValue);
            dr.FirstName = txtFirstName.Text;
            if (txtMiddleName.Text == "Middle Name (Optional)")
                dr.MiddleName = "";
            else
                dr.MiddleName = txtMiddleName.Text;
            dr.LastName = txtLastName.Text;
            dr.Email = hdnEmailAddress.Value;
            dr.Address = txtAddress1.Text;
            dr.Suburb = txtSuburb.Text;
            dr.SecondarySuburb = getSecondarySuburb();
            dr.StateID = Convert.ToInt32(ddlState.SelectedValue);
            dr.PhoneNumber = txtContactNumber.Text;
            dr.MobileNumber = txtMobile.Text;
            dr.Aggreement = Convert.ToBoolean(hdnAgreement.Value);
            dr.ProviderName = txtCompany.Text;
            dr.PostCode = Convert.ToInt32(txtPostCode.Text);
            dr.CreatedBy = hdnCreatedBy.Value;
            dr.CreatedDatetime = Convert.ToDateTime(hdnCreatedDatetime.Value);
            dr.ModifiedBy = WebSecurity.CurrentUserName;
            dr.ModifiedDatetime = DateTime.Now;

            if (radiobyEMail.Checked)
            {
                dr.PreferredContact = 1;
            }
            else if (radiobyMail.Checked)
            {
                dr.PreferredContact = 3;
            }
            else if (radiobyPhone.Checked)
            {
                dr.PreferredContact = 2;
            }

            return dr;
        }

        private string getSecondarySuburb()
        {
            String secondarySuburb = "";
            string separator = "|";
            int rowIndex = 0;

            if (ViewState["SuburbTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SuburbTable"];

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox suburb = (TextBox)GridViewSuburb.Rows[rowIndex].Cells[0].FindControl("txtSuburb");
                        rowIndex++;
                        if (String.IsNullOrEmpty(secondarySuburb))
                        {
                            if (!String.IsNullOrEmpty(suburb.Text))
                                secondarySuburb = suburb.Text;
                        }
                        else
                            secondarySuburb += separator + suburb.Text;
                    }
                }
            }
            return secondarySuburb;
        }

        private bool isError()
        {
            bool error = false;
            lblError.Text = "";

            if (ddlTitle.SelectedValue == "0")
            {
                lblError.Text = "Select your title";
                error = true;
                lblError.Visible = true;
            }
            if (string.IsNullOrEmpty(txtSuburb.Text))
            {
                lblError.Text = "Address is required";
                error = true;
                lblError0.Visible = true;
            }
            if (txtAddress1.Text == "Address Line 1")
            {
                txtAddress1.Text = "";
            }
            if (ddlState.SelectedValue == "0")
            {
                lblError.Text = "Address is required";
                lblError0.Visible = true;
                error = true;
            }
            return error;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Mode = SystemConstants.FormMode.Edit;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Mode = SystemConstants.FormMode.View;
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["SuburbTable"] != null)
            {


                DataTable dtCurrentTable = (DataTable)ViewState["SuburbTable"];
               
                if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count != 2)
                {
                    DataRow drCurrentRow = null;
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)GridViewSuburb.Rows[rowIndex].Cells[0].FindControl("txtSuburb");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Column1"] = box1.Text;

                        rowIndex++;
                    }

                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["SuburbTable"] = dtCurrentTable;

                    //Rebind the Grid with the current data
                    GridViewSuburb.DataSource = dtCurrentTable;
                    GridViewSuburb.DataBind();
                }

                else
                {

                    lblErrorSuburb.Text = "The maximum number of suburbs is 3";
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Column1", typeof(string)));

                dr = dt.NewRow();
                dr["Column1"] = string.Empty;
                dt.Rows.Add(dr);

                //Store the DataTable in ViewState
                ViewState["SuburbTable"] = dt;

                GridViewSuburb.DataSource = dt;
                GridViewSuburb.DataBind();
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetProviderSuburb(string[] secondarySub)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Column1", typeof(string)));

            foreach (var sub in secondarySub)
            {
                DataRow dr = dt.NewRow();
                dr["Column1"] = sub;
                dt.Rows.Add(dr);
            }
            //Store the DataTable in ViewState   

            GridViewSuburb.DataSource = dt;
            GridViewSuburb.DataBind();
            //Store the DataTable in ViewState
            ViewState["SuburbTable"] = dt;


            int rowIndex = 0;
            if (ViewState["SuburbTable"] != null)
            {
                DataTable dtSub = (DataTable)ViewState["SuburbTable"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 1; i < dtSub.Rows.Count; i++)
                    {
                        foreach (var Sub in secondarySub)
                        {
                            TextBox box1 = (TextBox)GridViewSuburb.Rows[rowIndex].Cells[0].FindControl("txtSuburb");
                            box1.Text = Sub;
                            rowIndex++;
                            //Store the DataTable in ViewState   
                        }
                    }
                }
            }
        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["SuburbTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["SuburbTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)GridViewSuburb.Rows[rowIndex].Cells[0].FindControl("txtSuburb");
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        rowIndex++;

                    }
                }
            }
        }
    }
}