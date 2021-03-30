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
        [Display(Name = "Chọn sản phẩm")]
        public int? ProductID { get; set; }
        [Display(Name = "Chọn bài viết")]
        public int? ArticleID { get; set; }

        [Column("Image")]
        [Display(Name = "Hình ảnh")]
    /*    [Required(ErrorMessage = "Bạn chưa chọn hình ảnh!")]*/
        public string Image1 { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public virtual Article Article { get; set; }

        public virtual Product Product { get; set; }
    }
}
