using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class PaymentStatusDao
    {
        AnDamDBContext db = null;
        public PaymentStatusDao()
        {
            db = new AnDamDBContext();
        }
       public List<PaymentStatus> ListAllPayment(int top )
        {
            return db.PaymentStatus.OrderBy(x => x.ID).Take(top).ToList();
        }
    }
}