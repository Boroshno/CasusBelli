using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CasusBelli.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
            "Default", // Route name
            "",        // URL with parameters
            new { controller = "Reconstruction", action = "Index" }  // Parameter defaults
        );

            routes.MapRoute(
                "Order",
                "{controller}/{action}/{productId}",
                new { controller = "Order", action = "Index", productId = UrlParameter.Optional }
                );
        }
    }
}