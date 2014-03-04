using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class CategoryNavigationUC : System.Web.UI.UserControl
    {
        public delegate void CategoryHandler(int ID);
        public event CategoryHandler RefreshNavigation;

        public int CategoryID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnCategoryID.Value))
                    return Convert.ToInt32(hdnCategoryID.Value);
                else
                    return 0;
            }
            set
            {
                hdnCategoryID.Value = value.ToString();
                SetNavigation(value);
            }

        }

        public void SetNavigation(int categoryID)
        {

            AdministrationEDSC.CategoryDTRow dr = new AdministrationDAC().RetrieveCategory(categoryID);

            List<CategoryNavigation> list = new List<CategoryNavigation>()
            {
                new CategoryNavigation(){ID=0, Name="Root"}
            };

            if (dr != null)
            {

                if (!dr.IsLevel1ParentNameNull())
                {
                    list.Add(new CategoryNavigation()
                    {
                        ID = dr.Level1ParentID,
                        Name = dr.Level1ParentName
                    });
                }

                if (!dr.IsLevel2ParentNameNull())
                {
                    list.Add(new CategoryNavigation()
                    {
                        ID = dr.Level2ParentID,
                        Name = dr.Level2ParentName
                    });
                }


                list.Add(new CategoryNavigation() { ID = dr.ID, Name = dr.Name });
            }

            ListView1.DataSource = list;
            ListView1.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;
            int ID = Convert.ToInt32(hdnID.Value);

            if (e.CommandName == "FindFolder")
            {
                if (RefreshNavigation != null)
                {
                    RefreshNavigation(ID);
                    SetNavigation(ID);
                }
            }

        }
    }

    class CategoryNavigation
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}