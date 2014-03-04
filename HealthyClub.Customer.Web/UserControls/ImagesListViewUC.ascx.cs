using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using HealthyClub.Utility;
using System.Web.UI.HtmlControls;

namespace HealthyClub.Web.UserControls
{
    public partial class ImagesListViewUC : System.Web.UI.UserControl
    {

        int index = 0;
        int index2 = 0;
        public int ActivityID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnActivityID.Value))
                    return Convert.ToInt32(hdnActivityID.Value);
                else return 0;
            }
            set
            {
                hdnActivityID.Value = value.ToString();
                Refresh();
            }
        }
   

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Refresh()
        {
            ListView1.DataSource = new CustomerDAC().RetrieveActivityImages(ActivityID);
            ListView1.DataBind();
            ListView2.DataSource = new CustomerDAC().RetrieveActivityImages(ActivityID);
            ListView2.DataBind();
            if (ListView1.DataSource == null)
            {
                divImagesList.Visible = theDiv.Visible = false;
                imgNoImage.Visible = true;
                imgNoImage.ImageUrl = "~/Content/StyleImages/no_Image.png";
            }
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgGal = e.Item.FindControl("imgGal1") as Image;
                HiddenField hdnImageID = e.Item.FindControl("hdnImageID1") as HiddenField;
                HiddenField hdnMainImage = e.Item.FindControl("hdnMainImage1") as HiddenField;
                HyperLink hlnkgal = e.Item.FindControl("hlnkgal1") as HyperLink;

                if (Convert.ToBoolean(!string.IsNullOrEmpty(hdnImageID.Value)))
                {
                    hlnkgal.NavigateUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
                    if (index != 0)
                        hlnkgal.Attributes.Add("rel", "Fullscreen[Gall]");
                    index++;
                    imgGal.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
                }
            }
        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgGal = e.Item.FindControl("imgGal2") as Image;
                HiddenField hdnImageID = e.Item.FindControl("hdnImageID2") as HiddenField;
                HiddenField hdnMainImage = e.Item.FindControl("hdnMainImage2") as HiddenField;

                HyperLink hlnkgal = e.Item.FindControl("hlnkgal2") as HyperLink;

                if (Convert.ToBoolean(!string.IsNullOrEmpty(hdnImageID.Value)))
                {
                    hlnkgal.NavigateUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
                    if (index2 == 0)
                        HyperLink1.NavigateUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
                    index2++;
                    imgGal.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
                }
            }
        }



    }
}