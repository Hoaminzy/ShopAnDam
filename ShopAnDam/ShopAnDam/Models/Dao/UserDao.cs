using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;
using ShopAnDam.Common;

namespace ShopAnDam.Models.Dao
{
    public class UserDao
    {
        AnDamDBContext db = null;
        public UserDao()
        {
            db = new AnDamDBContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(User entity)
        {
            try {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.PassWord))
                {
                    user.PassWord = entity.PassWord;
                }
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.CreateDate = entity.CreateDate;
                user.CreateBy = entity.CreateBy;
                db.SaveChanges();
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<User> ListAllPageList(string searchString,int page, int pageSize)
        {

           IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString)){//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public User GetByID(string UserName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == UserName);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public int Login(string UserName, string PassWord, bool isLoginAdmin =false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == UserName );
          
            if (result == null)
            {
                return 0; // Không có tài khoản
            }

            else
            {
                if(isLoginAdmin ==true  )
                {
                    if(result.GroupID == CommonConStants.ADMIN_GROUP || result.GroupID == CommonConStants.SUPPER_GROUP)
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
                    else
                    {
                        return -3;//không có quyền
                    }
                    
                }else
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
        }

        public bool Delete(int id)
        {
            try {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

      public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
                user.Status = !user.Status;
            db.SaveChanges();
                return user.Status;
            }
        }

}
       
    
