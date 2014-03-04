using BCUtility;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class RewardsAddition : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDDL();
                Refresh();

            }
        }


        private void initDDL()
        {
            var dt = new AdministrationDAC().RetrieveRewardTypes();
            foreach (var dr in dt)
            {
                ddlRewType.Items.Add(new ListItem(dr.TypeName, dr.Type.ToString()));
            }

            var discActDt = new AdministrationDAC().RetrieveActivities();
            foreach (var discActDr in discActDt)
            {
                ddlDiscAct.Items.Add(new ListItem(discActDr.Name, discActDr.ID.ToString()));
            }

            var reqActDt = new AdministrationDAC().RetrieveActivities();
            foreach (var reqActDr in reqActDt)
            {
                ddlReqActivityName.Items.Add(new ListItem(reqActDr.Name, reqActDr.ID.ToString()));
            }

            var freeActDt = new AdministrationDAC().RetrieveActivities();
            foreach (var freeActDr in freeActDt)
            {
                ddlFreeAct.Items.Add(new ListItem(freeActDr.Name, freeActDr.ID.ToString()));
            }

            var bonusActDt = new AdministrationDAC().RetrieveActivities();
            foreach (var bonusActDr in bonusActDt)
            {
                ddlBonusAct.Items.Add(new ListItem(bonusActDr.Name, bonusActDr.ID.ToString()));
            }

            var allsponsors = new AdministrationDAC().RetrieveSponsorsExplorer();
            foreach (var allsponsor in allsponsors)
            {
                ddlsponsors.Items.Add(new ListItem(allsponsor.Name, allsponsor.ID.ToString()));
            
            }

        }
        public void Refresh()
        {
            if (select.SelectedIndex == 0)
                selection.Text = "Discount Rate";
            else
                selection.Text = "Money Off";
            if(ddlRewType.SelectedIndex == 0)
                ddlRewType.SelectedIndex = 1;
            if (ddlRewType.SelectedIndex == (int)SystemConstants.RewardType.Gift)
            {
                discount.Visible = false;
                offer.Visible = false;
                other.Visible = false;
                gift.Visible = true;
            }
            else if (ddlRewType.SelectedIndex == (int)SystemConstants.RewardType.Offer)
            {
                offer.Visible = true;
                discount.Visible = false;

                other.Visible = false;
                gift.Visible = false;
            }
            else if (ddlRewType.SelectedIndex == (int)SystemConstants.RewardType.Other)
            {
                discount.Visible = false;
                offer.Visible = false;
                other.Visible = true;
                gift.Visible = false;
            }

            else if (ddlRewType.SelectedIndex == (int)SystemConstants.RewardType.Discount)
            {
                discount.Visible = true;
                offer.Visible = false;
                other.Visible = false;
                gift.Visible = false;
            }
            


        }

        protected void ddlRewType_SelectedIndexChanged(object sender, EventArgs e)
        {



            Refresh();
        }

        protected void select_index(object sender, EventArgs e)
        {

            Refresh();
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AdministrationEDSC.RewardDTRow dr = GetDetails();
                AdministrationEDSC.RewardsDetailsDTRow drDet = GetRwrdDetails();
                AdministrationEDSC.RewardImageDTRow drt = GetImgDetails();

                if (dr != null)
                {
                    if (drt != null)
                    {
                        dr.RewardImage = true;

                    }
                    else
                        dr.RewardImage = false;
                    new AdministrationBFC().SaveRewards(dr, drDet, drt);
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception esd)
            {
                addeddiv.Visible = true;
            
            }
            
        }

        private AdministrationEDSC.RewardImageDTRow GetImgDetails()
        {

            if (fileUpRewardImage.HasFile)
            {
                var dr = new AdministrationEDSC.RewardImageDTDataTable().NewRewardImageDTRow();
                dr.ID = 0;
                dr.Description = fileUpRewardImage.FileName;
                dr.ImageTitle = dr.Filename = fileUpRewardImage.FileName;
                dr.Filesize = fileUpRewardImage.PostedFile.ContentLength / 1024;
                dr.ImageStream = new ImageHandler().ReadFully(fileUpRewardImage.PostedFile.InputStream);
                return dr;
            }
            else return null;
        }

        

        protected AdministrationEDSC.RewardDTRow GetDetails()
        {
            var dr = new AdministrationEDSC.RewardDTDataTable().NewRewardDTRow();

            dr.ID = 0;
            Guid spnid= new Guid(ddlsponsors.Text);
            dr.ProviderID = spnid; 
            
            
            dr.RewardsName = rname.Text;
            dr.RewardDescription = rdesc.Text;

            if (txtCalendarFrom.Text != "")
            {
                DateTime RwrdExp = DateTime.Now;

                if (DateTime.TryParse(txtCalendarFrom.Text, out  RwrdExp))
                    dr.RewardExpiryDate = RwrdExp;
                else
                    dr.RewardExpiryDate = DateTime.Now.AddDays(90);
            }
            else
            {
                lbldate.Visible = true;
                return null;
            
            }
            dr.RequiredRewardPoint = Convert.ToInt32(point.Text);
            if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Gift))
                dr.RewardType = (int)SystemConstants.RewardType.Gift;
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Offer))
                dr.RewardType = (int)SystemConstants.RewardType.Offer;
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Other))
                dr.RewardType = (int)SystemConstants.RewardType.Other;
            else
                dr.RewardType = (int)SystemConstants.RewardType.Discount;
            dr.CategoryID = 0;
    
            if (disperc.Text != "")
            {
                if (select.SelectedIndex == 1)
                {
                    dr.Discount = 0;
                    dr.SupportOFF = Convert.ToInt32(disperc.Text);

                }
                else
                {
                    dr.SupportOFF = 0;
                    dr.Discount = Convert.ToInt32(disperc.Text);

                }
            }
            else
            {
                dr.Discount = 0;
                dr.SupportOFF = 0;
            
            }
            if (ddlReqActivityName.SelectedValue == "")
            {
                dr.RequiredActivityEnroll = 0;
            }
            else
            {
                dr.RequiredActivityEnroll = Convert.ToInt32(ddlReqActivityName.SelectedValue);
            
            }
            if (Bonuspoint.Text != "")
                dr.BonusPoint = Convert.ToInt32(Bonuspoint.Text);
            else
                dr.BonusPoint = 0;
            if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Gift))
                dr.FreeActivityID = 0;
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Offer))
                dr.FreeActivityID = Convert.ToInt32(ddlFreeAct.SelectedValue);
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Other))
                dr.FreeActivityID = Convert.ToInt32(ddlBonusAct.SelectedValue);
            else
                dr.FreeActivityID = Convert.ToInt32(ddlDiscAct.SelectedValue);


            
            dr.FreeGift = giftname.Text;
            dr.UsageTimes = Convert.ToInt32(usage.Text);
            dr.NofTimeUsed = 0;
            if (ddlrewsource.SelectedItem.Text == "Internal/Activity Based")
                dr.RewardSource = 1;
            else
                dr.RewardSource = 2;
            return dr;
        }

        public AdministrationEDSC.RewardsDetailsDTRow GetRwrdDetails()
        {
            var dt = new AdministrationEDSC.RewardsDetailsDTDataTable().NewRewardsDetailsDTRow();

            dt.RewardID = 0;
            dt.CreatedDateTime = DateTime.Now;

            if (txtCalendarFrom.Text != "")
            {
                DateTime RwrdExp = DateTime.Now;

                if (DateTime.TryParse(txtCalendarFrom.Text, out  RwrdExp))
                    dt.ExpiryDate = RwrdExp;
                else
                   dt.ExpiryDate = DateTime.Now.AddDays(90);
            }
            else
            {
                lbldate.Visible = true;
                return null;

            }
            
           
            dt.ModifiedDateTime = DateTime.Now;
            
            dt.Keywords = Keyword.Text;
            dt.CreatedBy = WebSecurity.CurrentUserName;
            dt.ModifiedBy = WebSecurity.CurrentUserName;
            return dt;
        }
    }
}