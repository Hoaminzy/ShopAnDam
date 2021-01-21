using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class ReviewDao
    {
        AnDamDBContext db = null;
        public ReviewDao()
        {
            db = new AnDamDBContext();
        }

    }
}