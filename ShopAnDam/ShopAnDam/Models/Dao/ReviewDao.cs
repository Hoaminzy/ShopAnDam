using PagedList;
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

        public IEnumerable<Review> ListAllPageList(string searchString, int page, int pageSize)
        {
            IQueryable<Review> model = db.Reviews;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Product.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public List<Review> ListAllRVProduct(long ProductID, int top)
        {
            return db.Reviews.Where(x => x.Status == true && x.ProductID == ProductID).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Review> ListAllRVArticle(long ArticleID, int top)
        {
            return db.Reviews.Where(x => x.Status == true && x.ArticleID == ArticleID).OrderBy(x => x.CreateDate).Take(top).ToList();
        }
        public Review getPersonByID(long id)
        {
            return db.Reviews.Find(id);
        }
        public long InsertRV(Review cmt)
        {
            db.Reviews.Add(cmt);
            db.SaveChanges();
            return cmt.ID;
        }

        public bool ChangeStatus(long id)
        {
            try
            {
                var rv = db.Reviews.Find(id);
                rv.Status = !rv.Status;
                db.SaveChanges();
                return rv.Status;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}