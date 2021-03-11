namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Articles = new HashSet<Article>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }

        [StringLength(100)]
      /*  [Display(Name = "Họ và Tên")]*/
        public string Name { get; set; }

        [StringLength(200)]
       /* [Display(Name = "Địa Chỉ")]*/

        public string Address { get; set; }

        [StringLength(50)]
        /*[Display(Name = "Email")]*/

        public string Email { get; set; }

        [StringLength(12)]
        /*[Display(Name = "Số điện thoại")]*/

        public string Phone { get; set; }
        /*[Display(Name = "Ngày tạo")]*/

        public DateTime? Createdate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [StringLength(50)]
      /*  [Display(Name = "Tên tài khoản")]*/

        public string UserName { get; set; }

        [StringLength(50)]
 /*       [Display(Name = "Mật khẩu")]*/

        public string PassWord { get; set; }
       /* [Display(Name = "Tỉnh thành")]*/

        public int ProvinID { get; set; }
       /* [Display(Name = "Quận huyện")]*/

        public int DistricID { get; set; }
        /*[Display(Name = "Trạng thái")]*/

        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
