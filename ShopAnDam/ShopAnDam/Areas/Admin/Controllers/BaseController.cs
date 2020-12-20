﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAnDam.Common;
using System.Web.Routing;
namespace ShopAnDam.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //    // GET: Admin/Base
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConStants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if(type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }else if(type == "wanring")
            {
                TempData["AlertType"] = "alert-warning";

            }else if(type == "error")
            {
                TempData["AlertType"] = "alert-danger";

            }
        }
    }
}