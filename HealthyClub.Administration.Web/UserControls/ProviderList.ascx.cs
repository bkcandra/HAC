using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using System.Web.Security;
using WebMatrix.WebData;
using HealthyClub.Utility;
using HealthyClub.Administration.BF;
using BCUtility;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ProviderList : System.Web.UI.UserControl
    {
        public string SearchString
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSearchString.Value))
                    return hdnSearchString.Value;
                else return "";
            }
            set
            {
                hdnSearchString.Value = value.ToString();
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
            SearchString = "";
            SetDataSource();
        }

        private void SetDataSource()
        {
            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("SearchString", SearchString);
            ods.MaximumRowsParameterName = "amount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.SelectMethod = "RetrieveProviderList";
            ods.SelectCountMethod = "RetrieveProviderListCount";
            ods.EnablePaging = true;

            gridview1.DataBind();

        }

        private AdministrationEDSC.ProviderProfilesDTDataTable GetSelected()
        {
            var dt = new AdministrationEDSC.ProviderProfilesDTDataTable();

            foreach (GridViewRow row in gridview1.Rows)
            {
                CheckBox chkSelected = row.FindControl("chkSelected") as CheckBox;


                if (chkSelected.Checked)
                {
                    Label lblEmail = row.FindControl("lblEmail") as Label;
                    LinkButton lblUsername = row.FindControl("lnkUserName") as LinkButton;
                    HiddenField hdnUserID = row.FindControl("hdnUserID") as HiddenField;
                    var dr = dt.NewProviderProfilesDTRow();
                    dr.Username = lblUsername.Text;
                    dr.UserID = new Guid(hdnUserID.Value);
                    dt.AddProviderProfilesDTRow(dr);
                }
            }
            return dt;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var dt = GetSelected();
            foreach (var dr in dt)
            {
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(dr.Username);
                Membership.DeleteUser(dr.Username, true);
            }
            Refresh();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SearchString = txtSearch.Text;
            SetDataSource();
        }

        protected void gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                var source = e.CommandSource as Control;
                GridViewRow row = source.Parent.Parent as GridViewRow;

                HiddenField hdnUserID = row.FindControl("hdnUserID") as HiddenField;
                if (!string.IsNullOrEmpty(hdnUserID.Value))
                {
                    var dr = new AdministrationDAC().RetrieveProviderProfiles(new Guid(hdnUserID.Value));
                    if (dr != null)
                    {
                        SetProviderDetails(dr);
                    }
                }
            }

        }

        private void SetProviderDetails(AdministrationEDSC.ProviderProfilesDTRow dr)
        {
            lnkConfirm.Enabled = lnkEdit.Enabled = lnkResenConfirmation.Enabled = true;
            lblUsername.Text = dr.Username;
            hdnID.Value = new MembershipHelper().GetUserID(dr.UserID).ToString();
            lblID.Text = "#" + hdnID.Value;
            lblGUID.Text = hdnUserID.Value = dr.UserID.ToString();
            hdnEmail.Value = dr.Email.ToString();
            if (WebSecurity.IsConfirmed(dr.Username))
            {
                hdnIsConfirmed.Value = true.ToString();
                lblConfirmed.Text = "Yes";
            }
            else
            {
                hdnIsConfirmed.Value = false.ToString();
                lblConfirmed.Text = "No";
            }
            int count = new AdministrationDAC().RetrieveProviderActivitiesCount(dr.UserID);
            lblActCount.Text = count.ToString();
            if (count > 1)
                lblActCount.Text += " Activities";
            else if (count == 0)
                lblActCount.Text = " No activity";
            else
                lblActCount.Text += " Activity";
            lnkEdit.NavigateUrl = "~/User/Provider.aspx?" + SystemConstants.ProviderID + "=" + hdnUserID.Value;

            hdnConfirmationToken.Value = new MembershipHelper().GetConfirmationCode(dr.UserID);
        }

        protected void gridview1_PageIndexChanged(object sender, EventArgs e)
        {
            SetDataSource();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            if (WebSecurity.ConfirmAccount(hdnConfirmationToken.Value))
                lblStatus.Text = "Account Confirmed - " + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
            else
                lblStatus.Text = "Failed to Confirm account";
        }

        protected void lnkResenConfirmation_Click(object sender, EventArgs e)
        {
            var MailConf = new AdministrationDAC().RetrieveWebConfiguration();
            var emTemp = new AdministrationDAC().RetrieveMailTemplate((int)SystemConstants.EmailTemplateType.ProviderWelcomeEmail);
            new AdministrationBFC().ParseEmail(emTemp, new Guid(hdnUserID.Value), hdnConfirmationToken.Value, (int)SystemConstants.EmailTemplateType.ProviderConfirmationEmail, 0);
            EmailSender.SendEmail(MailConf.SMTPAccount, hdnEmail.Value, emTemp.EmailSubject, emTemp.EmailBody, MailConf.SMTPHost, MailConf.SMTPPort, MailConf.SMTPUserName, MailConf.SMTPPassword, MailConf.SMTPSSL, MailConf.SMTPIIS);
            lblStatus.Text = "Email Sent - " + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
        }

        protected void gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgEmailIcon = e.Row.FindControl("imgEmailIcon") as Image;
                LinkButton lnkUserName = e.Row.FindControl("lnkUserName") as LinkButton;
                if (WebSecurity.IsConfirmed(lnkUserName.Text))
                {
                    imgEmailIcon.ImageUrl = "~/Content/StyleImages/Check.png";
                }
                else
                {
                    imgEmailIcon.ImageUrl = "~/Content/StyleImages/Cross.png";
                }
            }
        }
    }
}