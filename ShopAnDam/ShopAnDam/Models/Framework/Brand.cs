namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Thương hiệu")]
        [Required(ErrorMessage = "Hãy nhập tên thương hiệu")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Bạn chưa chọn logo cho thương hiệu")]
        public string Logo { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        public string CreateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
