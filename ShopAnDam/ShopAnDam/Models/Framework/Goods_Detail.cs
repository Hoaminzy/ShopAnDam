using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Framework
{
    public class Goods_Detail
    {
        [Key]
        public int GoodID { get; set; }

        public int? ProductID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? Quantity { get; set; }

        public decimal? Prices { get; set; }
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }
       
    }
}