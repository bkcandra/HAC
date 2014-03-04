using BCUtility;
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
using WebMatrix.WebData;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class SponsorManage : System.Web.UI.UserControl
    {
        public int FormMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFormMode.Value))
                {
                    return Convert.ToInt32(hdnFormMode.Value);
                }
                return (int)HealthyClub.Utility.SystemConstants.FormMode.View;
            }
            set
            {
                if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.New)
                {
                    lblspnName.Visible = false;
                    lbladdress.Visible = false;
                    lblCalendarFrom.Visible = false;
                    lblwebsite.Visible = false;
                    lblnumber.Visible = false;

                    txtspnName.Visible = true;
                    txtaddress.Visible = true;
                    txtCalendarFrom.Visible = true;
                    txtwebsite.Visible = true;
                    txtnumber.Visible = true;

                    lnkAdd.Visible = true;
                    lnkEdit.Visible = false;
                    lnkUpdate.Visible = false;
                    
                }
                else if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.Edit)
                {
                    lblspnName.Visible = false;
                    lbladdress.Visible = false;
                    lblCalendarFrom.Visible = false;
                    lblwebsite.Visible = false;
                    lblnumber.Visible = false;
                    
                    txtspnName.Visible = true;
                    txtaddress.Visible = true;
                    txtCalendarFrom.Visible = true;
                    txtwebsite.Visible = true;
                    txtnumber.Visible = true;

                    lnkAdd.Visible = false;
                    lnkEdit.Visible = false;
                    lnkUpdate.Visible = true;
                    
                }
                else if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.View)
                {
                    lblspnName.Visible = true;
                    lbladdress.Visible = true;
                    lblCalendarFrom.Visible = true;
                    lblwebsite.Visible = true;
                    lblnumber.Visible = true;
                    
                  

                    txtspnName.Visible = false;
                    txtaddress.Visible = false;
                    txtCalendarFrom.Visible = false;
                    txtwebsite.Visible = false;
                    txtnumber.Visible = false;

                    lnkAdd.Visible = false;
                    lnkEdit.Visible = true;
                    lnkUpdate.Visible = false;
                    
                }
                hdnFormMode.Value = value.ToString();
            }

        }

        public string SponsorID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSponsorID.Value))
                    return Convert.ToString(hdnSponsorID.Value);
                else return null;
            }
            set
            {
                hdnSponsorID.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Request.QueryString[SystemConstants.qs_SponsorsID] != null)
                {
                    FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.View;
                    SponsorID = Request.QueryString[SystemConstants.qs_SponsorsID];
                    setSponsorsInformation();
                }
                else
                       FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.New;
                
            }
        }
            
        public void setSponsorsInformation()
        {
            Guid Spnid= new Guid(SponsorID);
            var dr = new AdministrationDAC().RetrieveSponsorDetails(Spnid);
            if (dr != null)
            {
                lblspnName.Text = txtspnName.Text=dr.Name;
                lbladdress.Text = txtaddress.Text = dr.Address;
                lblCalendarFrom.Text = txtCalendarFrom.Text = Convert.ToString(dr.ContractExpiry);
                lblwebsite.Text = txtwebsite.Text = dr.Website;
                lblnumber.Text =  txtnumber.Text = Convert.ToString(dr.PhoneNumber);

            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.Edit;
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.SponsorDTRow sr = GetDetails();
            Guid sponsorid = new Guid(SponsorID);
            sr.ID = sponsorid;
            new AdministrationDAC().UpdateSponsor(sr);
            setSponsorsInformation();
            FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.View;
            addeddiv.Visible = true;
            rewardadded.Text = "Sponsor details are Updated";
        }

        public AdministrationEDSC.SponsorDTRow GetDetails()
        { 
            var sr = new AdministrationEDSC.SponsorDTDataTable().NewSponsorDTRow();
            sr.Name = txtspnName.Text;
            sr.Address = txtaddress.Text;
            sr.Website = txtwebsite.Text;
            sr.PhoneNumber = Convert.ToInt64(txtnumber.Text);
            sr.Status = 1;
            sr.ContractExpiry = Convert.ToDateTime(txtCalendarFrom.Text);
            
            return sr;
        
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.SponsorDTRow sr = GetDetails();
            Guid spnid = Guid.NewGuid();
            sr.ID = spnid;
            new AdministrationDAC().SaveSponsorDetail(sr);
            setSponsorsInformation();
            FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.View;
            addeddiv.Visible = true;
            rewardadded.Text = "Sponsor is Added";
        }

    }
}