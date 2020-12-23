namespace ShopAnDam.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supply()
        {
            Goods = new HashSet<Good>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        [Required]
        public string Address { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(12)]
        [Required]
        public string Phone { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Good> Goods { get; set; }
    }
}
