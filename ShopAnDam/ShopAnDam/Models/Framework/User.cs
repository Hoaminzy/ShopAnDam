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

        public string UserName { get; set; }

        [StringLength(50)]

        public string PassWord { get; set; }

        [StringLength(50)]
  
        public string GroupID { get; set; }

        [StringLength(50)]

        public string Name { get; set; }

        [StringLength(50)]

        public string Email { get; set; }

        [StringLength(11)]

        public string Phone { get; set; }
 
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }


        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
