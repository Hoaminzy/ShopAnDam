﻿using PagedList;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
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
            return db.Products.Where(x => x.Status==true).OrderByDescending(x=> x.Name).ToList();
        }
        public List<ProductViewmodel> ListAll()
        {
            var model = from v in db.Products
                        join ca in db.Categories on v.CategoryID equals ca.ID
                        join b in db.Brands on v.BrandID equals b.ID
                        select new ProductViewmodel()
                        {
                            ID = v.ID,
                            Name = v.Name,
                            Price = v.Price,
                            Quantity = v.Quantity,
                            HinhAnh = v.image,
                            MotiPrice = v.MotionPrice,
                            MetaTitle = v.MetaTitle,
                            CreateDate = v.CreateDate,
                            Status = v.Status,
                            CateName = ca.Name,
                            CateTiTle = ca.MetaTilte
                        };

            return model.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<ProductViewmodel> ListAllProduct(int top)
        {
           //List return db.Products.Where(x => x.Status== true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
          var model = from v in db.Products
                        join ca in db.Categories on v.CategoryID equals ca.ID
                        join b in db.Brands on v.BrandID equals b.ID
                        select new ProductViewmodel()
                        {
                            ID = v.ID,
                            Name = v.Name,
                            Price = v.Price,
                            Quantity = v.Quantity,
                            HinhAnh = v.image,
                            MotiPrice = v.MotionPrice,
                            MetaTitle = v.MetaTitle,
                            CreateDate = v.CreateDate,
                            Status = v.Status,
                            CateName = ca.Name,
                            CateTiTle = ca.MetaTilte
                        };

            return model.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
     
        }
        public List<ProductViewmodel> ListAllProductTopHot(int top)
        {
            var model = from v in db.Products
                        join ca in db.Categories on v.CategoryID equals ca.ID
                        join b in db.Brands on v.BrandID equals b.ID
                        select new ProductViewmodel()
                        {
                            ID = v.ID,
                            Name = v.Name,
                            Price = v.Price,
                            HinhAnh = v.image,
                            Quantity = v.Quantity,
                            MotiPrice = v.MotionPrice,
                            MetaTitle = v.MetaTitle,
                            TopHot = v.TopHot,
                            CreateDate = v.CreateDate,
                            Status = v.Status,
                            CateName = ca.Name,
                            CateTiTle = ca.MetaTilte
                        };

            return model.Where( x => x.Status==true && x.TopHot!= null && x.TopHot>DateTime.Now &&(x.MotiPrice !=null ||x.MotiPrice !=0)).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<ProductViewmodel> ListByCategoryByID( int CateID )
        {
            //return db.Products.Where(x => x.CategoryID == CateID).ToList();
            IQueryable<ProductViewmodel> model = from v in db.Products
                        join ca in db.Categories on v.CategoryID equals ca.ID
                        where v.CategoryID == CateID
                        join i in db.Images on v.ID equals i.ProductID
                        join b in db.Brands on v.BrandID equals b.ID
                        select new ProductViewmodel()
                        {
                            ID = v.ID,
                            Name = v.Name,
                            Image = i.Image1,
                            Price = v.Price,
                            HinhAnh = v.image,
                            Quantity = v.Quantity,
                            MotiPrice = v.MotionPrice,
                            MetaTitle = v.MetaTitle,
                            TopHot = v.TopHot,
                            Status = v.Status,
                            CreateDate = v.CreateDate,
                            CateName = ca.Name,
                            CateTiTle = ca.MetaTilte
                        };
                return model.Where(x => x.Status==true).ToList();
        }
        public long Insert(Product entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.ViewCount = 0;
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
                product.BrandID = entity.BrandID;
                product.CategoryID = entity.CategoryID;
                product.Title = entity.Title;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Price = entity.Price;
                product.image = entity.image;
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
        public List<Image> ListAllImage()
        {
            return db.Images.ToList();
        }


        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword) && x.Status == true).Select(x => x.Name).ToList();
        }
        public List<ProductViewmodel> Search(string keyword, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword) && x.Status == true).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.Products
                         join b in db.Categories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             Price = a.Price,
                             HinhAnh = a.image,
                             Quantity = a.Quantity,
                             MotiPrice = a.MotionPrice,
                             MetaTitle = a.MetaTitle,
                             TopHot = a.TopHot,
                             Status = a.Status,
                             CreateDate = a.CreateDate,
                             CateName = b.Name,
                             CateTiTle = b.MetaTilte
                         })
                         .AsEnumerable().Select(x => new ProductViewmodel()
                         {
                             Quantity = x.Quantity,

                             CateName = x.CateName,
                             CateTiTle = x.CateTiTle,
                             CreateDate = x.CreateDate,
                             ID = x.ID,
                             Image = x.HinhAnh,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             MotiPrice = x.MotiPrice
                         });
            model.OrderByDescending(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public Product GetByID(string Name)
        {
            return db.Products.SingleOrDefault(x => x.Name == Name);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
        public int CountProduct()
        {
            return db.Products.Count(x => x.Status == true);
        }
        public ProductViewmodel ViewProductDetail(int ProductID)// lấy thông tin sản phẩm theo id
        {
            IQueryable<ProductViewmodel> model = from v in db.Products
                        join ca in db.Categories on v.CategoryID equals ca.ID
                        where v.ID == ProductID
                        join i in db.Images on v.ID equals i.ProductID
                        join b in db.Brands on v.BrandID equals b.ID
                        join c in db.Reviews on v.ID equals c.ProductID
                        select new ProductViewmodel()
                        {
                            ID = v.ID,
                            Name = v.Name,
                            Title = v.Title,
                            Description= v.Description,
                            Image = i.Image1,
                            Price = v.Price,
                            MotiPrice = v.MotionPrice,
                            MetaTitle = v.MetaTitle,
                            TopHot = v.TopHot,
                            Status = v.Status,
                            CreateDate = v.CreateDate,
                            CateName = ca.Name,
                            CateTiTle = ca.MetaTilte,
                            Comment = c.comment
                            
                            
                        };
            return (ProductViewmodel)model;
        }

      
       
        public bool Delete(int id)
        {
            try
            {
                var pro = db.Products.Find(id);
                db.Products.Remove(pro);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public bool AddQuantity(int id, int? quantity)
        {
            try
            {
                var product = db.Products.Find(id);
                product.Quantity = product.Quantity + quantity;
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
            var pro = db.Products.Find(id);
            pro.Status = !pro.Status;
            db.SaveChanges();
            return pro.Status;
        }

        public bool CheckCode(string code)
        {
            return db.Products.Count(x => x.Code == code) > 0;
        }
    }
}