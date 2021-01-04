using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class SupplyController : BaseController
    {
        SupplyDao dao = new SupplyDao();
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new SupplyDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Supply = new SupplyDao().ViewDetail(id);
            return View(Supply);
        }


        [HttpPost]

        public ActionResult Create(Supply supply)
        {
            if (ModelState.IsValid)
            {
                var dao = new SupplyDao();


                long id = dao.Insert(supply);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Supply");

                }
                else
                {
                    SetAlert("Thêm thất bại!", "error");
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Supply supply)
        {
            if (ModelState.IsValid)
            {
                var dao = new SupplyDao();
                var res = dao.Update(supply);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Supply");
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
        public ActionResult Delete(int id)
        {
            new SupplyDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeSupply(long id)
        {
            var result = new SupplyDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}

