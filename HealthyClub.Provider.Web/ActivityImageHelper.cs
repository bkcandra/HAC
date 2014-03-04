using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using HealthyClub.Utility;


namespace HealthyClub.Providers.Web
{
    public class ActivityImageHelper
    {
        public static void UploadProductThumbnail(Image img, string filename)
        {
            //Image thumb = new ImageUtil().CreateThumbnail(img);
            //thumb.Save(filename);
            new BCUtility.ImageHandler().SaveAsThumbnail(img, filename);
        }

        public static void UpdateProductThumbnail(string oldFilename, string newFileName, string mainImageSaveDir)
        {
            if (File.Exists(oldFilename))
                File.Delete(oldFilename);

            byte[] arr = File.ReadAllBytes(newFileName);
            MemoryStream ms = new MemoryStream(arr);
            var img = Image.FromStream(ms);
            
            ms.Close();

            UploadProductThumbnail(img, mainImageSaveDir);
        }

        public static void CreateImageFromText(string fileName, string text, int width, int height, Color textColor, Font font)
        {
            Bitmap bmp = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(bmp);
            SolidBrush brush = new SolidBrush(textColor);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);
            g.DrawString(text, font, brush, new PointF());

            bmp.Save(fileName, ImageFormat.Jpeg);
        }
    }
}