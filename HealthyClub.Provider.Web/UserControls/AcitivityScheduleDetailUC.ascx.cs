using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using HealthyClub.Provider.EDS;
using HealthyClub.Provider.BF;
using System.Data;

namespace HealthyClub.Providers.Web.UserControls
{
    public partial class AcitivityScheduleDetailUC : System.Web.UI.UserControl
    {
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

        public ProviderEDSC.ActivityScheduleDTDataTable dt
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Refresh()
        {
            dt = new ProviderDAC().RetrieveActivitySchedules(ActivityID);
            if (dt != null)
            {
                SetdataSource();
            }
        }

        private void SetdataSource()
        {
            setTimetableGridDataSource();
            divGridView.Visible = true;
            divWeeklyView.Visible = false;
        }

        private void setTimetableGridDataSource()
        {
            ods.TypeName = typeof(ProviderBFC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("activityID", ActivityID.ToString());
            ods.SelectMethod = "RetrieveTimetableGrid";
            ods.SelectCountMethod = "RetrieveTimetableGridCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";
            var timetable = new ProviderBFC().RetrieveTimetableGrid(0,9999,ActivityID.ToString(),"");

            GridView1.DataSource = timetable;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Refresh();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetdataSource();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drView = e.Row.DataItem as DataRowView;
                ProviderEDSC.ActivityScheduleGridDTRow dr = drView.Row as ProviderEDSC.ActivityScheduleGridDTRow;

                Label lblDay = e.Row.FindControl("lblDay") as Label;
                Label lblTime = e.Row.FindControl("lblTime") as Label;
                Label lblDate = e.Row.FindControl("lblDate") as Label;

                if (dr.StartDateTime.Date == dr.EndDateTime.Date)
                {
                    lblDate.Text = dr.StartDateTime.Date.ToShortDateString();
                }
                else
                {
                    lblDate.Text = dr.StartDateTime.Date.ToShortDateString() + " - " + dr.EndDateTime.Date.ToShortDateString();
                }
                lblDay.Text = dr.StartDateTime.DayOfWeek.ToString();
                lblTime.Text = dr.StartDateTime.ToShortTimeString() + " - " + dr.EndDateTime.ToShortTimeString();
            }
        }

        protected void lnkEditTimetable_Click(object sender, EventArgs e)
        {

        }
    }
}