using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class SlideDao
    {

        AnDamDBContext db = null;
        public SlideDao()
        {
            db = new AnDamDBContext();
        }
        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status==true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public long Insert(Slide entity)
        {
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
       
        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Link = entity.Link;
                slide.Description = entity.Description;
                slide.Status = entity.Status;
                slide.CreateDate = DateTime.Now;
                slide.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Slide> ListAllPageList( int page, int pageSize)
        {

          
            return db.Slides.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
  
        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var Slide = db.Slides.Find(id);
                db.Slides.Remove(Slide);
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
            var Slide = db.Slides.Find(id);
            Slide.Status = !Slide.Status;
            db.SaveChanges();
            return Slide.Status;
        }
    }
}