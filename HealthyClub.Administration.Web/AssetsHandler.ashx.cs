using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthyClub.Utility;
using HealthyClub.Administration.DA;

namespace HealthyClub.Administration.Web
{
    /// <summary>
    /// Summary description for AssetsHandler
    /// </summary>
    public class AssetsHandler : IHttpHandler
    {
        int assetID = 0;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            if (context.Request.QueryString[SystemConstants.qs_AssetID] != null)
                assetID = Convert.ToInt32(context.Request.QueryString[SystemConstants.qs_AssetID]);

            if (assetID != 0)
            {
                // Retrieve ImageStream from db

                byte[] imageStream = new AdministrationDAC().RetrieveAssetBinary(assetID);
                if (imageStream != null)
                {
                    context.Response.ContentType = imageStream.ToString();
                    context.Response.BinaryWrite(imageStream);
                    context.Response.End();

                }
            }
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