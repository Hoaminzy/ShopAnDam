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

            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

           
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
             name: "Create Good ",
             url: "them-phieu",
             defaults: new { controller = "Good", action = "Create", id = UrlParameter.Optional },
             new[] { "ShopAnDam.Controllers" }
         );
            routes.MapRoute(
             name: "Create Good 1",
             url: "them-phieu-nhap",
             defaults: new { controller = "Good", action = "AddSP", id = UrlParameter.Optional },
             new[] { "ShopAnDam.Controllers" }
         );
            routes.MapRoute(
            name: "Dang Ky",
            url: "dang-ky",
            defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
            new[] { "ShopAnDam.Controllers" }
            );
            routes.MapRoute(
            name: "Dang Nhap",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
            new[] { "ShopAnDam.Controllers" }
            );
          routes.MapRoute(
            name: "Dang Xuat",
            url: "dang-xuat",
            defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
            new[] { "ShopAnDam.Controllers" }
            );
            routes.MapRoute(
         name: "Tai Khoan",
         url: "tai-khoan",
         defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional },
         new[] { "ShopAnDam.Controllers" }
         );
            routes.MapRoute(
      name: "Tim Kiem",
      url: "tim-kiem",
      defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
      new[] { "ShopAnDam.Controllers" }
      );
            routes.MapRoute(
     name: "Payment Success",
     url: "hoan-thanh",
     defaults: new { controller = "Cart", action = "PaymentSuccess", id = UrlParameter.Optional },
     new[] { "ShopAnDam.Controllers" }
     );
            routes.MapRoute(
name: "Payment Fail",
url: "loi-thanh-toan",
defaults: new { controller = "Cart", action = "PaymentFail", id = UrlParameter.Optional },
new[] { "ShopAnDam.Controllers" }
);
                    routes.MapRoute(
                       name: "Don Hang Cua Ban",
                       url: "danh-sach-don-hang",
                        defaults: new { controller = "Account", action = "MyOrder", id = UrlParameter.Optional },
                      new[] { "ShopAnDam.Controllers" }
                      );
                     routes.MapRoute(
            name: "My Article",
            url: "bai_viet_cua_ban",
            defaults: new { controller = "Account", action = "MyArticle", id = UrlParameter.Optional },
            new[] { "ShopAnDam.Controllers" }
            );
            routes.MapRoute(
          name: "DS Bai Viet",
            url: "danh-sach-bai-viet",
            defaults: new { controller = "Account", action = "BaiViet", id = UrlParameter.Optional },
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
           url: "bai-viet/{metaTitle}/{id}",
            defaults: new { controller = "Article", action = "Details", id = UrlParameter.Optional },
           new[] { "ShopAnDam.Controllers" }
           );

    

            routes.MapRoute(
          name: "Create Article",
         url: "tao-bai-viet",
          defaults: new { controller = "Account", action = "CreateArticle", id = UrlParameter.Optional },
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
 defaults: new { controller = "Cart", action = "Payments", id = UrlParameter.Optional },
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
