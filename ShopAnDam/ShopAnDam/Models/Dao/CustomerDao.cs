using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class CustomerDao
    {
        AnDamDBContext db = null;
        public CustomerDao()
        {
            db = new AnDamDBContext();
        }
        public IEnumerable<Customer> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Customer> model = db.Customers;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString) );
            }
            return model.OrderByDescending(x => x.Createdate).ToPagedList(page, pageSize);
        }

        public Customer ViewDetais(int id)
        {
            return db.Customers.Find(id);
        }
        public Customer GetByID(string Name)
        {
            return db.Customers.SingleOrDefault(x => x.Name == Name);
        }
    }
}