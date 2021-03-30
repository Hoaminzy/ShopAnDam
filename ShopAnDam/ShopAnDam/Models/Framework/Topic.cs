namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Topic")]
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            Articles = new HashSet<Article>();
        }

        public int ID { get; set; }
        
        [StringLength(150)]
        [Display(Name = "Chủ đề")]
     /*   [Required(ErrorMessage = "Hãy nhập chủ đề")]*/
 
        public string Name { get; set; }

        [StringLength(150)]
        [Display(Name = "Tiêu đề")]
        /*[Required(ErrorMessage = "Hãy nhập tiêu đề")]*/

        public string MetaTilte { get; set; }
        [Display(Name = "Vị tí hiển thị")]
        /*[Required(ErrorMessage = "Hãy nhập vị trí hiển thị")]*/
       /* [Required]*/
        public int? DisplayOrder { get; set; }

        [StringLength(100)]
        public string SeoTitle { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Ngày tạo")]

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
