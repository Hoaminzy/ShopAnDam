namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Good
    {
        public int ID { get; set; }

        public int? GoodID { get; set; }

        public int? SupplyID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public virtual Supply Supply { get; set; }

        public virtual Goods_Detail Goods_Detail { get; set; }
    }
}
