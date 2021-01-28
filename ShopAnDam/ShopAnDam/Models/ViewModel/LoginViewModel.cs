using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopAnDam.Models.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập *")]
        [Required(ErrorMessage = "Yêu cầu nhập tài khoản!")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu *")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu!")]
        public string PassWord { get; set; }
        public bool RememberMe {get; set;}
    }
}