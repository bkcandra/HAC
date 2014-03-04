using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.EDS;
using HealthyClub.Provider.DA;
using HealthyClub.Utility;


namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ActivityRegistrationGroup : System.Web.UI.UserControl
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

        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            tabRowAge.Visible = !chkAgeAll.Checked;
        }

        public ProviderEDSC.ActivityGroupingDTRow getActSuitability()
        {
            var dr = new ProviderEDSC.ActivityGroupingDTDataTable().NewActivityGroupingDTRow();
            dr.forMale = chkMale.Checked;
            dr.forFemale = chkFemale.Checked;
            dr.forChildren = chkChild.Checked;

            if (chkAgeAll.Checked)
            {
                dr.AgeFrom = 0;
                dr.AgeTo = 99;
            }
            else
            {
                dr.AgeFrom = Convert.ToInt32(txtOldFrom.Text);
                dr.AgeTo = Convert.ToInt32(txtOldTo.Text);
            }

            return dr;
        }

        public void CheckValid(out bool Error, out string ErrorText)
        {
            Error = false;
            ErrorText = "";

            if (!chkAgeAll.Checked)
            {
                if(string.IsNullOrEmpty(txtOldFrom.Text))
                {
                    Error=true;
                    ErrorText += SystemConstants.ErrorAgeFromisNull + "<br />";
                }
                if(string.IsNullOrEmpty(txtOldTo.Text))
                {
                    Error=true;
                    ErrorText += SystemConstants.ErrorAgeToisNull + "<br />";
                }
            }
        }

        public void SetActivityGroup()
        {
            if (EditMode)
            {
                ProviderDAC dac = new ProviderDAC();
                var dr = dac.RetrieveActivityGroup(ActivityID);
                var drdet = dac.RetrieveActivity(ActivityID);
                chkMale.Checked = dr.forMale;
                chkFemale.Checked = dr.forFemale;
                chkChild.Checked = dr.forChildren;
                if (dr.AgeFrom == 0 && dr.AgeTo == 99)
                    chkAgeAll.Checked = true;
                else
                {
                    txtOldFrom.Text = dr.AgeFrom.ToString();
                    txtOldTo.Text = dr.AgeTo.ToString();
                }
                tabRowAge.Visible = !chkAgeAll.Checked;

                if (!string.IsNullOrEmpty(drdet.Keywords))
                    txtKeyword.Text = drdet.Keywords;
            }
        }

        public string getKeywords()
        {
            string keywords = "";
            if (!string.IsNullOrEmpty(txtKeyword.Text))
                keywords = txtKeyword.Text;

            return keywords;
        }

      

    }
}