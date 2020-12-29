using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class ProductDao
    {

        AnDamDBContext db = null;
        public ProductDao()
        {
            db = new AnDamDBContext();
        }
        public List<Product> List()
        {
            return db.Products .ToList();
        }
    }
}