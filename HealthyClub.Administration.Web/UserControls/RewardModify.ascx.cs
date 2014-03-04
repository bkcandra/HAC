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
    public partial class RewardModify : System.Web.UI.UserControl
    {
        public int FormMode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFormMode.Value))
                {
                    return Convert.ToInt32(hdnFormMode.Value);
                }
                return (int)HealthyClub.Utility.SystemConstants.FormMode.View;
            }
            set
            {
                if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.New)
                {
                    lblRewName.Visible = true;
                    lblRewDesc.Visible = true;
                    lblCalendarFrom.Visible = true;
                    lblRewtype.Visible = true;
                    lblSponsor.Visible = true;
                    lblReqRewardPoints.Visible = true;
                    lblKeywords.Visible = true;
                    lblUsageTimes.Visible = true;
                    lblImage.Visible = true;
                    lblDiscSelect.Visible = true;
                    lblDiscAct.Visible = true;
                    lblDiscPerc.Visible = true;
                    lblReqActivityName.Visible = true;
                    lblFreeAct.Visible = true;
                    lblGiftName.Visible = true;
                    lblBonusPoint.Visible = true;
                    lblBonusAct.Visible = true;
                    lblrewsource.Visible = true;

                    txtRewName.Visible = true;
                    txtRewDesc.Visible = true;
                    txtCalendarFrom.Visible = true;
                    ddlRewType.Visible = true;
                    ddlSponsor.Visible = true;
                    txtReqRewardPoints.Visible = true;
                    txtKeywords.Visible = true;
                    txtUsageTimes.Visible = true;
                    ddlDiscSelect.Visible = true;
                    ddlDiscAct.Visible = true;
                    txtDiscPerc.Visible = true;
                    ddlReqActivityName.Visible = true;
                    ddlFreeAct.Visible = true;
                    txtGiftName.Visible = true;
                    txtBonusPoint.Visible = true;
                    ddlBonusAct.Visible = true;
                    ddlrewsource.Visible = true;
                    fileUpRewardImage.Visible = false;
                    imgPreview.Visible = true;

                    lnkEdit.Visible = true;
                    lnkUpdate.Visible = true;
                    lnkback.Visible = true;
                }
                else if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.Edit)
                {
                    lblRewName.Visible = false;
                    lblRewDesc.Visible = false;
                    lblCalendarFrom.Visible = false;
                    lblRewtype.Visible = false;
                    lblSponsor.Visible = false;
                    lblReqRewardPoints.Visible = false;
                    lblKeywords.Visible = false;
                    lblUsageTimes.Visible = false;
                    lblImage.Visible = false;
                    lblDiscSelect.Visible = false;
                    lblDiscAct.Visible = false;
                    lblDiscPerc.Visible = false;
                    lblReqActivityName.Visible = false;
                    lblFreeAct.Visible = false;
                    lblGiftName.Visible = false;
                    lblBonusPoint.Visible = false;
                    lblBonusAct.Visible = false;
                    lblrewsource.Visible = false;
                    lblImage.Visible = true;
                    txtRewName.Visible = true;
                    txtRewDesc.Visible = true;
                    txtCalendarFrom.Visible = true;
                    ddlRewType.Visible = true;
                    ddlSponsor.Visible = true;
                    txtReqRewardPoints.Visible = true;
                    txtKeywords.Visible = true;
                    txtUsageTimes.Visible = true;
                    ddlDiscSelect.Visible = true;
                    ddlDiscAct.Visible = true;
                    txtDiscPerc.Visible = true;
                    ddlReqActivityName.Visible = true;
                    ddlFreeAct.Visible = true;
                    txtGiftName.Visible = true;
                    txtBonusPoint.Visible = true;
                    ddlBonusAct.Visible = true;
                    ddlrewsource.Visible = true;
                    fileUpRewardImage.Visible = true;
                    imgPreview.Visible = false;

                    lnkEdit.Visible = false;
                    lnkUpdate.Visible = true;
                    lnkback.Visible = true;
                }
                else if (value == (int)HealthyClub.Utility.SystemConstants.FormMode.View)
                {
                    lblRewName.Visible = true;
                    lblRewDesc.Visible = true;
                    lblCalendarFrom.Visible = true;
                    lblRewtype.Visible = true;
                    lblSponsor.Visible = true;
                    lblReqRewardPoints.Visible = true;
                    lblKeywords.Visible = true;
                    lblUsageTimes.Visible = true;
                    lblImage.Visible = true;
                    lblDiscSelect.Visible = true;
                    lblDiscAct.Visible = true;
                    lblDiscPerc.Visible = true;
                    lblReqActivityName.Visible = true;
                    lblFreeAct.Visible = true;
                    lblGiftName.Visible = true;
                    lblBonusPoint.Visible = true;
                    lblBonusAct.Visible = true;
                    lblrewsource.Visible = true;
                  

                    txtRewName.Visible = false;
                    txtRewDesc.Visible = false;
                    txtCalendarFrom.Visible = false;
                    ddlRewType.Visible = false;
                    ddlSponsor.Visible = false;
                    txtReqRewardPoints.Visible = false;
                    txtKeywords.Visible = false;
                    txtUsageTimes.Visible = false;
                    ddlDiscSelect.Visible = false;
                    ddlDiscAct.Visible = false;
                    txtDiscPerc.Visible = false;
                    ddlReqActivityName.Visible = false;
                    ddlFreeAct.Visible = false;
                    txtGiftName.Visible = false;
                    txtBonusPoint.Visible = false;
                    ddlBonusAct.Visible = false;
                    ddlrewsource.Visible = false;
                    fileUpRewardImage.Visible = false;
                    lblImage.Visible = false;
                    imgPreview.Visible = true;

                    lnkEdit.Visible = true;
                    lnkUpdate.Visible = false;
                    lnkback.Visible = true;
                }
                hdnFormMode.Value = value.ToString();
            }

        }

        public int RewardID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnRewardID.Value))
                    return Convert.ToInt32(hdnRewardID.Value);
                else return 0;
            }
            set
            {
                hdnRewardID.Value = value.ToString();
            }
        }

        public Boolean TypeMode
        {
            get
            {
                if (!string.IsNullOrEmpty(EditMode.Value))
                    return Convert.ToBoolean(EditMode.Value);
                else return true;
            }
            set
            {
                EditMode.Value = value.ToString();
            }
        }

        public Boolean Imagecheck
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageAvail.Value))
                    return Convert.ToBoolean(ImageAvail.Value);
                else return true;
            }
            set
            {
                ImageAvail.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.View;
                if (Request.QueryString[SystemConstants.qs_RewardsID] != null)
                {
                    RewardID = Convert.ToInt32(Request.QueryString[SystemConstants.qs_RewardsID]);
                }
                initDDL();
                SetRewardInformation();
            }
        }

        private void initDDL()
        {
            var dt = new AdministrationDAC().RetrieveRewardTypes();
            foreach(var dr in dt)
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
                ddlSponsor.Items.Add(new ListItem(allsponsor.Name, allsponsor.ID.ToString()));

            }
        }

        public void SetRewardInformation()
        {
            var dr = new AdministrationDAC().RetrieveRewardInfo(RewardID);
            if (dr != null)
            {
                string check = Convert.ToString(dr.RewardImage);
                if (!string.IsNullOrEmpty(check))
                {
                    if (Convert.ToBoolean(check))
                    {
                        var dri = new AdministrationDAC().RetrieveRewardPrimaryImage(Convert.ToInt32(RewardID));
                        if (dri != null || dri.ImageStream != null)
                            //Convert byte directly, while its easier, its not suppose to be 
                            //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);
                            imgPreview.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_RewardThumbImageID + "=" + dri.ID;
                        Imagecheck = true;
                    }
                    else
                    {
                        imgPreview.ImageUrl = "~/Images/gift.jpg";
                        Imagecheck = true;
                    }
                }
                else
                {
                    imgPreview.ImageUrl = "~/Images/gift.jpg";
                    Imagecheck = true;
                }
                lblRewName.Text = txtRewName.Text = dr.RewardsName;
                lblRewDesc.Text = txtRewDesc.Text = dr.RewardDescription;
                lblCalendarFrom.Text = txtCalendarFrom.Text = Convert.ToString(dr.RewardExpiryDate.Date);
                Guid spnid = dr.ProviderID;
                var sr = new AdministrationDAC().RetrieveSponsorDetails(spnid);
                lblSponsor.Text = Convert.ToString(sr.Name);
                ddlSponsor.SelectedValue = Convert.ToString(sr.ID);

                lblReqRewardPoints.Text = txtReqRewardPoints.Text = Convert.ToString(dr.RequiredRewardPoint);
                lblKeywords.Text = txtKeywords.Text = dr.Keywords;
                lblUsageTimes.Text = txtUsageTimes.Text = Convert.ToString(dr.UsageTimes);

                if (dr.RewardSource == 1)
                    ddlrewsource.SelectedItem.Text = lblrewsource.Text ="Internal/Activity Based";
                else
                    ddlrewsource.SelectedItem.Text = lblrewsource.Text = "External/Other Products Based";
                 


                int rwdtype = dr.RewardType;

                if (rwdtype == (int)SystemConstants.RewardType.Gift)
                {
                    gift.Visible = true;
                    ddlRewType.SelectedValue = Convert.ToString((int)SystemConstants.RewardType.Gift);
                    lblRewtype.Text = "Gift";
                    lblRewtype.Text = ddlRewType.SelectedItem.Text;
                    lblGiftName.Text = txtGiftName.Text = dr.FreeGift;
                }
                else if (rwdtype == (int)SystemConstants.RewardType.Offer)
                {
                    offer.Visible = true;

                    ddlFreeAct.SelectedValue = Convert.ToString(dr.FreeActivityID);
                    lblFreeAct.Text = ddlFreeAct.SelectedItem.Text;
                    ddlRewType.SelectedValue = Convert.ToString((int)SystemConstants.RewardType.Offer);
                    lblRewtype.Text = "Offer";
                    ddlReqActivityName.SelectedValue = Convert.ToString(dr.RequiredActivityEnroll);
                    lblReqActivityName.Text = ddlReqActivityName.SelectedItem.Text;


                }
                else if (rwdtype == (int)SystemConstants.RewardType.Other)
                {
                    other.Visible = true;
                    ddlRewType.SelectedValue = Convert.ToString((int)SystemConstants.RewardType.Other);
                    lblRewtype.Text = "Other";
                    ddlBonusAct.SelectedValue = Convert.ToString(dr.FreeActivityID);
                    lblBonusAct.Text = ddlBonusAct.SelectedItem.Text;
                    txtBonusPoint.Text = lblBonusPoint.Text = Convert.ToString(dr.BonusPoint);
                   

                }
                else if (rwdtype == (int)SystemConstants.RewardType.Discount)
                {
                    discount.Visible = true;
                    ddlRewType.SelectedValue = Convert.ToString((int)SystemConstants.RewardType.Discount);
                    lblRewtype.Text = "Discount";
                    ddlDiscAct.SelectedValue = Convert.ToString(dr.FreeActivityID);
                    lblDiscAct.Text = ddlDiscAct.SelectedItem.Text;
                    if (dr.Discount != 0)
                    {
                        ddlDiscSelect.SelectedIndex = 0;
                        lblDiscSelect.Text = "Discounted Rate";
                        lblTextSelection.Text = "Percentage";
                        txtDiscPerc.Text = lblDiscPerc.Text = Convert.ToString(dr.Discount);

                    }
                    else
                    {

                        ddlDiscSelect.SelectedIndex = 1;
                        lblDiscSelect.Text = "Dollars Off";
                        lblTextSelection.Text = "Money Off ";
                        txtDiscPerc.Text = lblDiscPerc.Text = Convert.ToString(dr.SupportOFF);

                    }
                }
            }

        }

        public void Refresh()
        {
            if (ddlDiscSelect.SelectedIndex == 0)
                lblDiscSelect.Text = "Discount Rate";
            else
                lblDiscSelect.Text = "Money Off";
            if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Gift))
            {
                discount.Visible = false;
                offer.Visible = false;
                other.Visible = false;
                gift.Visible = true;
            }
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Offer))
            {
                offer.Visible = true;
                discount.Visible = false;

                other.Visible = false;
                gift.Visible = false;
            }
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Other))
            {
                discount.Visible = false;
                offer.Visible = false;
                other.Visible = true;
                gift.Visible = false;
            }

            else
            {
                discount.Visible = true;
                offer.Visible = false;
                other.Visible = false;
                gift.Visible = false;
            }


        }

        protected void rtype_SelectedIndexChanged(object sender, EventArgs e)
        {



            Refresh();
        }

        protected void select_index(object sender, EventArgs e)
        {

            Refresh();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.Edit;


        }

        protected void Back_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");

        }


        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.RewardDTRow dr = GetDetails();
            AdministrationEDSC.RewardsDetailsDTRow drDet = GetRwrdDetails();

            AdministrationEDSC.RewardImageDTRow drt = GetImgDetails();

            if (Imagecheck == true)
            {
                dr.RewardImage = true;

            }
            else
                dr.RewardImage = true;
            if (dr != null)
            {

                new AdministrationBFC().UpdateRewards(dr, drDet, drt);
                SetRewardInformation();    

            }
            addeddiv.Visible = true;
            FormMode = (int)HealthyClub.Utility.SystemConstants.FormMode.View;
        }

        private AdministrationEDSC.RewardImageDTRow GetImgDetails()
        {

            if (fileUpRewardImage.HasFile)
            {
                var dr = new AdministrationEDSC.RewardImageDTDataTable().NewRewardImageDTRow();
                dr.ID = new AdministrationDAC().getImageID(RewardID);
                dr.RewardID = RewardID;
                dr.Description = fileUpRewardImage.FileName;
                dr.ImageTitle = dr.Filename = fileUpRewardImage.FileName;
                dr.Filesize = fileUpRewardImage.PostedFile.ContentLength / 1024;
                dr.ImageStream = new ImageHandler().ReadFully(fileUpRewardImage.PostedFile.InputStream);
                return dr;
                Imagecheck = true;
            }
            else return null;
        }

        protected AdministrationEDSC.RewardDTRow GetDetails()
        {
            var dr = new AdministrationEDSC.RewardDTDataTable().NewRewardDTRow();

            dr.ID = RewardID;
            Guid spnid = new Guid(ddlSponsor.SelectedValue);
            dr.ProviderID = spnid;
            //dr.ProviderID = can be taken from sponsor or admin

            dr.RewardsName = txtRewName.Text;
            dr.RewardDescription = txtRewDesc.Text;

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
            dr.RequiredRewardPoint = Convert.ToInt32(txtReqRewardPoints.Text);

            if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Gift))
                dr.RewardType = (int)SystemConstants.RewardType.Gift;
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Offer))
                dr.RewardType = (int)SystemConstants.RewardType.Offer;
            else if (ddlRewType.SelectedValue == Convert.ToString((int)SystemConstants.RewardType.Other))
                dr.RewardType = (int)SystemConstants.RewardType.Other;
            else
                dr.RewardType = (int)SystemConstants.RewardType.Discount;
            dr.CategoryID = 0;

            if (txtDiscPerc.Text != "")
            {
                if (ddlDiscSelect.SelectedIndex == 1)
                {
                    dr.Discount = 0;
                    dr.SupportOFF = Convert.ToInt32(txtDiscPerc.Text);

                }
                else
                {
                    dr.SupportOFF = 0;
                    dr.Discount = Convert.ToInt32(txtDiscPerc.Text);

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
            if (txtBonusPoint.Text != "")
                dr.BonusPoint = Convert.ToInt32(txtBonusPoint.Text);
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


            dr.FreeGift = txtGiftName.Text;
            dr.UsageTimes = Convert.ToInt32(txtUsageTimes.Text);
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
            
            dt.RewardID = RewardID;
            dt.ID = new AdministrationDAC().getDetailsID(RewardID);
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
          
            dt.Keywords = txtKeywords.Text;
            dt.CreatedBy = WebSecurity.CurrentUserName;
            dt.ModifiedBy = WebSecurity.CurrentUserName;
            return dt;
        }


    }

}






