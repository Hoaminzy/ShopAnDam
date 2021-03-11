using PagedList;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class GoodDao
    {

        AnDamDBContext db = null;
        public GoodDao()
        {
            db = new AnDamDBContext();
        }

        public IEnumerable<Goods_Detail> ListAllPaging(string SearchSanPham, int page, int pageSize)
        {
            IQueryable<Goods_Detail> model = db.Good_Detail;
            if (!string.IsNullOrEmpty(SearchSanPham))
            {
                model = model.Where(x => x.GoodID.ToString().Contains(SearchSanPham));
            }
            return model.OrderByDescending(x => x.GoodID).ToPagedList(page, pageSize);
        }

        public IEnumerable<GoodViewModel> ListAllGood(ref int totalRecord, int page = 1, int pageSize = 10)
        {
            totalRecord = db.Goods.Count();
            var model = (from a in db.Supplies
                         join b in db.Goods on a.ID equals b.SupplyID
                         join c in db.Good_Detail on b.ID equals c.GoodID
                         join d in db.Products on b.ProductID equals d.ID
                         select new
                         {
                             GoodID = b.ID,
                             SupplyName = a.Name,
                             ProductName = d.Name,
                             QuantityYC = b.Quantity
                         })
                         .AsEnumerable().Select(x => new GoodViewModel()
                         {
                             GoodID = x.GoodID,
                             NameProduct = x.ProductName,
                             NameSupply = x.SupplyName,
                             QuantityYC = x.QuantityYC,

                         });
            model.OrderByDescending(x => x.NameProduct).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public long Insert(Good good)
        {
            db.Goods.Add(good);
            db.SaveChanges();
            return good.ID;
        }
    }
}