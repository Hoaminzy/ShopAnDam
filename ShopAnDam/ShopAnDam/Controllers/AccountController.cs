using ShopAnDam.Models.Framework;
using ShopAnDam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAnDam.Models.Dao;

namespace ShopAnDam.Controllers
{
 
    public class AccountController : Controller
    {
        AnDamDBContext db = null;
        // GET: Account
        public ActionResult Index()
        {
            var sessionUser = (CustomerLogin)Session[CommonConStants.USER_SESSION];
            if (Session[CommonConStants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            var dao = new OrderDao();
            var model = dao.getOrderByIdUser(sessionUser.CustomerID);
            return View(model);
        }
        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            if (Session[CommonConStants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            else
            {
                var acc = new CustomerDao().ViewDetail(id);
                return View(acc);
            }
        }

        [HttpPost]
        public ActionResult EditAccount(string Name, string Address, string Email, string Phone)
        {

            var user = new Customer();
            var dao = new CustomerDao();
            var session = (CustomerLogin)Session[CommonConStants.USER_SESSION];
            if (session != null)
            {
                user.UserName = session.UserName;
                user.ID = (int)session.CustomerID;
                user.Createdate = DateTime.Now;
                user.Name = Name;
                user.Email = Email;
                user.Phone = Phone;
                user.Address = Address;
                user.Status = true;
            }
            else
            {
                return Redirect("/User/Login");
            }
            var result = dao.Update(user);
            //var checkuser = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == formCollection["sTenTaiKhoan"]);

            if (result)
            {
                session.Name = Name;
                session.Email = Email;
                session.Phone = Phone;
                session.Address = Address;
                   SetAlert("Cập nhật thành công", "success");
                  ViewBag.Success = "Tạo tài khoản thành công!";

            }
            else
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            if (Session[Common.CommonConStants.USER_SESSION] == null)
            {
                return Redirect("/User/Login");
            }
            else
            {
                var acc = new CustomerDao().ViewDetail(id);
                return View(acc);
            }
          
        }
        [HttpPost]
        public ActionResult ChangePassword(string OldPassword, string NewPassword)
        {
            if (ModelState.IsValid)
            {
                var session = (CustomerLogin)Session[CommonConStants.USER_SESSION];
                var dao = new CustomerDao();
                var result = dao.ChangePassword(session.UserName, Encryptor.MD5Hash(OldPassword), Encryptor.MD5Hash(NewPassword));
                if (result == 1)
                {
                    SetAlert("Đổi mật khẩu thành công!", "success");
                }

                else
                {
                    SetAlert("Đổi mật khẩu thất bại!", "error");
                }
            }
            return View();
        }
        public ActionResult OrderDetails(int id, int page = 1, int pageSize = 10)
        {
            var order = new OrderDao().getById(id);
            ViewBag.Order = order;
            int totalRecord = 0;
            var model = new OrderDao().ListDetailByOrderID(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        [ChildActionOnly]
        public ActionResult MyArticle(/*int page = 1, int pagesize = 10*/)
        {
            if (Session[CommonConStants.USER_SESSION] == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                var dao = new ArticleDao();
                var session = (CustomerLogin)Session[CommonConStants.USER_SESSION];
                var model = dao.ListAllByUser(session.UserName, (int)session.CustomerID/*, page, pagesize*/);
             /*   int totalRecord = 0;

                ViewBag.Total = totalRecord;
                ViewBag.Page = page;
                //ViewBag.Tagid = new BaiVietDAO().getTag(tagid);
                int maxPage = 10;
                int totalPage = 0;

                totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
                ViewBag.TotalPage = totalPage;
                ViewBag.MaxPage = maxPage;
                ViewBag.First = 1;
                ViewBag.Last = totalPage;
                ViewBag.Next = page + 1;
                ViewBag.Prev = page - 1;*/
                return PartialView(model);
            }
        }

        public void setViewBag(long? selectedID = null)
        {
            var dao = new TopicDao();
            ViewBag.TopicID = new SelectList(dao.getList(), "ID", "Name", selectedID);
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            setViewBag();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateArticle(Article model)
        {
            if (ModelState.IsValid)
            {
                var session = (CustomerLogin)Session[CommonConStants.USER_SESSION];
                model.CreateBy = session.UserName;
                model.CreateDate = DateTime.Now;
                new ArticleDao().Insert(model);
                SetAlert("Thêm bài viết thành công, chờ kiểm duyệt", "success");
                return RedirectToAction("Index");
            }
            setViewBag();
            return View();
        }


        public ActionResult EditArticle(int id)
        {
            var dao = new ArticleDao();
            var article = dao.ViewDetail(id);

            setViewBag(article.TopicID);
            return View(article);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(Article model, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {
                //var dao = new BaiVietDAO();
                int TopicID = int.Parse(formcollection["hdnIDbaiviet"]);

                var session = (CustomerLogin)Session[CommonConStants.USER_SESSION];
                model.CreateBy = session.UserName;
                //var result = dao.Edit(model);
                model.CreateDate = DateTime.Now;
                model.TopicID = TopicID;

                new ArticleDao().Update(model);
                SetAlert("Sửa bài viết thành công", "success");

                return RedirectToAction("Index");
            }

            setViewBag(model.TopicID);

            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ArticleDao().Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
        //hủy hóa đơn
        public JsonResult CancelOrder(int id)
        {
            var result = new OrderDao().CancelOrder(id);
            return Json(new
            {
                status = result
            });
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

    }
}