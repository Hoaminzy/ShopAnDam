using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class BrandController : BaseController
    {
        //BrandDao dao = new BrandDao();

        // GET: Admin/Brand
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new BrandDao();
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
            var Brand = new BrandDao().ViewDetail(id);
            return View(Brand);
        }


        [HttpPost]

        public ActionResult Create(Brand Brand)
        {
            if (ModelState.IsValid)
            {
                var dao = new BrandDao();
            

                long id = dao.Insert(Brand);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Brand");

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
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var dao = new BrandDao();
                var res = dao.Update(brand);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Brand");
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
            new BrandDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public JsonResult ChangeBrand(long id)
        //{
        //    var result = new BrandDao().ChangeStatus(id);
        //    return Json(new
        //    {
        //        status = result
        //    }); ;
        //}

    }
}