using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;
using ShopAnDam.Common;

namespace ShopAnDam.Models.Dao
{
    public class CategoryDao
    {
        AnDamDBContext db = null;
        public CategoryDao()
        {
            db = new AnDamDBContext();
        }
        public List<Category> getAll()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.CreateDate).ToList();
        }

        
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTilte = entity.MetaTilte;
                category.DisplayOrder = entity.DisplayOrder;
                category.SeoTitle = entity.SeoTitle;
                category.Status = entity.Status;
                category.CreateDate = DateTime.Now;
                category.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Category> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTilte.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public List<Image> ListAllImage()
        {
            return db.Images.ToList();
        }
        public Category GetByID(string Name)
        {
            return db.Categories.SingleOrDefault(x => x.Name == Name);
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }
   

        public bool Delete(int id)
        {
            try
            {
                var Category = db.Categories.Find(id);
                db.Categories.Remove(Category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }

}


