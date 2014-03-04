using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HealthyClub.Provider.BF;
using HealthyClub.Utility;
using HealthyClub.Provider.DA;
using WebMatrix.WebData;

namespace HealthyClub.Providers.Web.Activity
{
    public partial class EditActivity : System.Web.UI.Page
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

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthUser())
                {
                    InitActivityInformation();
                }
                else
                {
                    Response.StatusCode = 404;
                    Response.Redirect("~/404.html");
                }
            }
        }

        private void InitActivityInformation()
        {

            SetActivityDetail();
            SetActivityDescription();
            SetActivityTimetable();
            SetActivityGrouping();
            SetActivityImages();

            var dr = new ProviderDAC().RetrieveActivity(ActivityID);
            btnRestore.Visible = false; BtnDelete.Visible = true;
            radActive.Enabled = radInactive.Enabled = true;
            DescriptionTabContainer.Enabled = btnSubmit.Enabled = true;
            lblStatus.Visible = false;
            if (dr.Status == (int)SystemConstants.ActivityStatus.Deleting)
            {
                radInactive.Checked = true; radActive.Checked = false;
                btnRestore.Visible = true; BtnDelete.Visible = false;
                radActive.Enabled = radInactive.Enabled = false;
                DescriptionTabContainer.Enabled =btnSubmit.Enabled= false;
                lblStatus.Visible = true;
                lblStatus.Text = "Activity will be deleted on " + dr.ModifiedDateTime.AddDays(3).ToShortDateString();
            }
            else if (dr.Status == (int)SystemConstants.ActivityStatus.Active)
            {
                
                radActive.Checked = true;
                DescriptionTabContainer.Enabled = true;
            }
            else if (dr.Status == (int)SystemConstants.ActivityStatus.NotActive)
            {
                radInactive.Checked = true;
                DescriptionTabContainer.Enabled = true;
            }
        }

        private void SetActivityImages()
        {
            ActivityImageUC1.ActivityID = ActivityID;
        }

        private void SetActivityDetail()
        {
            ActivityRegistrationDetailUC1.ActivityID = ActivityID;
            ActivityRegistrationDetailUC1.EditMode = true;
            //Category is not yet loaded,Set Activity information is moved to initRegistration
        }

        private void SetActivityDescription()
        {
            ActivityRegistrationDescriptionUC1.ActivityID = ActivityID;
            ActivityRegistrationDescriptionUC1.EditMode = true;
            ActivityRegistrationDescriptionUC1.Refresh();
        }

        private void SetActivityTimetable()
        {
            ActivityRegistrationTimetableUC1.ActivityID = ActivityID;
            ActivityRegistrationTimetableUC1.EditMode = true;
            ActivityRegistrationTimetableUC1.initTimetable();
        }

        private void SetActivityGrouping()
        {
            activityregistrationgroup1.ActivityID = ActivityID;
            activityregistrationgroup1.EditMode = true;
            activityregistrationgroup1.SetActivityGroup();
        }

        private bool AuthUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                ProviderID = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);
                var ownerLogin = new ProviderBFC().CheckActivityOwner(ActivityID, ProviderID);

                return ownerLogin;
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

            ActivityRegistrationDetailUC1.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            ActivityRegistrationDescriptionUC1.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            ActivityRegistrationTimetableUC1.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;
            activityregistrationgroup1.CheckValid(out ActivityisError, out ActivityErrors);
            ErrorMessage += ActivityErrors;
            if (ActivityisError)
                isError = ActivityisError;

            lblError.Text = ErrorMessage;
            return isError;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isNotValid = !checkValid();
            if (isNotValid)
            {
                //drActivity
                var drDetail = ActivityRegistrationDetailUC1.getDetails();
                drDetail.Status = (int)SystemConstants.ActivityStatus.Active;
                string shortDescription = "";
                string fullDescription = "";

                ActivityRegistrationDescriptionUC1.getActivityDetails(out shortDescription, out fullDescription);
                drDetail = ActivityRegistrationDescriptionUC1.getDetails(drDetail);
                drDetail.ShortDescription = "";
                drDetail.FullDescription = fullDescription;
                DateTime activityExpiryDate = DateTime.Today;
                bool usingTimetable;
                ActivityRegistrationTimetableUC1.getExpiry(out activityExpiryDate, out usingTimetable);
                drDetail.ExpiryDate = activityExpiryDate;
                drDetail.ProviderID = ProviderID;
                if (usingTimetable)
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.Datagrid;
                else
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.noTimetable;

                //drActivityContact
                var contactDetails = ActivityRegistrationDetailUC1.getContactDetail();

                //dtSchedule
                var dtActSchedule = ActivityRegistrationTimetableUC1.getTimetable(false);

                //drGrouping
                var drActGrouping = activityregistrationgroup1.getActSuitability();
                drDetail.Keywords = activityregistrationgroup1.getKeywords();
                int actID;


                /*//drActivity
                var drDetail = ActivityRegistrationDetailUC1.getDetails();
                drDetail.Status = (int)SystemConstants.ActivityStatus.Active;
                string shortDescription = "";
                string fullDescription = "";
                ActivityRegistrationDescriptionUC1.getActivityDetails(out shortDescription, out fullDescription);
                drDetail.ShortDescription = shortDescription;
                drDetail.FullDescription = fullDescription;
                DateTime activityExpiryDate = DateTime.Today;
                bool usingtimetable;
                ActivityRegistrationTimetableUC1.getExpiry(out activityExpiryDate, out usingtimetable);
                drDetail.ExpiryDate = activityExpiryDate;
                drDetail.ProviderID = ProviderID;
                if (usingtimetable)
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.Datagrid;
                else
                    drDetail.TimetableType = (int)SystemConstants.ScheduleViewFormat.noTimetable;

                //drActivityContact
                var contactDetails = ActivityRegistrationDetailUC1.getContactDetail();

                //drGrouping
                var drActGrouping = ActivityRegistrationGroup1.getActSuitability();
                string keywords = ActivityRegistrationGroup1.getKeywords();
                drDetail.Keywords = keywords;

                drDetail.ID = contactDetails.ActivityID = drActGrouping.ActivityID = ActivityID;

                //dtSchedule
                */

                foreach (var drActSchedule in dtActSchedule)
                {
                    drActSchedule.ActivityID = ActivityID;
                }
                ProviderBFC.UpdateActivity(ActivityID, drDetail, contactDetails, drActGrouping, dtActSchedule);

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

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            new ProviderDAC().ChangeStatus(ActivityID, (int)SystemConstants.ActivityStatus.NotActive, WebSecurity.CurrentUserName);
            InitActivityInformation();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string Username = WebSecurity.CurrentUserName;
            if (string.IsNullOrEmpty(Username))
                Username = "ERR_GETUSR";

            if (ActivityID != 0 || ActivityID != -1)
            {
                //new ProviderDAC().DeleteActivity(actID);
                new ProviderDAC().ChangeStatus(ActivityID, (int)SystemConstants.ActivityStatus.Deleting, Username);
            }
            InitActivityInformation();
        }
    }
}