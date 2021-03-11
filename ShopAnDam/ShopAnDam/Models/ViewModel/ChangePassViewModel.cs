using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class ChangePassViewModel
    {
        [Key]
        public long ID { get; set; }
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Hãy nhập Mật khẩu cũ")]
        public string OldPassword { get; set; }
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        public string NewPassword { get; set; }
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu mới")]
        [Compare("sNewPassword", ErrorMessage = "Mật khẩu nhập lại không trùng khớp")]
        public string NewPasswordRe { get; set; }
    }
}