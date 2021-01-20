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
              name: "Product ",
              url: "san-pham/{metaTitle}-{id}",
              defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
              new[] { "ShopAnDam.Controllers" }
          );

            routes.MapRoute(
              name: "San Pham ",
              url: "san-pham",
              defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
              new[] { "ShopAnDam.Controllers" }
          );
            routes.MapRoute(
              name: "Product Category",
              url: "san-pham/{metaTitle}/{id}",
              defaults: new { controller = "Product", action = "CategoryView", id = UrlParameter.Optional },
              new[] { "ShopAnDam.Controllers" }
          );
            routes.MapRoute(
        name: "Contact",
        url: "lien-he/{id}",
        defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
        new[] { "ShopAnDam.Controllers" }
    );
            routes.MapRoute(
             name: "Product Details",
            url: "chi-tiet/{metaTitle}/{id}",
             defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional },
            new[] { "ShopAnDam.Controllers" }
            );
            routes.MapRoute(
            name: "Article",
           url: "bai-viet",
            defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional },
           new[] { "ShopAnDam.Controllers" }
           );
            routes.MapRoute(
            name: "Article Details",
           url: "chi-tiet/{metaTitle}-{id}",
            defaults: new { controller = "Article", action = "Details", id = UrlParameter.Optional },
           new[] { "ShopAnDam.Controllers" }
           );
            routes.MapRoute(
        name: "Cart",
        url: "gio-hang",
        defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
        namespaces: new[] { "OnlineShop.Controllers" }
    );
            routes.MapRoute(
        name: "Add Cart",
       url: "them-gio-hang",
        defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
       new[] { "ShopAnDam.Controllers" }
       );
            routes.MapRoute(
 name: "Payment",
url: "thanh-toan",
 defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
new[] { "ShopAnDam.Controllers" }
);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "ShopAnDam.Controllers" }
            );
        }
    }
}
