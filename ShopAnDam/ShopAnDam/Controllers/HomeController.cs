﻿using ShopAnDam.Models;
using ShopAnDam.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.slide = new SlideDao().ListAll();
            ViewBag.product = new ProductDao().ListAllProduct(3);
            ViewBag.product1 = new ProductDao().ListAllProductTopHot(3);
            ViewBag.category = new CategoryDao().getAll();
            ViewBag.brand = new BrandDao().getall(3);
            ViewBag.article = new ArticleDao().List(3);
            ViewBag.image = new ProductDao().ListAllImage();
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().List();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult CartMini()
        {
            var cart = Session[Common.CommonConStants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}