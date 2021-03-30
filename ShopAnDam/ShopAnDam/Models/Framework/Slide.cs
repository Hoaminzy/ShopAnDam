namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        [Display(Name = "Mã")]
        public int ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Vị trí hiển thị")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn")]
        public string Link { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        public string CreateBy { get; set; }
    }
}
