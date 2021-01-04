using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class SupplyDao
    {
        AnDamDBContext db = null;
        public SupplyDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(Supply entity)
        {
            db.Supplies.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Supply entity)
        {
            try
            {
                var supply = db.Supplies.Find(entity.ID);
                supply.Name = entity.Name;
                supply.Address = entity.Address;
                supply.Email = entity.Email;
                supply.Phone = entity.Phone;
                supply.CreateDate = DateTime.Now;
                supply.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Supply> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Supply> model = db.Supplies;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public Supply GetByID(string Name)
        {
            return db.Supplies.SingleOrDefault(x => x.Name == Name);
        }
        public Supply ViewDetail(int id)
        {
            return db.Supplies.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var supply = db.Supplies.Find(id);
                db.Supplies.Remove(supply);
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
            var supply = db.Supplies.Find(id);
            supply.Status = !supply.Status;
            db.SaveChanges();
            return supply.Status;
        }
    }
}
