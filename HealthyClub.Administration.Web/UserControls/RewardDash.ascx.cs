using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.EDS;
using System.Web.Security;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using System.Net;
using System.Xml.Linq;
using WebMatrix.WebData;
using Google.GData.Analytics;
using System.Configuration;
using System.Data;
namespace HealthyClub.Administration.Web.UserControls
{
    public partial class RewardDash : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();

                if (!IsPostBack)
                {


                    int count = DateTime.Now.Year - 1990 + 1;
                    List<int> listYears = Enumerable.Range(1990, count).ToList();
                    for (int i = 0; i < count; i++)
                    {
                        ddlYears.Items.Add(Convert.ToString(listYears[i]));
                    }

                    ddlYears.SelectedValue = Convert.ToString(DateTime.Now.Year);

                    Refresh();
                    ddlYears_SelectedIndexChanged(null, null);
                    
                }
            }
        }


        protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] x = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            Guid userId = Guid.Empty;
            string fName = null;
            if (Session[SystemConstants.ses_FName] != null)
                fName = (String)(Session[SystemConstants.ses_FName]);

          
                var dt = new AdministrationDAC().RetrieveallVouchers();

                decimal[] y = new decimal[12];
                int allyear = 0;

                for (int i = 1; i < 13; i++)
                {
                    int redemption = 0;

                    foreach (DataRow dtr in dt.Rows)
                    {
                        DateTime date = (System.DateTime)dtr["IssueDate"];

                        int yr = date.Year;
                        if (Convert.ToString(yr).Equals(ddlYears.SelectedValue))
                        {
                            if (date.Month.Equals(i))
                            {

                                redemption++;
                                y[i - 1] = redemption;
                                
                            }
                           
                        }
                    }
                    allyear += redemption;

                }
                
                BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, Name = "Monthly Total Redemption", BarColor = "#22536e " });



                BarChart1.CategoriesAxis = string.Join(",", x);
                BarChart1.ChartTitle = string.Format("Total Number of Redemption in " + ddlYears.SelectedItem.Value + ": " + allyear);
                if (x.Length > 3)
                {
                    BarChart1.ChartWidth = (x.Length * 100).ToString();
                }
                BarChart1.Visible = ddlYears.SelectedItem.Value != "";
                BarChart1.ChartWidth = "800";

            

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

        private void initDash()
        {
            AdministrationDAC dac = new AdministrationDAC();

            int activerew = dac.RetrieveActiveRewards();
            int inactiverew = dac.RetrieveInactiveRewards();


            lblActiveRewards.Text = activerew.ToString();
            

            int rewardsexp = dac.RetrieveExpiredRewards();
            

            lblTotalRewards.Text = dac.RetrieveRewardsExplorerCount().ToString();
            lblSponsors.Text = "0";
            try
            {
                lblVisits.Text = VisitsNumber();
            }
            catch (Exception ex)
            {
                lblVisits.Text = "ERR";
                lblerror.Text = ex.Message;
            }
        
            lblredempt.Text = dac.RetrieveTotalRedempted().ToString();
            lblnever.Text = dac.Retrieveneverred().ToString();



        }


    }
}