using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class OrderViewModel
    {

        public User user { get; set; }
        public Order order { get; set; }
        public PaymentStatus paymentStatus{get; set;}
        public int ID { get; set; }
        [Display(Name = "Tên người nhận")]
        public string NameShip { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneShip { get; set; }
        [Display(Name = "Email")]
        public string MailShip { get; set; }
        [Display(Name = "Địa chỉ cụ thể")]
        public string AdressShip { get; set; }

        [Display(Name = "Tỉnh:")]
        public int? ProvintID { get; set; }

        [Display(Name = "Quận/ Huyện")]
        public int? DistrictID { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal? Price { get; set; }
       
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [Display(Name = "Trạng thái thanh toán")]
        public int? PaymentMethod  { get; set; }

        [Display(Name = "Hình thức thanh toán")]
        public string FormOfPayment { get; set; }
        
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
    }
}