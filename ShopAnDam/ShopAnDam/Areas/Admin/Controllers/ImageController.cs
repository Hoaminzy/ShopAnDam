using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
  
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ImageDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ImageDao();
            ViewBag.ProductID = new SelectList(dao.ListAllProduct(), "ID", "Name", selectedId);
            ViewBag.ArticleID = new SelectList(dao.ListAllArticle(), "ID", "Name", selectedId);

        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Image = new ImageDao().ViewDetail(id);
            SetViewBag();

            return View(Image);
        }


        [HttpPost]

        public ActionResult Create(Image img)
        {
            if (ModelState.IsValid)
            {
                var dao = new ImageDao();
                img.CreateDate = DateTime.Now;

                long id = dao.Insert(img);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Image");

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
        public ActionResult Edit(Image img)
        {
            if (ModelState.IsValid)
            {
                var dao = new ImageDao();
                img.CreateDate = DateTime.Now;
                var res = dao.Update(img);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Image");
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
            new ImageDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        
    }
}