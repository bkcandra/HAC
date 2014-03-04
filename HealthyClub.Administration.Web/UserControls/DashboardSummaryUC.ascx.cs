using Google.GData.Analytics;
using HealthyClub.Administration.DA;
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
    public partial class DashboardSummaryUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }
        public string VisitsNumber()
        {

            string visits = string.Empty;
            string username = "healthyaustraliaclub@gmail.com";
            string pass = "healthyclub3121";
            string gkey = "?key=AIzaSyCYI0zTx4iGzL4gMqaplbd1TfkF9vMukZs";

            string dataFeedUrl = "https://www.google.com/analytics/feeds/data" + gkey;
            string accountFeedUrl = "https://www.googleapis.com/analytics/v2.4/management/accounts" + gkey;

            Google.GData.Analytics.AnalyticsService service = new Google.GData.Analytics.AnalyticsService("HealthyAustraliaClub");
            service.setUserCredentials(username, pass);

            DataQuery query1 = new DataQuery(dataFeedUrl);

            query1.Ids = "ga:76315108";
            query1.Metrics = "ga:visits";
            query1.Sort = "ga:visits";

            query1.GAStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            query1.GAEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            query1.StartIndex = 1;

            DataFeed dataFeedVisits = service.Query(query1);

            foreach (DataEntry entry in dataFeedVisits.Entries)
            {
                string st = entry.Title.Text;
                string ss = entry.Metrics[0].Value;
                visits = ss;
            }

            return visits;
        }

        private void Refresh()
        {
            if (WebSecurity.IsAuthenticated)
            {
                initDash();
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        private void initDash()
        {
            AdministrationDAC dac = new AdministrationDAC();

            int actCount = dac.RetrieveActivitiesCount((int)SystemConstants.ActivityStatus.Active, true) + dac.RetrieveActivitiesCount((int)SystemConstants.ActivityStatus.WillExpire, true);
            lblApprovedActivity.Text = actCount.ToString();
            lblDeletedAct.Text = dac.RetrieveActivitiesCount((int)SystemConstants.ActivityStatus.Deleting, true).ToString();
            lblTotalActivity.Text = dac.RetrieveActivitiesCount().ToString();
            //lblApprovedActivity.Text = dac.RetrieveApprovedActivitiesCount().ToString();
            lblCat.Text = dac.RetrieveCategoriesCount().ToString();
            lblMember.Text = dac.RetrieveCustomerListCount("").ToString();
            lblProviders.Text = dac.RetrieveProviderListCount("").ToString();
            lblWaitingActivity.Text = dac.RetrievePendingActivitiesCount().ToString();
            lblExpiredAct.Text = dac.RetrieveActivitiesCount((int)SystemConstants.ActivityStatus.Expired, true).ToString();

            try
            {
                lblstat.Text = VisitsNumber();
            }
            catch (Exception ex)
            {
                lblstat.Text = "ERR";
                lblerror.Text = ex.Message;
            }
        }
    }
}