using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.BF;
using HealthyClub.Utility;
using System.IO;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityImageListView : System.Web.UI.UserControl
    {
        public enum UIMode { Edit, View }

        public void SetDataListItemMode(DataListItem item, bool isEdit)
        {

            Label lblImageDescription = item.FindControl("lblImageDescription") as Label;
            Label lblImageTitle = item.FindControl("lblImageTitle") as Label;

            TextBox txtImageTitle = item.FindControl("txtImageTitle") as TextBox;
            Control ckImageDescription = item.FindControl("txtImageDescription") as Control;

            LinkButton lnkDeleteImage = item.FindControl("lnkDeleteImage") as LinkButton;
            LinkButton lnkMainImage = item.FindControl("lnkMainImage") as LinkButton;
            HiddenField hdnMainImage = item.FindControl("hdnMainImage") as HiddenField;

            if (Convert.ToBoolean(hdnMainImage.Value))
                lnkMainImage.Visible = false;
            else
                lnkMainImage.Visible = isEdit;

            lblImageDescription.Visible = !isEdit;
            lblImageTitle.Visible = !isEdit;

            txtImageTitle.Visible = isEdit;
            ckImageDescription.Visible = isEdit;

            lnkDeleteImage.Visible = isEdit;

            lnkCancel.Visible = isEdit;
            lnkEdit.Visible = !isEdit;
            lnkSave.Visible = isEdit;


        }

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
            if (!IsPostBack)
            {
                foreach (DataListItem item in DataList1.Items)
                {
                    SetDataListItemMode(item, false);
                }
            }
        }

        public void Refresh()
        {
            SetDataSource();

        }

        private void SetDataSource()
        {
            var dt = new AdministrationDAC().RetrieveActivityImages(ActivityID);
            DataList1.DataSource = dt;
            DataList1.DataBind();
            if (dt.Count == 0)
            {
                divNoImageListview.Visible = true;
                divImageListview.Visible = false;
            }
            else
            {
                divNoImageListview.Visible = false;
                divImageListview.Visible = true;
            }
        }

        private void SetImage(AdministrationEDSC.ActivityContactDetailDTRow dr)
        {

        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Image imgThumbnail = e.Item.FindControl("imgThumbnail") as Image;
            HiddenField hdnImageID = e.Item.FindControl("hdnImageID") as HiddenField;
            HiddenField hdnMainImage = e.Item.FindControl("hdnMainImage") as HiddenField;
            LinkButton lnkMainImage = e.Item.FindControl("lnkMainImage") as LinkButton;
            HyperLink hlnkImage = e.Item.FindControl("hlnkImage") as HyperLink;

            imgThumbnail.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;
            hlnkImage.NavigateUrl = new AdministrationBFC().RetrieveImageUrl(ActivityID, Convert.ToInt32(hdnImageID.Value));

            if (Convert.ToBoolean(hdnMainImage.Value))
            {
                lnkMainImage.Visible = false;
                imgThumbnail.BorderStyle = BorderStyle.Solid;
                imgThumbnail.BorderColor = System.Drawing.Color.FromArgb(133, 171, 241);

                //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);                 
                //Convert byte directly, while its easier, its not suppose to be
                imgThumbnail.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value;

            }
            SetDataListItemMode(e.Item, false);
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "SetAsMainImage" || e.CommandName == "DeleteImage" || e.CommandName == "ViewImage" || e.CommandName == "SaveImage" || e.CommandName == "CancelEditImage" || e.CommandName == "EditImage")
            {
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                HiddenField hdnImageID = e.Item.FindControl("hdnImageID") as HiddenField;
                int imageID = Convert.ToInt32(hdnImageID.Value);
                AdministrationDAC dac = new AdministrationDAC();
                AdministrationBFC bfc = new AdministrationBFC();

                Label lblImageDescription = e.Item.FindControl("lblImageDescription") as Label;
                Label lblImageTitle = e.Item.FindControl("lblImageTitle") as Label;
                TextBox txtImageTitle = e.Item.FindControl("txtImageTitle") as TextBox;
                TextBox ckImageDescription = e.Item.FindControl("txtImageDescription") as TextBox;
                HiddenField hdnFilesize = e.Item.FindControl("hdnFilesize") as HiddenField;

                if (e.CommandName == "DeleteImage")
                {
                    string imageVirtualPath = "";
                    string imageThumbVirtualPath = "";
                    bfc.DeleteActivityImage(ActivityID, imageID, Convert.ToInt32(hdnFilesize.Value), out imageThumbVirtualPath, out imageVirtualPath);
                    Refresh();
                }
                else if (e.CommandName == "SetAsMainImage")
                {
                    dac.UpdateActivityPrimaryImage(ActivityID, imageID);
                    Refresh();
                    foreach (DataListItem item in DataList1.Items)
                    {
                        SetDataListItemMode(item, true);
                    }
                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            foreach (DataListItem item in DataList1.Items)
            {
                SetDataListItemMode(item, true);
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
            AdministrationDAC dac = new AdministrationDAC();
            foreach (DataListItem item in DataList1.Items)
            {
                SetDataListItemMode(item, false);

                Label lblImageDescription = item.FindControl("lblImageDescription") as Label;
                Label lblImageTitle = item.FindControl("lblImageTitle") as Label;
                TextBox txtImageTitle = item.FindControl("txtImageTitle") as TextBox;
                TextBox ckImageDescription = item.FindControl("txtImageDescription") as TextBox;
                HiddenField hdnImageID = item.FindControl("hdnImageID") as HiddenField;
                HiddenField hdnActvityImageID = item.FindControl("hdnImageInfoID") as HiddenField;
                HiddenField hdnFilesize = item.FindControl("hdnFilesize") as HiddenField;
                HiddenField hdnFilename = item.FindControl("hdnFilename") as HiddenField;
                HiddenField hdnMainImage = item.FindControl("hdnMainImage") as HiddenField;

                var dr = dt.NewActivityImageDetailDTRow();
                dr.ID = Convert.ToInt32(hdnImageID.Value);
                dr.ImageTitle = txtImageTitle.Text;
                dr.Filename = hdnFilename.Value;
                dr.isPrimaryImage = Convert.ToBoolean(hdnMainImage.Value);
                dr.Description = ckImageDescription.Text;
                dr.ActivityID = Convert.ToInt32(hdnActivityID.Value);
                dr.ActivityImageID = Convert.ToInt32(hdnActvityImageID.Value);
                dr.Filesize = Convert.ToInt32(hdnFilesize.Value);
                dt.AddActivityImageDetailDTRow(dr);
            }
            foreach (var dr in dt)
            {
                dac.UpdateActivityImage(dr);
            }

            foreach (DataListItem item in DataList1.Items)
            {
                SetDataListItemMode(item, false);
            }
            Refresh();
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Refresh();
            foreach (DataListItem item in DataList1.Items)
            {
                SetDataListItemMode(item, false);
            }
        }




    }
}