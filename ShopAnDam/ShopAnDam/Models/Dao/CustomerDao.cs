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

        public long Insert(Customer entity)
        {
            db.Customers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Customer entity)
        {
            try
            {
                var customer = db.Customers.Find(entity.ID);
                customer.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.PassWord))
                {
                    customer.PassWord = entity.PassWord;
                }
                customer.Email = entity.Email;
                customer.Phone = entity.Phone;
                customer.Createdate = DateTime.Now;
                customer.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
      

        public Customer ViewDetail(int id)
        {
            return db.Customers.Find(id);
        }

    

        public Customer ViewDetais(int id)
        {
            return db.Customers.Find(id);
        }
        public Customer GetByID(string UserName)
        {
            return db.Customers.SingleOrDefault(x => x.UserName == UserName);
        }
  
        public bool Delete(int id)
        {
            try
            {
                var Customer = db.Customers.Find(id);
                db.Customers.Remove(Customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int Login(string UserName, string PassWord)
        {
            var result = db.Customers.FirstOrDefault(x => x.UserName == UserName);

            if (result == null)
            {
                return 0; // Không có tài khoản
            }   
                else
                {
                    if (result.Status == false)
                    {
                        return -1; //Tài khoản bị khóa
                    }
                    else
                    {
                        if (result.PassWord == PassWord)
                            return 1; //tài khoản đúng
                        else
                            return -2; //tài khoản sai
                    }
                }

            }


        public int ChangePassword(string username, string oldpass, string newpass)
        {

            var result = db.Customers.SingleOrDefault(x => x.UserName == username);

            if (result.PassWord == oldpass)
            {
                var user = new Customer();
                result.PassWord = newpass;
                db.SaveChanges();
                return 1;//đổi mk thành công
            }
            else
            {
                return 0; //k ko đúng
            }

        }
        public bool ChangeStatus(long id)
        {
            try
            {
                var Customer = db.Customers.Find(id);
                Customer.Status = !Customer.Status;
                db.SaveChanges();
                return Customer.Status;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckUsername(string UserName)
        {
            return db.Customers.Count(x => x.UserName == UserName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }
        public bool CheckPhone(string phone)
        {
            return db.Customers.Count(x => x.Phone == phone) > 0;
        }
   
    }
}