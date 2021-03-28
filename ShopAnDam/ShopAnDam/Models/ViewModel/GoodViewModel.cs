using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class GoodViewModel
    {
        public Product product { get; set; }
        public Supply supply { get; set; }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GoodID { get; set; }
        [Key]
        [Column(Order = 1)]

        public int SupplyID { get; set; }
        [Key]
        [Column(Order = 2)]

        public int ProductID { get; set; }
        public string NameSupply { get; set; }
        public string NameProduct { get; set; }
        public int QuantityYC { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}