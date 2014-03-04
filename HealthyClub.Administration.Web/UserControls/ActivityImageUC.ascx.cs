using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
 
using System.IO;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityImageUC : System.Web.UI.UserControl
    {
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
                ActivityImageListView1.ActivityID = ActivityID;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckActivityImage();
            }
        }

        private void CheckActivityImage()
        {
            //checkdatabase for record
            //if not exist, create img folder & thumb folder
            var dr = new AdministrationDAC().RetrieveActivityImageInformation(ActivityID);
            if (dr == null)
            {
                dr = new AdministrationEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
                //Create new ImageInformationRecord
                dr.ActivityID = ActivityID;
                dr.FreeStorage = SystemConstants.MaxActivityImageStorage;
                dr.StorageUsed = 0;
                dr.ImageAmount = 0;

                new AdministrationDAC().createActivityImageInformation(dr);

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

        private AdministrationEDSC.ActivityImageDetailDTRow GetData()
        {
            int imagesCount = new AdministrationDAC().RetrieveActivityImagesCount(ActivityID);

            AdministrationEDSC.ActivityImageDetailDTRow dr = new AdministrationEDSC.ActivityImageDetailDTDataTable().NewActivityImageDetailDTRow();
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
            var drInfo = new AdministrationDAC().RetrieveActivityImageInformation(ActivityID);

            CheckActivityImage();
            if (fileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                var dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
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

                        new AdministrationBFC().CreateActivityImages(ActivityID, drInfo, dt);
                    }
                   
                }
                else
                {
                    lblUploadStatus.Text = "Only image files are accepted.";
                }
            }
            else lblUploadStatus.Text = "No files selected.";
            lblUploadStatus.Visible = true;

            
        }
        /*
         if (fileUpload1.HasFile)
            {

                lblUploadStatus.Visible = false;
                AdministrationEDSC.ActivityImageDTRow iinfo = new AdministrationDAC().RetrieveActivityImageInformation(ActivityID); ;
                int maxSizeKB = iinfo.FreeStorage;
                int fileSize = fileUpload1.PostedFile.ContentLength / 1024;

                AdministrationEDSC.ActivityImageDetailDTRow dr = GetData();

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
                        new AdministrationBFC().CreateActivityImage(dr, out imageID, fileSize);

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