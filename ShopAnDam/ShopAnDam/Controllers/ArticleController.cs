using ShopAnDam.Common;
using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var model = new ArticleDao().ListAllPading(page, pagesize);
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new ArticleDao().ViewDetail(id);
            ViewBag.ArComment = new ReviewDao().ListAllRVArticle(model.ID, 5);
            return View(model);
        }

        [HttpPost]
        public JsonResult AddComment(string comment, string ArticleID/*, string name, string email*/)
        {
            var review = new Review();
            var userSession = (CustomerLogin)Session[CommonConStants.USER_SESSION];
            int idar = int.Parse(ArticleID);
            /* review.Customer.Name = name;
             review.Customer.Email = email;*/
            review.comment = comment;
            review.ArticleID = idar;
            review.CreateDate = DateTime.Now;
            review.CreateBy = userSession.Name;
            review.Status = true;



            var id = new ReviewDao().InsertRV(review);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                //send mail

            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}