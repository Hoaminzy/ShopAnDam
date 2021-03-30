using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Areas.Admin.Controllers
{
    //[Authorize]
    public class HomeController :BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {

            var productDao = new ProductDao();
            var articleDao = new ArticleDao();
            var orderDao = new OrderDao();
            var orderDetailDao = new OrderDetailDao();
            ViewBag.CountProduct = productDao.CountProduct();
            ViewBag.CountContent = articleDao.CountArticle();
            ViewBag.CountPendingOrders = orderDao.CountPendingOrders();
            ViewBag.SumOrder = orderDetailDao.SumOrder();
            return View();




        }


    }
}