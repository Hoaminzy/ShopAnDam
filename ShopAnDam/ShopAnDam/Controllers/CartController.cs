using ShopAnDam.Models;
using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ShopAnDam.Models.ViewModel;
using System.Xml.Linq;

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

        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/Data/Provinces_Data.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceViewModel>();
            ProvinceViewModel tinhThanh = null;
            foreach (var item in xElements)
            {
                tinhThanh = new ProvinceViewModel();
                tinhThanh.ID = int.Parse(item.Attribute("id").Value);
                tinhThanh.Name = item.Attribute("value").Value;
                list.Add(tinhThanh);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }


        public JsonResult LoadDistrict(int tinhThanhID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/Data/Provinces_Data.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == tinhThanhID);

            var list = new List<DistrictViewModel>();
            DistrictViewModel quanhuyen = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                quanhuyen = new DistrictViewModel();
                quanhuyen.ID = int.Parse(item.Attribute("id").Value);
                quanhuyen.Name = item.Attribute("value").Value;
                quanhuyen.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(quanhuyen);

            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}