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
    public class TopicDao
    {
        AnDamDBContext db = null;
        public TopicDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(Topic entity)
        {
            db.Topics.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Topic entity)
        {
            try
            {
                var topic = db.Topics.Find(entity.ID);
                topic.Name = entity.Name;
                topic.MetaTilte = entity.MetaTilte;
                topic.SeoTitle = entity.SeoTitle;
                topic.DisplayOrder = entity.DisplayOrder;
                topic.Status = entity.Status;
                topic.CreateDate = DateTime.Now;
                topic.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Topic> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Topic> model = db.Topics;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public Topic GetByID(string Name)
        {
            return db.Topics.SingleOrDefault(x => x.Name == Name);
        }

        public Topic ViewDetail(int id)
        {
            return db.Topics.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var topic = db.Topics.Find(id);
                db.Topics.Remove(topic);
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
            var topic = db.Topics.Find(id);
            topic.Status = !topic.Status;
            db.SaveChanges();
            return topic.Status;
        }
    }

}
