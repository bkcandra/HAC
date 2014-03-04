using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using HealthyClub.Utility;

namespace HealthyClub.Provider.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            routes.MapPageRoute("PageRouting", "Pages/{" + SystemConstants.PageID + "}", "~/Pages/Default.aspx");
            routes.MapPageRoute("ActivityRouting", "Activity/{" + SystemConstants.qs_ActivitiesID + "}/{" + SystemConstants.ActivityName + "}", "~/Activity/Default.aspx");
        }
    }
}
