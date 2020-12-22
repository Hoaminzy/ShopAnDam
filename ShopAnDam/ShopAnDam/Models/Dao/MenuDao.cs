using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{

    public class MenuDao
    {

        AnDamDBContext db = null;
        public MenuDao()
        {
            db = new AnDamDBContext();
        }
        public List<Menu> List()
        {
            return db.Menus.ToList();
        }
    }
}