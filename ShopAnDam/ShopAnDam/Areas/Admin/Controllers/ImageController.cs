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

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Image = new ImageDao().ViewDetail(id);
            return View(Image);
        }


        [HttpPost]

        public ActionResult Create(Product_Image Image)
        {
            if (ModelState.IsValid)
            {
                var dao = new ImageDao();
                Image.CreateDate = DateTime.Now;

                long id = dao.Insert(Image);
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
        public ActionResult Edit(Product_Image Image)
        {
            if (ModelState.IsValid)
            {
                var dao = new ImageDao();
                Image   .CreateDate = DateTime.Now;
                var res = dao.Update(Image);
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