using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models
{
   
    public class UserModel
    {
        private AnDamDBContext context = null;
        public UserModel()
        {
            context = new AnDamDBContext();
        }
        public bool Login(string UserName, string PassWord)
        {
            {
                object[] sqlParams =
                {
                    new SqlParameter("@UserName", UserName),
                    new SqlParameter("@PassWord", PassWord),
                };
                var res = context.Database.SqlQuery<bool>("pr_user_login @UserName, @PassWord",sqlParams).SingleOrDefault();
                return res;
            }
        
    }

    
    }
}