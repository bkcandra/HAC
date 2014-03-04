using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Customer.EDS;
using HealthyClub.Customer.DA;
using System.Web.Security;
using HealthyClub.Utility;
using WebMatrix.WebData;
using HealthyClub.Customer.BF;
using BCUtility;
using System.Text.RegularExpressions;

namespace HealthyClub.Customer.Web.UserControls
{
    public partial class RewardCheckout : System.Web.UI.UserControl
    {
        public bool cartchange = true;
        public bool cartupdate = false;
        
        
        public string selected
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnselected.Value))
                    return hdnselected.Value;
                else return "0";
            }
            set
            {
                hdnselected.Value = value.ToString();
            }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            Refresh();
            
            
        }
        protected void Remove_Click(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            int RewardID = Convert.ToInt32(ClickedButton.CommandName);
            RewardCart.Instance.RemoveItem(RewardID);
            Response.Redirect(Request.RawUrl);
        }

        
        protected void continueshop_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rewards/Rewardshop.aspx");

        }
        protected void checkout_Click(object sender, EventArgs e)
        {
            Checkoutview.Visible = true;

        }
        protected void Generate_Click(object sender, EventArgs e)
        {
           
           
            print.Visible = true;
            coupons.Visible = true;
            Refresh();
        }
        protected void Voucher_Click(object sender, EventArgs e)
        {
            string IDs = RewardCart.Instance.getItems();
            string dates = RewardCart.Instance.getExpiry();
            string Names = RewardCart.Instance.getNames();
            string[] Name = Names.Split('|');
            string[] ID = IDs.Split('|');
            string[] expdate = dates.Split('|');
            int i;
            int count = RewardCart.Instance.getitemno();

            string separator = "|";

            for (i = 0; i < count; i++)
            {
                int quant = RewardCart.Instance.getItemQuant(Convert.ToInt32(ID[i]));
                Random rnd = new Random();
                string[] coupon = new string[count + quant];
                for (int k = 0; k < coupon.Length; k++)
                {
                    coupon[k] = GenerateCoupon(6, rnd);
                }

                for (int j = 0; j < quant; j++)
                {

                    CustomerEDSC.VoucherDetailsDTRow dr = new CustomerEDSC.VoucherDetailsDTDataTable().NewVoucherDetailsDTRow();
                    CustomerDAC dac = new CustomerDAC();
                    dr.RewardID = Convert.ToInt32(ID[i]);
                    dr.SponsorID = dac.getSponsorID(Convert.ToInt32(ID[i]));

                    dr.ExpiryDate = Convert.ToDateTime(expdate[i]);
                    dr.IssueDate = DateTime.Today;
                    dr.UsageStatus = true;
                    dr.RewardName = Name[i];
                    dr.VoucherCode = coupon[i + j];

                    dac.InsertNewVoucherDetail(dr);
                    if (!String.IsNullOrEmpty(selected))
                        selected += separator;
                    selected += Convert.ToString(dr.VoucherCode);

                }

            }
            redempted.Visible = true;
            
        }

        

        public static string GenerateCoupon(int length, Random random)
        {
            string characters = "0123456789";
            System.Text.StringBuilder result = new System.Text.StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        protected void lblquant_textchanged(object sender, EventArgs e)
        {


        }

        
        protected void Updatecart_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eitem in RewardCheckoutView.Items)
            {
                foreach (Control t in eitem.Controls)
                {
                    TextBox txt = t.FindControl("lblquant") as TextBox;
                                   
                     HiddenField hdnRewardsId = t.FindControl("hdnRewardsID") as HiddenField;

                     int rwdid = Convert.ToInt32(hdnRewardsId.Value);
                     int newquant = Convert.ToInt32(txt.Text);
                     RewardCart.Instance.SetItemnewQuantity(rwdid, newquant);
                }
            }
            
            cartupdate = true;
            Refresh();
            
            

        }
        protected void Refresh()
        {
            ods.TypeName = typeof(CustomerDAC).FullName;
            ods.SelectParameters.Clear();
            string RewardID = "0";
            RewardID = RewardCart.Instance.getItems();
            int subTotal = 0;
            subTotal += RewardCart.Instance.GetSubTotal();
            int rwdpts = Convert.ToInt32(Session[SystemConstants.ses_Rwdpts]);
            if (subTotal > rwdpts)
            {
                cartchange = false;
                RewardCart.Instance.setquanttozero();
                cannotadd.Visible = true;
            }
            else
            {
                cartchange = true;
                cannotadd.Visible = false;
                lblTotal.Text = Convert.ToString(subTotal);
            }        
            ods.SelectParameters.Add("RewardID", RewardID.ToString());
            ods.SelectMethod = "RetrieveRewardCart";
            ods.SelectCountMethod = "RetrieveRewardCartCount";
            RewardCheckoutView.DataSourceID = "ods";
            
            ods1.TypeName = typeof(CustomerDAC).FullName;
            ods1.SelectParameters.Clear();
            ods1.SelectParameters.Add("VoucherID", selected.ToString());
            ods1.SelectMethod = "RetrieveVouchers";
            ods1.SelectCountMethod = "RetrieveVoucherCount";
            Couponview.DataSourceID = "ods1";
            subTotal = 0;
            subTotal += RewardCart.Instance.GetSubTotal();
            lblTotal.Text = Convert.ToString(subTotal);
            
        }

       protected void Couponview_ItemDataBound(object sender, ListViewItemEventArgs e)
       {
           
           
           Label memname = e.Item.FindControl("memname") as Label;
           Label desc = e.Item.FindControl("Description") as Label;
          
          if (othername.Text != "")
              memname.Text = othername.Text;
          else
              memname.Text = Convert.ToString(Session[SystemConstants.ses_FName]) + " " + Convert.ToString(Session[SystemConstants.ses_LName]);
        
       }
       protected void RewardCheckoutView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HyperLink HlnkRewardsName = e.Item.FindControl("HlnkRewardsName") as HyperLink;
            HiddenField hdnRewardsID = e.Item.FindControl("hdnRewardsID") as HiddenField;
            
            System.Web.UI.WebControls.Image imgPreview = e.Item.FindControl("imgPreview") as System.Web.UI.WebControls.Image;
            HlnkRewardsName.NavigateUrl = "~/Rewards/Rewards.aspx?" + SystemConstants.qs_RewardsID + "=" + hdnRewardsID.Value;
            HiddenField hdnExpiryDate = e.Item.FindControl("hdnExpiryDate") as HiddenField;
            TextBox lblQuant = e.Item.FindControl("lblquant") as TextBox;
            HiddenField hdnquant = e.Item.FindControl("hiddenquant") as HiddenField;
            HiddenField hdnRewardImage = e.Item.FindControl("hdnRewardImage") as HiddenField;
            
            
            hdnquant.Value = Convert.ToString(RewardCart.Instance.getItemQuantity(Convert.ToInt32(hdnRewardsID.Value)));
            if (cartupdate == true)
            {
                if (cartchange == false)
                {
                    lblQuant.Text = hdnquant.Value;
                    RewardCart.Instance.SetItemnewQuantity(Convert.ToInt32(hdnRewardsID.Value), Convert.ToInt32(lblQuant.Text));
                    
                    
                }
                else
                {
                    int rewardid = Convert.ToInt32(hdnRewardsID.Value);
                    int quantcart = RewardCart.Instance.getItemnewQuantity(rewardid);
                    lblQuant.Text = Convert.ToString(quantcart);
                    RewardCart.Instance.SetItemQuantity(rewardid, quantcart);
                    hdnquant.Value = lblQuant.Text;
                } 
            }
            else
            {
                lblQuant.Text = hdnquant.Value;
                RewardCart.Instance.SetItemnewQuantity(Convert.ToInt32(hdnRewardsID.Value), Convert.ToInt32(lblQuant.Text));
            
            }
            Button Remove = e.Item.FindControl("Remove") as Button;
            Remove.CommandName = hdnRewardsID.Value;
            Label lblTotal = e.Item.FindControl("lblTotal") as Label;
            if (!string.IsNullOrEmpty(hdnRewardImage.Value))
            {
                if (Convert.ToBoolean(hdnRewardImage.Value))
                {
                    var dr = new CustomerDAC().RetrieveRewardPrimaryImage(Convert.ToInt32(hdnRewardsID.Value));
                    if (dr != null || dr.ImageStream != null)
                        //Convert byte directly, while its easier, its not suppose to be 
                        //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);
                        imgPreview.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_RewardThumbImageID + "=" + dr.ID;
                }
                else
                    imgPreview.ImageUrl = "~/Images/gift.jpg";
            }
            else
                imgPreview.ImageUrl = "~/Images/gift.jpg";
            
            

        }
        protected void RewardCheckoutView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {


        }
        protected void RewardCheckoutView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            
        }
    }
}