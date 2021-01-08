using PagedList;
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
            return db.Products.ToList();
        }
        public List<Product> ListAllProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListAllProductTopHot(int top)
        {
            return db.Products.Where( x => x.Status==true && x.TopHot!= null && x.TopHot>DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.ImageID = entity.ImageID;
                product.BrandID = entity.BrandID;
                product.CategoryID = entity.CategoryID;
                product.Title = entity.Title;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Price = entity.Price;
                product.MotionPrice = entity.MotionPrice;
                product.Quantity = entity.Quantity;
                product.IncludeVAT = entity.IncludeVAT;
                product.TopHot = entity.TopHot;
                product.ViewCount = entity.ViewCount;
                product.Tags = entity.Tags;
                product.Status = entity.Status;
                product.CreateDate = DateTime.Now;
                product.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Product> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public List<Brand> ListAllBrand()
        {
            return db.Brands.ToList();
        }
        public List<Category> ListAllCategory()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public List<Product_Image> ListAllImage()
        {
            return db.Product_Image.ToList();
        }
        public Product GetByID(string Name)
        {
            return db.Products.SingleOrDefault(x => x.Name == Name);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var Product = db.Products.Find(id);
                db.Products.Remove(Product);
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
            var Product = db.Products.Find(id);
            Product.Status = !Product.Status;
            db.SaveChanges();
            return Product.Status;
        }
    }
}