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
using ShopAnDam.Models.Framework;
using ShopAnDam.Common;
using System.Configuration;
using ShopAnDam.NganLuong;
namespace ShopAnDam.Controllers
{
   
    public class CartController : Controller
    {
       /* private string MerchantID = ConfigurationManager.AppSettings["MerchantID"].ToString();
        private string MerchantPassword = ConfigurationManager.AppSettings["MerchantPassword"].ToString();
        private string MerchantEmail = ConfigurationManager.AppSettings["MerchantEmail"].ToString();
        private string currentLink = ConfigurationManager.AppSettings["currentLink"].ToString();*/
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
        [HttpGet]
        public ActionResult Payments()
        {
            var cart = Session[Common.CommonConStants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Payments(string NameShip, string email, string SDT, string address, string sBankCode, string FormOfPayment, FormCollection formCollection )
        {
            var order = new Order();
            var sessionCustom = (CustomerLogin)Session[CommonConStants.USER_SESSION];
            var TenTinhThanh = formCollection["hdnTenTinhThanh"];
            var TenQuanHuyen = formCollection["hdnTenQuanHuyen"];
            string diachi = TenQuanHuyen + ", " + TenTinhThanh;

            if (sessionCustom != null)
            {

                order.CustomersID = sessionCustom.CustomerID;
              
            }
            order.CreateDate = DateTime.Now;
            order.NameShip = NameShip;
            order.MailShip = email;
            order.PhoneShip = SDT;
            order.AdressShip = address + ", " + TenQuanHuyen + ", " + TenTinhThanh;
           order.FormOfPayment = FormOfPayment;
            order.Status = 1;
            try { 
           
                var id = new OrderDao().Insert(order);
                //không lấy được ID hóa đơn này
                var cart = (List<CartItem>)Session[CommonConStants.CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var dao = new ProductDao();
                    var orderDetail = new Order_Detail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Quantity = item.Quantity;
                    if (item.Product.MotionPrice != null && item.Product.MotionPrice != 0)
                    {
                        orderDetail.Price = item.Product.MotionPrice;
                    }
                    else
                    {
                        orderDetail.Price = item.Product.Price;
                    }
                
                    detailDao.Insert(orderDetail);
                  
                    if (item.Product.MotionPrice != null && item.Product.MotionPrice != 0)
                    {
                        total += (item.Product.MotionPrice.GetValueOrDefault(0) * item.Quantity);
                    }
                    else
                    {
                        total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    }
                    dao.AddQuantity(item.Product.ID, item.Quantity);

                }
               /* if (!FormOfPayment.Equals("COD"))
                {
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = MerchantID;
                    info.Merchant_password = MerchantPassword;
                    info.Receiver_email = MerchantEmail;

                    info.cur_code = "vnd";
                    info.bank_code = sBankCode;

                    info.Order_code = id.ToString();
                    info.Total_amount = total.ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toán đơn hàng ANDAM88";
                    info.return_url = currentLink + "/hoan-thanh";
                    info.cancel_url = currentLink + "/loi-thanh-toan";

                    info.Buyer_fullname = NameShip;
                    info.Buyer_email = email;
                    info.Buyer_mobile = SDT;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, FormOfPayment);

                    if (result.Error_code == "00")
                    {
                        Response.Redirect(result.Checkout_url);
                       *//* return Redirect("/hoan-thanh");*//*
                    }
                    else
                    {
                        return Redirect("/loi-thanh-toan");
                    }

                }*/
                /* string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/SendMail.html"));

                content = content.Replace("{{NameShip}}", NameShip);
                content = content.Replace("{{PhoneShip}}", SDT);
                content = content.Replace("{{MailShip}}", email);
                content = content.Replace("{{AddressShip}}", address + diachi);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new Mail().SendMail(email, "Đơn hàng mới từ ANDAM88", content);
                new Mail().SendMail(toEmail, "Đơn hàng mới từ ANDAM88", content);*/
            }
            catch (Exception ex)
            {
                string script = "<script>alert('" + ex.Message + "');</script>";

                 return Redirect("/loi-thanh-toan");
               /* return Redirect("/hoan-thanh");*/
            }
            Session[CommonConStants.CartSession] = null;
            return Redirect("/hoan-thanh");
        }

        public ActionResult PaymentSuccess()
        {
          /* var order = new Order();
            String Token = Request["token"];
            if (Token != null)
            {
                long orderID = long.Parse(Request["Order_code"]);
                RequestCheckOrder info = new RequestCheckOrder();
                info.Merchant_id = MerchantID;
                info.Merchant_password = MerchantPassword;
                info.Token = Token;
                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
                ViewBag.Message = result.errorCode + result.payerName;
                order.ID = orderID;
                order.Status = 1;
                order.Payment_Method = 1;
                new OrderDao().Update(order);

            }*/

            return View();
        }
        public ActionResult PaymentFail()
        {
            //long orderID = long.Parse(Request["Order_code"]);

            //new OrderDAO().CancelOrder(orderID);
            return View();
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