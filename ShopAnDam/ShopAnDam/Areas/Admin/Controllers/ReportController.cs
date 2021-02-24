using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 4)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPageList(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }

        public ActionResult CanceledOrder(string SearchSanPham, int page = 1, int pagesize = 5)
        {
            var dao = new OrderDao();
            var model = dao.ListAllCancelOrderPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }
        public ActionResult ReportOrder(string SearchSanPham, int page = 1, int pagesize = 5)
        {
            var dao = new OrderDao();
            var model = dao.ListAllOrderPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }

        public ActionResult Sales(int page = 1, int pagesize = 5)//Thống kê danh thu bán hàng
        {
            int totalRecord = 0;
            var dao = new OrderDao();
            var model = dao.ListDetailByOrder(ref totalRecord, page, pagesize);

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
    }
}