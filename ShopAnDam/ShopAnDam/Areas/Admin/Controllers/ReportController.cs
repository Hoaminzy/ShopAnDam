using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Report
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 4)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPageList(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
    }
}