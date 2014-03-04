using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivitiesManagerUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            SetActivitiesTable();
        }

        private void SetActivitiesTable()
        {
            var Activities = new AdministrationDAC().RetrieveActivitiesExplorer();

            rptActivities.DataSource = Activities;
            rptActivities.DataBind();

        }

        protected void rptActivities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnStatus = e.Item.FindControl("hdnStatus") as HiddenField;
                Label lblStatus = e.Item.FindControl("lblStatus") as Label;

                if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Active).ToString())
                {
                    lblStatus.Text = "Active";
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.Expired).ToString())
                {
                    lblStatus.Text = "Expired";
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.NotActive).ToString())
                {
                    lblStatus.Text = "Not Active";
                }
                else if (hdnStatus.Value == ((int)SystemConstants.ActivityStatus.WillExpire).ToString())
                {
                    lblStatus.Text = "Expiring";
                }

                HiddenField hdnIsApproved = e.Item.FindControl("hdnIsApproved") as HiddenField;
                Label lblApproved = e.Item.FindControl("lblApproved") as Label;
                Label lblActID = e.Item.FindControl("lblActID") as Label;

                if (hdnIsApproved.Value == true.ToString())
                {
                    lblApproved.Text = "Approved";
                }
                else if (hdnIsApproved.Value == false.ToString())
                {
                    lblApproved.Text = "Waiting Approval";
                }
                else if (String.IsNullOrEmpty(hdnIsApproved.Value))
                {
                    lblApproved.Text = "Not Approved";
                }

                HiddenField hdnProviderID = e.Item.FindControl("hdnProviderID") as HiddenField;
                HyperLink hlnkProviderID = e.Item.FindControl("hlnkProviderID") as HyperLink;
                hlnkProviderID.NavigateUrl = "~/User/Provider.aspx?" + SystemConstants.ProviderID + "=" + hdnProviderID.Value;
                HiddenField hdnActivityID = e.Item.FindControl("hdnActivityID") as HiddenField;
                HyperLink hlnkActivityName = e.Item.FindControl("hlnkActivityName") as HyperLink;
                hlnkActivityName.NavigateUrl = "~/Activities/Activity.aspx?" + SystemConstants.ActivityID + "=" + hdnActivityID.Value;
                lblActID.Text = hdnActivityID.Value;
            }
        }

        private void ModifyCheckbox(bool selectAll, bool selectExpired)
        {
            foreach (RepeaterItem item in rptActivities.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnActivityID = item.FindControl("hdnActivityID") as HiddenField;
                    Label lblStatus = item.FindControl("lblStatus") as Label;

                    chkAct.Checked = selectAll;
                    if (selectExpired && lblStatus.Text == SystemConstants.ActivityStatus.Expired.ToString())
                        chkAct.Checked = selectExpired;
                }
            }
        }

        private List<int> GetSelectedActivityID()
        {
            List<int> Selected = new List<int>();

            foreach (RepeaterItem item in rptActivities.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnActivityID = item.FindControl("hdnActivityID") as HiddenField;
                    Label lblActID = item.FindControl("lblActID") as Label;

                    if (chkAct.Checked)
                    {
                        Selected.Add(Convert.ToInt32(lblActID.Text));
                    }
                }
            }
            return Selected;
        }

        private List<int> GetSelectedProviderID()
        {
            List<int> Selected = new List<int>();

            foreach (RepeaterItem item in rptActivities.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnActivityID = item.FindControl("hdnActivityID") as HiddenField;

                    if (chkAct.Checked)
                    {
                        Selected.Add(Convert.ToInt32(hdnProviderID.Value));
                    }
                }
            }
            return Selected;
        }

        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {
            ModifyCheckbox(true, false);
        }

        protected void lnkUnselectAll_Click(object sender, EventArgs e)
        {
            ModifyCheckbox(false, false);
        }

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            var dt = GetSelectedActivityID();
            if (dt.Count != 0)
            {
                new AdministrationBFC().ConfirmActivities(dt);
            }
            Refresh();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var dt = GetSelectedActivityID();
            if (dt.Count != 0)
            {
                new AdministrationBFC().DeleteActivities(dt);
            }
            Refresh();
        }

        protected void lnkSelectExp_Click(object sender, EventArgs e)
        {
            ModifyCheckbox(false, true);
        }

        protected void lnkExtendActivities_Click(object sender, EventArgs e)
        {
            divSuccess.Visible = divError.Visible = false;
            if (Convert.ToInt32(txtExtend.Text) > 0)
            {
                try
                {
                    //var selectedDT = GetSelectedActivityID();
                    int activitiesCount = 0;
                    new AdministrationBFC().ExtendActivitiesExpiryDate(Convert.ToInt32(txtExtend.Text), out activitiesCount);
                    divSuccess.Visible = true;
                    lblSuccess.Text = activitiesCount + "Activities are edited, activity database will be updated every 2AM.";
                }
                catch (Exception ex)
                {
                    divError.Visible = true;
                    lblError.Text = "error = " + ex.Message;
                }
            }
            else
            {
                divError.Visible = true;
                lblError.Text = "Extend day must be greater than zero!";
            }
        }
    }
}