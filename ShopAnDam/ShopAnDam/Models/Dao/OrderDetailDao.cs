using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class OrderDetailDao
    {
        AnDamDBContext db = null;
        public OrderDetailDao()
        {
            db = new AnDamDBContext();
        }
        public bool Insert(Order_Detail detail)
        {
            try
            {
                db.Order_Detail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public string SumOrder()
        {
            return db.Order_Detail.Sum(x => x.Price * x.Quantity).GetValueOrDefault().ToString("N0");
        }
    }
}