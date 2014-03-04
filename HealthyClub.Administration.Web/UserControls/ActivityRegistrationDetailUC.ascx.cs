using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using CKEditor.NET;
using WebMatrix.WebData;
using System.Net;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityRegistrationDetailUC : System.Web.UI.UserControl
    {
        public Boolean EditMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnEdit.Value))
                    return Convert.ToBoolean(hdnEdit.Value);
                else return false;
            }
            set
            {
                hdnEdit.Value = value.ToString();
            }
        }

        public int ActivityID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnACtivityID.Value))
                    return Convert.ToInt32(hdnACtivityID.Value);
                else return 0;
            }
            set
            {
                hdnACtivityID.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitRegistration();
            }
        }

        protected void chkDiscount_CheckedChanged(object sender, EventArgs e)
        {
            //discountedFee.Visible = chkDiscount.Checked;
        }

        private void setProviderData(AdministrationEDSC.ProviderProfilesDTRow dr)
        {
            ddlTitle.SelectedValue = dr.Title.ToString();
            txtFirstName.Text = dr.FirstName;
            txtLastName.Text = dr.LastName;
            txtEmail.Text = dr.Email;
            txtConfirmEmailAddress.Text = dr.Email;
            //txtAddress1.Text = dr.Address;
            //foreach (ListItem item in ddlSuburbs.Items)
            //{
            //    if (item.Text == dr.Suburb.ToString())
            //        ddlSuburbs.SelectedItem.Text = dr.Suburb.ToString();
            //}
            //ddlState.SelectedValue = dr.StateID.ToString();
            //txtPostCode.Text = dr.PostCode.ToString();
            txtContactNumber.Text = dr.PhoneNumber;
            txtMobile.Text = dr.MobileNumber;
        }

        public void InitRegistration()
        {
            SetProviderLogin();

            setDDL();
            if (EditMode)
                SetActivityInformation();
        }

        public void SetActivityInformation()
        {
            if (EditMode)
            {
                var dr = new AdministrationDAC().RetrieveActivityExplorer(ActivityID);
                txtActivityName.Text = dr.Name;
                txtWebsite.Text = dr.Website;

                ddlTitle.SelectedValue = dr.Title.ToString();
                txtFirstName.Text = dr.FirstName;
                txtLastName.Text = dr.LastName;
                txtEmail.Text = txtConfirmEmailAddress.Text = dr.Email;

                txtAddress1.Text = dr.Address;
                ddlSuburbs.SelectedValue = dr.SuburbID.ToString();
                ddlState.SelectedValue = dr.StateID.ToString();
                txtPostCode.Text = dr.PostCode.ToString();
                txtContactNumber.Text = dr.PhoneNumber;
                txtMobile.Text = dr.MobileNumber;

                ddlTitle0.SelectedValue = dr.AltTitle.ToString();
                txtFirstName0.Text = dr.AltFirstName;
                txtMiddleName0.Text = dr.AltMiddleName;
                txtLastName0.Text = dr.AltLastName;
                txtEmailalt.Text = txtConfirmEmailalt.Text = dr.AltEmail;
                // txtAddress2.Text = dr.AltAddress;
                //ddlSuburbs0.SelectedValue = dr.AltSuburbID.ToString();
                //ddlState0.SelectedValue = dr.AltStateID.ToString();
                //txtPostCode0.Text = dr.AltPostCode.ToString();
                txtContactNumber0.Text = dr.AltPhoneNumber;
                txtMobile0.Text = dr.AltMobileNumber;
            }

        }

        private void SetProviderLogin()
        {
            hdnProviderUsername.Value = WebSecurity.CurrentUserName;
        }

        private void setDDL()
        {
            AdministrationEDSC.v_SuburbExplorerDTDataTable dt = new AdministrationDAC().RetrieveSuburbs();

            ddlSuburbs.Items.Clear();

            ListItem li = new ListItem("Suburb", "0");
            ddlSuburbs.Items.Add(li);
            foreach (var dr in dt)
            {
                string name = "";

                if (dr.ID != 0)
                    name = dr.Name;


                li = new ListItem(name, dr.ID.ToString());
                ddlSuburbs.Items.Add(li);
            }

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
        }

        protected void chkUseProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseProvider.Checked)
            {
                var dr = new AdministrationDAC().RetrieveProviderProfiles((hdnProviderUsername.Value));
                setProviderData(dr);
                txtFirstName.Focus();
            }
            else
            {
                clearProviderData();
            }
        }

        private void clearProviderData()
        {
            ddlTitle.SelectedValue = "0";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtConfirmEmailAddress.Text = "";
            txtContactNumber.Text = "";
            txtMobile.Text = "";
        }

        protected void chkUseSecondaryContact_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSecondaryContact.Checked)
            {
                tabSecondaryContact.Visible = false;
                hdnUsingSecondary.Value = false.ToString();
            }
            else
            {
                tabSecondaryContact.Visible = true;
                hdnUsingSecondary.Value = true.ToString();
            }
        }

        public AdministrationEDSC.ActivityDTRow getDetails()
        {
            var dr = new AdministrationEDSC.ActivityDTDataTable().NewActivityDTRow();

            dr.Name = txtActivityName.Text;
            dr.ActivityCode = txtActivityName.Text;
            dr.Website = txtWebsite.Text;

            dr.CreatedBy = dr.ModifiedBy = WebSecurity.CurrentUserName;
            dr.CreatedDateTime = dr.ModifiedDateTime = DateTime.Today;
            return dr;
        }

        public void CheckValid(out bool Error, out string ErrorText)
        {
            ErrorText = "";
            Error = false;

            if (txtActivityName.Text == "")
            {
                Error = true;
                ErrorText += SystemConstants.ErrorActivityNameisNull + "<br />";
            }
            if (txtFirstName.Text == "")
            {
                Error = true;
                ErrorText += SystemConstants.ContactDetailsIsNull + "<br />";
            }
            if (txtEmail.Text == "")
            {
                Error = true;
                ErrorText += SystemConstants.ContactEmailIsNull + "<br />";
            }
            if (txtAddress1.Text == "" || ddlState.SelectedIndex == 0 || txtPostCode.Text == "" || ddlSuburbs.SelectedValue == "0")
            {
                Error = true;
                ErrorText += SystemConstants.ContactAddressIsNull + "<br />";
            }
            if (!string.IsNullOrEmpty(txtWebsite.Text))
            {
                if (txtWebsite.Text.StartsWith("http://"))
                    txtWebsite.Text = txtWebsite.Text.Substring("http://".Length);
                if (!CheckUriValid("http://" + txtWebsite.Text, "HEAD"))
                {
                    if (!CheckUriValid("http://" + txtWebsite.Text, "GET"))
                    {
                        Error = true;
                        ErrorText += SystemConstants.WebAddressIsInvalid + "<br />";
                    }
                }
            }
        }

        private bool CheckUriValid(string url, string method)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = method;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public AdministrationEDSC.ActivityContactDetailDTRow getContactDetail()
        {
            AdministrationEDSC.ActivityContactDetailDTRow dr = new AdministrationEDSC.ActivityContactDetailDTDataTable().NewActivityContactDetailDTRow();

            dr.Title = Convert.ToInt32(ddlTitle.SelectedValue);
            dr.Username = WebSecurity.CurrentUserName;
            dr.FirstName = txtFirstName.Text;
            dr.MiddleName = "";
            dr.LastName = txtLastName.Text;
            dr.Email = txtEmail.Text;
            dr.Address = txtAddress1.Text;
            dr.SuburbID = Convert.ToInt32(ddlSuburbs.SelectedValue);
            dr.StateID = Convert.ToInt32(ddlState.SelectedValue);
            dr.PostCode = Convert.ToInt32(txtPostCode.Text);
            dr.PhoneNumber = txtContactNumber.Text;
            dr.MobileNumber = txtMobile.Text;


            dr.AltTitle = Convert.ToInt32(ddlTitle0.SelectedValue);
            if (txtFirstName0.Text == "First Name" || chkUseSecondaryContact.Checked)
                dr.AltFirstName = "";
            else dr.AltFirstName = txtFirstName0.Text;
            dr.AltMiddleName = "";


            if (txtLastName0.Text == "Last Name" || chkUseSecondaryContact.Checked)
                dr.AltLastName = "";
            else dr.AltLastName = txtLastName0.Text;
            if (!chkUseSecondaryContact.Checked)
            {
                dr.AltEmail = txtEmailalt.Text;
                dr.AltSuburbID = Convert.ToInt32(ddlSuburbs.SelectedValue);
                dr.AltStateID = Convert.ToInt32(ddlState.SelectedValue);
                dr.AltPostCode = Convert.ToInt32(txtPostCode.Text);
                dr.AltPhoneNumber = txtContactNumber0.Text;
                dr.AltMobileNumber = txtMobile0.Text;
                dr.AltAddress = txtAddress1.Text;
            }
            else
            {
                dr.AltAddress = dr.AltEmail = dr.AltPhoneNumber = dr.AltMobileNumber = "";
                dr.AltSuburbID = dr.AltStateID = dr.AltPostCode = 0;
            }


            return dr;
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckTitle();
        }
        private void CheckTitle()
        {
            if (ddlTitle.SelectedValue == "0")
            {
                txtFirstName_TextBoxWatermarkExtender.WatermarkText = "Name";
                txtLastName.Text = "";
                txtLastName.Visible = false;
                txtFirstName.Width = Unit.Pixel(300);
            }
            else
            {
                txtFirstName_TextBoxWatermarkExtender.WatermarkText = "Given Name";
                txtLastName.Visible = true;
                txtFirstName.Width = Unit.Pixel(150);
            }
        }
    }
}


