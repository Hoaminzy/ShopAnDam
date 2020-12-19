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
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            //var model = brandDao.ListAll().Skip((page - 1) * pageSize).Take(pageSize);
            //int totalRow = brandDao.ListAll().Count;

            return Json(brandDao.ListAll(), JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Add(Brand emp)
        {
            return Json(brandDao.Add(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var Brand = brandDao.ListAll().Find(x => x.ID.Equals(ID));
            return Json(Brand, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Brand emp)
        {
            return Json(brandDao.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(brandDao.Delete(ID), JsonRequestBehavior.AllowGet);
        }

    }
}