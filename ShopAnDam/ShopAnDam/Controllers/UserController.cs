using BotDetect.Web.Mvc;
using ShopAnDam.Common;
using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;


namespace ShopAnDam.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConStants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
       // [ValidateAntiForgeryToken] //Chống request và respone liên tục
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new CustomerLogin();
                    userSession.UserName = user.UserName;
                    userSession.CustomerID = user.ID;
                    userSession.PassWord = user.PassWord;
                    userSession.Name = user.Name;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    userSession.Address = user.Address;
                    //userSession.ProvinID = user.ProvinID;
                  //  userSession.DistricID = user.DistricID;
                    Session.Add(CommonConStants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View(model);
        }

            [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterViewModel model, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                if (dao.CheckUsername(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (dao.CheckEmail(model.Phone))
                {
                    ModelState.AddModelError("", "Số điện thoại đã được sử dụng!");
                }
                else
                {
                    var TenTinhThanh = formcollection["hdnTenTinhThanh"];
                    var TenQuanHuyen = formcollection["hdnTenQuanHuyen"];

                    var customer = new Customer();
                    customer.Name = model.Name;
                    customer.UserName = model.UserName;
                    customer.PassWord = Encryptor.MD5Hash(model.PassWord);
                    customer.Phone = model.Phone;
                    customer.Email = model.Email;
                    customer.Address = model.Address + "," + TenQuanHuyen + "," + TenTinhThanh;
                    if (!string.IsNullOrEmpty(model.ProvintID))
                    {
                        customer.ProvinID = int.Parse(model.ProvintID);
                    }
                    if (!string.IsNullOrEmpty(model.DistrictID))
                    {
                        customer.DistricID = int.Parse(model.DistrictID);
                    }
                    customer.Createdate = DateTime.Now;
                    //  customer.Status = true;
                    var result = dao.Insert(customer);
                    if (result > 0)
                    {
                        ViewBag.Success = "Tạo tài khoản thành công!";
                        model = new RegisterViewModel();
                        RedirectToAction("User", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo tài khoản không thành công!");
                    }
                }
                
            }
            return View(model);
        }

        public  JsonResult LoadProvince()
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
