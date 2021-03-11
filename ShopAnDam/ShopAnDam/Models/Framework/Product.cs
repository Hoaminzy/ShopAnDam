namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Images = new HashSet<Image>();
            Order_Detail = new HashSet<Order_Detail>();
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }
        [Display(Name = "Thương hiệu")]
        public int? BrandID { get; set; }
        [Display(Name = "Danh mục sản phẩm")]
        public int? CategoryID { get; set; }

        [StringLength(150)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")]
        public string Name { get; set; }
      
        
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(150)]
        [Display(Name = "Tiêu đề ngắn")]
        [Required(ErrorMessage = "Hãy nhập tiêu đề sản phẩm")]
        public string Title { get; set; }

        [StringLength(150)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá khuyến mại")]
        public decimal? MotionPrice { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Hãy nhập số lượng sản phẩm")]
        public int? Quantity { get; set; }

        public bool IncludeVAT { get; set; }

        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh sản phẩm")]
        public string image { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
