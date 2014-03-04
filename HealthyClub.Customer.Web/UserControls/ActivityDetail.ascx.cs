using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Customer.EDS;
using System.Web.Security;
using HealthyClub.Customer.BF;
using HealthyClub.Customer.DA;
using System.Net;
using System.Xml.Linq;
using WebMatrix.WebData;
using Segmentio;
using Segmentio.Model;



namespace HealthyClub.Web.UserControls
{
    public partial class ActivityDetail : System.Web.UI.UserControl
    {
        public string Address
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAddress.Value))
                    return hdnAddress.Value;
                else return "";
            }
            set
            {
                hdnAddress.Value = value.ToString();
                setGMapLoc();
            }
        }

        private void setGMapLoc()
        {
            double Lat = GetCoordinates(Address).Latitude;
            double Lng = GetCoordinates(Address).Longitude;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "setGMap", "FindLocation('" + Lat + "','" + Lng + "','" + Address + "')", true);

        }

        static string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        string location = string.Empty;

        public static Coordinate GetCoordinates(string Address)
        {
            using (var client = new WebClient())
            {
                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(Address));

                var request = WebRequest.Create(requestUri);

                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());
                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");


                return new Coordinate(Convert.ToDouble(lat.Value), Convert.ToDouble(lng.Value));
            }
        }

        public struct Coordinate
        {
            private double lat;
            private double lng;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lng = longitude;

            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lng; } set { lng = value; } }

        }

        //Reverse Gecoding
        public static void RetrieveFormatedAddress(string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);

            using (WebClient wc = new WebClient())
            {
                string result = wc.DownloadString(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants() where elm.Name == "status" select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants() where elm.Name == "formatted_address" select elm).FirstOrDefault();
                    requestUri = res.Value;
                }
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
            if (!IsPostBack)
            {
                if (Page.RouteData.Values[SystemConstants.qs_ActivitiesID] != null)
                    ActivityID = Convert.ToInt32(Page.RouteData.Values[SystemConstants.qs_ActivitiesID].ToString());
                if (Request.QueryString[SystemConstants.qs_ActivitiesID] != null)
                    ActivityID = Convert.ToInt32(Request.QueryString[SystemConstants.qs_ActivitiesID]);
                if (ActivityID != 0)
                {
                    var dr = new CustomerDAC().RetrieveActivityImages(ActivityID);
                    if (dr != null)
                        ImagesListViewUC1.ActivityID = ActivityID;
                    else
                        gallery.Visible = false;
                    Refresh();
                }
            }
        }

        private void Refresh()
        {
            var dr = new CustomerDAC().RetrieveActivityExplorer(ActivityID);
            if (dr != null)
            {

                SetTitle(dr.ProviderID);
                SetActivityInformation(dr);
                SetTimetableInformation();
                ActivityNavigationUC1.SetNavigation(dr.Name, dr.ActivityID, dr.CategoryID, dr.CategoryName);
            }
            else
            {
                Response.Redirect("~/Activities");
            }
        }

        private void SetTitle(Guid providerID)
        {


            CustomerDAC dac = new CustomerDAC();

            if (dac.IsUserImageExist(providerID))
            {
                divWithImage.Visible = true;
                divNoImage.Visible = false;
                int ImageID = new CustomerBFC().getProviderPrimaryImage(providerID);
                if (ImageID != 0)
                    ProviderImage.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_UserImageID + "=" + ImageID;
                else
                {
                    divWithImage.Visible = false;
                    divNoImage.Visible = true;
                }
            }
            else
            {
                divWithImage.Visible = false;
                divNoImage.Visible = true;
            }

        }

        private void SetTimetableInformation()
        {
            ScheduleViewerUC1.ActivityID = Convert.ToInt32(hdnActivityID.Value);
            ScheduleViewerUC1.timetableFormat = (int)SystemConstants.TimetableFormat.Seasonal;
        }

        private void SetActivityInformation(CustomerEDSC.v_ActivityExplorerDTRow dr)
        {
            divProductDesc.InnerHtml = dr.FullDescription;
            if (!string.IsNullOrEmpty(dr.Price))
            {
                divPriceDescription.InnerHtml = dr.Price;
            }
            else
            {
                divPriceDescription.InnerHtml = "This is a free activity";
            }
            if (!string.IsNullOrEmpty(dr.eligibilityDescription))
            {
                divEligivibility.InnerHtml = dr.eligibilityDescription;
            }

            trSuitability.Visible = !string.IsNullOrEmpty(dr.eligibilityDescription);
            lblContact.Text = dr.PhoneNumber;
            hlnkWebsite.Text = dr.Website;
            hlnkWebsite.NavigateUrl = "http://" + dr.Website;
            lblUpdate.Text = dr.ModifiedDateTime.Date.ToShortDateString();
            if (!string.IsNullOrEmpty(dr.Suburb))
            {
                lblAddress.Text = dr.Address;
                lblSub.Text = dr.Suburb + ",";
                lblState.Text = dr.StateName;
                lblPostCode.Text = dr.PostCode.ToString();
            }
            else
            {
                lblAddress.Text = SystemConstants.ErrorAddressnotGiven;
                lblSub.Visible = lblState.Visible = lblPostCode.Visible = false;
            }
            lblProvider.Text = lblProviderWImage.Text = dr.ProviderName;
            lblTitle.Text = lblTitleWImage.Text = dr.Name;
            lblContactName.Text = dr.FirstName + " " + dr.LastName;
            lblEmailAddress.Text = dr.Email;
            Address = dr.Address + " " + dr.Suburb + " " + dr.StateName + " " + dr.PostCode;

            Page.Title = dr.ProviderName + " - " + dr.Name + ", Healthy Australia Club";

            trAddress.Visible = !String.IsNullOrEmpty(Address);
            trCP.Visible = !String.IsNullOrEmpty(lblContactName.Text);
            trPhone.Visible = !String.IsNullOrEmpty(lblContact.Text);
            trEmail.Visible = !String.IsNullOrEmpty(dr.Email);
            trWebsite.Visible = !String.IsNullOrEmpty(dr.Website);
            trDescription.Visible = !String.IsNullOrEmpty(dr.FullDescription);

        }

        private bool AuthUser()
        {
            if (Membership.GetUser() != null)
            {
                var providerID = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                var ownerLogin = new CustomerBFC().CheckActivityOwner(ActivityID, providerID);

                return ownerLogin;
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
                return false;
            }
        }

        protected void lnkEditActivity_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Activity/EditActivity.aspx?" + SystemConstants.ActivityID + "=" + ActivityID);
        }
    }
}