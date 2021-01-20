namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            Images1 = new HashSet<Image>();
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }

        public int? TopicID { get; set; }

        public int? UserID { get; set; }
        public int? CustomerID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Images { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime? ViewCount { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
