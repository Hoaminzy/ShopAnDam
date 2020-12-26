using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace ShopAnDam.Models.Framework
{
    
    [Serializable]
    public partial class Role_User
    {
      
        [Key]
        //[Column(Order = 0)]
        [StringLength(50)]
        public string GroupID { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(50)]
        public string RoleID { get; set; }
    }
}
