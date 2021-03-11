using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Tên đăng nhập *")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập!")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu *")]
        [Required(ErrorMessage ="Yêu cầu nhập mật khẩu!")]
        [StringLength(20, MinimumLength =6, ErrorMessage ="Mật khẩu phải có ít nhất 6 kí tự.")]
        public string PassWord { get; set; }
        [Display(Name = "Xác nhân mật khẩu *")]
        [Required(ErrorMessage = "Yêu cầu nhập xác nhận mật khẩu!")]
        [Compare("PassWord", ErrorMessage ="Mật khẩu không khớp!")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Họ Tên *")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên.")]
        public string Name { get; set; }
        [Display(Name = "Email *")]
        [Required(ErrorMessage = "Yêu cầu nhập email.")]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại *")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        [RegularExpression(@"0[0-9\s.-]{9,13}", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string Phone { get; set; }
        [Display(Name = "Thành phố *")]
        [Required(ErrorMessage = "Yêu cầu chọn thành phố")]
        public string ProvintID { get; set; }

        [Display(Name = "Quận/Huyện *")]
        [Required(ErrorMessage = "Yêu cầu chọn quận/huyện")]
        public string DistrictID { get; set; }
        [Display(Name = "Địa chỉ cụ thể *")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string Address { get; set; }

        public DateTime? CreateDate { get; set; }
     

    }
}