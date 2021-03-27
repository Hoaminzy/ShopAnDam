using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Admin/Review
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ReviewDao();
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
        public JsonResult ChangeStatus(long id)
        {
            var result = new ReviewDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}