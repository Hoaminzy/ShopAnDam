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
        public int ID { get; set; }
        public int GoodID { get; set; }

    
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }
       
    }
}