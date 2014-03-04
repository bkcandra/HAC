using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyClub.Providers.Web
{
    public class ReportParameterPasser
    {

        //Name that will be used as key for Session object
        private const string SESSION_PARSER = "ReportParameterPasser";

        //Variables to store the data (used to be individual
        // session key/value pairs)
       
        bool useTimetable = true;
        bool nameVisible = true;
        bool shortDescription = true;
        bool eligibility = true;
        bool address = true;
        bool website = true;
        bool price = true;
        bool customSearch = false;
        int timetableFormat = 1;
        int categoryID = 0;
        int column = 1;
        Guid providerID = Guid.Empty;
        string sortValue = "";
        string title = "";
        string searchKey = "";
        int ageFrom = 0;
        int ageTo = 99;
        int postCode = 0;

        public bool CustomReport
        {
            get
            {
                return customSearch;
            }
            set
            {
                customSearch = value;
            }
        }

        public bool UseTimetable
        {
            get
            {
                return useTimetable;
            }
            set
            {
                useTimetable = value;
            }
        }

        public bool NameVisible
        {
            get
            {
                return nameVisible;
            }
            set
            {
                nameVisible = value;
            }
        }

        public bool ShortDescriptionVisible
        {
            get
            {
                return shortDescription;
            }
            set
            {
                shortDescription = value;
            }
        }

        public bool EligibilityVisible
        {
            get
            {
                return eligibility;
            }
            set
            {
                eligibility = value;
            }
        }

        public bool AddressVisible
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public bool WebsiteVisible
        {
            get
            {
                return website;
            }
            set
            {
                website = value;
            }
        }

        public bool PriceVisible
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public Guid ProviderID
        {
            get
            {
                return providerID;
            }
            set
            {
                providerID = value;
            }
        }

        public int CategoryID
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
        }

        public int TimetableFormat
        {
            get
            {
                return timetableFormat;
            }
            set
            {
                timetableFormat = value;
            }
        }

        public int AgeFrom
        {
            get
            {
                return ageFrom;
            }
            set
            {
                ageFrom = value;
            }
        }

        public int AgeTo
        {
            get
            {
                return ageTo;
            }
            set
            {
                ageTo = value;
            }
        }

        public int PostCode
        {
            get
            {
                return postCode;
            }
            set
            {
                postCode = value;
            }
        }

        public string SearchKey
        {
            get
            {
                return searchKey;
            }
            set
            {
                searchKey = value;
            }
        }

        public string SortValue
        {
            get
            {
                return sortValue;
            }
            set
            {
                sortValue = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        //Private constructor so cannot create an instance
        // without using the correct method.  This is 
        // critical to properly implementing
        // as a singleton object, objects of this
        // class cannot be created from outside this
        // class
        private ReportParameterPasser()
        {
        }

        //Create as a static method so this can be called using
        // just the class name (no object instance is required).
        // It simplifies other code because it will always return
        // the single instance of this class, either newly created
        // or from the session
        public static ReportParameterPasser GetCurrentParameters()
        {
            ReportParameterPasser parameters;

            if (null == System.Web.HttpContext.Current.Session[SESSION_PARSER])
            {
                //No current session object exists, use private constructor to 
                // create an instance, place it into the session
                parameters = new ReportParameterPasser();
                System.Web.HttpContext.Current.Session[SESSION_PARSER] = parameters;
            }
            else
            {
                //Retrieve the already instance that was already created
                parameters = (ReportParameterPasser)System.Web.HttpContext.Current.Session[SESSION_PARSER];
            }

            //Return the single instance of this class that was stored in the session
            return parameters;
        }

    }
}