using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        //ProductDao dao = new ProductDao();

        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductDao();
            ViewBag.BrandID = new SelectList(dao.ListAllBrand(), "ID", "Name", selectedId);
            ViewBag.CategoryID = new SelectList(dao.ListAllCategory(), "ID", "Name", selectedId);          

        }
        [HttpGet]
        public ActionResult View(int id)
        {
            var dao = new ProductDao().ViewDetail(id);
            return View(dao);
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
          
            var Product = new ProductDao().ViewDetail(id);
            SetViewBag();
            return View(Product);
        }


        [HttpPost]
        public ActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                Product.CreateDate = DateTime.Now;

                long id = dao.Insert(Product);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Product");

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
        public ActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                Product.CreateDate = DateTime.Now;
                var res = dao.Update(Product);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Product");
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
            new ProductDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeProduct(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); 
        }

    }
}