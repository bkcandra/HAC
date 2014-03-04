using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Administration.Web.Activities
{
    public partial class Activity : System.Web.UI.Page
    {
        public string urlReferrer
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnReferrer.Value))
                    return hdnReferrer.Value;
                else return "~";
            }
            set
            {
                hdnReferrer.Value = value.ToString();
            }
        }

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

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.ActivityID] != null)
            {
                ActivityID = Convert.ToInt32(Request.QueryString[SystemConstants.ActivityID]);
            }
            if (Request.UrlReferrer != null)
            {
                urlReferrer = Request.UrlReferrer.ToString();
            }
            hdnIsApproved.Value = "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthUser())
                {
                    InitActivityInformation();
                }
            }
        }

        private void InitActivityInformation()
        {
            var dr = new AdministrationDAC().RetrieveActivity(ActivityID);
            ProviderID = dr.ProviderID;
            RadYes.Checked = dr.isApproved;
            RadNo.Checked = !dr.isApproved;
            SetActivityDetail();
            SetActivityDescription();
            SetActivityTimetable(dr.TimetableType);
            SetActivityGrouping();
            SetActivityImages();
        }

        private void SetActivityImages()
        {
            ActivityImageUC.ActivityID = ActivityID;
        }

        private void SetActivityDetail()
        {

            ActivityRegistrationDetailUC.ActivityID = ActivityID;
            ActivityRegistrationDetailUC.EditMode = true;

        }

        private void SetActivityDescription()
        {
            ActivityRegistrationDescriptionUC.ActivityID = ActivityID;
            ActivityRegistrationDescriptionUC.EditMode = true;
            ActivityRegistrationDescriptionUC.Refresh();
        }

        private void SetActivityTimetable(int TimetableType)
        {
            ActivityRegistrationTimetableUC.ActivityID = ActivityID;
            ActivityRegistrationTimetableUC.EditMode = true;
            ActivityRegistrationTimetableUC.initTimetable(TimetableType);
        }

        private void SetActivityGrouping()
        {
            ActivityRegistrationGroup.ActivityID = ActivityID;
            ActivityRegistrationGroup.EditMode = true;
            ActivityRegistrationGroup.SetActivityGroup();
        }

        private bool AuthUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
                return false;
            }
        }

        private bool checkValid()
        {
            bool isError = false;
            bool ActivityisError = false;
            String ActivityErrors = "";
            String ErrorMessage = "";

            ActivityRegistrationDetailUC.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            ActivityRegistrationDescriptionUC.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            ActivityRegistrationTimetableUC.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            ActivityRegistrationGroup.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;

            lblError.Text = ErrorMessage;
            return isError;
        }

        private void Save()
        {
            bool isNotValid = !checkValid();
            if (isNotValid)
            {
                //drActivity
                var drDetail = ActivityRegistrationDetailUC.getDetails();
                drDetail.Status = (int)SystemConstants.ActivityStatus.Active;
                string shortDescription = "";
                string fullDescription = "";

                ActivityRegistrationDescriptionUC.getActivityDetails(out shortDescription, out fullDescription);
                drDetail = ActivityRegistrationDescriptionUC.getDetails(drDetail);
                drDetail.isApproved = RadYes.Checked;
                drDetail.ShortDescription = "";
                drDetail.FullDescription = fullDescription;
                DateTime activityExpiryDate = DateTime.Today;
                bool usingTimetable;
                ActivityRegistrationTimetableUC.getExpiry(out activityExpiryDate, out usingTimetable);
                drDetail.ExpiryDate = activityExpiryDate;
                drDetail.ProviderID = ProviderID;
                if (usingTimetable)
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.Datagrid;
                else
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.noTimetable;

                //drActivityContact
                var contactDetails = ActivityRegistrationDetailUC.getContactDetail();

                //dtSchedule
                var dtActSchedule = ActivityRegistrationTimetableUC.getTimetable(false);

                //drGrouping
                var drActGrouping = ActivityRegistrationGroup.getActSuitability();
                drDetail.Keywords = ActivityRegistrationGroup.getKeywords();
                int actID;

                foreach (var drActSchedule in dtActSchedule)
                {
                    drActSchedule.ActivityID = ActivityID;
                }
                AdministrationBFC.UpdateActivity(ActivityID, drDetail, contactDetails, drActGrouping, dtActSchedule);

                Response.Redirect("~/Activities");
            }
            else
            {
                lblError.Visible = divError.Visible = true;
            }
        }

      

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(urlReferrer);
        }

        protected void hdnIsApproved_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}