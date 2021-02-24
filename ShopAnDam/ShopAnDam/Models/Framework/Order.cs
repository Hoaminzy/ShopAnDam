namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Detail = new HashSet<Order_Detail>();
        }

        public long ID { get; set; }

        public int? CustomersID { get; set; }

        [StringLength(50)]
        public string NameShip { get; set; }

        [StringLength(12)]
        public string PhoneShip { get; set; }

        [StringLength(250)]
        public string AdressShip { get; set; }

        [StringLength(50)]
        public string MailShip { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public int Status { get; set; }

        [StringLength(50)]
        public string FormOfPayment { get; set; }

        public int? Payment_Method { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public int? UserID { get; set; }

        public int? ProvinID { get; set; }

        public int? DistricID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }

        public virtual User User { get; set; }
    }
}
