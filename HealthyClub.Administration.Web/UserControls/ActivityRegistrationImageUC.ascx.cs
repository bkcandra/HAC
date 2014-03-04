using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using HealthyClub.Utility;
using System.IO;
using System.Web.UI.HtmlControls;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using System.Data;
using HealthyClub.Administration.BF;


namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ActivityRegistrationImageUC : System.Web.UI.UserControl
    {
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

        public Guid ProviderID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnProviderID.Value))
                    return new Guid(hdnProviderID.Value);
                else return Guid.Empty;
            }
            set
            {
                hdnProviderID.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgBtnUpload_Click(object sender, EventArgs e)
        {
            var dt = GetImages();
            if (dt == null)
            {
                dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
            }
            if (fileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
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
                                SizeUploaded = SizeUploaded + Convert.ToInt32(hpf.ContentLength / 1024);
                                if (SizeUploaded <= SystemConstants.MaxActivityImageStorage)
                                {

                                    var dr = dt.NewActivityImageDetailDTRow();

                                    dr.ActivityID = 0;
                                    dr.ActivityImageID = 0;
                                    dr.ImageStream = new BCUtility.ImageHandler().ReadFully(hpf.InputStream);
                                    
                                    dr.Filename = sFileName;
                                    dr.Filesize = hpf.ContentLength / 1024;
                                    dt.AddActivityImageDetailDTRow(dr);

                                    ImageUploaded += 1;
                                }
                                else
                                {
                                    iFailedCnt += 1;
                                    lblUploadStatus.Text += "</br><b>" + iFailedCnt + "</b> file(s) could not be uploaded. Maximum Size per activity is" + SystemConstants.MaxActivityImageStorage + " Kb";
                                }
                                /*
                                // SAVE THE FILE IN A FOLDER.
                                string imagePath = SystemConstants.GetTempActImageDirectoryURL(ProviderID.ToString(), ActionKey, sFileName);
                                string thumbPath = SystemConstants.GetTempActivityImageThumbURL(ProviderID.ToString(), ActionKey, sFileName);

                                if (imagePath.StartsWith("~"))
                                    imagePath = Server.MapPath(imagePath);

                                if (thumbPath.StartsWith("~"))
                                    thumbPath = Server.MapPath(thumbPath);

                                //ActivityImageHelper.UploadProductThumbnail(img, thumbPath);
                                hpf.SaveAs(imagePath);
                                 */



                            }
                        }
                        lblUploadStatus.Text = "<b>" + ImageUploaded + "</b> file(s) uploaded.";


                    }
                    else lblUploadStatus.Text = "Max. 10 files allowed.";
                }
                else
                {
                    lblUploadStatus.Text = "Only image files are accepted.";
                }

            }
            else lblUploadStatus.Text = "No files selected.";
            lblUploadStatus.Visible = true;
            if (ImageUploaded > 0)
            {
                SetDataSource(dt);
                divUploadSuccessfull.Visible = true;
            }

        }

        internal void initUploader(Guid providerID, string key)
        {
            ActionKey = key;
            ProviderID = providerID;
            // string Path = SystemConstants.ImageDirectory + "/" + activityID + "/" + activityID + "_" + imageID + "_" + prodImage.Filename;
            //Ensure Directory is exist
            //var path = System.IO.Directory.CreateDirectory(Server.MapPath(@SystemConstants.TmpActImageDirectory + "/" + ProviderID + "/"));
            //Create ImageDir provider and key

            //System.IO.Directory.CreateDirectory(Server.MapPath(@SystemConstants.TmpActImageDirectory + "/" + ProviderID + "/" + ActionKey + "/"));
            //Create ImageThumbDir
            //System.IO.Directory.CreateDirectory(Server.MapPath(@SystemConstants.ActImageDirectory + "/" + ProviderID + "/" + ActionKey + "/" + @SystemConstants.ImageThumbDirectory));

        }

        private void SetDataSource(AdministrationEDSC.ActivityImageDetailDTDataTable dt)
        {
            dtImageLib.DataSource = dt;
            dtImageLib.DataBind();
        }

        protected void dtImageLib_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "RemoveImage")
            {
                HiddenField hdnID = e.Item.FindControl("hdnID") as HiddenField;
                var dt = GetImages(Convert.ToInt32(hdnID.Value));

                SetDataSource(dt);
                ImageUploaded = dt.Count;
                lblUploadStatus.Text = "<b>" + ImageUploaded + "</b> file(s) uploaded.";
                if (ImageUploaded == 0)
                    divUploadSuccessfull.Visible = false;
            }
        }

        protected void dtImageLib_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgUpload = e.Item.FindControl("imgUpload") as Image;
                HiddenField hdnImageStream = e.Item.FindControl("hdnImageStream") as HiddenField;

                DataRowView drView = e.Item.DataItem as DataRowView;
                var dr = drView.Row as AdministrationEDSC.ActivityImageDetailDTRow;

                hdnImageStream.Value = Convert.ToBase64String(dr.ImageStream);
                imgUpload.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(dr.ImageStream);

            }
        }

        public AdministrationEDSC.ActivityImageDTRow GetImageDetail()
        {
            var dr = new AdministrationEDSC.ActivityImageDTDataTable().NewActivityImageDTRow();
            dr.ActivityID = 0;
            dr.StorageUsed = SizeUploaded;
            dr.FreeStorage = SystemConstants.MaxActivityImageStorage - SizeUploaded;
            dr.ImageAmount = ImageUploaded;
            return dr;
        }

        public AdministrationEDSC.ActivityImageDetailDTDataTable GetImages()
        {
            if (ImageUploaded != 0)
            {
                var dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
                foreach (DataListItem item in dtImageLib.Items)
                {
                    var dr = dt.NewActivityImageDetailDTRow();
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Image imgUpload = item.FindControl("imgUpload") as Image;
                        HiddenField hdnImageStream = item.FindControl("hdnImageStream") as HiddenField;
                        HiddenField hdnImageSize = item.FindControl("hdnImageSize") as HiddenField;
                        HiddenField hdnID = item.FindControl("hdnID") as HiddenField;

                        Label lblName = item.FindControl("lblName") as Label;
                        dr.ID = Convert.ToInt32(hdnID.Value);
                        dr.ActivityID = 0;
                        dr.ActivityImageID = 0;
                        dr.ImageStream = Convert.FromBase64String(hdnImageStream.Value);
                        dr.Filename = lblName.Text;
                        dr.ImageTitle = "";
                        dr.Description = "";
                        dr.isPrimaryImage = false;
                        dr.Filesize = Convert.ToInt32(hdnImageSize.Value);
                        dt.AddActivityImageDetailDTRow(dr);
                    }
                }
                return dt;
            }
            else return null;
        }

        public AdministrationEDSC.ActivityImageDetailDTDataTable GetImages(int Removed)
        {
            if (ImageUploaded != 0)
            {
                var dt = new AdministrationEDSC.ActivityImageDetailDTDataTable();
                foreach (DataListItem item in dtImageLib.Items)
                {
                    var dr = dt.NewActivityImageDetailDTRow();
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField hdnID = item.FindControl("hdnID") as HiddenField;
                        if (Convert.ToInt32(hdnID.Value) != Removed)
                        {
                            Image imgUpload = item.FindControl("imgUpload") as Image;
                            HiddenField hdnImageStream = item.FindControl("hdnImageStream") as HiddenField;
                            HiddenField hdnImageSize = item.FindControl("hdnImageSize") as HiddenField;

                            Label lblName = item.FindControl("lblName") as Label;

                            dr.ID = Convert.ToInt32(hdnID.Value);
                            dr.ActivityID = 0;
                            dr.ActivityImageID = 0;
                            dr.ImageStream = Convert.FromBase64String(hdnImageStream.Value);
                            dr.Filename = lblName.Text;
                            dr.ImageTitle = "";
                            dr.Description = "";
                            dr.isPrimaryImage = false;
                            dr.Filesize = Convert.ToInt32(hdnImageSize.Value);
                            dt.AddActivityImageDetailDTRow(dr);
                        }
                    }
                }
                return dt;
            }
            else return null;
        }

    }

}