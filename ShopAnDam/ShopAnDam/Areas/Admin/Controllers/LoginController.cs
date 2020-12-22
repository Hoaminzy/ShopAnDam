using ShopAnDam.Areas.Admin.Code;
using ShopAnDam.Areas.Admin.Model;
using ShopAnDam.Models;
using ShopAnDam.Models.Dao;
using ShopAnDam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Chống request và respone liên tục
        public ActionResult  Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName,Encryptor.MD5Hash(model.PassWord), true);
                if (result==1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID =user.ID;
                    Session.Add(CommonConStants.USER_SESSION, userSession);
                    return RedirectToAction("Index","Home");
                }else if (result == 0)
                {

                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }else if(result == -1)
                {

                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if (result == -3)
                {

                    ModelState.AddModelError("", "Tài khoản không được phép đăng nhập tại đây!");
                }
                else
                {
                    ModelState.AddModelError("", "Xin mời đăng nhập lại!");
                }
            }
            return View("Index");
           


            //var result = new UserModel().Login(model.UserName, model.PassWord);
            //if (result && ModelState.IsValid)
            //{
            //    SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            //}
            //return View(model);

            //if (CustomMemberShipProvider.ValidateUser(model.UserName, model.PassWord) && ModelState.IsValid)
            //{
            //    SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            //}
            //return View(model);

        }

    }
}

    //< authentication mode = "Forms" >
 
    //   < forms loginUrl = "~/Admin/Login" timeout = "2880" />
    
    //    </ authentication >
    
    //    < membership defaultProvider = "CustomMemberShipProvider" >
     
    //       < providers >
     
    //         < add name = "CustomMemberShipProvider" type = "ShopAnDam.Areas.Code.CustomMemberShipProvider" connectionStringName = "AnDamDBContext"  enablePasswordRetrieval = "false" enablePasswordReset = "true" requiresQuestionAnswer = "false" requiresUniqueEmail = "false" maxInvalPasswprdAttempts = "3" minRequiresPasswordLength = "4" minRequiredMonalphanumbericCharacters = "0" passwordAttempWindow = "10" ApplicationName = "ShopAnDam" />
                          
    //                            </ providers >
                          
    //                          </ membership >