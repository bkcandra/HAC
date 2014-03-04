using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Customer.DA;

namespace HealthyClub.Web.UserControls
{
    public partial class PagesUC : System.Web.UI.UserControl
    {
        public string PageName
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageName.Value))
                    return hdnPageName.Value;
                else return "";
            }
            set
            {
                hdnPageName.Value = value.ToString();
            }
        }

        public int PageID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageID.Value))
                    return Convert.ToInt32(hdnPageID.Value);
                else return 0;
            }
            set
            {
                hdnPageID.Value = value.ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Page.RouteData.Values[SystemConstants.PageID] != null)
            //    PageID = Convert.ToInt32(Page.RouteData.Values[SystemConstants.PageID].ToString());
            //else if (Request.QueryString[SystemConstants.PageID] != null)
            //    PageID = Convert.ToInt32(Request.QueryString[SystemConstants.PageID]);
            //else
            //{
            if (Page.RouteData.Values[SystemConstants.PageName] != null)
                PageName = Page.RouteData.Values[SystemConstants.PageName].ToString();
            else if (Request.QueryString[SystemConstants.PageName] != null)
            {
                PageName = Request.QueryString[SystemConstants.PageName];
            }
            else
            {
                Response.Redirect("~/404.html");
                Response.End();
            }
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            if (!string.IsNullOrEmpty(PageName))
            {
                var drname = new CustomerDAC().RetrievePage(PageName);
                if (drname != null)
                {
                    Page.Title = drname.Title;
                    Page.MetaDescription = drname.MetaDescription;
                    divContent.InnerHtml = drname.PageContent;
                }
                else
                {
                }
            }
            else
            {
                var dr = new CustomerDAC().RetrievePage(PageID);
                if (dr != null)
                {
                    Page.Title = dr.Title;
                    Page.MetaDescription = dr.MetaDescription;
                    divContent.InnerHtml = dr.PageContent;
                }
                else
                {
                    divContent.Controls.Clear();
                    var uc = this.LoadControl("~/UserControls/PageNotFoundUC.ascx") as PageNotFoundUC;

                    if (divContent != null)
                        divContent.Controls.Add(uc);
                }
            }
        }
    }
}