using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        //SlideDao dao = new SlideDao();

        // GET: Admin/Slide
        public ActionResult Index( int page = 1, int pageSize = 5)
        {
            var dao = new SlideDao();
            var model = dao.ListAllPageList(page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Slide = new SlideDao().ViewDetail(id);
            return View(Slide);
        }


        [HttpPost]

        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();


                long id = dao.Insert(slide);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Slide");

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
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();
                var res = dao.Update(slide);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Slide");
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
            SetAlert("Xóa thành công!", "success");
            new SlideDao().Delete(id);
          
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeSlide(long id)
        {
            var result = new SlideDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

    }
}