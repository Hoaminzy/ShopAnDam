using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class ArticleDao
    {

        AnDamDBContext db = null;
        public ArticleDao()
        {
            db = new AnDamDBContext();
        }
        public List<Article> List(int top)
        {
            return db.Articles.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).Take(3).ToList();
        }

        public long Insert(Article entity)
        {
            db.Articles.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Article entity)
        {
            try
            {
                var article = db.Articles.Find(entity.ID);
                article.TopicID = entity.TopicID;
                article.ImageID = entity.ImageID;
                article.Name = entity.Name;
                article.MetaTitle = entity.MetaTitle;
                article.Title = entity.Title;
                article.MetaTitle = entity.MetaTitle;
                article.Description = entity.Description;
                article.Content = entity.Content;
                article.ViewCount = entity.ViewCount;
                article.Status = entity.Status;
                article.CreateDate = DateTime.Now;
                article.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Article> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Article> model = db.Articles;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public List<Topic> ListAllTopic()
        {
            return db.Topics.Where(x => x.Status == true).ToList();
        }

        public List<Product_Image> ListAllImage()
        {
            return db.Product_Image.ToList();
        }
        public Article GetByID(string Name)
        {
            return db.Articles.SingleOrDefault(x => x.Name == Name);
        }

        public Article ViewDetail(int id)
        {
            return db.Articles.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var Article = db.Articles.Find(id);
                db.Articles.Remove(Article);
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
            var Article = db.Articles.Find(id);
            Article.Status = !Article.Status;
            db.SaveChanges();
            return Article.Status;
        }
    }
}