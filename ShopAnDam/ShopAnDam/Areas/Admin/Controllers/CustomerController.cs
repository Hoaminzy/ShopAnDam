using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CustomerDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
      [HttpGet]
        public ActionResult View(int id)
        {
            var dao = new CustomerDao().ViewDetais(id);
            return View(dao);
        }
        public JsonResult ChangeCustomer(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}