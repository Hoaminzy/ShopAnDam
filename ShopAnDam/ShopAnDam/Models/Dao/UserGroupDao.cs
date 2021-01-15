using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class UserGroupDao
    {
        AnDamDBContext db = null;
        public UserGroupDao()
        {
            db = new AnDamDBContext();
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }
        public List<Brand> ListAllBrand()
        {
            return db.Brands.ToList();
        }
        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status==true).ToList();
        }

        public List<Image> ListAllImage()
        {
            return db.Images.ToList();
        }
    }
}