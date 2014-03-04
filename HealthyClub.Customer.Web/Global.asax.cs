using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HealthyClub.Customer.Web;
using WebMatrix.WebData;
using Segmentio;



namespace HealthyClub.Customer.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Analytics.Initialize("om5qi91nth3m257f4jcp");
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                              "Users", "ID", "UserName", autoCreateTables: true);

            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

       void Application_BeginRequest(object sender, EventArgs e)
        {
           
            //HttpApplication app = sender as HttpApplication;
            //if (app != null)
            //{
            //    string domain = "www.healthyaustraliaclub.com.au";
            //    string host = app.Request.Url.Host.ToLower();
            //    string path = app.Request.Url.PathAndQuery;
            //    if (!String.Equals(host, domain))
            //    {
            //        Uri newURL = new Uri(app.Request.Url.Scheme +
            //        "://" + domain + path);
            //        app.Context.Response.RedirectPermanent(
            //        newURL.ToString(), endResponse: true);
            //    }
            //}

            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (string.IsNullOrEmpty(url) ||
                System.IO.Directory.Exists(Server.MapPath(url)))
                return;

            if (url.EndsWith("/"))
            {
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", url.Substring(0, url.Length - 1));
                Response.End();
            }
        }

    }
}
