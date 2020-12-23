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
    public class BrandDao
    {
        AnDamDBContext db = null;
        public BrandDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(Brand entity)
        {
            db.Brands.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Brand entity)
        {
            try
            {
                var Brand = db.Brands.Find(entity.ID);    
                    Brand.Name= entity.Name;
                Brand.Logo = entity.Logo;
                Brand.CreateDate = DateTime.Now;
                Brand.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Brand> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Brand> model = db.Brands;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString) );
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public Brand GetByID(string Name)
        {
            return db.Brands.SingleOrDefault(x => x.Name == Name);
        }

        public Brand ViewDetail(int id)
        {
            return db.Brands.Find(id);
        }
     

        public bool Delete(int id)
        {
            try
            {
                var Brand = db.Brands.Find(id);
                db.Brands.Remove(Brand);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool ChangeStatus(long id)
        //{
        //    var Brand = db.Brands.Find(id);
        //    Brand.Status = !Brand.Status;
        //    db.SaveChanges();
        //    return Brand.Status;
        //}
    }

}
