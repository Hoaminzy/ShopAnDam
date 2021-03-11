namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Articles = new HashSet<Article>();
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [StringLength(50)]
      /*  [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Hãy nhập tên tài khoản")]*/
        public string UserName { get; set; }

        [StringLength(50)]
  /*      [Display(Name = "Vị trí hiển thị")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]*/
        public string PassWord { get; set; }

        [StringLength(50)]
  /*      [Display(Name = "Quyền")]
        [Required(ErrorMessage = "Vui lòng chọn quyền")]*/
        public string GroupID { get; set; }

        [StringLength(50)]
    /*    [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Hãy nhập tên người dùng")]*/
        public string Name { get; set; }

        [StringLength(50)]
      /*  [Display(Name = "Email")]
        [Required(ErrorMessage = "Hãy nhập email tài khoản")]*/
        public string Email { get; set; }

        [StringLength(11)]
      /*  [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Hãy nhập số điện thoại")]*/
        public string Phone { get; set; }
     /*   [Display(Name = "Ngày tạo")]*/
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }
    /*    [Display(Name = "Trạng thái")]*/

        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
