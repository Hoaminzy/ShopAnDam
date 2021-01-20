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
        //public ActionResult Details()
        //{
        //    return View();
        //}
        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var model = new CategoryDao().getAll();
            return PartialView(model);
        }
         public ActionResult CategoryView(int id)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryByID(id) ;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.image = new ImageDao().ViewImageByIDProduct(product.ID);
            ViewBag.category = new CategoryDao().ViewDetail(product.ID);
            return View(product);
        }


    }
}