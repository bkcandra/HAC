using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class ActivityNavigationUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        internal void SetNavigation(string activityName, int ActivityID, int CategoryID, string CategoryName)
        {

            List<ActivityNavigation> list = new List<ActivityNavigation>()
            {
                new ActivityNavigation(){ID=0, Name="Activities"}                
            };

            if (!string.IsNullOrEmpty(CategoryName))
            {
                list.Add(new ActivityNavigation()
                {
                    ID = CategoryID,
                    Name = CategoryName
                });
            }

            list.Add(new ActivityNavigation() { ID = -1, Name = activityName });


            ListView1.DataSource = list;
            ListView1.DataBind();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HyperLink hlnkNavigation = e.Item.FindControl("hlnkNavigation") as HyperLink;
                HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;
                if (hdnID.Value == "0")
                    hlnkNavigation.NavigateUrl = "~/Activities";
                else if (hdnID.Value == "-1")
                {
                    hlnkNavigation.NavigateUrl = "";
                    hlnkNavigation.CssClass = "Act";
                }
                else
                {
                    hlnkNavigation.NavigateUrl = "~/Activities/Default.aspx?CategoryID=" + hdnID.Value;
                }

            }
        }
    }
    class ActivityNavigation
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}