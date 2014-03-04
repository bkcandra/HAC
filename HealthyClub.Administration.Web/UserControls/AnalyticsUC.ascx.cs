using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.GData.Analytics;




namespace HealthyClub.Administration.Web.UserControls
{
    public partial class AnalyticsUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void InitGACon()
        {
            string username = "healthyaustraliaclub@gmail.com";
            string pass = "healthyclub3121";
            string gkey = "?key=AIzaSyCYI0zTx4iGzL4gMqaplbd1TfkF9vMukZs";

            string dataFeedUrl = "https://www.google.com/analytics/feeds/data" + gkey;
            string accountFeedUrl = "https://www.googleapis.com/analytics/v2.4/management/accounts" + gkey;

            AnalyticsService service = new AnalyticsService("HealthyAustraliaClub");
            service.setUserCredentials(username, pass);

            DataQuery query1 = new DataQuery(dataFeedUrl);


            query1.Ids = "ga:12345678";
            query1.Metrics = "ga:visits";
            query1.Sort = "ga:visits";

            query1.GAStartDate = new DateTime(2012, 1, 2).ToString("yyyy-MM-dd");
            query1.GAEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            query1.StartIndex = 1;

            DataFeed dataFeedVisits = service.Query(query1);

            foreach (DataEntry entry in dataFeedVisits.Entries)
            {
                string st = entry.Title.Text;
                string ss = entry.Metrics[0].Value;
            }
        }

        private void Refresh()
        {
            try
            {
                InitGACon();
            }
            catch (Exception ex)
            {
               string error = ex.Message;
            }
        }


    }
}