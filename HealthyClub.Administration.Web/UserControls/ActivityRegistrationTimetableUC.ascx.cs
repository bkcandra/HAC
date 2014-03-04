using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;

using System.Data;
using HealthyClub.Utility;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityRegistrationTimetableUC : System.Web.UI.UserControl
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

        public AdministrationEDSC.ActivityScheduleGridDTDataTable timeTableDT
        {
            get;
            set;
        }

        public AdministrationEDSC.ActivityScheduleDetailDTDataTable ExceptionDT
        {
            get;
            set;
        }

        public AdministrationEDSC.ActivityScheduleDTDataTable ScheduleDT
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void initTimetable(int TimetableType)
        {
            SetTimeDDL();
            if (TimetableType == (int)SystemConstants.ScheduleViewFormat.noTimetable)
            {
                radEnableTimetable.Checked = false;
                radNoTimetable.Checked = true;
                divTimetable.Visible = false;
            }
            else
            {
                if (EditMode)
                {
                    ScheduleDT = new AdministrationDAC().RetrieveActivitySchedules(ActivityID);
                }
                SetScheduleDataSource();
                SetTimetableDataSource();
            }

        }

        private void SetTimeDDL()
        {

            //ddlFrom

            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":00 AM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":15 AM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":30 AM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":45 AM"));
            for (int hour = 1; hour <= 11; hour++)
            {
                for (int minutes = 1; minutes <= 4; minutes++)
                {
                    if (minutes == 1)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":00 AM"));
                    else if (minutes == 2)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":15 AM"));
                    if (minutes == 3)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":30 AM"));
                    else if (minutes == 4)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":45 AM"));
                }
                //ddlTimeStart.SelectedIndex = 1;
            }
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":00 PM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":15 PM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":30 PM"));
            ddlTimeStart.Items.Add(new ListItem((12).ToString("00") + ":45 PM"));
            for (int hour = 1; hour <= 11; hour++)
            {
                for (int minutes = 1; minutes <= 4; minutes++)
                {
                    if (minutes == 1)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":00 PM"));
                    else if (minutes == 2)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":15 PM"));
                    if (minutes == 3)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":30 PM"));
                    else if (minutes == 4)
                        ddlTimeStart.Items.Add(new ListItem((hour).ToString("00") + ":45 PM"));
                }
                //ddlTimeStart.SelectedIndex = 1;
            }



            //ddlTo
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":00 AM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":15 AM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":30 AM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":45 AM"));
            for (int hour = 1; hour <= 11; hour++)
            {
                for (int minutes = 1; minutes <= 4; minutes++)
                {
                    if (minutes == 1)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":00 AM"));
                    else if (minutes == 2)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":15 AM"));
                    if (minutes == 3)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":30 AM"));
                    else if (minutes == 4)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":45 AM"));
                }
                //ddlTimeStart.SelectedIndex = 1;
            }
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":00 PM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":15 PM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":30 PM"));
            ddlTimeEnds.Items.Add(new ListItem((12).ToString("00") + ":45 PM"));
            for (int hour = 1; hour <= 11; hour++)
            {
                for (int minutes = 1; minutes <= 4; minutes++)
                {
                    if (minutes == 1)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":00 PM"));
                    else if (minutes == 2)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":15 PM"));
                    if (minutes == 3)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":30 PM"));
                    else if (minutes == 4)
                        ddlTimeEnds.Items.Add(new ListItem((hour).ToString("00") + ":45 PM"));
                }
            }

            //ddlTimeEnds.SelectedIndex = 2;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        private AdministrationEDSC.ActivityScheduleDetailDTDataTable GetSelected()
        {
            AdministrationEDSC.ActivityScheduleDetailDTDataTable dt = new AdministrationEDSC.ActivityScheduleDetailDTDataTable();
            foreach (GridViewRow row in gridviewPreview.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox1 = row.FindControl("chkboxSelected") as CheckBox;
                    HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                    Label lblDate = row.FindControl("lblDate") as Label;
                    var dr = dt.NewActivityScheduleDetailDTRow();
                    if (CheckBox1.Checked)
                    {
                        dr.ID = Convert.ToInt32(hdnID.Value);
                        dr.StartDateTime = Convert.ToDateTime(lblDate);
                        dr.EndDateTime = Convert.ToDateTime(lblDate);
                        dt.AddActivityScheduleDetailDTRow(dr);
                    }
                }
            }
            return dt;
        }

        public AdministrationEDSC.ActivityScheduleGridDTDataTable GetTimetablePreview(bool checkDelete)
        {
            AdministrationEDSC.ActivityScheduleGridDTDataTable dt = new AdministrationEDSC.ActivityScheduleGridDTDataTable();
            foreach (GridViewRow row in gridviewPreview.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    Label lblDay = row.FindControl("lblDay") as Label;
                    Label lblTime = row.FindControl("lblTime") as Label;
                    Label lblDate = row.FindControl("lblDate") as Label;
                    HiddenField hdnStartDateTime = row.FindControl("hdnStartDateTime") as HiddenField;
                    HiddenField hdnEndDateTime = row.FindControl("hdnEndDateTime") as HiddenField;
                    CheckBox chkboxSelected = row.FindControl("chkboxSelected") as CheckBox;

                    if (checkDelete)
                    {
                        if (!chkboxSelected.Checked)
                        {
                            var dr = dt.NewActivityScheduleGridDTRow();

                            dr.StartDateTime = Convert.ToDateTime(hdnStartDateTime.Value);
                            dr.EndDateTime = Convert.ToDateTime(hdnEndDateTime.Value);

                            dt.AddActivityScheduleGridDTRow(dr);
                        }
                    }
                    else if (!checkDelete)
                    {
                        var dr = dt.NewActivityScheduleGridDTRow();

                        dr.StartDateTime = Convert.ToDateTime(hdnStartDateTime.Value);
                        dr.EndDateTime = Convert.ToDateTime(hdnEndDateTime.Value);

                        dt.AddActivityScheduleGridDTRow(dr);
                    }
                }
            }
            return dt;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drView = e.Row.DataItem as DataRowView;
                AdministrationEDSC.ActivityScheduleGridDTRow dr = drView.Row as AdministrationEDSC.ActivityScheduleGridDTRow;

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

        private void SetScheduleDataSource()
        {
            gridviewTimetable.DataSource = ScheduleDT;
            gridviewTimetable.DataBind();
        }

        private void SetTimetableDataSource()
        {
            if (ScheduleDT != null)
            {
                timeTableDT = new AdministrationBFC().CalculateRecurrence(ScheduleDT);
                gridviewPreview.DataSource = timeTableDT;
                gridviewPreview.DataBind();
            }
        }

        private void SetTimetablePreview()
        {
            var dt = getTimetable(true);
            ScheduleViewerUC1.InitTimetablePreview(dt);
        }

        public void getExpiry(out DateTime activityExpiry, out bool usingTimetable)
        {
            usingTimetable = radEnableTimetable.Checked;

            if (radEnableTimetable.Checked)
            {
                ScheduleDT = getTimetable(false);
                List<DateTime> expiry = new List<DateTime>();
                foreach (var dr in ScheduleDT)
                    expiry.Add(dr.ActivityExpiryDate);

                expiry.Sort((a, b) => a.CompareTo(b));

                activityExpiry = expiry[expiry.Count() - 1];
            }
            else
            {
                activityExpiry = DateTime.Now.AddDays(90);
            }
        }

        internal void CheckValid(out bool Error, out string ErrorText)
        {
            Error = false;
            ErrorText = "";

            if (radEnableTimetable.Checked)
            {
                var dt = getTimetable(false);
                if (dt.Count == 0)
                {
                    Error = true;
                    ErrorText += SystemConstants.ErrorActivityTimetableIsZero + "<br />";
                }
                foreach (var dr in dt)
                {
                    if (dr.ActivityExpiryDate <= DateTime.Today)
                    {
                        Error = true;
                        ErrorText += SystemConstants.ErrorActivityExpired + "<br />";
                    }
                }

                if (!EditMode)
                {
                    if (ScheduleDT != null)
                        if (txtEndDate.Text == "" && ScheduleDT.Count == 0)
                        {
                            Error = true;
                            ErrorText += SystemConstants.ErrorActivityEndDateisNull + "<br />";
                        }
                }
                else if (radNoTimetable.Checked)
                {

                }
            }
        }

        private void CheckActivityDay(DayOfWeek day)
        {
            if (day == DayOfWeek.Monday)
                chkMonday.Checked = true;
            if (day == DayOfWeek.Tuesday)
                chkTuesday.Checked = true;
            if (day == DayOfWeek.Wednesday)
                chkWebnesday.Checked = true;
            if (day == DayOfWeek.Thursday)
                chkThursday.Checked = true;
            if (day == DayOfWeek.Friday)
                chkFriday.Checked = true;
            if (day == DayOfWeek.Saturday)
                chkSaturday.Checked = true;
            if (day == DayOfWeek.Sunday)
                chkSunday.Checked = true;
        }

        protected void txtCalendarFrom_TextChanged(object sender, EventArgs e)
        {
            /*
            DateTime date;
            if (!string.IsNullOrEmpty(txtCalendarFrom.Text))
            {
                if (DateTime.TryParse(txtCalendarFrom.Text, out date))
                    CheckActivityDay(DateTime.Parse(txtCalendarFrom.Text).DayOfWeek);
            }*/
        }

        protected void txtCalendarTo_TextChanged(object sender, EventArgs e)
        {
            /*
            DateTime date;
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                if (DateTime.TryParse(txtEndDate.Text, out date))
                    CheckActivityDay(DateTime.Parse(txtEndDate.Text).DayOfWeek);
            }*/
        }

        public AdministrationEDSC.ActivityScheduleDTRow getTimetableInput()
        {
            if (ScheduleDT == null)
                ScheduleDT = new AdministrationEDSC.ActivityScheduleDTDataTable();
            var drShced = ScheduleDT.NewActivityScheduleDTRow();

            drShced.OnMonday = false;
            drShced.OnTuesday = false;
            drShced.OnWednesday = false;
            drShced.OnThursday = false;
            drShced.OnFriday = false;
            drShced.OnSaturday = false;
            drShced.OnSunday = false;

            drShced.ActivityStartDatetime = Convert.ToDateTime(txtCalendarFrom.Text + " " + ddlTimeStart.SelectedItem.Text);
            drShced.ActivityEndDatetime = Convert.ToDateTime(txtEndDate.Text + " " + ddlTimeEnds.SelectedItem.Text);
            drShced.ActivityExpiryDate = Convert.ToDateTime(txtEndDate.Text);



            drShced.RecurrenceType = (int)SystemConstants.RecurrenceSchedule.Weekly;
            if (radWeekly.Checked)
                drShced.RecurEvery = 1;
            else if (radFortnightly.Checked)
                drShced.RecurEvery = 2;
            else if (radMonthly.Checked)
                drShced.RecurEvery = 4;
            else if (radNoRecurrence.Checked)
            {
                drShced.RecurEvery = 0;
                drShced.RecurrenceType = (int)SystemConstants.RecurrenceSchedule.NotRecurring;
            }
            drShced.OnMonday = chkMonday.Checked;
            drShced.OnTuesday = chkTuesday.Checked;
            drShced.OnWednesday = chkWebnesday.Checked;
            drShced.OnThursday = chkThursday.Checked;
            drShced.OnFriday = chkFriday.Checked;
            drShced.OnSaturday = chkSaturday.Checked;
            drShced.OnSunday = chkSunday.Checked;



            return drShced;
        }

        public AdministrationEDSC.ActivityScheduleDTDataTable getTimetable(bool checkDelete)
        {
            ScheduleDT = new AdministrationEDSC.ActivityScheduleDTDataTable();
            foreach (GridViewRow row in gridviewTimetable.Rows)
            {
                CheckBox chkboxSelected0 = row.FindControl("chkboxSelected0") as CheckBox;
                HiddenField hdnStartDateTime = row.FindControl("hdnStartDateTime") as HiddenField;
                HiddenField hdnEndDateTime = row.FindControl("hdnEndDateTime") as HiddenField;
                HiddenField hdnExpiryDateTime = row.FindControl("hdnExpiryDateTime") as HiddenField;
                HiddenField hdnRecurrenceType = row.FindControl("hdnRecurrenceType") as HiddenField;
                HiddenField hdnRecurEvery = row.FindControl("hdnRecurEvery") as HiddenField;
                HiddenField hdnOnMonday = row.FindControl("hdnOnMonday") as HiddenField;
                HiddenField hdnOnTuesday = row.FindControl("hdnOnTuesday") as HiddenField;
                HiddenField hdnOnWednesday = row.FindControl("hdnOnWednesday") as HiddenField;
                HiddenField hdnOnThursday = row.FindControl("hdnOnThursday") as HiddenField;
                HiddenField hdnOnFriday = row.FindControl("hdnOnFriday") as HiddenField;
                HiddenField hdnOnSaturday = row.FindControl("hdnOnSaturday") as HiddenField;
                HiddenField hdnOnSunday = row.FindControl("hdnOnSunday") as HiddenField;
                if (checkDelete)
                {
                    if (!chkboxSelected0.Checked)
                    {
                        var dr = ScheduleDT.NewActivityScheduleDTRow();

                        dr.ActivityID = ActivityID;
                        dr.ActivityStartDatetime = Convert.ToDateTime(hdnStartDateTime.Value);
                        dr.ActivityEndDatetime = Convert.ToDateTime(hdnEndDateTime.Value);
                        dr.ActivityExpiryDate = Convert.ToDateTime(hdnExpiryDateTime.Value);
                        dr.RecurrenceType = Convert.ToInt32(hdnRecurrenceType.Value);
                        dr.RecurEvery = Convert.ToInt32(hdnRecurEvery.Value);
                        dr.OnMonday = Convert.ToBoolean(hdnOnMonday.Value);
                        dr.OnTuesday = Convert.ToBoolean(hdnOnTuesday.Value);
                        dr.OnWednesday = Convert.ToBoolean(hdnOnWednesday.Value);
                        dr.OnThursday = Convert.ToBoolean(hdnOnThursday.Value);
                        dr.OnFriday = Convert.ToBoolean(hdnOnFriday.Value);
                        dr.OnSaturday = Convert.ToBoolean(hdnOnSaturday.Value);
                        dr.OnSunday = Convert.ToBoolean(hdnOnSunday.Value);

                        ScheduleDT.AddActivityScheduleDTRow(dr);
                    }
                }
                else
                {
                    var dr = ScheduleDT.NewActivityScheduleDTRow();

                    dr.ActivityID = ActivityID;
                    dr.ActivityStartDatetime = Convert.ToDateTime(hdnStartDateTime.Value);
                    dr.ActivityEndDatetime = Convert.ToDateTime(hdnEndDateTime.Value);
                    dr.ActivityExpiryDate = Convert.ToDateTime(hdnExpiryDateTime.Value);
                    dr.RecurrenceType = Convert.ToInt32(hdnRecurrenceType.Value);
                    dr.RecurEvery = Convert.ToInt32(hdnRecurEvery.Value);
                    dr.OnMonday = Convert.ToBoolean(hdnOnMonday.Value);
                    dr.OnTuesday = Convert.ToBoolean(hdnOnTuesday.Value);
                    dr.OnWednesday = Convert.ToBoolean(hdnOnWednesday.Value);
                    dr.OnThursday = Convert.ToBoolean(hdnOnThursday.Value);
                    dr.OnFriday = Convert.ToBoolean(hdnOnFriday.Value);
                    dr.OnSaturday = Convert.ToBoolean(hdnOnSaturday.Value);
                    dr.OnSunday = Convert.ToBoolean(hdnOnSunday.Value);

                    ScheduleDT.AddActivityScheduleDTRow(dr);
                }
            }
            return ScheduleDT;
        }

        protected void gridviewTimetable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string dayOfWeek = "";
                int index = 0;
                DataRowView drView = e.Row.DataItem as DataRowView;
                AdministrationEDSC.ActivityScheduleDTRow dr = drView.Row as AdministrationEDSC.ActivityScheduleDTRow;

                Label lblTime = e.Row.FindControl("lblTime") as Label;
                Label lblRecurring = e.Row.FindControl("lblRecurring") as Label;
                Label lblExpiry = e.Row.FindControl("lblExpiry") as Label;

                lblTime.Text = dr.ActivityStartDatetime.ToString("dd/MM/yyyy hh:mm tt") + " - " + dr.ActivityEndDatetime.ToString("dd/MM/yyyy hh:mm tt");
                if (dr.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Daily)
                    lblRecurring.Text = "Yes, Every " + dr.RecurEvery + " day(s)";
                else if (dr.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Weekly)
                {
                    if (dr.OnMonday)
                    {
                        dayOfWeek += " Monday, ";
                        index++;
                    }
                    if (dr.OnTuesday)
                    { dayOfWeek += "Tuesday, "; index++; }
                    if (dr.OnWednesday)
                    { dayOfWeek += "Wednesday, "; index++; }
                    if (dr.OnThursday)
                    { dayOfWeek += "Thursday, "; index++; }
                    if (dr.OnFriday)
                    { dayOfWeek += "Friday, "; index++; }
                    if (dr.OnSaturday)
                    { dayOfWeek += "Saturday, "; index++; }
                    if (dr.OnSunday)
                    { dayOfWeek += "Sunday, "; index++; }
                    if (index > 0)
                    {

                        dayOfWeek = dayOfWeek.Substring(0, dayOfWeek.Length - 1);
                        string recurr = "Yes, every " + dr.RecurEvery + " week(s) on " + dayOfWeek;

                        
                        if (index == 1)
                        {
                            int commaIndex = recurr.LastIndexOf(",");
                            recurr = recurr.Remove(commaIndex, 1);
                        }
                        else
                        {
                            int commaIndex = recurr.LastIndexOf(",");
                            recurr = recurr.Remove(commaIndex, 1);

                            commaIndex = recurr.LastIndexOf(",");
                            recurr = recurr.Remove(commaIndex, 1);
                            recurr = recurr.Insert(commaIndex, " and ");
                        }
                        lblRecurring.Text = recurr;
                    }
                }
                else if (dr.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.NotRecurring)
                    lblRecurring.Text = "Not Recurring";



                lblExpiry.Text = dr.ActivityExpiryDate.ToShortDateString();
            }

        }

        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.ActivityScheduleDTDataTable dt = new AdministrationEDSC.ActivityScheduleDTDataTable();
            foreach (GridViewRow row in gridviewPreview.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkboxSelected = row.FindControl("chkboxSelected") as CheckBox;
                    chkboxSelected.Checked = true;
                }
            }
        }

        protected void lnkSelect2All_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.ActivityScheduleDTDataTable dt = new AdministrationEDSC.ActivityScheduleDTDataTable();
            foreach (GridViewRow row in gridviewTimetable.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkboxSelected = row.FindControl("chkboxSelected0") as CheckBox;
                    chkboxSelected.Checked = true;
                }
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (timeTableDT == null)
                timeTableDT = new AdministrationEDSC.ActivityScheduleGridDTDataTable();
            timeTableDT = GetTimetablePreview(true);
            SetTimetableDataSource();
        }

        protected void lnkDelete2_Click(object sender, EventArgs e)
        {
            ScheduleDT = getTimetable(true);
            SetScheduleDataSource();
            SetTimetableDataSource();
            SetTimetablePreview();
            if (gridviewTimetable.Rows.Count == 0)
                btnAdd.Text = "Add Timetable";
        }

        protected void chkFullTimetable_CheckedChanged(object sender, EventArgs e)
        {
            gridviewPreview.Visible = chkFullTimetable.Checked;
        }

        protected void radNoTimetable_CheckedChanged(object sender, EventArgs e)
        {
            divTimetable.Visible = radEnableTimetable.Checked;
        }

        protected void radEnableTimetable_CheckedChanged(object sender, EventArgs e)
        {
            divTimetable.Visible = radEnableTimetable.Checked;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            if (infoIsValid())
            {
                if (timeTableDT == null)
                    timeTableDT = new AdministrationEDSC.ActivityScheduleGridDTDataTable();

                ScheduleDT = getTimetable(false);

                AdministrationEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                if (txtEndDate.Text == "")
                {
                    lblError.Text = "* Required";
                    txtEndDate.Focus();
                }
                else
                {
                    var aSched = getTimetableInput();
                    ScheduleDT.AddActivityScheduleDTRow(aSched);
                    SetScheduleDataSource();
                    SetTimetableDataSource();
                    SetTimetablePreview();
                    btnAdd.Text = "Add another timetable";
                }
            }
            else
            {

                lblError.Visible = true;
            }
        }

        private bool infoIsValid()
        {
            bool valid = true;
            int yearDiff = Convert.ToDateTime(txtEndDate.Text).Year - Convert.ToDateTime(txtCalendarFrom.Text).Year;
            if (Convert.ToDateTime(txtCalendarFrom.Text).DayOfYear >= (365 * yearDiff) + Convert.ToDateTime(txtEndDate.Text).DayOfYear)
            {
                valid = false;
                lblError.Text = "End Date must be greater than Start Date.";
            }
            if ((Convert.ToDateTime(txtCalendarFrom.Text).DayOfYear == Convert.ToDateTime(txtEndDate.Text).DayOfYear) && (Convert.ToDateTime(ddlTimeStart.SelectedItem.Text).TimeOfDay >= Convert.ToDateTime(ddlTimeEnds.SelectedItem.Text).TimeOfDay))
            {
                valid = false;
                lblError.Text = "End Time must be greater than Start Time.";
            }
            return valid;

        }




    }

}