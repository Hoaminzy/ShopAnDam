﻿using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAnDam.Common;
using PagedList;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPageList(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet] 
        public ActionResult Create()
        {
            return View();
        }
      
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        public ActionResult Create( User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encrypMd5Pass = Encryptor.MD5Hash(user.PassWord);
                user.PassWord = encrypMd5Pass;
                 
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm thành công");
                }
            }

            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.PassWord))
                {
                    var encrypMd5Pass = Encryptor.MD5Hash(user.PassWord);
                    user.PassWord = encrypMd5Pass;

                }

                var res = dao.Update(user);
                if (res)
                {
                    return RedirectToAction("Index", "User");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thành công");
                }
            }

            return View("Index");
        }
        [HttpDelete]
       public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeUser(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }
    }
}