namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Display(Name = "Mã")]
        public int ID { get; set; }

        [StringLength(150)]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(12)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
