using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using HealthyClub.Utility;

namespace HealthyClub.Web.UserControls
{
    public partial class TimeViewer : System.Web.UI.UserControl
    {

        public int ActivityID
        {
            get
            {
                if (hdntimetable.Value != "")
                    return Convert.ToInt32(hdnActivityID.Value);
                else return 2;
            }
            set
            {
                hdnActivityID.Value = value.ToString();
            }
        }

        public int timetableFormat
        {
            get
            {
                if (hdntimetable.Value != "")
                    return Convert.ToInt32(hdntimetable.Value);
                else return 2;
            }
            set
            {
                hdntimetable.Value = value.ToString();
                initTimetable();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void initTimetable()
        {
            var SchedulesDT = new CustomerDAC().RetrieveActivitySchedules(ActivityID);
            if (SchedulesDT != null)
            {
                var schedule = CreateSeasonalTimetable(SchedulesDT);

                ListView1.DataSource = schedule;
                ListView1.DataBind();
            }
            else
            {
                divNoTimetable.Visible = true;
                lblNoTimetable.Text = SystemConstants.ErrorSchedulenotGiven;
            }
        }

        private List<List<slot>> CreateSeasonalTimetable(CustomerEDSC.ActivityScheduleDTDataTable SchedulesDT)
        {
            List<List<slot>> schedule = new List<List<slot>>();
            List<slot> monday = new List<slot>();
            List<slot> tuesday = new List<slot>();
            List<slot> wednesday = new List<slot>();
            List<slot> thursday = new List<slot>();
            List<slot> friday = new List<slot>();
            List<slot> saturday = new List<slot>();
            List<slot> sunday = new List<slot>();

            foreach (var schedules in SchedulesDT)
            {
                if (!schedules.OnMonday && !schedules.OnTuesday && !schedules.OnWednesday && !schedules.OnThursday
                     && !schedules.OnFriday && !schedules.OnSaturday && !schedules.OnSunday)
                {
                    DayOfWeek day = schedules.ActivityStartDatetime.DayOfWeek;
                    if (day == DayOfWeek.Monday)
                        schedules.OnMonday = true;
                    if (day == DayOfWeek.Tuesday)
                        schedules.OnTuesday = true;
                    if (day == DayOfWeek.Wednesday)
                        schedules.OnWednesday = true;
                    if (day == DayOfWeek.Thursday)
                        schedules.OnThursday = true;
                    if (day == DayOfWeek.Friday)
                        schedules.OnFriday = true;
                    if (day == DayOfWeek.Saturday)
                        schedules.OnSaturday = true;
                    if (day == DayOfWeek.Sunday)
                        schedules.OnSunday = true;
                }

                if (schedules.OnMonday)
                {
                    slot slMon = new slot();
                    slMon.startTime = schedules.ActivityStartDatetime;
                    slMon.EndTime = schedules.ActivityEndDatetime;
                    slMon.Day = DayOfWeek.Monday.ToString();
                    monday.Add(slMon);
                }
                if (schedules.OnTuesday)
                {
                    slot slTue = new slot();
                    slTue.startTime = schedules.ActivityStartDatetime;
                    slTue.EndTime = schedules.ActivityEndDatetime;
                    slTue.Day = DayOfWeek.Tuesday.ToString();
                    tuesday.Add(slTue);

                }
                if (schedules.OnWednesday)
                {
                    slot slWednesday = new slot();
                    slWednesday.startTime = schedules.ActivityStartDatetime;
                    slWednesday.EndTime = schedules.ActivityEndDatetime;
                    slWednesday.Day = DayOfWeek.Wednesday.ToString();
                    wednesday.Add(slWednesday);
                }
                if (schedules.OnThursday)
                {
                    slot slThursday = new slot();
                    slThursday.startTime = schedules.ActivityStartDatetime;
                    slThursday.EndTime = schedules.ActivityEndDatetime;
                    slThursday.Day = DayOfWeek.Thursday.ToString();
                    thursday.Add(slThursday);
                }
                if (schedules.OnFriday)
                {
                    slot slFriday = new slot();
                    slFriday.startTime = schedules.ActivityStartDatetime;
                    slFriday.EndTime = schedules.ActivityEndDatetime;
                    slFriday.Day = DayOfWeek.Friday.ToString();
                    friday.Add(slFriday);
                }
                if (schedules.OnSaturday)
                {
                    slot slSaturday = new slot();
                    slSaturday.startTime = schedules.ActivityStartDatetime;
                    slSaturday.EndTime = schedules.ActivityEndDatetime;
                    slSaturday.Day = DayOfWeek.Saturday.ToString();
                    saturday.Add(slSaturday);
                }
                if (schedules.OnSunday)
                {
                    slot slSunday = new slot();
                    slSunday.startTime = schedules.ActivityStartDatetime;
                    slSunday.EndTime = schedules.ActivityEndDatetime;
                    slSunday.Day = DayOfWeek.Sunday.ToString();
                    sunday.Add(slSunday);
                }
            }
            if (monday.Count() != 0)
            {
                monday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                monday.Reverse();
                schedule.Add(monday);
            }
            if (tuesday.Count() != 0)
            {
                tuesday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                tuesday.Reverse();
                schedule.Add(tuesday);
            }
            if (wednesday.Count() != 0)
            {
                wednesday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                wednesday.Reverse();
                schedule.Add(wednesday);
            }
            if (thursday.Count() != 0)
            {
                thursday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                thursday.Reverse();
                schedule.Add(thursday);
            }
            if (friday.Count() != 0)
            {
                friday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                friday.Reverse();
                schedule.Add(friday);
            }
            if (saturday.Count() != 0)
            {
                saturday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                saturday.Reverse();
                schedule.Add(saturday);
            }
            if (sunday.Count() != 0)
            {
                sunday.Sort((x, y) => x.EndTime.CompareTo(y.EndTime));
                sunday.Reverse();
                schedule.Add(sunday);
            }
            return schedule;
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblDay = e.Item.FindControl("lblDay") as Label;
            List<slot> schedules = e.Item.DataItem as List<slot>;
            ListView listView2 = e.Item.FindControl("ListView2") as ListView;
            ScheduleDetailViewerUC scslot = e.Item.FindControl("ScheduleDetailViewerUC") as ScheduleDetailViewerUC;
            scslot.GenerateTimeSlot(schedules);
            lblDay.Text = schedules[0].Day;
        }
    }

    public class slot
    {
        public string Day { get; set; }
        public DateTime startTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}