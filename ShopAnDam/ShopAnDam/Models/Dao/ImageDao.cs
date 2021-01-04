using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class ImageDao
    {
        AnDamDBContext db = null;
        public ImageDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(Product_Image entity)
        {
            db.Product_Image.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Product_Image entity)
        {
            try
            {
                var img = db.Product_Image.Find(entity.ID);
                img.Image = entity.Image;
                img.CreateDate = DateTime.Now;
                img.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Product_Image> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Product_Image> model = db.Product_Image;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Image.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public Product_Image GetByID(string Image)
        {
            return db.Product_Image.SingleOrDefault(x => x.Image == Image);
        }

        public Product_Image ViewDetail(int id)
        {
            return db.Product_Image.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var img = db.Product_Image.Find(id);
                db.Product_Image.Remove(img);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
