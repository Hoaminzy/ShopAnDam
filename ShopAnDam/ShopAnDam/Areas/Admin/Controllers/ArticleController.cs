using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        //ArticleDao dao = new ArticleDao();

        // GET: Admin/Article
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ArticleDao();
            var model = dao.ListAllPageList(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ArticleDao();
            ViewBag.TopicID = new SelectList(dao.ListAllTopic(), "ID", "Name", selectedId);
            ViewBag.ImageID = new SelectList(dao.ListAllImage(), "ID", "Image", selectedId);

        }
        [HttpGet]
        public ActionResult View(int id)
        {
            var dao = new ArticleDao().ViewDetail(id);
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

            var Article = new ArticleDao().ViewDetail(id);
            SetViewBag();
            return View(Article);
        }


        [HttpPost]

        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDao();
                article.CreateDate = DateTime.Now;

                long id = dao.Insert(article);
                if (id > 0)
                {
                    SetAlert("Thêm thành công!", "success");
                    return RedirectToAction("Index", "Article");

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
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                var dao = new ArticleDao();
                article.CreateDate = DateTime.Now;
                var res = dao.Update(article);
                if (res)
                {
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Index", "Article");
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
            new ArticleDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeArticle(long id)
        {
            var result = new ArticleDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            }); ;
        }

    }
}