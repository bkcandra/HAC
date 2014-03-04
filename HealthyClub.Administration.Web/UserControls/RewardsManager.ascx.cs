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
    public partial class RewardsManager : System.Web.UI.UserControl
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
            SetRewardsTable();
        }

        private void SetRewardsTable()
        {
            var Rewards = new AdministrationDAC().RetrieveRewardsExplorer();

            rptRewards.DataSource = Rewards;
            rptRewards.DataBind();

        }
        protected void rptRewards_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdntimes = e.Item.FindControl("Timesused") as HiddenField;
                HiddenField hdnused = e.Item.FindControl("Usagetimes") as HiddenField;
                Label lblStatus = e.Item.FindControl("lblStatus") as Label;
                Label lblRwdID = e.Item.FindControl("lblRwdID") as Label;
                if (hdntimes.Value == "")
                    hdntimes.Value = "0";
                if (hdnused.Value == "")
                    hdnused.Value = "0";
                if (Convert.ToInt32(hdntimes.Value) >= Convert.ToInt32(hdnused.Value))
                    lblStatus.Text = "Inactive";
                else
                    lblStatus.Text = "Active";
                Label lblleft = e.Item.FindControl("Coupons_left") as Label;
                lblleft.Text = Convert.ToString(Convert.ToInt32(hdnused.Value) - Convert.ToInt32(hdntimes.Value));
                HiddenField hdnRwdtype = e.Item.FindControl("hdnRwdtype") as HiddenField;
                Label lblRwdtype = e.Item.FindControl("lblRwdtype") as Label;
                int rwdtype = Convert.ToInt32(hdnRwdtype.Value);
                if (rwdtype == (int)SystemConstants.RewardType.Discount)
                    lblRwdtype.Text = "Discount";
                else if (rwdtype == (int)SystemConstants.RewardType.Offer)
                    lblRwdtype.Text = "Offer";
                else if (rwdtype == (int)SystemConstants.RewardType.Other)
                    lblRwdtype.Text = "Other";
                else if (rwdtype == (int)SystemConstants.RewardType.Gift)
                    lblRwdtype.Text = "Gift";

                HiddenField hdnProviderID = e.Item.FindControl("hdnProviderID") as HiddenField;
                HyperLink hlnkProviderID = e.Item.FindControl("hlnkProviderID") as HyperLink;
                HyperLink hlnkSponsorName = e.Item.FindControl("hlnkSponsorName") as HyperLink;
                Guid spnId = new Guid(hdnProviderID.Value);
                hlnkSponsorName.NavigateUrl = "~/Rewards/SponsorTools.aspx?" + SystemConstants.qs_SponsorsID + "=" + hdnProviderID.Value;
                Label lblsponsor = e.Item.FindControl("lblsponsor") as Label;
                AdministrationDAC dac = new AdministrationDAC();
                lblsponsor.Text = dac.getSponsorName(spnId);
                HiddenField hdnRewardsID = e.Item.FindControl("hdnRewardsID") as HiddenField;
                HyperLink hlnkRewardsName = e.Item.FindControl("hlnkRewardName") as HyperLink;
                hlnkRewardsName.NavigateUrl = "~/Rewards/Modify.aspx?" + SystemConstants.qs_RewardsID + "=" + hdnRewardsID.Value;
                
            }
        }
         private void ModifyCheckbox(bool selectAll, bool selectExpired)
        {
            foreach (RepeaterItem item in rptRewards.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnRewardsID = item.FindControl("hdnRewardsID") as HiddenField;
                    Label lblStatus = item.FindControl("lblStatus") as Label;

                    chkAct.Checked = selectAll;
                    if (selectExpired && lblStatus.Text == SystemConstants.ActivityStatus.Expired.ToString())
                        chkAct.Checked = selectExpired;
                }
            }
        }

        private List<int> GetSelectedRewardID()
        {
            List<int> Selected = new List<int>();

            foreach (RepeaterItem item in rptRewards.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnRewardsID = item.FindControl("hdnRewardsID") as HiddenField;
                    Label lblRwdID = item.FindControl("lblRwdID") as Label;

                    if (chkAct.Checked)
                    {
                        Selected.Add(Convert.ToInt32(lblRwdID.Text));
                    }
                }
            }
            return Selected;
        }

        private List<int> GetSelectedProviderID()
        {
            List<int> Selected = new List<int>();

            foreach (RepeaterItem item in rptRewards.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkAct = item.FindControl("chkAct") as CheckBox;
                    HiddenField hdnProviderID = item.FindControl("hdnProviderID") as HiddenField;
                    HiddenField hdnRewardsID = item.FindControl("hdnRewardsID") as HiddenField;

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

     

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var dt = GetSelectedRewardID();
            if (dt.Count != 0)
            {
                new AdministrationBFC().DeleteRewards(dt);
            }
            Refresh();
        }

        protected void lnkSelectExp_Click(object sender, EventArgs e)
        {
            ModifyCheckbox(false, true);
        }

      /*  protected void lnkExtendRewards_Click(object sender, EventArgs e)
        {
            divSuccess.Visible = divError.Visible = false;
            if (Convert.ToInt32(txtExtend.Text) > 0)
            {
                try
                {
                    //var selectedDT = GetSelectedRewardID();
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
        }*/
    }
}
 