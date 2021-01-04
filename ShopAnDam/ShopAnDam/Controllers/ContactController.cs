using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActive();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.Content = content;
            feedback.CreateDate = DateTime.Now;
            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true,
                }) ; 
            }else
            {
                return Json(new
                {
                    status = false,
            }) ;
            }
        }

    }
}