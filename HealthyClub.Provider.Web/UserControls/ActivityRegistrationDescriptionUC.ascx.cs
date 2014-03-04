using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.DA;
using HealthyClub.Provider.EDS;
using HealthyClub.Utility;


namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ActivityRegistrationDescription : System.Web.UI.UserControl
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
                if (!string.IsNullOrEmpty(hdnActivityID.Value))
                    return Convert.ToInt32(hdnActivityID.Value);
                else return 0;
            }
            set
            {
                hdnActivityID.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(UpdatePaneldesc, UpdatePaneldesc.GetType(), "load()", "load()", true);
            ddlFeesCategory.Attributes.Add("title", "Choose activity requirements");
            ddlFeesCategory.Items[1].Attributes.Add("title", "No requirements to attend the activity,This activity is a free activity");
            ddlFeesCategory.Items[2].Attributes.Add("title", "No requirements to attend the activity,This activity is a paid activity ");
            ddlFeesCategory.Items[3].Attributes.Add("title", "there are requirements that must be met if customer wants to attend this activity, this activity is a free activity");
            ddlFeesCategory.Items[4].Attributes.Add("title", "there are requirements that must be met if you want to attend this activity,this activity is a paid activity");
        }

        internal void Refresh()
        {
            ClearField();
            if (EditMode)
            {
                setActivityDescription();
            }
        }
        /*
        public void SetActivityInformation()
        {
            if (EditMode)
            {
                var dr = new ProviderDAC().RetrieveActivityExplorer(ActivityID);
                ddlCategorylvl2.Visible = true;
                if (dr.CategoryLevel1ParentID != 0)
                {
                    ddlCategorylvl1.SelectedValue = dr.CategoryLevel1ParentID.ToString();
                    CheckCategorylvl1();
                    ddlCategorylvl2.SelectedValue = dr.CategoryID.ToString();
                }
                else
                    ddlCategorylvl1.SelectedValue = dr.CategoryID.ToString();

                ddlFeesCategory.SelectedValue = dr.ActivityType.ToString();
                SetEligibilityView();
                txtEligibility.Text = dr.eligibilityDescription;
                txtFee.Text = dr.Price;
                if (dr.isMembershipRequired == null)
                    dr.isMembershipRequired = false;
                radIsMemberReqYes.Checked = dr.isMembershipRequired;
                radIsMemberReqNo.Checked = !dr.isMembershipRequired;

                if (dr.isMembershipRequired == null)
                    dr.isMembershipRequired = true;
                radCommenceYes.Checked = dr.isCommenceAnytime;
                radCommenceNo.Checked = !dr.isCommenceAnytime;
            }
        }
        */
        private void setActivityDescription()
        {
            var dr = new ProviderDAC().RetrieveActivity(ActivityID);
            txtFullDesc.Text = dr.FullDescription;

            List<ListItem> SelectedCat = new List<ListItem>();
            if (dr.CategoryID != 0)
            {
                var Cat = new ProviderDAC().RetrieveCategory(dr.CategoryID);
                ListItem cat1 = new ListItem(Cat.Name, Cat.ID.ToString());
                SelectedCat.Add(cat1);
            }
            if (dr.SecondaryCategoryID1 != 0)
            {
                var Cat = new ProviderDAC().RetrieveCategory(dr.SecondaryCategoryID1);
                ListItem cat2 = new ListItem(Cat.Name, Cat.ID.ToString());
                SelectedCat.Add(cat2);
            }

            if (dr.SecondaryCategoryID2 != 0)
            {
                var Cat = new ProviderDAC().RetrieveCategory(dr.SecondaryCategoryID2);
                ListItem cat3 = new ListItem(Cat.Name, Cat.ID.ToString());
                SelectedCat.Add(cat3);
            }
            if (dr.SecondaryCategoryID3 != 0)
            {
                var Cat = new ProviderDAC().RetrieveCategory(dr.SecondaryCategoryID3);
                ListItem cat4 = new ListItem(Cat.Name, Cat.ID.ToString());
                SelectedCat.Add(cat4);
            }

            if (dr.SecondaryCategoryID4 != 0)
            {
                var Cat = new ProviderDAC().RetrieveCategory(dr.SecondaryCategoryID4);
                ListItem cat5 = new ListItem(Cat.Name, Cat.ID.ToString());
                SelectedCat.Add(cat5);
            }
            ActivityRegistrationCategory1.setSelectedCategory(SelectedCat);

            //set fees category
            ddlFeesCategory.SelectedValue = dr.ActivityType.ToString();
            SetEligibilityView();
            if (dr.ActivityType == (int)SystemConstants.ActivityFeeCategory.Private_Paid || dr.ActivityType == (int)SystemConstants.ActivityFeeCategory.Public_Paid)
            {
                txtFee.Text = dr.Price;
            }
            if (dr.ActivityType == (int)SystemConstants.ActivityFeeCategory.Private_Paid || dr.ActivityType == (int)SystemConstants.ActivityFeeCategory.Private_Free)
            {
                txtEligibility.Text = dr.eligibilityDescription;
            }

            radCommenceYes.Checked = dr.isCommenceAnytime;
            radCommenceNo.Checked = !dr.isCommenceAnytime;
            radIsMemberReqYes.Checked = dr.isMembershipRequired;
            radIsMemberReqNo.Checked = !dr.isMembershipRequired;
        }

        private void ClearField()
        {
            txtFullDesc.Text = "";
        }

        internal void getActivityDetails(out string shortDescription, out string FullDescription)
        {

            shortDescription = "";
            FullDescription = txtFullDesc.Text;
        }

        protected void ddlFeesCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEligibilityView();
        }

        private void SetEligibilityView()
        {
            if (Convert.ToInt32(ddlFeesCategory.SelectedValue) == (int)SystemConstants.ActivityFeeCategory.Private_Paid || Convert.ToInt32(ddlFeesCategory.SelectedValue) == (int)SystemConstants.ActivityFeeCategory.Public_Paid)
            {
                tableFees.Visible = true;
                if (Convert.ToInt32(ddlFeesCategory.SelectedValue) == (int)SystemConstants.ActivityFeeCategory.Private_Paid)
                {
                    //trEligibility.Visible = true;
                    trEligibility1.Visible = true;
                }
                else
                {
                    //trEligibility.Visible = false;
                    trEligibility1.Visible = false;
                }
            }
            else
            {
                if (Convert.ToInt32(ddlFeesCategory.SelectedValue) == (int)SystemConstants.ActivityFeeCategory.Private_Free)
                {
                    //trEligibility.Visible = true;
                    trEligibility1.Visible = true;
                }
                else
                {
                    //trEligibility.Visible = false;
                    trEligibility1.Visible = false;
                }
                tableFees.Visible = false;
            }
        }


        public ProviderEDSC.ActivityDTRow getDetails(ProviderEDSC.ActivityDTRow dr)
        {
            dr.ActivityType = Convert.ToInt32(ddlFeesCategory.SelectedValue);
            dr.eligibilityDescription = txtEligibility.Text;
            dr.Price = txtFee.Text;
            dr.isApproved = false;
            dr.isCommenceAnytime = radCommenceYes.Checked;
            dr.isMembershipRequired = radIsMemberReqYes.Checked;


            var selectedCat = ActivityRegistrationCategory1.GetSelectedCategory();
            for (int i = 1; i <= selectedCat.Count; i++)
            {
                if (i == 1)
                {
                    dr.CategoryID = Convert.ToInt32(selectedCat[0].Value);
                    dr.SecondaryCategoryID1 = 0;
                    dr.SecondaryCategoryID2 = 0;
                    dr.SecondaryCategoryID3 = 0;
                    dr.SecondaryCategoryID4 = 0;
                }
                else if (i == 2)
                    dr.SecondaryCategoryID1 = Convert.ToInt32(selectedCat[1].Value);
                else if (i == 3)
                    dr.SecondaryCategoryID2 = Convert.ToInt32(selectedCat[2].Value);
                else if (i == 4)
                    dr.SecondaryCategoryID3 = Convert.ToInt32(selectedCat[3].Value);
                else if (i == 5)
                    dr.SecondaryCategoryID4 = Convert.ToInt32(selectedCat[4].Value);
            }



            return dr;
        }

        public void CheckValid(out bool Error, out string ErrorText)
        {
            ErrorText = "";
            Error = false;

            var selectedCat = ActivityRegistrationCategory1.GetSelectedCategory();
            if (selectedCat.Count == 0)
            {
                Error = true;
                ErrorText += SystemConstants.ErrorActivityCategoryNotSet + "<br />";
            }
            if (ddlFeesCategory.SelectedValue == "0")
            {
                Error = true;
                ErrorText += SystemConstants.ErrorActivityTypeNotSet + "<br />";
            }
            if (ddlFeesCategory.SelectedValue == ((int)SystemConstants.ActivityFeeCategory.Private_Paid).ToString() || ddlFeesCategory.SelectedValue == ((int)SystemConstants.ActivityFeeCategory.Public_Paid).ToString())
            {
                if (txtFee.Text == "")
                {
                    Error = true;
                    ErrorText += SystemConstants.FeesDescriptionIsNull + "<br />";
                }
            }
        }

    }
}