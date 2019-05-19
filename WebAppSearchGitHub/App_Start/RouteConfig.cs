using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAppSearchGitHub
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{searchParams}",
                defaults: new { controller = "Home", action = "IndexAsync", searchParams = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "AddToBookmarks",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "AddToBookmarks", repositoryId = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "CleanBookmark",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "CleanBookmark", repositoryId = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "CleanAllBookmarks",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "CleanAllBookmarks" }
           );
        }
    }
}
