using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class ActivityListingNavigation : System.Web.UI.UserControl
    {
        public delegate void ListingNavigation(int Type);
        public event ListingNavigation CloseNavEvent;

        public bool Filtered
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFiltered.Value))
                    return Convert.ToBoolean(hdnFiltered.Value);
                else return false;
            }
            set
            {
                hdnFiltered.Value = value.ToString();
            }
        }

        public string SearchKey
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSearchKey.Value))
                    return hdnSearchKey.Value;
                else return null;
            }
            set
            {
                hdnSearchKey.Value = value;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetNavigation();
        }

        internal void SetNavigation()
        {
            List<ActivityListNavigation> list = new List<ActivityListNavigation>();

            if (!string.IsNullOrEmpty(SearchKey))
            {
                list.Add(new ActivityListNavigation()
                {
                    Name = SearchKey,
                    Type = (int)SystemConstants.ListingNavigationType.search
                });
            }
            if (Filtered)
            {
                list.Add(new ActivityListNavigation()
                {
                    Name = "",
                    Type = (int)SystemConstants.ListingNavigationType.filter
                });
            }
            if (list.Count != 0)
            {
                ListView1.DataSource = list;
                ListView1.DataBind();
            }
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblNavigation = e.Item.FindControl("lblNav") as Label;

                HiddenField hdnType = e.Item.FindControl("hdnType") as HiddenField;
                if (hdnType.Value == ((int)SystemConstants.ListingNavigationType.search).ToString())
                    lblNavigation.Text = "Search : '" + lblNavigation.Text + "'";


                else if (hdnType.Value == ((int)SystemConstants.ListingNavigationType.filter).ToString())
                {
                    lblNavigation.Text = "Filtered";

                }
            }
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName != null && !string.IsNullOrEmpty(e.CommandName))
            {
                HiddenField hdnType = e.Item.FindControl("hdnType") as HiddenField;

                if (e.CommandName == "CloseNav")
                {
                    if (CloseNavEvent != null)
                    {
                        CloseNavEvent(Convert.ToInt32(hdnType.Value));
                    }
                }
            }
        }
    }

    class ActivityListNavigation
    {
        public int Type { get; set; }
        public string Name { get; set; }
    }


}