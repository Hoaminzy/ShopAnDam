using ShopAnDam.Common;
using ShopAnDam.Models;
using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
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
            ViewBag.SPComment = new ReviewDao().ListAllRVProduct(product.ID, 5);
            return View(product);
        }

         [HttpPost]
        public JsonResult AddComment(string comment,string ProductID/*, string name, string email*/)
        {
            var review = new Review();
            var userSession = (CustomerLogin)Session[CommonConStants.USER_SESSION];
            int idsp = int.Parse(ProductID);
           /* review.Customer.Name = name;
            review.Customer.Email = email;*/
            review.comment = comment;
            review.ProductID = idsp;
            review.CreateDate = DateTime.Now;
            review.CreateBy = userSession.Name;
            review.Status = true;



            var id = new ReviewDao().InsertRV(review);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                //send mail

            }

            else
                return Json(new
                {
                    status = false
                });

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