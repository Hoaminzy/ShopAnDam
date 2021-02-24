using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Framework
{
    [Table("PaymentStatus")]
    public class PaymentStatus
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateDate { get; set; }

     
     
    }
}