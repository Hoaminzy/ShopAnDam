using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class ImageViewModel
    {
        public int ID { get; set; }
       public string Image { get; set; }
        public string HinhAnhSp { get; set; } //hình ảnh bên bảng sp
        public string NameSP { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}