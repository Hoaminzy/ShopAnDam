using ShopAnDam.Common;
using ShopAnDam.Models.Dao;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopAnDam.Areas.Admin.Controllers
{
    public class GoodController : BaseController
    {
        // GET: Admin/Good
        //đơn nhập hàng
       
        private const string SESSION_GOOD = "SESSION_GOOD";
        public ActionResult Index(string SearchSanPham, int page = 1, int pagesize = 5)
        {
            var dao = new GoodDao();
            var model = dao.ListAllPaging(SearchSanPham, page, pagesize);
            ViewBag.SearchSanPham = SearchSanPham;
            return View(model);
        }

        public ActionResult Details( int page = 1, int pagesize = 10)
        {
            int totalRecord = 0;
            var dao = new GoodDao();
            var model = dao.ListAllGood(ref totalRecord, page, pagesize);

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

        public ActionResult AddSP(int ProductID, int soluong)
        {
            var product = new ProductDao().ViewDetail(ProductID);
            var productsession = Session[SESSION_GOOD];
            if (productsession != null)
            {
                var productlist = (List<GoodViewModel>)productsession;
                if (productlist.Exists(x => x.product.ID == ProductID))
                {
                    foreach (var item in productlist)
                    {
                        if (item.product.ID == ProductID)
                        {
                            item.QuantityYC += soluong;
                        }
                    }
                }
                else
                {
                    var productItem = new GoodViewModel();
                    productItem.product = product;
                    productItem.QuantityYC = soluong;
                    productlist.Add(productItem);
                }
                // gán vào sesion
                Session[SESSION_GOOD] = productlist;
            }
            else
            {
                //tạo mới đối tượng cart item
                var productItem = new GoodViewModel();
                productItem.product = product;
                productItem.QuantityYC = soluong;
                var productlist = new List<GoodViewModel>();
                productlist.Add(productItem);
                // gán vào sesion
                Session[SESSION_GOOD] = productlist;
            }
            //setViewBagNCC();
            return Redirect("/Admin/Good/Create");
        }
        public JsonResult Update(String productModel)
        {
            var jsonProduct = new JavaScriptSerializer().Deserialize<List<GoodViewModel>>(productModel);
            var sessionProduct = (List<GoodViewModel>)Session[SESSION_GOOD];
            foreach (var item in sessionProduct)
            {
                var jsonItem = jsonProduct.SingleOrDefault(x => x.product.ID == item.product.ID);
                if (jsonItem != null)
                {
                    item.QuantityYC = jsonItem.QuantityYC;
                }
            }
            Session[SESSION_GOOD] = sessionProduct;
            return Json(new
            {
                status = true
            });
        }
        public void setViewBagSupply(long? selectedID = null)
        {
            var dao = new SupplyDao();
            ViewBag.SupplyID = dao.ListAll();
        }
        public void setViewBagSanPham(long? selectedID = null)
        {
            var dao = new ProductDao();
            ViewBag.ProductID = dao.List();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var productsession = Session[SESSION_GOOD];
            var productlist = new List<GoodViewModel>();
            if (productsession != null)
            {
                productlist = (List<GoodViewModel>)productsession;
            }
            setViewBagSupply();
            setViewBagSanPham();
            return View(productlist);
        }
         [HttpPost]
         public ActionResult Create(FormCollection formcollection)
         {
             var good = new Good();
            var userSession = (UserLogin)Session[CommonConStants.USER_SESSION];
            good.CreateBy = userSession.UserName;
            good.CreateDate = DateTime.Now;
            int ncc = int.Parse(formcollection["hdnNCC"]);
            good.SupplyID = ncc;
            try
             {
                 var id = new GoodDao().Insert(good);
               
                 var goodlist = (List<GoodViewModel>)Session[SESSION_GOOD];
                 var detailDao = new GoodDao();
                 var addquantity = new ProductDao();
                 foreach (var item in goodlist)
                 {
                     var goods = new Good_Detail();
                   
                     goods.ProductID = item.product.ID;
                     goods.GoodID = (int)id;
                     goods.Quantity = item.QuantityYC;
                    goods.Prices = item.Prices;
                     detailDao.InsertGoodDetail(goods);
                     addquantity.AddQuantity(item.product.ID, item.QuantityYC);
                 }
             }
             catch (Exception ex)
             {
                 return Redirect("/Admin/Good/Create");
             }
             Session[SESSION_GOOD] = null;
             return Redirect("/Admin/Good/Details");
         }
    }
}