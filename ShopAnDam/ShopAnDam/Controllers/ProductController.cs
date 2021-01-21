using ShopAnDam.Models;
using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopAnDam.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var product = new ProductDao().ListAll();
            return View(product);

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
            var model = new ProductDao().ListByCategoryByID(id);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.image = new ImageDao().ViewImageByIDProduct(product.ID);
            ViewBag.category = new CategoryDao().ViewDetail(product.ID);
            return View(product);
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Common.CommonConStants.CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status = true
            });
        }
    }
        
}