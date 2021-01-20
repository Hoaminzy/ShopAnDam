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
   
    public class CartController : Controller
    {
     
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[Common.CommonConStants.CartSession];
            var list = new List<CartItem>();
            if(cart!= null)
            {
                list = (List<CartItem>)cart;
            }
       
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[Common.CommonConStants.CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[Common.CommonConStants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[Common.CommonConStants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Common.CommonConStants.CartSession] = sessionCart;
            return Json(new

            {
                status = true
            });
        }
        public ActionResult AddItem (int ProductID, int Quantity)
        {
            var product = new ProductDao().ViewDetail(ProductID);
            var cart = Session[Common.CommonConStants.CartSession];
            if(cart != null) //nếu chưa có sp nào trong giỏ hàng thì thêm mới. Nếu đã tồn tài thì tăng sl +1
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=>x.Product.ID == ProductID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == ProductID)
                        {
                            item.Quantity += Quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = Quantity;
                    list.Add(item);

                    //gán vào session
                    Session[Common.CommonConStants.CartSession] = list;
                }
               
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = Quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[Common.CommonConStants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            var cart = Session[Common.CommonConStants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }
    }
}