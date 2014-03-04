using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using System.IO;
using System.Drawing.Printing;
using System.Text;
using System.Web.UI.HtmlControls;
using HealthyClub.Provider.DA;
using HealthyClub.Providers.Web.UserControls;
using HealthyClub.Providers.Web;
using HealthyClub.Provider.EDS;
using HealthyClub.Provider.BF;

namespace HealthyClub.Providers.Web.Report
{
    public partial class Report : System.Web.UI.Page
    {
        public string Title
        {
            get
            {
                if (hdnTitle.Value != "")
                    return hdnTitle.Value;
                else return "";
            }
            set
            {
                hdnTitle.Value = value.ToString();
            }
        }

        public bool UseTimetable
        {
            get
            {
                if (hdnUseTimetable.Value != "")
                    return Convert.ToBoolean(hdnUseTimetable.Value);
                else return false;
            }
            set
            {
                hdnUseTimetable.Value = value.ToString();
            }
        }

        public bool NameVisible
        {
            get
            {
                if (hdnName.Value != "")
                    return Convert.ToBoolean(hdnName.Value);
                else return false;
            }
            set
            {
                hdnName.Value = value.ToString();
            }
        }

        public bool ShortDescriptionVisible
        {
            get
            {
                if (hdnShortDescription.Value != "")
                    return Convert.ToBoolean(hdnShortDescription.Value);
                else return false;
            }
            set
            {
                hdnShortDescription.Value = value.ToString();
            }
        }

        public bool EligibilityVisible
        {
            get
            {
                if (hdnEligibility.Value != "")
                    return Convert.ToBoolean(hdnEligibility.Value);
                else return false;
            }
            set
            {
                hdnEligibility.Value = value.ToString();
            }
        }

        public bool AddressVisible
        {
            get
            {
                if (hdnAddress.Value != "")
                    return Convert.ToBoolean(hdnAddress.Value);
                else return false;
            }
            set
            {
                hdnAddress.Value = value.ToString();
            }
        }

        public bool WebsiteVisible
        {
            get
            {
                if (hdnTitle.Value != "")
                    return Convert.ToBoolean(hdnWebsite.Value);
                else return false;
            }
            set
            {
                hdnWebsite.Value = value.ToString();
            }
        }

        public bool PriceVisible
        {
            get
            {
                if (hdnTitle.Value != "")
                    return Convert.ToBoolean(hdnPrice.Value);
                else return false;
            }
            set
            {
                hdnPrice.Value = value.ToString();
            }
        }

        public Guid ProviderID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnProviderID.Value))
                    return new Guid(hdnProviderID.Value);
                else return Guid.Empty;
            }
            set
            {
                hdnProviderID.Value = value.ToString();
            }
        }

        public int Timetable
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnColumn.Value))
                    return Convert.ToInt32(hdnColumn.Value);
                else return 2;
            }
            set
            {
                hdnColumn.Value = value.ToString();
            }
        }

        public string SortValue
        {
            get
            {
                if (hdnTitle.Value != "")
                    return hdnSortValue.Value;
                else return "1";
            }
            set
            {
                hdnSortValue.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitListview();

            }
        }

        private void SetReportAttribute(Guid providerID, bool isNameVisible, bool isShortDescriptionVisible,
            bool isEligibilityVisible, bool isAddressVisible, bool isWebsiteVisible,
            bool isPriceVisible, int timetableFormat, bool useTimetable)
        {
            ProviderID = providerID;
            NameVisible = isNameVisible;
            ShortDescriptionVisible = isShortDescriptionVisible;
            EligibilityVisible = isEligibilityVisible;
            AddressVisible = isAddressVisible;
            WebsiteVisible = isWebsiteVisible;
            PriceVisible = isPriceVisible;
            Timetable = timetableFormat;
            UseTimetable = useTimetable;
        }

        private void SortProducts()
        {
            if (SortValue == "1")
            {
                ViewerListview.Sort("CreatedDateTime", SortDirection.Descending);
            }
            else if (SortValue == "2")
            {
                ViewerListview.Sort("Price", SortDirection.Ascending);
            }
            else if (SortValue == "3")
            {
                ViewerListview.Sort("Price", SortDirection.Descending);
            }
            else if (SortValue == "4")
            {
                ViewerListview.Sort("Name", SortDirection.Ascending);
            }
            else if (SortValue == "5")
            {
                ViewerListview.Sort("Name", SortDirection.Descending);
            }
            else if (SortValue == "6")
            {
                ViewerListview.Sort("BrandName", SortDirection.Ascending);
            }
            else if (SortValue == "7")
            {
                ViewerListview.Sort("BrandName", SortDirection.Descending);
            }
            //DataPager1.SetPageProperties(StartRow, PageSize, false);
            //SetPageSize();

        }

        protected void ViewerListview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblPostcode = e.Item.FindControl("lblPostcode") as Label;
            Label lblName = e.Item.FindControl("lblName") as Label;
            Label lblShortDescription = e.Item.FindControl("lblShortDescription") as Label;
            Label lblEligibility = e.Item.FindControl("lblEligibility") as Label;
            Label LblAddress = e.Item.FindControl("LblAddress") as Label;
            Label lblSuburb = e.Item.FindControl("lblSuburb") as Label;
            Label lblState = e.Item.FindControl("lblState") as Label;
            HtmlGenericControl divEligibility = e.Item.FindControl("divEligibility") as HtmlGenericControl;
            HiddenField hdnActivityID = e.Item.FindControl("hdnActivityID") as HiddenField;

            LblAddress.Visible = lblSuburb.Visible = lblState.Visible = lblPostcode.Visible = AddressVisible;
            lblEligibility.Visible = EligibilityVisible;
            lblName.Visible = NameVisible;
            lblShortDescription.Visible = ShortDescriptionVisible;
            divEligibility.Visible = EligibilityVisible;
            ScheduleViewerUC ScheduleViewerUC = e.Item.FindControl("ScheduleViewerUC1") as ScheduleViewerUC;
            if (UseTimetable)
            {
                //if (Timetable == (int)SystemConstants.TimetableFormat.Weekly)
                // {}

                ScheduleViewerUC.ActivityID = Convert.ToInt32(hdnActivityID.Value);
                ScheduleViewerUC.timetableFormat = (int)SystemConstants.TimetableFormat.Seasonal;
                
            }
            else
            {
                ScheduleViewerUC.Visible = false;
            }
        }

        private void SetDataSource()
        {
            ReportParameterPasser param = ReportParameterPasser.GetCurrentParameters();

            ods.TypeName = typeof(ProviderDAC).FullName;
            ods.EnablePaging = true;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("CategoryID", param.CategoryID.ToString());
            ods.SelectParameters.Add("ProviderID", ProviderID.ToString());
            ods.SelectParameters.Add("SearchKey", param.SearchKey.ToString());
            ods.SelectParameters.Add("ageFrom", param.AgeFrom.ToString());
            ods.SelectParameters.Add("ageTo", param.AgeTo.ToString());
            ods.SelectParameters.Add("postCode", param.PostCode.ToString());

            ods.SelectMethod = "RetrieveProviderActivitiesFilteredReport";
            ods.SelectCountMethod = "RetrieveProviderActivitiesFilteredReportCount";
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SortParameterName = "sortExpression";

            ViewerListview.DataSourceID = "ods";
            SortProducts();


        }


        private void InitListview()
        {
            ReportParameterPasser param = ReportParameterPasser.GetCurrentParameters();
            divTitle.InnerHtml = Title = param.Title;


            SetReportAttribute(param.ProviderID, param.NameVisible, param.ShortDescriptionVisible, param.EligibilityVisible, param.AddressVisible, param.WebsiteVisible, param.PriceVisible, param.TimetableFormat, param.UseTimetable);
            ViewerListview.GroupItemCount = 2;

            SetDataSource();
            SortProducts();
            ViewerListview.DataBind();

        }


    }


}