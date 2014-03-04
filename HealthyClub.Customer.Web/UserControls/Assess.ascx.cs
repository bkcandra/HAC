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
using System.Configuration;
using System.Data;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class Assess : System.Web.UI.UserControl
    {
                  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                int count=DateTime.Now.Year - 1990 + 1;
                List<int> listYears = Enumerable.Range(1990, count).ToList();
                for (int i = 0; i < count; i++)
                {
                    ddlYears.Items.Add(Convert.ToString(listYears[i]));
                }

                ddlYears.SelectedValue=Convert.ToString(DateTime.Now.Year);
                
                Refresh();
                ddlYears_SelectedIndexChanged(null, null);
               
            }
        }
        
        protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] x = {"JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            Guid userId = Guid.Empty;
            string fName = null;
            if (Session[SystemConstants.ses_FName] != null)
                fName = (String)(Session[SystemConstants.ses_FName]);

            var dr = new CustomerDAC().RetrieveUserRewardDetails(fName);
            if (dr != null)
            {
                userId = dr.UserID;
                var dt = new CustomerDAC().RetrieveActAttendance(userId);
                
                decimal[] y = new decimal[12];
                decimal[] n = new decimal[12];

                for (int i = 1; i < 13; i++)
                {
                    int rpts = 0;

                    foreach (DataRow dtr in dt.Rows)
                    {
                        DateTime date = (System.DateTime)dtr["CreatedDateTime"];

                        int yr = date.Year;
                        if (Convert.ToString(yr).Equals(ddlYears.SelectedValue))
                        {
                            if (date.Month.Equals(i))
                            {
                                n[i - 1]++;
                                rpts += (System.Int32)dtr["Earnrewards"];
                                y[i - 1] = rpts;
                                
                            }
                        }

                    }
                }

                BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, Name = "Rewards   ", BarColor = "#3a8bb8" });
                BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = n, Name = "Total Number of Activities", BarColor = "#22536e " });



                BarChart1.CategoriesAxis = string.Join(",", x);
                BarChart1.ChartTitle = string.Format("Reward Distribution in " + ddlYears.SelectedItem.Value);
                if (x.Length > 3)
                {
                    BarChart1.ChartWidth = (x.Length * 100).ToString();
                }
                BarChart1.Visible = ddlYears.SelectedItem.Value != "";
                BarChart1.ChartWidth = "680";
                
            }
        
        }

        protected void Refresh()
        {

            Guid userId = Guid.Empty;
            if (WebSecurity.IsAuthenticated)
            {
                string fName = null;
                if (Session[SystemConstants.ses_FName] != null)
                    fName = (String)(Session[SystemConstants.ses_FName]);

                var dr = new CustomerDAC().RetrieveUserRewardDetails(fName);
                if (dr != null)
                {
                    RewardPts.Text = Convert.ToString(dr.RewardPoint);
                    Redeempts.Text = Convert.ToString(dr.RedeemedtPoint);
                    Bonuspts.Text = Convert.ToString(dr.BonusPoint);
                    userId = dr.UserID;

                }
            }
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.SelectParameters.Clear();
           
            ods.SelectParameters.Add("UserId", Convert.ToString(userId));
            ods.SelectMethod = "RetrieveActAttendance";
            ods.SelectCountMethod = "RetrieveActAttCount";
            ActivityAttView.DataSourceID = "ods";
            
        
        }
        protected void ActivityAttView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HyperLink actname = e.Item.FindControl("lblActname") as HyperLink;
            HiddenField Activityid = e.Item.FindControl("hdnActivityID") as HiddenField;
            actname.NavigateUrl = "~/Activity/Default?ActivitiesID="+ Activityid.Value;

        }

        protected void ActivityAttView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            
            Refresh();
            ddlYears_SelectedIndexChanged(null, null);
            
        }
        protected void ActivityAttView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            
        }
 
    }
}