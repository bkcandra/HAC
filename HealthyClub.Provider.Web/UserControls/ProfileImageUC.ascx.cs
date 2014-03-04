using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Provider.EDS;
using HealthyClub.Utility;
using HealthyClub.Provider.DA;
using HealthyClub.Provider.BF;
using System.IO;
using WebMatrix.WebData;


namespace HealthyClub.Providers.Web.UserControls
{
    public partial class ProfileImageUC : System.Web.UI.UserControl
    {
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

        public int ImageUploaded
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnImageAmount.Value))
                    return Convert.ToInt32(hdnImageAmount.Value);
                else return 0;
            }
            set
            {
                hdnImageAmount.Value = value.ToString();
            }
        }

        public int SizeUploaded
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnStorageUsed.Value))
                    return Convert.ToInt32(hdnStorageUsed.Value);
                else return 0;
            }
            set
            {
                hdnStorageUsed.Value = value.ToString();
            }
        }

        protected void Page_init(object sender, EventArgs e)
        {
            int ID = WebSecurity.CurrentUserId;
            if (ID != 0)
            {
                Guid userID = new MembershipHelper().GetProviderUserKey(ID);
                ProviderID = userID;
            }
            else
                Response.Redirect("~");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (WebSecurity.IsAuthenticated)
                    Refresh();
                else
                {
                    Response.Redirect("~/Account/Login");
                }
            }
        }

        public void Refresh()
        {
            CheckUserImage();
            SetDataSource();

        }

        private void CheckUserImage()
        {
            //checkdatabase for record
            //if not exist, create img folder & thumb folder
            var dr = new ProviderDAC().RetrieveUserImageInformation(ProviderID);
            if (dr == null)
            {
                ProviderDAC dac = new ProviderDAC();
                ProviderEDSC.UserImageDTRow userImage = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
                userImage.UserID = ProviderID;
                userImage.StorageUsed = userImage.ImageAmount = 0;
                userImage.FreeStorage = SystemConstants.MaxUserImageStorage;

                int userImageID = 0;
                dac.CreateUserImageInformation(userImage, out userImageID);
            }

        }

        private void SetDataSource()
        {
            var dr = new ProviderDAC().RetrieveUserImageInformation(ProviderID);
            if (dr != null)
            {
                hdnUserID.Value = dr.UserID.ToString();
                hdnStorageUsed.Value = dr.StorageUsed.ToString();
                hdnFreeStorage.Value = dr.FreeStorage.ToString();
                hdnImageAmount.Value = dr.ImageAmount.ToString();
                hdnID.Value = dr.ID.ToString();

                var imageDts = new ProviderDAC().RetrieveUserImages(ProviderID);
                ListView1.DataSource = imageDts;
                ListView1.DataBind();
            }
        }

        private ProviderEDSC.UserImageDetailDTRow GetData()
        {
            int imagesCount = new ProviderDAC().RetrieveUserImagesCount(ProviderID);

            ProviderEDSC.UserImageDetailDTRow dr = new ProviderEDSC.UserImageDetailDTDataTable().NewUserImageDetailDTRow();
            dr.UserImageID = 0;
            dr.UserID = ProviderID;
            dr.Filename = FileUpload1.FileName;
            dr.Description = "";
            if (imagesCount == 0)
                dr.isPrimaryImage = true;
            else
                dr.isPrimaryImage = false;
            dr.ImageTitle = "";
            dr.Filesize = FileUpload1.PostedFile.ContentLength / 1024;
            return dr;
        }

        protected void imgBtnUpload_Click(object sender, EventArgs e)
        {
            int uploaded = 0;
            var usrImagDet = GetUserImageInfo();
            var dt = new ProviderEDSC.UserImageDetailDTDataTable();
            if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(FileUpload1.PostedFile.ContentType, "image/\\S+"))
                {
                    string ext = FileUpload1.PostedFile.ContentType;
                    if (ext == "bmp" || ext == ".bmp")
                    {
                        lblWarningUpload.Text = ".bmp extension is not supported";
                    }
                    else
                    {

                        int iFailedCnt = 0;
                        HttpFileCollection hfc = Request.Files;
                        lblWarningUpload.Text = "Select <b>" + hfc.Count + "</b> file(s)";

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
                                        var dr = dt.NewUserImageDetailDTRow();
                                        dr.UserID = ProviderID;
                                        dr.UserImageID = Convert.ToInt32(hdnID.Value);
                                        dr.Filename = dr.ImageTitle = sFileName;
                                        dr.ImageType = (int)SystemConstants.userImageType.ProviderIcon;
                                        dr.Description = "";
                                        dr.isPrimaryImage = false;
                                        dr.Filesize = SizeUploaded;
                                        dr.ImageStream = new BCUtility.ImageHandler().ReadFully(hpf.InputStream);
                                        dt.AddUserImageDetailDTRow(dr);
                                        dr.Filesize = hpf.ContentLength / 1024;
                                        uploaded += 1;

                                        usrImagDet.FreeStorage = usrImagDet.FreeStorage - dr.Filesize;
                                        usrImagDet.StorageUsed = SizeUploaded = SizeUploaded + dr.Filesize;
                                        
                                    }
                                    else
                                    {
                                        iFailedCnt += 1;
                                        lblWarningUpload.Text += "</br><b>" + iFailedCnt + "</b> file(s) could not be uploaded. Maximum Size per activity is" + SystemConstants.MaxActivityImageStorage + " Kb";
                                    }
                                }
                            }
                            lblWarningUpload.Text = "<b>" + uploaded + "</b> file(s) uploaded.";
                        }
                        else lblWarningUpload.Text = "Max. 10 files allowed.";
                    }
                }
                else
                {
                    lblWarningUpload.Text = "Only image files are accepted.";
                }
            }
            else lblWarningUpload.Text = "No files selected.";

            lblWarningUpload.Visible = true;
            if (uploaded > 0)
            {
                usrImagDet.ImageAmount = ImageUploaded + uploaded;
                new ProviderBFC().AddNewProviderImages(ProviderID, dt, usrImagDet);
                SetDataSource();
            }

        }

        private ProviderEDSC.UserImageDTRow GetUserImageInfo()
        {
            var dr = new ProviderEDSC.UserImageDTDataTable().NewUserImageDTRow();
            dr.ID = Convert.ToInt32(hdnID.Value);
            dr.UserID = ProviderID;
            dr.StorageUsed = SizeUploaded;
            dr.FreeStorage = Convert.ToInt32(hdnFreeStorage.Value);
            dr.ImageAmount = ImageUploaded;
            return dr;
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgProfileImage = e.Item.FindControl("imgProfileImage") as Image;
                HiddenField hdnPrimary = e.Item.FindControl("hdnMainImage") as HiddenField;
                HiddenField hdnImageID = e.Item.FindControl("hdnImageID") as HiddenField;
                HiddenField hdnImageName = e.Item.FindControl("hdnImageName") as HiddenField;
                LinkButton lnkSetPrimary = e.Item.FindControl("lnkSetPrimary") as LinkButton;

                if (Convert.ToBoolean(hdnPrimary.Value))
                {
                    lnkSetPrimary.Visible = false;
                }
                if (Convert.ToBoolean(!string.IsNullOrEmpty(hdnImageID.Value)))
                {
                    imgProfileImage.ImageUrl = "~/ImageHandler.ashx?" + SystemConstants.qs_UserImageID + "=" + hdnImageID.Value;
                }
            }
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
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
                bfc.DeleteUserImage(ProviderID, imageID, Convert.ToInt32(hdnFilesize.Value), out imageThumbVirtualPath, out imageVirtualPath);

                string imageFilePath = Server.MapPath(imageVirtualPath);
                string imageThumbFilePath = Server.MapPath(imageThumbVirtualPath);

                FileInfo image = new FileInfo(imageFilePath);
                FileInfo imageThumb = new FileInfo(imageThumbFilePath);
                if (image.Exists)
                    File.Delete(imageFilePath);
                if (imageThumb.Exists)
                    File.Delete(imageThumbFilePath);
            }
            else if (e.CommandName == "SetAsPrimaryImage")
            {
                dac.UpdateUserPrimaryImage(ProviderID, imageID);
            }
            Refresh();
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

        }
    }
}