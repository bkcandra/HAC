using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.EDS;
using HealthyClub.Utility;
using HealthyClub.Provider.BF;
using HealthyClub.Provider.DA;

using System.IO;

namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ActivityImageUC : System.Web.UI.UserControl
    
    {
        public enum UIMode { Edit, View }

        public void SetDataListItemMode(DataListItem item, bool isEdit)
        {

            Label lblImageDescription = item.FindControl("lblImageDescription") as Label;
            Label lblImageTitle = item.FindControl("lblImageTitle") as Label;

            TextBox txtImageTitle = item.FindControl("txtImageTitle") as TextBox;
            Control ckImageDescription = item.FindControl("txtImageDescription") as Control;

            //LinkButton lnkDeleteImage = item.FindControl("lnkDeleteImage") as LinkButton;
            LinkButton lnkMainImage = item.FindControl("lnkMainImage") as LinkButton;
            HiddenField hdnMainImage = item.FindControl("hdnMainImage") as HiddenField;

            if (Convert.ToBoolean(hdnMainImage.Value))
                lnkMainImage.Visible = false;
            //
            //    lnkMainImage.Visible = isEdit;

            lblImageDescription.Visible = !isEdit;
            lblImageTitle.Visible = !isEdit;

            txtImageTitle.Visible = isEdit;
            ckImageDescription.Visible = isEdit;

            //lnkDeleteImage.Visible = isEdit;

            lnkCancel.Visible = isEdit;
            lnkEdit.Visible = !isEdit;
            lnkSave.Visible = isEdit;


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckActivityImage();
                foreach (DataListItem item in DataList1.Items)
                {
                    SetDataListItemMode(item, false);
                }
            }
        }

        public void Refresh()
        {
            
            SetDataSource();
            DataList1.DataBind();
        }

        private void SetDataSource()
        {
            DataList1.DataSource = new ProviderDAC().RetrieveActivityImages(ActivityID);
        }

        private void SetImage(ProviderEDSC.ActivityContactDetailDTRow dr)
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
            hlnkImage.NavigateUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_ImageID + "=" + hdnImageID.Value; 

            if (Convert.ToBoolean(hdnMainImage.Value))
            {
                lnkMainImage.Visible = false;
                imgThumbnail.BorderStyle = BorderStyle.Solid;
                imgThumbnail.BorderColor = System.Drawing.Color.FromArgb(133, 171, 241);

                //imgPreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);                 Convert byte directly, while its easier, its not suppose to be
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
                ProviderDAC dac = new ProviderDAC();
                ProviderBFC bfc = new ProviderBFC();

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
                    lblUploadStatus.Visible = false;
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
            var dt = new ProviderEDSC.ActivityImageDetailDTDataTable();
            ProviderDAC dac = new ProviderDAC();
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




    
        public int ActivityID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnActivityID.Value))
                {
                    return Convert.ToInt32(hdnActivityID.Value);
                }
                else
                    return 0;
            }
            set
            {
                hdnActivityID.Value = value.ToString();
                Refresh();
            }
        }

        public int ImageUploaded
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnImgUploaded.Value))
                    return Convert.ToInt32(hdnImgUploaded.Value);
                else return 0;
            }
            set
            {
                hdnImgUploaded.Value = value.ToString();
            }
        }

        public int SizeUploaded
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSizeUploaded.Value))
                    return Convert.ToInt32(hdnSizeUploaded.Value);
                else return 0;
            }
            set
            {
                hdnSizeUploaded.Value = value.ToString();
            }
        }

        public string ActionKey
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnActionCode.Value))
                    return hdnActionCode.Value;
                else return "ABCDE123";
            }
            set
            {
                hdnActionCode.Value = value.ToString();
            }
        }


        private void CheckActivityImage()
        {
            //checkdatabase for record
            //if not exist, create img folder & thumb folder
            if (ActivityID != 0)
            {
                var dr = new ProviderDAC().RetrieveActivityImageInformation(ActivityID);
                if (dr == null)
                {
                    dr = new ProviderEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
                    //Create new ImageInformationRecord
                    dr.ActivityID = ActivityID;
                    dr.FreeStorage = SystemConstants.MaxActivityImageStorage;
                    dr.StorageUsed = 0;
                    dr.ImageAmount = 0;

                    new ProviderDAC().createActivityImageInformation(dr);

                    // string Path = SystemConstants.ImageDirectory + "/" + activityID + "/" + activityID + "_" + imageID + "_" + prodImage.Filename;
                    //Ensure Directory is exist
                    //Server.MapPath(SystemConstants.ActImageDirectory + "/" + ActivityID + "/");
                    //Create ImageDir

                    //System.IO.Directory.CreateDirectory(Server.MapPath(@SystemConstants.ActImageDirectory + "/" + ActivityID + "/"));

                    //Create ImageThumbDir
                    //System.IO.Directory.CreateDirectory(Server.MapPath(@SystemConstants.ActImageDirectory + "/" + ActivityID + "/" + @SystemConstants.ImageThumbDirectory));
                }
                else
                {
                    divUploadSuccessfull.Visible = true;
                }
            }
        }

        private ProviderEDSC.ActivityImageDetailDTRow GetData()
        {
            int imagesCount = new ProviderDAC().RetrieveActivityImagesCount(ActivityID);

            ProviderEDSC.ActivityImageDetailDTRow dr = new ProviderEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
            dr.ActivityID = ActivityID;
            dr.ActivityImageID = 0;
            dr.Filename = fileUpload1.FileName;
            dr.Description = "";
            if (imagesCount == 0)
                dr.isPrimaryImage = true;
            else
                dr.isPrimaryImage = false;
            dr.ImageTitle = "";
            dr.Filesize = fileUpload1.PostedFile.ContentLength / 1024;
            return dr;
        }

        protected void imgBtnUpload_Click(object sender, EventArgs e)
        {
            ImageUploaded = 0;
            var drInfo = new ProviderDAC().RetrieveActivityImageInformation(ActivityID);

            CheckActivityImage();
            if (fileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                var dt = new ProviderEDSC.ActivityImageDetailDTDataTable();
                if (System.Text.RegularExpressions.Regex.IsMatch(fileUpload1.PostedFile.ContentType, "image/\\S+"))
                {
                    int iFailedCnt = 0;
                    HttpFileCollection hfc = Request.Files;
                    lblUploadStatus.Text = "Select <b>" + hfc.Count + "</b> file(s)";

                    if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
                    {
                        for (int i = 0; i <= hfc.Count - 1; i++)
                        {
                            HttpPostedFile hpf = hfc[i];
                            if (hpf.ContentLength > 0)
                            {
                                string sFileName = Path.GetFileName(hpf.FileName);
                                string sFileExt = Path.GetExtension(hpf.FileName);

                                SizeUploaded = SizeUploaded + Convert.ToInt32(hpf.ContentLength / 1024);
                                if (SizeUploaded <= drInfo.FreeStorage)
                                {

                                    var dr = dt.NewActivityImageDetailDTRow();

                                    dr.ActivityID = ActivityID;
                                    dr.ActivityImageID = drInfo.ID;
                                    dr.ImageStream = new BCUtility.ImageHandler().ReadFully(hpf.InputStream);
                                    dr.Filename = sFileName;
                                    dr.Filesize = hpf.ContentLength / 1024;
                                    dr.ImageTitle = "";
                                    dr.Description = "";
                                    dt.AddActivityImageDetailDTRow(dr);

                                    ImageUploaded += 1;
                                }
                                else
                                {
                                    iFailedCnt += 1;
                                    lblUploadStatus.Text += "</br><b>" + iFailedCnt + "</b> file(s) could not be uploaded. Maximum Size per activity is" + SystemConstants.MaxActivityImageStorage + " Kb";
                                }

                            }
                        }

                        lblUploadStatus.Text = "<b>" + ImageUploaded + "</b> file(s) uploaded.";
                    }
                    else lblUploadStatus.Text = "Max. 10 files allowed.";


                    if (ImageUploaded > 0)
                    {
                        drInfo.ImageAmount = drInfo.ImageAmount + ImageUploaded;
                        drInfo.StorageUsed = drInfo.StorageUsed + SizeUploaded;
                        drInfo.FreeStorage = SystemConstants.MaxActivityImageStorage - drInfo.StorageUsed;
                        divUploadSuccessfull.Visible = true;

                        new ProviderBFC().CreateActivityImages(ActivityID, drInfo, dt);
                    }

                }
                else
                {
                    lblUploadStatus.Text = "Only image files are accepted.";
                }
            }
            else lblUploadStatus.Text = "No files selected.";
            lblUploadStatus.Visible = true;

            Refresh();
        }
        /*
         if (fileUpload1.HasFile)
            {

                lblUploadStatus.Visible = false;
                ProviderEDSC.ActivityImageDTRow iinfo = new ProviderDAC().RetrieveActivityImageInformation(ActivityID); ;
                int maxSizeKB = iinfo.FreeStorage;
                int fileSize = fileUpload1.PostedFile.ContentLength / 1024;

                ProviderEDSC.ActivityImageDetailDTRow dr = GetData();

                if (System.Text.RegularExpressions.Regex.IsMatch(fileUpload1.PostedFile.ContentType, "image/\\S+") && (fileUpload1.PostedFile.ContentLength > 0))
                {
                    if (fileSize > maxSizeKB)
                    {
                        lblUploadStatus.Visible = true;
                        lblUploadStatus.Text = "Upload failed, Maximum Upload Limit is " + maxSizeKB + "kb";
                    }

                    else
                    {
                        int imageID = 0;
                        new ProviderBFC().CreateActivityImage(dr, out imageID, fileSize);

                        var img = System.Drawing.Image.FromStream(fileUpload1.FileContent);
                        string imagePath = SystemConstants.GetActivityImageURL(ActivityID, imageID, fileUpload1.FileName);
                        string thumbPath = SystemConstants.GetActivityImageThumbURL(ActivityID, imageID, fileUpload1.FileName);

                        if (imagePath.StartsWith("~"))
                            imagePath = Server.MapPath(imagePath);

                        if (thumbPath.StartsWith("~"))
                            thumbPath = Server.MapPath(thumbPath);

                        ActivityImageHelper.UploadProductThumbnail(img, thumbPath);
                        fileUpload1.SaveAs(imagePath);

                        ActivityImageListView1.Refresh();
                    }
                }
                else
                {
                    lblUploadStatus.Text = "Only image files are accepted.";
                }
            }
            else
            {
                lblUploadStatus.Text = "No File Selected.";
            }
         */
    }
}