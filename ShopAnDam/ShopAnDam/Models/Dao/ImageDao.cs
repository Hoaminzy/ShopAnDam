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
        
        public long Insert(Image entity)
        {
            db.Images.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Image entity)
        {
            try
            {
                var img = db.Images.Find(entity.ID);
                img.Image1 = entity.Image1;
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
        public IEnumerable<Image> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Image> model = db.Images;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Image1.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public List<Product> ListAllProduct()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }

        public List<Article> ListAllArticle()
        {
            return db.Articles.Where(x => x.Status == true).ToList();
        }

        public Image GetByID(string Image1)
        {
            return db.Images.SingleOrDefault(x => x.Image1 == Image1);
        }

        public Image ViewDetail(int id)
        {
            return db.Images.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var img = db.Images.Find(id);
                db.Images.Remove(img);
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
