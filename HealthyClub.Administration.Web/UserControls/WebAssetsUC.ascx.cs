using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Utility;
using HealthyClub.Administration.BF;
using HealthyClub.Administration.EDS;
using HealthyClub.Administration.DA;
using System.IO;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class WebAssetsUC : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            SetDataSource();
        }

        private void SetDataSource()
        {

            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectMethod = "RetrieveWebAssets";
            ods.SelectCountMethod = "RetrieveWebAssetsCount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.MaximumRowsParameterName = "amount";
            ods.EnablePaging = true;

            ListViewAsset.DataSourceID = "ods";
            ListViewAsset.DataBind();
        }

        protected void imgBtnUpload_Click(object sender, EventArgs e)
        {
            int ImageUploaded = 0;

            if (fileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                var dt = new AdministrationEDSC.WebAssetsDTDataTable();
                if (System.Text.RegularExpressions.Regex.IsMatch(fileUpload1.PostedFile.ContentType, "image/\\S+"))
                {
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


                                var dr = dt.NewWebAssetsDTRow();

                                dr.FileStream = new BCUtility.ImageHandler().ReadFully(hpf.InputStream);
                                dr.Filename = sFileName;
                                dr.FileSize = hpf.ContentLength / 1024;
                                dr.Filename = dr.Filetitle = sFileName;
                                dr.FileType = sFileExt;
                                dr.Link = "";
                                dt.AddWebAssetsDTRow(dr);

                                ImageUploaded += 1;

                            }
                        }

                        lblUploadStatus.Text = "<b>" + ImageUploaded + "</b> file(s) uploaded.";
                    }
                    else lblUploadStatus.Text = "Max. 10 files allowed.";

                    new AdministrationBFC().CreateAssetsInformation(dt);
                    Refresh();
                }
                else
                {
                    lblUploadStatus.Text = "Only image files are accepted.";
                }
            }
            else lblUploadStatus.Text = "No files selected.";
            lblUploadStatus.Visible = true;


        }

        protected void ListViewAsset_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hdnAssetID = e.Item.FindControl("hdnAssetID") as HiddenField;
            Image imgAsset = e.Item.FindControl("imgAsset") as System.Web.UI.WebControls.Image;
            //Label lblUrl = e.Item.FindControl("lblUrl") as Label;

            imgAsset.ImageUrl = "~/AssetsHandler.ashx?" + SystemConstants.qs_AssetID + "=" + hdnAssetID.Value;

            //lblUrl.Text = SystemConstants.CustomerUrl + "ImageHandler.ashx?" + SystemConstants.qs_AssetID + "=" + hdnAssetID.Value;
        }

        protected void ListViewAsset_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAsset" || e.CommandName == "viewAssetDetail")
            {
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                HiddenField hdnAssetID = e.Item.FindControl("hdnAssetID") as HiddenField;
                HiddenField hdnFilename = e.Item.FindControl("hdnFilename") as HiddenField;
                HiddenField hdnFileext = e.Item.FindControl("hdnFileext") as HiddenField;
                HiddenField hdnFilesize = e.Item.FindControl("hdnFilesize") as HiddenField;

                int AssetID = Convert.ToInt32(hdnAssetID.Value);
                AdministrationDAC dac = new AdministrationDAC();
                AdministrationBFC bfc = new AdministrationBFC();

                if (e.CommandName == "DeleteAsset")
                {
                    dac.DeleteAsset(AssetID);
                    Refresh();

                }
                else if (e.CommandName == "viewAssetDetail")
                {
                    lblFilesize.Text = hdnFilesize.Value + " KB";
                    lblTitle.Text = hdnFilename.Value;
                    lblExtension.Text = hdnFileext.Value;
                    lblUrl.Text = SystemConstants.CustomerUrl + "ImageHandler.ashx?" + SystemConstants.qs_AssetID + "=" + hdnAssetID.Value; ;
                    ModalPopupExtender1.Show();
                }
            }
        }
    }
}