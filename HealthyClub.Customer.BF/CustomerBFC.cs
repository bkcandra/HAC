using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using System.Data;
using HealthyClub.Utility;
using System.Transactions;
using System.Globalization;
using System.Web;
using BCUtility;
using System.Diagnostics;

namespace HealthyClub.Customer.BF
{
    public class CustomerBFC
    {
        public static void UpdateActivity(CustomerEDSC.ActivityDTRow drDetail, CustomerEDSC.ActivityContactDetailDTRow contactDetails, CustomerEDSC.ActivityGroupingDTRow drActGrouping, CustomerEDSC.ActivityScheduleDTDataTable dtActSchedule)
        {
            CustomerDAC dac = new CustomerDAC();

            using (TransactionScope trans = new TransactionScope())
            {
                dac.UpdateActivity(drDetail);
                dac.UpdateActivityContactDetail(contactDetails);
                dac.DeleteActivitySchedules(drDetail.ID);
                foreach (var drActSchedule in dtActSchedule)
                    dac.CreateActivitySchedule(drActSchedule);
                dac.UpdateActivityGrouping(drActGrouping);

                trans.Complete();
            }
        }

        public static void SaveActivity(CustomerEDSC.ActivityDTRow ActivityDetailDR, CustomerEDSC.ActivityContactDetailDTRow contactDetailsDR, CustomerEDSC.ActivityGroupingDTRow ActGroupingDR, CustomerEDSC.ActivityScheduleDTDataTable ActScheduleDT)
        {
            CustomerDAC dac = new CustomerDAC();

            using (TransactionScope trans = new TransactionScope())
            {
                int activityID;
                dac.CreateActivities(ActivityDetailDR, out activityID);

                //Activity was Created, update all foreign key
                contactDetailsDR.ActivityID = activityID;
                ActGroupingDR.ActivityID = activityID;



                //CreateContactDetails
                dac.CreateActivityContactDetail(contactDetailsDR);

                //Create Schedule
                foreach (var ActScheduleDR in ActScheduleDT)
                {
                    ActScheduleDR.ActivityID = activityID;
                    dac.CreateActivitySchedule(ActScheduleDR);
                }
                //Create Grouping
                dac.CreateActivityGrouping(ActGroupingDR);

                trans.Complete();
            }
        }


        public string RefineSearchKeyreward(string SearchKey)
        {
            if (isImplementAdvanceSearch())
            {
                String SearchPhrase = "";
                var keyCollection = new CustomerDAC().SearchKeywordCollection(SearchKey);
                if (keyCollection.Count == 0 || keyCollection == null)
                    return SearchKey;
                else
                {
                    foreach (var key in keyCollection)
                    {
                        SearchPhrase = SearchPhrase + key.Keywords + ";";
                    }
                }
                return SearchPhrase;
            }
            else
                return SearchKey;


        }


        public List<string> RefineSearchKey(string InputQueries)
        {
            List<string> SearchParameters = new List<string>();
            String SearchQuery = "";
            String DaysQuery = "";
            String LocationsQuery = "";
            String TimeQuery = "";
            List<TimeSpan> Timelist = new List<TimeSpan>();

            List<string> keywords = InputQueries.ToUpper().Split(' ').OfType<string>().ToList();

            if (isImplementAdvanceSearch())
            {
                /*Delete Prepositions/Postposition from keyword
             * See SystemConstants.Prepositions for list of prepositions             
             */
                List<string> exclude = (SystemConstants.Prepositions + SystemConstants.Conjunctions).Split(';').Select(x => x.Trim()).OfType<string>().ToList();

                HashSet<string> preposHash = new HashSet<string>(exclude.Select(x => x.ToUpper()));
                keywords.RemoveAll(x => preposHash.Contains(x.ToUpper()));

                /*Match and save locations from keyword                
                 First we retrieve available locations from db
                 */
                var suburbs = new CustomerDAC().RetrieveSuburbs().ToList();

                List<SuburbExplorer> SubsCont = new List<SuburbExplorer>();
                foreach (var suburb in suburbs)
                {
                    string[] subs = suburb.Name.Split(' ');
                    foreach (var sub in subs)
                    {
                        SubsCont.Add(new SuburbExplorer()
                        {
                            ID = suburb.ID,
                            Name = sub
                        });
                    }
                }

                var matchedLocs = SubsCont.Where(x => keywords.Contains(x.Name.ToUpper()));
                HashSet<int> locsID = new HashSet<int>(matchedLocs.Select(x => x.ID));
                IEnumerable<CustomerEDSC.v_SuburbExplorerDTRow> matchedSuburbs = suburbs.Where(x => locsID.Contains(x.ID));

                StringBuilder lq = new StringBuilder();
                foreach (var matchedSuburb in matchedSuburbs)
                    lq.Append(matchedSuburb.Name + ";");
                LocationsQuery = lq.ToString();

                HashSet<string> locationsHash = new HashSet<string>(SubsCont.Select(x => x.Name.ToUpper()));
                keywords.RemoveAll(x => locationsHash.Contains(x.ToUpper()));

                /*Match and save Days from keyword                
                 */
                List<string> dayofweek = new List<string> { DayOfWeek.Monday.ToString(), DayOfWeek.Tuesday.ToString(),
                DayOfWeek.Wednesday.ToString(),DayOfWeek.Thursday.ToString(),DayOfWeek.Friday.ToString(),
                DayOfWeek.Saturday.ToString(),DayOfWeek.Sunday.ToString(),};

                HashSet<string> dayofweekHash = new HashSet<string>(dayofweek.Select(x => x.ToUpper()));
                var days = keywords.Where(x => dayofweekHash.Contains(x.ToUpper()));
                if (days.Count() != 0)
                    DaysQuery = String.Join(";", days);

                keywords.RemoveAll(x => dayofweekHash.Contains(x.ToUpper()));

                foreach (var key in keywords)
                {
                    DateTime dateTime = new DateTime();
                    //12hourformat h:mm tt 	6:30 AM 
                    if (DateTime.TryParseExact(key, @"h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    if (DateTime.TryParseExact(key, @"hmm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"hmm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    if (DateTime.TryParseExact(key, @"h:mmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"h:mmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    if (DateTime.TryParseExact(key, @"hmmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"hmmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    if (DateTime.TryParseExact(key, @"h tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    if (DateTime.TryParseExact(key, @"htt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    //24hourformat HH:mm 14:30
                    else if (DateTime.TryParseExact(key, @"HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else if (DateTime.TryParseExact(key, @"Hmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    {
                        Timelist.Add(dateTime.TimeOfDay);
                    }
                    else
                    {
                        var keyCollections = new CustomerDAC().SearchKeywordCollection(key);
                        if (keyCollections.Count == 0 || keyCollections == null)
                        {
                            foreach (var keycol in keyCollections)
                            {
                                SearchQuery = SearchQuery + keycol.Keywords + ";";
                            }
                        }
                        if (!string.IsNullOrEmpty(SearchQuery))
                        {
                            SearchQuery = SearchQuery + ";" + key;
                        }
                        else
                        {
                            SearchQuery = key;
                        }
                    }
                }
            }
            else
            {
                SearchQuery = InputQueries.Replace(' ', ';');
            }
            if (Timelist.Count != 0)
            {
                Timelist = Timelist.OrderBy(row => row.Hours).ToList();
                if (Timelist.Count >= 2)
                {
                    TimeQuery = Timelist[0].ToString() + "-" + Timelist[Timelist.Count - 1].ToString();
                }
                else if (Timelist.Count == 1)
                {
                    TimeQuery = Timelist[0].ToString();
                }
            }
            SearchQuery = SearchQuery.Replace(" ", string.Empty);
            SearchQuery = SearchQuery.Replace(";;", ";");
            if (SearchQuery.EndsWith(";"))
                SearchQuery = SearchQuery.TrimEnd(';');
            SearchQuery = SearchQuery.Trim();

            SearchParameters.Add(SystemConstants.Query + SearchQuery);
            if (!string.IsNullOrEmpty(LocationsQuery))
                SearchParameters.Add(SystemConstants.Location + LocationsQuery);
            if (!string.IsNullOrEmpty(DaysQuery))
                SearchParameters.Add(SystemConstants.Day + DaysQuery);
            if (!string.IsNullOrEmpty(TimeQuery))
                SearchParameters.Add(SystemConstants.Time + TimeQuery);
            return SearchParameters;
        }

        private bool isweekDay(string key)
        {
            if (key.Equals(DayOfWeek.Monday.ToString()) || key.Equals(DayOfWeek.Tuesday.ToString()) ||
                key.Equals(DayOfWeek.Wednesday.ToString()) || key.Equals(DayOfWeek.Thursday.ToString()) ||
                key.Equals(DayOfWeek.Friday.ToString()) || key.Equals(DayOfWeek.Saturday.ToString()) ||
                key.Equals(DayOfWeek.Sunday.ToString()))
                return true;
            else return false;

        }

        public bool isImplementAdvanceSearch()
        {
            return new CustomerDAC().CheckAdvanceSearch();
        }

        public bool CheckActivityOwner(int ActivityID, Guid providerID)
        {
            var dr = new CustomerDAC().RetrieveActivity(ActivityID);
            if (dr != null)
            {
                if (dr.ProviderID.Equals(providerID))
                    return true;
                else return false;
            }
            else return false;
        }

        #region ActivityListing
        /*
        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivitiesbySearchPhrase(string SearchPhrase, string ProviderID, int amount, int startIndex, String sortExpression)
        {
            CustomerDAC dac = new CustomerDAC();

            var dt = dac.RetrieveActivitiesbySearchPhrase(new Guid(ProviderID), SearchPhrase, startIndex, amount, sortExpression);
            List<int> ls = new List<int>();

            foreach (var dr in dt)
            {
                ls.Add(dr.ID);
            }

            var dtEx = new CustomerEDSC.v_ActivityExplorerDTDataTable();

            foreach (var lsItem in ls)
            {
                dtEx.Addv_ActivityExplorerDTRow(new CustomerDAC().RetrieveActivityExplorer(lsItem));
            }
            return dtEx;
        }

        public int RetrieveProviderActivitiesbySearchPhraseCount(string SearchPhrase, string ProviderID)
        {
            CustomerDAC dac = new CustomerDAC();

            int count = dac.RetrieveActivitiesbySearchPhraseCount(new Guid(ProviderID), SearchPhrase);

            return count;
        }

        public CustomerEDSC.v_ActivityExplorerDTDataTable RetrieveProviderActivities(string ProviderID, string categoryID, int amount, int startIndex, String sortExpression)
        {
            CustomerDAC dac = new CustomerDAC();

            var dt = dac.RetrieveProviderActivities(new Guid(ProviderID), startIndex, amount, sortExpression);
            List<int> ls = new List<int>();

            foreach (var dr in dt)
            {
                ls.Add(dr.ID);
            }

            var dtEx = new CustomerDAC().RetrieveActivityExplorersbyIDs(ls);
            return dtEx;
        }

        public int RetrieveProviderActivitiesCount(string ProviderID, string categoryID)
        {
            CustomerDAC dac = new CustomerDAC();

            int count = dac.RetrieveProviderActivitiesCount(new Guid(ProviderID));

            return count;
        }
*/
        # endregion

        public CustomerEDSC.ActivityScheduleGridDTDataTable RetrieveTimetableGrid(int startIndex, int amount, string activityID, string sortExpression)
        {
            var slots = new CustomerDAC().RetrieveActivitySchedules(Convert.ToInt32(activityID));
            var timeTableDT = CalculateRecurrence(slots);
            timeTableDT.DefaultView.Sort = "StartDateTime ASC";

            if (timeTableDT.Count() != 0)
            {
                var dtTimetable = new CustomerEDSC.ActivityScheduleGridDTDataTable();
                timeTableDT.Skip(startIndex).Take(amount).CopyToDataTable(dtTimetable, LoadOption.PreserveChanges);
                dtTimetable.DefaultView.Sort = "StartDateTime ASC";
                return dtTimetable;
            }
            else
                return null;
        }

        public int RetrieveTimetableGridCount(string activityID)
        {
            var dtSchedule = new CustomerDAC().RetrieveActivitySchedules(Convert.ToInt32(activityID));
            var timeTableDT = CalculateRecurrence(dtSchedule);


            return timeTableDT.Count();
        }

        public CustomerEDSC.ActivityScheduleGridDTDataTable CalculateRecurrence(CustomerEDSC.ActivityScheduleDTRow drSched)
        {
            var timeTableDT = new CustomerEDSC.ActivityScheduleGridDTDataTable();

            int startYear = drSched.ActivityStartDatetime.Year;
            int yearDifference = drSched.ActivityExpiryDate.Year - drSched.ActivityStartDatetime.Year;

            if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Daily)
            {
                int dayDifference = (drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) - drSched.ActivityStartDatetime.DayOfYear;
                int recurTimes = dayDifference / drSched.RecurEvery;

                for (int i = 0; i <= recurTimes; i++)
                {
                    //InsertRecurrenceStartTime
                    DateTime recurStartDate = drSched.ActivityStartDatetime.AddDays(drSched.RecurEvery * i);
                    DateTime recurEndDate = drSched.ActivityEndDatetime.AddDays(drSched.RecurEvery * i);

                    CustomerEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                    dr.StartDateTime = recurStartDate;
                    dr.EndDateTime = recurEndDate;
                    timeTableDT.AddActivityScheduleGridDTRow(dr);
                }
            }
            else if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Weekly)
            {
                decimal weekDifference = ((drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) / 7) - (drSched.ActivityStartDatetime.DayOfYear / 7);
                decimal recurTimes = weekDifference / drSched.RecurEvery;

                for (decimal i = 1; i <= recurTimes + 1; i++)
                {
                    for (int weekday = 1; weekday <= 7; weekday++)
                    {
                        if ((drSched.ActivityStartDatetime.DayOfYear + ((drSched.ActivityStartDatetime.Year - startYear) * 365)) > (drSched.ActivityExpiryDate.DayOfYear) + (yearDifference * 365))
                            break;

                        if (drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Monday && drSched.OnMonday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Tuesday && drSched.OnTuesday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Wednesday && drSched.OnWednesday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Thursday && drSched.OnThursday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Friday && drSched.OnFriday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Saturday && drSched.OnSaturday ||
                            drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Sunday && drSched.OnSunday)
                        {
                            CustomerEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                            dr.StartDateTime = drSched.ActivityStartDatetime;
                            dr.EndDateTime = drSched.ActivityEndDatetime;
                            timeTableDT.AddActivityScheduleGridDTRow(dr);
                        }


                        drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays(1);
                        drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays(1);
                        //recurStartDate = startDateTime.AddDays(1 * i + weekday);
                    }
                    drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                    drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                }
            }
            return timeTableDT;
        }

        public CustomerEDSC.ActivityScheduleGridDTDataTable CalculateRecurrence(CustomerEDSC.ActivityScheduleDTDataTable dtSched)
        {
            var timeTableDT = new CustomerEDSC.ActivityScheduleGridDTDataTable();
            foreach (var drSched in dtSched)
            {
                int startYear = drSched.ActivityStartDatetime.Year;
                int yearDifference = drSched.ActivityExpiryDate.Year - drSched.ActivityStartDatetime.Year;

                if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Daily)
                {
                    int dayDifference = (drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) - drSched.ActivityStartDatetime.DayOfYear;
                    int recurTimes = dayDifference / drSched.RecurEvery;

                    for (int i = 0; i <= recurTimes; i++)
                    {
                        //InsertRecurrenceStartTime
                        DateTime recurStartDate = drSched.ActivityStartDatetime.AddDays(drSched.RecurEvery * i);
                        DateTime recurEndDate = drSched.ActivityEndDatetime.AddDays(drSched.RecurEvery * i);

                        CustomerEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                        dr.StartDateTime = recurStartDate;
                        dr.EndDateTime = recurEndDate;
                        timeTableDT.AddActivityScheduleGridDTRow(dr);
                    }
                }
                else if (drSched.RecurrenceType == (int)SystemConstants.RecurrenceSchedule.Weekly)
                {
                    decimal weekDifference = ((drSched.ActivityExpiryDate.DayOfYear + (yearDifference * 365)) / 7) - (drSched.ActivityStartDatetime.DayOfYear / 7);
                    decimal recurTimes = weekDifference / drSched.RecurEvery;

                    for (decimal i = 1; i <= recurTimes + 1; i++)
                    {
                        for (int weekday = 1; weekday <= 7; weekday++)
                        {
                            if ((drSched.ActivityStartDatetime.DayOfYear + ((drSched.ActivityStartDatetime.Year - startYear) * 365)) > (drSched.ActivityExpiryDate.DayOfYear) + (yearDifference * 365))
                                break;

                            if (drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Monday && drSched.OnMonday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Tuesday && drSched.OnTuesday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Wednesday && drSched.OnWednesday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Thursday && drSched.OnThursday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Friday && drSched.OnFriday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Saturday && drSched.OnSaturday ||
                                drSched.ActivityStartDatetime.DayOfWeek == DayOfWeek.Sunday && drSched.OnSunday)
                            {
                                CustomerEDSC.ActivityScheduleGridDTRow dr = timeTableDT.NewActivityScheduleGridDTRow();
                                dr.StartDateTime = drSched.ActivityStartDatetime;
                                dr.EndDateTime = drSched.ActivityEndDatetime;
                                timeTableDT.AddActivityScheduleGridDTRow(dr);
                            }


                            drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays(1);
                            drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays(1);
                            //recurStartDate = startDateTime.AddDays(1 * i + weekday);
                        }
                        drSched.ActivityStartDatetime = drSched.ActivityStartDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                        drSched.ActivityEndDatetime = drSched.ActivityEndDatetime.AddDays((7 * Convert.ToInt32(drSched.RecurEvery) - 7));
                    }
                }
            }
            timeTableDT.DefaultView.Sort = "StartDateTime ASC";
            return timeTableDT;
        }

        public void ParseEmail(CustomerEDSC.v_EmailExplorerDTRow emTemp, Guid userID, string token, int EmailTemplateType, int activityID)
        {
            var dr = new CustomerDAC().RetrieveUserProfiles(userID);

            if (dr == null)
            {
                var drprov = new CustomerDAC().RetrieveProviderProfiles(userID);
                if (drprov != null)
                {
                    if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                    {
                        //Provider Fullname
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        //Provider ConfirmationTokenuri
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        //Provider Confirmationurl
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                        //Provider ConfirmationToken
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ForgotPassword)
                    {
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", drprov.Username);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@recoverylinkwithtoken]", SystemConstants.CustomerUrl + "Account/PasswordRecovery.aspx?" + SystemConstants.token + "=" + token);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired2week)
                    {
                        var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                    {
                        var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                    else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                    {
                        var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", drprov.FirstName + " " + drprov.LastName);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                        emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                    }
                }
            }
            else
            {
                if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail)
                {
                    //Provider Fullname
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                    //Provider ConfirmationTokenuri
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                    //Provider ConfirmationToken
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                    //Provider Confirmationurl
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                    //Provider ConfirmationToken
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ConfirmationEmail)
                {
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@loginurl]", SystemConstants.CustomerUrl + "Account/login.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationTokenwithurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx?" + SystemConstants.token + "=" + token);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@token]", token);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@confirmationurl]", SystemConstants.CustomerUrl + "Account/Confirm.aspx");
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.ForgotPassword)
                {
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@username]", dr.Username);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@recoverylinkwithtoken]", SystemConstants.CustomerUrl + "Account/PasswordRecovery.aspx?" + SystemConstants.token + "=" + token);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@cancelregistration]", SystemConstants.CustomerUrl + "Account/CancelAccount.aspx?" + SystemConstants.userID + "=" + userID);
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired2week)
                {
                    var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired1week)
                {
                    var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (activityDR.ExpiryDate.DayOfYear - DateTime.Now.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
                else if (EmailTemplateType == (int)SystemConstants.EmailTemplateType.Expired)
                {
                    var activityDR = new CustomerDAC().RetrieveActivity(activityID);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@fullname]", dr.FirstName + " " + dr.LastName);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityname]", activityDR.Name);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@activityid]", activityDR.ID.ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expirydays]", (DateTime.Now.DayOfYear - activityDR.ExpiryDate.DayOfYear).ToString());
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@providerurl]", SystemConstants.ProviderUrl);
                    emTemp.EmailBody = emTemp.EmailBody.Replace("[@expireDate]", activityDR.ExpiryDate.ToShortDateString());
                }
            }

        }

        public int getProviderPrimaryImage(Guid providerID)
        {
            CustomerDAC dac = new CustomerDAC();

            var dt = dac.RetrieveUserImages(providerID);

            foreach (var dr in dt)
            {
                if (dr.isPrimaryImage)
                {
                    return dr.ID;
                }
            }
            return 0;
        }


        public void createRefforAllUser()
        {
            var userProf = new CustomerDAC().RetrieveUserProfiles();
            var RefTable = new CustomerDAC().RetrieveUserReferences().Select(x => x.UserID);

            var usrs = userProf.Where(x => !RefTable.Contains(x.UserID));
            foreach (var usr in usrs)
            {
                var drRef = new CustomerEDSC.UserReferenceDTDataTable().NewUserReferenceDTRow();
                drRef.UserID = usr.UserID;
                drRef.ReferenceID = GenerateUserRefID(usr.LastName, usr.FirstName);
                new CustomerDAC().insertNewUserReference(drRef);
            }
        }
        public string GenerateUserRefID(string firstName, string LastName)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var RefTable = new CustomerDAC().RetrieveUserReferences();

            string Name = "";
            if (!string.IsNullOrEmpty(LastName))
                Name = LastName.ToUpper();
            else
                Name = firstName.ToUpper();

            StringBuilder sb = new StringBuilder();

            if (Name.Length >= 3)
                sb.Append(Name.Substring(0, 3));
            else
                sb.Append(ObjectHandler.GetRandomKey(3));
            List<string> nameRef = new List<string>(RefTable.Where(x => x.ReferenceID.Substring(0, 3).Equals(sb.ToString())).Select(y => y.ReferenceID.Substring(3, 4)));

            string nextCode = nameRef.Count() == 0 ? sb.ToString() + "0001" : sb.ToString() + (int.Parse(nameRef.OrderByDescending(i => i).First()) + 1).ToString("0000");
            var a = sw.Elapsed;
            return nextCode;
        }

    }

    public class Reward
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public string Description { get; set; }
        public DateTime Expiry { get; set; }
        public Reward(int id)
        {

            var dr = new CustomerDAC().RetrieveRewardInfo(Convert.ToInt32(id));
            if (dr != null)
            {
                this.Point = dr.RequiredRewardPoint;
                this.Description = dr.RewardsName;
                this.Expiry = dr.RewardExpiryDate;
            }

        }
    }

    public class CartItem : IEquatable<CartItem>
    {
        #region Properties

        // A place to store the quantity in the cart  
        // This property has an implicit getter and setter.  
        public int Quantity { get; set; }
        public int newquant { get; set; }
        private int _rewardId;
        public int rewardId
        {
            get { return _rewardId; }
            set
            {
                // To ensure that the Prod object will be re-created  
                _reward = null;
                _rewardId = value;
            }
        }

        private Reward _reward = null;
        public Reward Prod
        {
            get
            {
                // Lazy initialization - the object won't be created until it is needed  
                if (_reward == null)
                {
                    _reward = new Reward(rewardId);
                }
                return _reward;
            }
        }

        public DateTime Expiry
        {

            get { return Prod.Expiry; }
        }
        public string Description
        {
            get { return Prod.Description; }
        }

        public int UnitPoint
        {
            get { return Prod.Point; }
        }


        public int TotalPoint
        {
            get
            {
                if (newquant == 0)
                    return UnitPoint * Quantity;
                else
                    return UnitPoint * newquant;
            }
        }

        #endregion

        // CartItem constructor just needs a rewardId  
        public CartItem(int rewardId)
        {
            this.rewardId = rewardId;
        }

        /** 
         * Equals() - Needed to implement the IEquatable interface 
         *    Tests whether or not this item is equal to the parameter 
         *    This method is called by the Contains() method in the List class 
         *    We used this Contains() method in the ShoppingCart AddItem() method 
         */
        public bool Equals(CartItem item)
        {
            return item.rewardId == this.rewardId;
        }
    }

    public class RewardCart
    {

        #region Properties

        public List<CartItem> Items { get; private set; }

        #endregion

        #region Singleton Implementation
        // Readonly properties can only be set in initialization or in a constructor  
        public static readonly RewardCart Instance;

        // The static constructor is called as soon as the class is loaded into memory  
        static RewardCart()
        {
            // If the cart is not in the session, create one and put it there  
            // Otherwise, get it from the session  
            if (HttpContext.Current.Session["ASPNETRewardCart"] == null)
            {
                Instance = new RewardCart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["ASPNETRewardCart"] = Instance;
            }
            else
            {
                Instance = (RewardCart)HttpContext.Current.Session["ASPNETRewardCart"];
            }
        }

        // A protected constructor ensures that an object can't be created from outside  


        #endregion

        #region Item Modification Methods
        /** 
     * AddItem() - Adds an item to the shopping  
     */
        public void AddItem(int productId)
        {
            // Create a new item to add to the cart  
            CartItem newItem = new CartItem(productId);

            // If this item already exists in our list of items, increase the quantity  
            // Otherwise, add the new item to the list  
            if (Items.Contains(newItem))
            {
                foreach (CartItem item in Items)
                {
                    if (item.Equals(newItem))
                    {
                        item.Quantity++;
                        return;
                    }
                }
            }
            else
            {
                newItem.Quantity = 1;
                Items.Add(newItem);
            }
        }

        /** 
         * SetItemQuantity() - Changes the quantity of an item in the cart 
         */
        public void SetItemQuantity(int productId, int quantity)
        {
            // If we are setting the quantity to 0, remove the item entirely  
            if (quantity == 0)
            {
                RemoveItem(productId);
                return;
            }

            // Find the item and update the quantity  
            CartItem updatedItem = new CartItem(productId);

            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.Quantity = quantity;
                    return;
                }
            }
        }

        public void SetItemnewQuantity(int productId, int quantity)
        {

            // Find the item and update the quantity  
            CartItem updatedItem = new CartItem(productId);

            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.newquant = quantity;
                    return;
                }
            }
        }

        public void setquanttozero()
        {

            // Find the item and update the quantity  

            foreach (CartItem item in Items)
            {
                item.newquant = 0;

            }
        }

        /** 
         * RemoveItem() - Removes an item from the shopping cart 
         */
        public void RemoveItem(int productId)
        {
            CartItem removedItem = new CartItem(productId);
            Items.Remove(removedItem);
        }
        public int getItemnewQuantity(int ProductId)
        {

            // Find the item and update the quantity  
            CartItem updatedItem = new CartItem(ProductId);

            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    return item.newquant;
                }
            }
            return 0;
        }

        public int getItemQuantity(int ProductId)
        {

            // Find the item and update the quantity  
            CartItem updatedItem = new CartItem(ProductId);

            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    return item.Quantity;
                }
            }
            return 0;
        }
        #endregion

        #region Reporting Methods
        /** 
     * GetSubTotal() - returns the total price of all of the items 
     *                 before tax, shipping, etc. 
     */
        public int getQuantity()
        {
            int quant = 0;
            foreach (CartItem item in Items)
                quant += item.Quantity;
            return quant;

        }

        public int getitemno()
        {
            int quant = 0;
            foreach (CartItem item in Items)
                quant++;
            return quant;

        }
        public int GetSubTotal()
        {
            int subTotal = 0;
            foreach (CartItem item in Items)
                subTotal += item.TotalPoint;

            return subTotal;
        }

        public string getItems()
        {
            string selected = "";
            string separator = "|";

            foreach (CartItem item in Items)
            {
                if (!String.IsNullOrEmpty(selected))
                    selected += separator;
                selected += Convert.ToString(item.rewardId);
            }
            return selected;
        }

        public string getExpiry()
        {
            string selected = "";
            string separator = "|";

            foreach (CartItem item in Items)
            {
                if (!String.IsNullOrEmpty(selected))
                    selected += separator;
                selected += Convert.ToString(item.Expiry);
            }
            return selected;



        }
        public string getNames()
        {
            string selected = "";
            string separator = "|";

            foreach (CartItem item in Items)
            {
                if (!String.IsNullOrEmpty(selected))
                    selected += separator;
                selected += Convert.ToString(item.Description);
            }
            return selected;



        }

        public int getItemQuant(int reward)
        {
            int quant = 0;
            CartItem newItem = new CartItem(reward);
            foreach (CartItem item in Items)
            {
                if (item.Equals(newItem))
                    quant = item.Quantity;
            }
            return quant;
        }
        #endregion
    }
}
