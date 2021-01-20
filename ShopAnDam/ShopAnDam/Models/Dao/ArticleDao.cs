using PagedList;
using ShopAnDam.Models.Framework;
using ShopAnDam.Models.ViewModel;
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
            return db.Articles.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<ArticleViewModel> ListAll()
        {
            var model = from a in db.Articles
                        join b in db.Topics on a.TopicID equals b.ID
                        join i in db.Images on a.ID equals i.ArticleID
                        join d in db.Users on a.UserID equals d.ID
                        join e in db.Customers on a.CustomerID equals e.ID
                        join f in db.Reviews on a.ID equals f.ArticleID
            select new ArticleViewModel()
            {
                ID = a.ID,
                Name = a.Name,
                MetaTitle = a.MetaTitle,
                Title = a.Title,
                Images1 = a.Images,
                Description = a.Description,
                Content = a.Content,
                status = a.Status,
                CreateDate = a.CreateDate,
                TopicName = b.Name,
                HinhAnh = i.Image1,
                CustomerName = e.Name,
                UserName = d.Name,
                comment = f.comment
            };
            return model.Where(x => x.status == true).OrderByDescending(x => x.CreateDate).ToList();
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
                article.Name = entity.Name;
                article.MetaTitle = entity.MetaTitle;
                article.Title = entity.Title;
                article.Images = entity.Images;
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