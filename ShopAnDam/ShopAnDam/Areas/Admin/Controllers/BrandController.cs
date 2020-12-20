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
        BrandDao brandDao = new BrandDao();
 
        // GET: Admin/Brand
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new BrandDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public JsonResult List()
        {
             return Json(brandDao.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Brand emp)
        {
            long id = brandDao.Add(emp);
            if (id > 0)
            {
                SetAlert("Thêm thành công!", "success");
                return Json(brandDao.Add(emp), JsonRequestBehavior.AllowGet);
            }
            else
            {
                SetAlert("Thêm thất bại!", "error");
                return Json( JsonRequestBehavior.AllowGet);
            }
           
        }
        public JsonResult GetbyID(int ID)
        {
            var Brand = brandDao.ListAll().Find(x => x.ID.Equals(ID));
            return Json(Brand, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Brand emp)
        {
            SetAlert("Sửa thành công!", "success");
            return Json(brandDao.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            long id = brandDao.Delete(ID);
            if (id > 0)
            {
                SetAlert("Xóa thành công!", "success");
                return Json(brandDao.Delete(ID), JsonRequestBehavior.AllowGet);
            }
            else
            {
                SetAlert("Xóa thất bại!", "error");
                return Json( JsonRequestBehavior.AllowGet);
            }

        }

    }
}