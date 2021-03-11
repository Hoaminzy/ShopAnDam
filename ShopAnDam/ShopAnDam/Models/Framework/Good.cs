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

        public int? SupplyID { get; set; }
        public int? ProductID { get; set; }
        public int Quantity { get; set; }

        public decimal? Prices { get; set; }
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

    }
}
