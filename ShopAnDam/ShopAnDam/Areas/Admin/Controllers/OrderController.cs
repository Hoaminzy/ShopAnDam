using ShopAnDam.Common;
using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string SearchHoaDon, string dateOfOrder, int page = 1, int pagesize = 4)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(SearchHoaDon, dateOfOrder, page, pagesize);
            ViewBag.SearchHoaDon = SearchHoaDon;
            ViewBag.dateOfOrder = dateOfOrder;
            return View(model);
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
        public void paymentStatus(int? selectedID = null)
        {
            var dao = new PaymentStatusDao();
            ViewBag.Status = new SelectList(dao.ListAllPayment(2), "ID", "Name", selectedID);
        }
        public ActionResult Edit(int id)
        {
            var dao = new OrderDao();
            var order = dao.getById(id);

            paymentStatus(order.Status);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[ShopAnDam.Common.CommonConStants.USER_SESSION];
                model.NameShip = session.UserName;
                //var result = dao.Edit(model);
                new OrderDao().Update(model);
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }

            paymentStatus(model.Status);

            return View();
        }


    }
}