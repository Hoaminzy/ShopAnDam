using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Areas.Admin.Code
{
    [Serializable] //Lưu thông tin session tuần tự hóa nhị phân
    public class UserSession
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}