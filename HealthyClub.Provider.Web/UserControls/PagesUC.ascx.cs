using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Provider.DA;

namespace HealthyClub.Providers.Web.UserControls
{
    public partial class PagesUC : System.Web.UI.UserControl
    {

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
            if (Page.RouteData.Values[SystemConstants.PageID] != null)
                PageID = Convert.ToInt32(Page.RouteData.Values[SystemConstants.PageID].ToString());
            else if (Request.QueryString[SystemConstants.PageID] != null)
                PageID = Convert.ToInt32(Request.QueryString[SystemConstants.PageID]);
            else
            {
                Response.Redirect("~/404.html");
                Response.End();
            }
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

            var dr = new ProviderDAC().RetrievePage(PageID);
            if (dr != null)
            {
                Page.Title =lblTitle.Text= dr.Title;
                Page.MetaDescription = dr.MetaDescription;
                divContent.InnerHtml = dr.PageContent;
            }
            else
            {
                //divContent.Controls.Clear();
                //var uc = this.LoadControl("~/UserControls/PageNotFoundUC.ascx") as PageNotFoundUC;

                //if (divContent != null)
                //    divContent.Controls.Add(uc);
            }
        }


    }

}