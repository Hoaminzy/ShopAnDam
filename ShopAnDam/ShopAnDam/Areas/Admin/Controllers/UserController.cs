using ShopAnDam.Models.Dao;
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
        [CheckCredentail(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPageList(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [CheckCredentail(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpGet]
        [CheckCredentail(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            SetViewBag();
            return View(user);
        }



        [HttpPost]
        [CheckCredentail(RoleID = "ADD_USER")]

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
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "User");

                }
                else
                {
                    SetAlert("Thêm thất bại!", "error");
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            return PartialView("Index");
        }

        [HttpPost]
        [CheckCredentail(RoleID = "EDIT_USER")]

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
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật thất bại!", "error");
                    ModelState.AddModelError("", "Cập nhật thất bại!");
                }
            }

            return View("Index");
        }
      

        [HttpDelete]
        [CheckCredentail(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckCredentail(RoleID = "EDIT_USER")]
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