using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class SlideDao
    {

        AnDamDBContext db = null;
        public SlideDao()
        {
            db = new AnDamDBContext();
        }
        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status==true).OrderBy(x => x.DisplayOrder).ToList();
        }

    }
}