using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var model = new CategoryDao().getAll();
            return PartialView(model);
        }
         public ActionResult CategoryView(int cateID)
        {
            var category = new CategoryDao().ViewDetail(cateID);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryByID(cateID) ;
            return View(model);
        }




    }
}