using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthyClub.Utility;
using HealthyClub.Provider.DA;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using BCUtility;

namespace HealthyClub.Providers.Web
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        int imgID = 0;
        int UserimgID = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            if (context.Request.QueryString[SystemConstants.qs_ImageID] != null)
            {
                imgID = Convert.ToInt32(context.Request.QueryString[SystemConstants.qs_ImageID]);

                if (imgID != 0)
                {
                    byte[] imageByte = new ProviderDAC().RetrieveImageBinary(imgID);
                    if (imageByte != null && imageByte.Length > 0)
                    {
                        ImageResponse(context, imageByte);
                        context.Response.End();
                    }
                }
            }

            if (context.Request.QueryString[SystemConstants.qs_ThumbImageID] != null)
            {
                imgID = Convert.ToInt32(context.Request.QueryString[SystemConstants.qs_ThumbImageID]);

                if (imgID != 0)
                {
                    byte[] imageByte = new ProviderDAC().RetrieveImageBinary(imgID);
                    if (imageByte != null && imageByte.Length > 0)
                    {
                        ImageResponse(context, imageByte, SystemConstants.ThumbnailWidth, SystemConstants.ThumbnailHeight);
                        context.Response.End();
                    }
                }
            }

            if (context.Request.QueryString[SystemConstants.qs_AssetID] != null)
                imgID = Convert.ToInt32(context.Request.QueryString[SystemConstants.qs_AssetID]);
            if (imgID != 0)
            {
                // Retrieve ImageStream from db
                byte[] imageStream = new ProviderDAC().RetrieveAssetBinary(imgID);
                if (imageStream != null)
                {
                    ImageResponse(context, imageStream);
                    context.Response.End();
                }
            }

            if (context.Request.QueryString[SystemConstants.qs_UserImageID] != null)
                UserimgID = Convert.ToInt32(context.Request.QueryString[SystemConstants.qs_UserImageID]);
            if (UserimgID != 0)
            {
                // Retrieve ImageStream from db
                byte[] imageStream = new ProviderDAC().RetrieveUserImageBinary(UserimgID);
                if (imageStream != null)
                {
                    ImageResponse(context, imageStream);
                    context.Response.End();
                }
            }
        }

        private void ImageResponse(HttpContext context, byte[] imageByte)
        {
            //creating memoryStream object
            using (MemoryStream mm = new MemoryStream())
            {

                // Retrieve ImageStream from db
                System.Drawing.Image b;
                //creating object of bitmap
                Bitmap bitMap = null;

                //writing to memoryStream
                mm.Write(imageByte, 0, imageByte.Length);
                b = System.Drawing.Image.FromStream(mm);
                bitMap = new System.Drawing.Bitmap(b, b.Width, b.Height);
                //creating graphic object, to produce High Quality images.
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    g.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality =
                    System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(bitMap, 0, 0, b.Width, b.Height);
                    g.Dispose(); b.Dispose(); mm.Dispose();
                    //changing content type of handler page
                    context.Response.ContentType = "image/jpeg";
                    //saving bitmap image
                    bitMap.Save(context.Response.OutputStream,
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                    bitMap.Dispose();
                }
            }
        }

        private void ImageResponse(HttpContext context, byte[] imageByte, int Width, int Height)
        {
            //creating memoryStream object
            using (MemoryStream mm = new MemoryStream())
            {
                Bitmap bitMap = CreateThumbnail(imageByte, true, Width, Height);
                context.Response.ContentType = "image/jpeg";
                //saving bitmap image
                bitMap.Save(context.Response.OutputStream,
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                bitMap.Dispose();
            }
        }

        public Bitmap CreateThumbnail(byte[] imageByte,
    bool maintainAspectRatio, int desiredWidth, int desiredHeight)
        {
            Bitmap bmp = null;

            MemoryStream memStream = new MemoryStream(imageByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);

            if (maintainAspectRatio)
            {
                AspectRatio aspectRatio = new AspectRatio();
                aspectRatio.WidthAndHeight(img.Width, img.Height,
            desiredWidth, desiredHeight);
                bmp = new Bitmap(img, aspectRatio.Width, aspectRatio.Height);
            }
            else
            {
                bmp = new Bitmap(img, desiredWidth, desiredHeight);
            }
            memStream.Dispose();

            return bmp;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}