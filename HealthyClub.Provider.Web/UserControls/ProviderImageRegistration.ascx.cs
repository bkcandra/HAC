using HealthyClub.Provider.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Provider.Web.UserControls
{
    public partial class ProviderImageRegistration : System.Web.UI.UserControl
    {
        public bool isSupported
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnisSupported.Value))
                    return Convert.ToBoolean(hdnisSupported.Value);
                else return false;
            }
            set
            {
                hdnisSupported.Value = value.ToString();
            }
        }

        public string Name
        {
            get
            {
                return lblName.Text;
            }

            set
            {
                lblName.Text = value.ToString();
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

        public string fileName
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFileName.Value))
                    return hdnFileName.Value;
                else return "";
            }
            set
            {
                hdnFileName.Value = value.ToString();
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
            if (fileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                if (fileUpload1.PostedFile.ContentLength >=  SystemConstants.ImageLimitInByte())
                {
                    lblUploadStatus.Text = "Maximum file size is 4MB.";
                    lblUploadStatus.ForeColor = Color.Red;
                }
                else
                {

                    if (System.Text.RegularExpressions.Regex.IsMatch(fileUpload1.PostedFile.ContentType, "image/\\S+"))
                    {
                        string ext = fileUpload1.PostedFile.ContentType;
                        if (ext == "bmp" || ext == ".bmp")
                        {
                            lblUploadStatus.Text = ".bmp extension is not supported";
                            lblUploadStatus.ForeColor = Color.Red;
                        }
                        else
                        {
                            int iFailedCnt = 0;

                            string sFileName = Path.GetFileName(fileUpload1.FileName);

                            if (SizeUploaded <= SystemConstants.MaxActivityImageStorage)
                            {
                                Stream input = fileUpload1.PostedFile.InputStream;
                                byte[] buffer = new byte[input.Length];
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    int read;
                                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        ms.Write(buffer, 0, read);
                                    }
                                    if (isSupported)
                                    {
                                        UploadedTitle.Text = "Preview";
                                        ProviderImagePreview.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ms.ToArray());
                                    }
                                    else
                                    {
                                        ProviderImagePreview.Visible = false;
                                        lblimageTitle.Visible = true;
                                        lblimageTitle.Text = sFileName + " - " + SizeUploaded + "kb";
                                        UploadedTitle.Text = "Uploaded Image";
                                    }
                                    hdnImageStream.Value = Convert.ToBase64String(ms.ToArray());
                                }

                                fileName = fileUpload1.FileName;

                                SizeUploaded = fileUpload1.PostedFile.ContentLength / 1024;
                                ImageUploaded += 1;
                            }
                            else
                            {
                                iFailedCnt += 1;
                                lblUploadStatus.Text += "</br><b>" + iFailedCnt + "</b> file could not be uploaded. Maximum Size per activity is" + SystemConstants.MaxActivityImageStorage + " Kb";
                                lblUploadStatus.ForeColor = Color.Red;
                            }

                            lblUploadStatus.Text = "Image uploaded.";
                            lblUploadStatus.ForeColor = ColorTranslator.FromHtml("#1B274F");
                        }
                    }
                    else
                    {
                        lblUploadStatus.Text = "Only image files are accepted."; lblUploadStatus.ForeColor = Color.Red;
                    }

                }
            }
            else { lblUploadStatus.Text = "No files selected."; lblUploadStatus.ForeColor = Color.Red; }
            lblUploadStatus.Visible = true;
            if (ImageUploaded > 0)
            {
                divUploadSuccessfull.Visible = true;
            }

        }

        internal void initUploader(Guid providerID, string key)
        {
            ActionKey = key;
            ProviderID = providerID;
        }

        public bool isImageValid()
        {
            if (ImageUploaded != 0)
                return true;
            else return false;
        }

        public ProviderEDSC.UserImageDTRow GetUserImage()
        {

            var dr = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
            dr.UserID = ProviderID;
            dr.StorageUsed = SizeUploaded;
            dr.FreeStorage = SystemConstants.MaxUserImageStorage - SizeUploaded;
            dr.ImageAmount = ImageUploaded;
            return dr;
        }

        public ProviderEDSC.UserImageDetailDTDataTable GetUserImageDetail()
        {
            var dt = new ProviderEDSC.UserImageDetailDTDataTable();
            {
                var dr = dt.NewUserImageDetailDTRow();
                dr.UserID = ProviderID;
                dr.UserImageID = 0;
                dr.Filename = dr.ImageTitle = fileName;
                dr.ImageType = (int)SystemConstants.userImageType.ProviderIcon;
                dr.Description = "";
                dr.isPrimaryImage = true;
                dr.Filesize = SizeUploaded;
                dr.ImageStream = Convert.FromBase64String(hdnImageStream.Value);
                dt.AddUserImageDetailDTRow(dr);
            }
            return dt;
        }
    }
}