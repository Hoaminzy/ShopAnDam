using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Areas.Admin.Model
{

    public class LoginModel
    {
        [Required (ErrorMessage ="Mời nhập tên đăng nhập")]
        public String UserName { set; get; }
        [Required (ErrorMessage ="Mời nhập mật khẩu")]
        public String PassWord { set; get; }
        public bool Rememberme { set; get; }
    }
}