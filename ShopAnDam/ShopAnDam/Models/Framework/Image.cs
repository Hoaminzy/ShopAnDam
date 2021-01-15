namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        public int? ArticleID { get; set; }

        [Column("Image")]
        public string Image1 { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public virtual Article Article { get; set; }

        public virtual Product Product { get; set; }
    }
}
