using PagedList;
using ShopAnDam.Common;
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
        public IEnumerable<ArticleViewModel> ListAllPading(int page, int pageSize)
        {
            IQueryable<ArticleViewModel> model = from a in db.Articles
                        join b in db.Topics on a.TopicID equals b.ID
                        //join i in db.Images on a.ID equals i.ArticleID
                        //join d in db.Users on a.UserID equals d.ID
                        //join e in db.Customers on a.CustomerID equals e.ID
                        //join f in db.Reviews on a.ID equals f.ArticleID
            select new ArticleViewModel()
            {
                ID = a.ID,
                Name = a.Name,
                MetaTitle = a.MetaTitle,
                Title = a.Title,
                Images1 = a.Images,
                //Description = a.Description,
                //Content = a.Content,
                Status = a.Status,
                CreateDate = a.CreateDate,
                TopicName = b.Name,
                //HinhAnh = i.Image1,
                //CustomerName = e.Name,
                //UserName = d.Name,
                //Comment = f.comment
            };
            return model.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Article> ListAllByPage()
        {
            IQueryable<Article> model = db.Articles;
            return model.Where(x => x.Status == true).OrderByDescending(x => x.CreateDate).ToList();

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

        public IEnumerable<Article> ListAllByUser(string username, long ID/*, int page, int pageSize*/)
        {
            /* var model = (from a in db.Articles
                          join b in db.Customers
                          on a.CreateBy equals b.UserName
                          where b.UserName == username  
                          select new
                          {
                              Title = a.Title,
                              Topic = a.TopicID,
                              MetaTitle = a.MetaTitle,
                              Image = a.Images,
                              Description = a.Description,
                              Content = a.Content,
                              CreateBy = a.CreateBy,
                              CreateDate = a.CreateDate,
                              ID = a.ID

                          }).AsEnumerable().Select(x => new Article()
                          {
                              Title = x.Title,
                              TopicID = x.Topic,
                              MetaTitle = x.MetaTitle,
                              Images = x.Image,
                              Description = x.Description,
                              Content = x.Content,
                              CreateBy = x.CreateBy,
                              CreateDate = x.CreateDate,
                              ID = x.ID
                          });*/
            IEnumerable<Article> model = db.Articles;
            return model.Where(x => x.CustomerID == ID).OrderByDescending(x => x.CreateDate).ToList(/*page, pageSize*/);
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