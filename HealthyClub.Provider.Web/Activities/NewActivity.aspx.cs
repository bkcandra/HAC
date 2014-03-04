using BCUtility;
using HealthyClub.Provider.BF;
using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Provider.Web.Activities
{
    public partial class NewActivity : System.Web.UI.Page
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

        public string ActionKey
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnActionCode.Value))
                    return hdnActionCode.Value;
                else return "ABCDE123";
            }
            set
            {
                hdnActionCode.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitRegistration();
                


            }
        }

        protected void Page_init(object sender, EventArgs e)
        {

        }

        private void InitRegistration()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                ActionKey = ObjectHandler.GetRandomKey(8);
                ProviderID = new MembershipHelper().GetProviderUserKey(WebSecurity.CurrentUserId);
                ActivityRegistrationDetailUC.InitRegistration();
                ActivityRegistrationTimetableUC.initTimetable();
                ActivityRegistrationImageUC.initUploader(ProviderID, ActionKey);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblError.Visible = divError.Visible = false;
            bool isnotValid = checkValid();
            if (!isnotValid)
            {
                //drActivity
                var drDetail = ActivityRegistrationDetailUC.getDetails();
                drDetail.Status = (int)SystemConstants.ActivityStatus.Active;
                string shortDescription = "";
                string fullDescription = "";

                ActivityRegistrationDescriptionUC.getActivityDetails(out shortDescription, out fullDescription);
                drDetail = ActivityRegistrationDescriptionUC.getDetails(drDetail);
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

                //drImage
                var drImageDetail = ActivityRegistrationImageUC.GetImageDetail();
                var dtImages = ActivityRegistrationImageUC.GetImages();

                ProviderBFC.SaveActivity(drDetail, contactDetails, drActGrouping, dtActSchedule, drImageDetail, dtImages, out actID);


                Response.Redirect("~/Activities/");
            }
            else
            {
                lblError.Visible = divError.Visible = true;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Activities/");
        }
    }
}