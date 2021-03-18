using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models
{
    [Serializable]
    public class CartItem
    {
        public Customer customer { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Thành phố *")]
        [Required(ErrorMessage = "Yêu cầu chọn thành phố")]
        public string ProvintID { get; set; }

        [Display(Name = "Quận/Huyện *")]
        [Required(ErrorMessage = "Yêu cầu chọn quận/huyện")]
        public string DistrictID { get; set; }
    }
}