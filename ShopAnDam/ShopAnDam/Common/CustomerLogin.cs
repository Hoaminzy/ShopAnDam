using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Common
{
    [Serializable]
    public class CustomerLogin
    {
        public long CustomerID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
         public string Address { get; set; }
        public int ProvinID { get; set; }
        public int DistricID { get; set; }
        public string GroupID { get; set; }
    }
}