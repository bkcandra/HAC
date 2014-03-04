using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.BF;
using System.Web.Security;
using HealthyClub.Utility;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class webLog : System.Web.UI.UserControl
    {
        public SystemConstants.FormMode noteMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnNoteMode.Value))
                    return (SystemConstants.FormMode)Convert.ToInt32(hdnNoteMode.Value);
                else
                    return SystemConstants.FormMode.View;
            }
            set
            {
                hdnNoteMode.Value = Convert.ToInt32(value).ToString();

                if (value == SystemConstants.FormMode.New)
                {
                    btnNoteSave.Visible = true;
                    btnNoteCancel.Visible = true;

                    btnNoteEdit.Visible = false;
                    lblStatus.Visible = true;
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    btnNoteSave.Visible = true;
                    btnNoteCancel.Visible = true;

                    btnNoteEdit.Visible = false;
                    lblStatus.Visible = true;
                }
                else if (value == SystemConstants.FormMode.View)
                {
                    btnNoteSave.Visible = false;
                    btnNoteCancel.Visible = false;

                    btnNoteEdit.Visible = true;
                    lblStatus.Visible = false;

                    lblStatus.Text = "";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Refresh();
        }

        private void Refresh()
        {
            if (ddlMaintenanceType.SelectedValue == "1")
            {
                listviewActivities.Visible = true;
                ListViewLogAction.Visible = true;

                ListViewWebLogGroup.Visible = false;
                ListViewActLog.Visible = false;

                SetMaintenanceDataSource();
            }
            else
            {
                listviewActivities.Visible = false;
                ListViewLogAction.Visible = false;

                ListViewWebLogGroup.Visible = true;
                ListViewActLog.Visible = true;
                SetActivityDataSource();
            }
        }

        private void SetActivityDataSource()
        {
            ListViewWebLogGroup.DataSource = new AdministrationDAC().RetrieveActivityLogGroups();
            ListViewWebLogGroup.DataBind();
        }

        private void SetMaintenanceDataSource()
        {
            listviewActivities.DataSource = new AdministrationDAC().RetrieveLogs();
            listviewActivities.DataBind();
        }

        protected void btnUpdateActivity_Click(object sender, EventArgs e)
        {
            new AdministrationBFC().ActivityMaintenance(Membership.GetUser().UserName);
            Refresh();
        }





        protected void btnNoteEdit_Click(object sender, EventArgs e)
        {
            noteMode = SystemConstants.FormMode.Edit;
        }

        protected void btnNoteCancel_Click(object sender, EventArgs e)
        {
            noteMode = SystemConstants.FormMode.View;
            string message = new AdministrationDAC().RetrieveActivityLogNote(Convert.ToInt32(HdnNoteValue.Value)); ;
            if (!string.IsNullOrEmpty(message))
                txtNote.Text = message;
        }

        protected void btnNoteSave_Click(object sender, EventArgs e)
        {
            noteMode = SystemConstants.FormMode.View;
            if (hdnNoteActionID.Value == ((int)SystemConstants.LogNoteType.ActivityLog).ToString())
            {
                new AdministrationDAC().UpdateActivityLogNote(Convert.ToInt32(HdnNoteValue.Value), txtNote.Text);
            }
        }

        protected void ListViewLogAction_ItemDataBound(object sender, ListViewCommandEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnCategory = e.Item.FindControl("hdnCategory") as HiddenField;
                Label lblActionLog = e.Item.FindControl("lblActionLog") as Label;
                if (hdnCategory.Value == ((int)SystemConstants.maintenanceCategoryAction.ActivityStatusChange).ToString())
                    lblActionLog.Text = "Change Activity Status";
                else if (hdnCategory.Value == ((int)SystemConstants.maintenanceCategoryAction.ActivityStatusChangeAndSendingEmail).ToString())
                    lblActionLog.Text = "Change Activity Status, Sending Notification";
            }
        }

        protected void ListViewLogAction_ItemCommand(object source, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                LinkButton lnkDtails = e.CommandSource as LinkButton;
                RepeaterItem row = lnkDtails.Parent as RepeaterItem;

                HiddenField hdnNote = row.FindControl("hdnNote") as HiddenField;
                HiddenField hdnActionID = row.FindControl("hdnActionID") as HiddenField;

                HiddenField hdnMessage = row.FindControl("hdnMessage") as HiddenField;
                if (e.CommandName == "ShowDetails")
                {
                    hdnMessage.Value = hdnMessage.Value.Replace("%NL%", Environment.NewLine);
                    txtMessage.Text = hdnMessage.Value;
                    //HdnNoteValue.Value = txtNote.Text = hdnNote.Value;
                    //hdnNoteActionID.Value = hdnActionID.Value;
                }
            }
        }

        protected void ddlMaintenanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void ListViewActLog_ItemCommand(object source, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                LinkButton lnkDtails = e.CommandSource as LinkButton;
                RepeaterItem row = lnkDtails.Parent as RepeaterItem;

                HiddenField hdnNote = row.FindControl("hdnNote") as HiddenField;
                HiddenField hdnLogType = row.FindControl("hdnLogType") as HiddenField;

                HiddenField hdnMessage = row.FindControl("hdnMessage") as HiddenField;
                if (e.CommandName == "ShowDetails")
                {
                    hdnMessage.Value = hdnMessage.Value.Replace("%NL%", Environment.NewLine);
                    txtMessage.Text = hdnMessage.Value;
                    HdnNoteValue.Value = txtNote.Text = hdnNote.Value;
                    hdnNoteActionID.Value = ((int)SystemConstants.LogNoteType.ActivityLog).ToString();
                }
            }
        }

        protected void ListViewActLog_ItemDataBound(object sender, ListViewCommandEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnNotificationNumber = e.Item.FindControl("hdnNotificationNumber") as HiddenField;
                Label lblNotificationNumber = e.Item.FindControl("lblNotificationNumber") as Label;

                Label lblActLogType = e.Item.FindControl("lblActLogType") as Label;

                lblActLogType.Text = "Change Activity Status ";
                if (hdnNotificationNumber.Value == ((int)SystemConstants.NotificationType.Expiring2week).ToString())
                    lblNotificationNumber.Text = "2 Week Expiry Notification";
                else if (hdnNotificationNumber.Value == ((int)SystemConstants.NotificationType.Expiring1week).ToString())
                    lblNotificationNumber.Text = "1 Week Expiry Notification";
                else if (hdnNotificationNumber.Value == ((int)SystemConstants.NotificationType.Expired).ToString())
                    lblNotificationNumber.Text = "Expired Notification";
                else if (hdnNotificationNumber.Value == ((int)SystemConstants.NotificationType.NoNotification).ToString())
                    lblNotificationNumber.Text = "No Notification";
            }
        }

        protected void listviewActivities_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                LinkButton lnkDtails = e.CommandSource as LinkButton;

                HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;
                HiddenField hdnMessage = e.Item.FindControl("hdnMessage") as HiddenField;
                if (e.CommandName == "ShowDetails")
                {
                    hdnMessage.Value = hdnMessage.Value.Replace("%NL%", Environment.NewLine);
                    txtMessage.Text = hdnMessage.Value;
                    ListViewLogAction.DataSource = new AdministrationDAC().RetrieveLogActions(Convert.ToInt32(hdnID.Value));
                    ListViewLogAction.DataBind();
                }
            }
        }

        protected void listviewActivities_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnCategory = e.Item.FindControl("hdnCategory") as HiddenField;
                Label lblCategory = e.Item.FindControl("lblCategory") as Label;
                if (hdnCategory.Value == ((int)SystemConstants.maintenanceCategory.MaintenanceActivityStatus).ToString())
                    lblCategory.Text = "Activities Maintenance";
            }
        }

        protected void ListViewWebLogGroup_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hdnActID = e.Item.FindControl("hdnActID") as HiddenField;
                Label lblActivityName = e.Item.FindControl("lblActivityName") as Label;
                Label lblCreatedDate = e.Item.FindControl("lblCreatedDate") as Label;

                var dr = new AdministrationDAC().RetrieveActivity(Convert.ToInt32(hdnActID.Value));
                if (dr != null)
                    lblActivityName.Text = dr.Name;
                else
                    lblActivityName.Text = "[Activity Name]";

                DateTime exp = Convert.ToDateTime(lblCreatedDate.Text);
                lblCreatedDate.Text = exp.ToShortDateString();
            }
        }

        protected void ListViewWebLogGroup_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                LinkButton lnkDtails = e.CommandSource as LinkButton;
                HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;

                if (e.CommandName == "ShowDetails")
                {
                    //hdnMessage.Value = hdnMessage.Value.Replace("%NL%", Environment.NewLine);
                    txtMessage.Text = "";
                    ListViewActLog.DataSource = new AdministrationDAC().RetrieveActivitiesLogActions(Convert.ToInt32(hdnID.Value));
                    ListViewActLog.DataBind();
                }
            }
        }
    }
}