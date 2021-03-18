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
        [Display(Name = "Chủ đề")]
        [Required(ErrorMessage = "Vui lòng chọn chủ đề bài viết")]
        public int? TopicID { get; set; }

        [StringLength(500)]
        [Display(Name = "Tên bài viết")]
        [Required(ErrorMessage = "Nhập tên bài viết")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Thẻ meta")]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Tiêu đề ngắn")]
        [Required(ErrorMessage = "Hãy nhập tiêu đề ngắn")]
        public string Title { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh")]
        public string Images { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Lượt xem")]
        public DateTime? ViewCount { get; set; }
        [Display(Name = "Trạng thái")]

        public bool Status { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public int? UserID { get; set; }

        public int? CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
