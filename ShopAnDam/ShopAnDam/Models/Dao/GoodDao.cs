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

        public IEnumerable<GoodViewModel> ListAllPaging(string Searchncc, int page, int pageSize)
        {
            // IQueryable<Good> model = db.Goods;
            var model = (from a in db.Goods
                         join b in db.Supplies on a.SupplyID equals b.ID
                         select new
                         {
                             GoodID = a.ID,
                             SupplyName = b.Name,
                             CreateDate = a.CreateDate
                         })
                        .AsEnumerable().Select(x => new GoodViewModel()
                        {
                            GoodID = x.GoodID,
                            NameSupply = x.SupplyName,
                            CreateDate = x.CreateDate
                        });

            if (!string.IsNullOrEmpty(Searchncc))
            {
                model = model.Where(x => x.SupplyID.ToString().Contains(Searchncc));
            }
            return model.OrderByDescending(x => x.GoodID).ToPagedList(page, pageSize);
        }

        public IEnumerable<GoodViewModel> ListAllGood(/*ref int totalRecord, int page = 1, int pageSize = 10*/)
        {
           /* totalRecord = db.Goods.Count();*/
            var model = (from a in db.Supplies
                         join b in db.Goods on a.ID equals b.SupplyID
                         join c in db.Good_Detail on b.ID equals c.GoodID
                         join d in db.Products on c.ProductID equals d.ID
                         select new
                         {
                             GoodID = b.ID,
                             SupplyName = a.Name,
                             ProductName = d.Name,
                             QuantityYC = c.Quantity
                         })
                         .AsEnumerable().Select(x => new GoodViewModel()
                         {
                             GoodID = x.GoodID,
                             NameProduct = x.ProductName,
                             NameSupply = x.SupplyName,
                             QuantityYC = x.QuantityYC,

                         });
          
            return model.OrderByDescending(x => x.NameProduct).ToList();
        }
        public int Insert(Good good)
        {
            db.Goods.Add(good);
            db.SaveChanges();
            return good.ID;
        }
        public bool InsertGoodDetail(Good_Detail goods)
        {
            try
            {
                db.Good_Detail.Add(goods);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
         
        }
    }
}