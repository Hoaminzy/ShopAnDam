namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Goods_Detail
    {
        public int ID { get; set; }

        public int GoodID { get; set; }

        public int ProductID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public virtual Good Good { get; set; }

        public virtual Product Product { get; set; }
    }
}
