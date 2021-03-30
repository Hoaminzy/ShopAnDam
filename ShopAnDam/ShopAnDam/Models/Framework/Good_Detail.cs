using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Framework
{
    public class Good_Detail
    {
        [Key]
      
        public int ID { get; set; }
          public int? GoodID { get; set; }
        public int? ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal? Prices { get; set; }

        public decimal? Tongtien { get; set; }
    }
}