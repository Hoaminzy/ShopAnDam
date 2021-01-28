using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            ViewBag.article = new ArticleDao().ListAll();
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}