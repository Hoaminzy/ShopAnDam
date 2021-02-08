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
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var model = new ArticleDao().ListAllPading(page, pagesize);
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new ArticleDao().ViewDetail(id);
            return View(model);
        }
    }
}