using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class TopicController : BaseController
    {
        //TopicDao dao = new TopicDao();

        // GET: Admin/Topic
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new TopicDao();
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
            var topic = new TopicDao().ViewDetail(id);
            return View(topic);
        }


        [HttpPost]

        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                var dao = new TopicDao();
                topic.CreateDate = DateTime.Now;

                long id = dao.Insert(topic);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Topic");

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
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                var dao = new TopicDao();
                topic.CreateDate = DateTime.Now;
                var res = dao.Update(topic);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Topic");
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
            new TopicDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeTopic(long id)
        {
            var result = new TopicDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

    }
}