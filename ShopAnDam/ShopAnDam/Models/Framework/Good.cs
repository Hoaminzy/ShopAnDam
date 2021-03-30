namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Good
    {
     /*   [Display(Name = "Mã phiếu nhập")]*/
        public int ID { get; set; }
       /* [Display(Name = "Nhà cung cấp")]*/
        public int SupplyID { get; set; }
 /*       [Display(Name = "Trạng thái")]*/
        public bool Status { get; set; }
        /*[Display(Name = "Ngày lập")]*/
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
       /* [Display(Name = "Người lập")]*/
        public string CreateBy { get; set; }

    }
}
