using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HealthyClub.Administration.Web;
using WebMatrix.WebData;

namespace HealthyClub.Administration.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
/*
            WebSecurity.CreateUserAndAccount("Admin", "Admin");
            Roles.CreateRole(SystemConstants.AdministratorRole);
            Roles.CreateRole(SystemConstants.CustomerRole);
            Roles.CreateRole(SystemConstants.ProviderRole);
            Roles.AddUserToRole("Admin", "Administrator");
            */
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
