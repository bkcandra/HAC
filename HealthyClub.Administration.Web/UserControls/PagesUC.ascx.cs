using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.DA;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class PagesUC : System.Web.UI.UserControl
    {
        public int PageType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnPageType.Value))
                    return Convert.ToInt32(hdnPageType.Value);
                else return 1;
            }
            set
            {
                hdnPageType.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Refresh();
        }

        private void Refresh()
        {
            SetDataSource();
        }

        private void SetDataSource()
        {
            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("PageType", PageType.ToString());
            ods.SelectMethod = "RetrievePages";
            ods.EnablePaging = true;
            ods.SelectCountMethod = "RetrievePagesCount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.MaximumRowsParameterName = "amount";

            gridView1.DataSourceID = "ods";
            gridView1.DataBind();
        }

        protected void gridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDynamicPage" || e.CommandName == "EditDynamicPage")
            {
                var lnkEdit = e.CommandSource as Control;
                GridViewRow Row = lnkEdit.Parent.Parent as GridViewRow;
                HiddenField hdnPage = Row.FindControl("hdnPage") as HiddenField;


                if (e.CommandName == "DeleteDynamicPage")
                {
                    new AdministrationDAC().DeletePage(Convert.ToInt32(hdnPage.Value));
                    Refresh();
                }
                else if (e.CommandName == "EditDynamicPage")
                {
                    Response.Redirect("~/Pages/PageSetup.aspx?" + SystemConstants.PageID + "=" + hdnPage.Value + "&" + SystemConstants.PageType + "=" + PageType);
                }
            }
        }

        protected void gridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnPage = e.Row.FindControl("hdnPage") as HiddenField;
                Label lblPagelink = e.Row.FindControl("lblPagelink") as Label;

                lblPagelink.Text = SystemConstants.CustomerUrl + "Pages/" + hdnPage.Value;
            }
        }

    }
}