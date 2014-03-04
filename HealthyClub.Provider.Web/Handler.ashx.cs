using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace HealthyClub.Provider.Web
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            if (context.Request.QueryString[SystemConstants.username] != null)
            {
                string strUserName = "";
                if (context.Request.QueryString[SystemConstants.username] != null)
                    strUserName = context.Request.QueryString[SystemConstants.username];
                //Check userName Here
                bool strReturnStatus = true;
                if (WebSecurity.UserExists(strUserName))
                {
                    strReturnStatus = false;
                }
                context.Response.Clear();
                context.Response.Write(strReturnStatus);
                context.Response.End();
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