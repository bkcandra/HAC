using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Customer.EDS;
using HealthyClub.Customer.DA;
using System.Web.Security;
using WebMatrix.WebData;

namespace HealthyClub.Web.UserControls
{
    public partial class CustomerAccountUC : System.Web.UI.UserControl
    {
        public Guid UserID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnUserID.Value))
                    return new Guid(hdnUserID.Value);
                else return Guid.Empty;
            }
            set
            {
                hdnUserID.Value = value.ToString();
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
                    lblUsername.Visible = true;
                    lblEmailAdress.Visible = true;

                    lblName.Visible = true;
                    lblAddress.Visible = true;
                    lblAddress2.Visible = true;
                    lblSex.Visible = true;

                    lblPhoneNumber.Visible = true;
                    lblPrefered.Visible = true;
                    lblMobileNumber.Visible = true;
                    lblBirthdate.Visible = true;

                    txtDOB.Visible = false;
                    radMale.Visible = false;
                    radFemale.Visible = false;
                    txtEmailAdress.Visible = false;
                    txtEmailAdress2.Visible = false;
                    ddlTitle.Visible = false;
                    txtFirstName.Visible = false;
                    txtLastName.Visible = false;
                    txtAddress1.Visible = false;
                    txtSuburb.Visible = false;
                    ddlState.Visible = false;
                    txtPostCode.Visible = false;
                    txtContactNumber.Visible = false;
                    txtMobileNumber.Visible = false;
                    radiobyEMail.Visible = false;
                    radiobyMail.Visible = false;
                    radiobyPhone.Visible = false;

                    btnCancel.Visible = false;
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblUsername.Visible = true;
                    lblEmailAdress.Visible = false;

                    lblName.Visible = false;
                    lblAddress.Visible = false;
                    lblAddress2.Visible = false;
                    lblSex.Visible = false;

                    lblPhoneNumber.Visible = false;
                    lblPrefered.Visible = false;
                    lblMobileNumber.Visible = false;
                    lblBirthdate.Visible = false;

                    txtDOB.Visible = true;
                    radMale.Visible = true;
                    radFemale.Visible = true;
                    txtEmailAdress.Visible = true;
                    txtEmailAdress2.Visible = true;
                    ddlTitle.Visible = true;
                    txtFirstName.Visible = true;
                    txtLastName.Visible = true;
                    txtAddress1.Visible = true;
                    txtSuburb.Visible = true;
                    ddlState.Visible = true;
                    txtPostCode.Visible = true;
                    txtContactNumber.Visible = true;
                    txtMobileNumber.Visible = true;
                    radiobyEMail.Visible = true;
                    radiobyMail.Visible = true;
                    radiobyPhone.Visible = true;

                    btnCancel.Visible = true;
                    btnEdit.Visible = false;
                    btnSave.Visible = true;
                }
                //else
                //    lblAddEditTitle.Text = "Brand Detail";
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
                    SetUserInformation();
                }
        }

        private void SetDDL()
        {
            CustomerEDSC.StateDTDataTable dtState = new CustomerDAC().RetrieveStates();

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

            CustomerEDSC.StateDTDataTable dtState2 = new CustomerDAC().RetrieveStates();

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

        private void SetUserInformation()
        {
            var dr = new CustomerDAC().RetrieveUserProfiles(UserID);
            if (dr != null)
            {
                hdnID.Value = dr.ID.ToString();
                lblUsername.Text = dr.Username;
                lblEmailAdress.Text = dr.Email;
                txtEmailAdress.Text = dr.Email;
                txtEmailAdress2.Text = dr.Email;

                ddlTitle.SelectedValue = dr.Title.ToString();
                lblName.Text = ddlTitle.SelectedItem.Text + " " + dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;
                hdnAccountDeletion.Value = dr.AccountDeletion.ToString();
                txtFirstName.Text = dr.FirstName;
                txtLastName.Text = dr.LastName;
                txtAddress1.Text = dr.Address;
                lblAddress.Text = dr.Address;

                txtSuburb.Text = dr.Suburb.ToString();
                lblAddress2.Text = txtSuburb.Text + ", " + ddlState.SelectedItem.Text + " " + dr.PostCode.ToString();
                ddlState.SelectedValue = dr.StateID.ToString();

                txtPostCode.Text = dr.PostCode.ToString();
                txtContactNumber.Text = dr.PhoneNumber;
                lblPhoneNumber.Text = dr.PhoneNumber;
                txtContactNumber.Text = dr.MobileNumber;
                lblMobileNumber.Text = dr.MobileNumber;

                hdnUsername.Value = dr.Username;
                hdnEmailAddress.Value = dr.Email;
                hdnAgreement.Value = dr.Aggreement.ToString();
                lblBirthdate.Text = dr.DOB.ToShortDateString();
                txtDOB.Text = dr.DOB.ToShortDateString();

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

                if (dr.Gender == (int)SystemConstants.Gender.Male)
                {
                    radMale.Checked = true;
                    lblSex.Text = "Male";
                }
                else if (dr.Gender == (int)SystemConstants.Gender.Female)
                {
                    radFemale.Checked = true;
                    lblSex.Text = "Female";
                }
            }
            else if (dr == null)
            {
                var drProvider = new CustomerDAC().RetrieveProviderProfiles(UserID);
                if (drProvider != null)
                {
                    Response.Redirect(SystemConstants.ProviderUrl + "Account/");
                }
                else
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Account/Login.aspx");
                }
            }



        }

        private bool authUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                UserID = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);
                if (UserID != Guid.Empty)
                    return true;
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return false;
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
                return false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Mode = SystemConstants.FormMode.View;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SetUserInformation();
            Mode = SystemConstants.FormMode.Edit;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!isError())
            {
                CustomerEDSC.UserProfilesDTRow dr = GetRegistrationData();

                CustomerDAC dac = new CustomerDAC();
                dac.UpdateUserProfiles(dr);
                Membership.GetUser().Email = dr.Email;
            }
            Mode = SystemConstants.FormMode.View;
            SetUserInformation();
        }

        private CustomerEDSC.UserProfilesDTRow GetRegistrationData()
        {
            CustomerEDSC.UserProfilesDTRow dr = new CustomerEDSC.UserProfilesDTDataTable().NewUserProfilesDTRow();

            dr.ID = Convert.ToInt32(hdnID.Value);
            dr.UserID = UserID;
            dr.Username = hdnUsername.Value;
            dr.Title = Convert.ToInt32(ddlTitle.SelectedValue);
            dr.FirstName = txtFirstName.Text;
            dr.MiddleName = "";
            dr.LastName = txtLastName.Text;
            dr.Email = hdnEmailAddress.Value;
            dr.Address = txtAddress1.Text;
            dr.Suburb = txtSuburb.Text;
            dr.StateID = Convert.ToInt32(ddlState.SelectedValue);
            dr.PhoneNumber = txtContactNumber.Text;
            dr.MobileNumber = txtMobileNumber.Text;
            dr.Aggreement = Convert.ToBoolean(hdnAgreement.Value);
            dr.DOB = Convert.ToDateTime(txtDOB.Text);
            dr.PostCode = Convert.ToInt32(txtPostCode.Text);
            dr.CreatedBy = hdnCreatedBy.Value;
            dr.AccountDeletion = Convert.ToBoolean(hdnAccountDeletion.Value);
            dr.CreatedDatetime = Convert.ToDateTime(hdnCreatedDatetime.Value);
            dr.ModifiedBy = WebSecurity.CurrentUserName;
            dr.ModifiedDatetime = DateTime.Now;

            if (radiobyEMail.Checked)
                dr.PreferredContact = 1;
            else if (radiobyMail.Checked)
                dr.PreferredContact = 3;
            else if (radiobyPhone.Checked)
                dr.PreferredContact = 2;

            if (radFemale.Checked)
                dr.Gender = (int)SystemConstants.Gender.Female;
            else if (radMale.Checked)
                dr.Gender = (int)SystemConstants.Gender.Male;
            return dr;
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
            if (string.IsNullOrEmpty(lblAddress2.Text))
            {
                lblError.Text = "address is required";
                error = true;
                lblError0.Visible = true;
            }
            if (txtAddress1.Text == "Address Line 1")
            {
                txtAddress1.Text = "";
            }
            if (ddlState.SelectedValue == "0")
            {
                lblError.Text = "Select your address";
                lblError0.Visible = true;
                error = true;
            }
            return error;
        }
    }
}