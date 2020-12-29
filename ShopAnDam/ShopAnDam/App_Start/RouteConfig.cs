using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopAnDam
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "ShopAnDam.Controllers" }
            );

            routes.MapRoute(
              name: "Default",
              url: "{san-pham}/{metaTitle}-{id}",
              defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
              new[] { "ShopAnDam.Controllers" }
          );
        }
    }
}
