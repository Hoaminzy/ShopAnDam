using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class ProductViewmodel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public decimal? MotiPrice { get; set; }
        public string MetaTitle { get; set; }
        public bool Status { get; set; }
        public DateTime? TopHot { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CateName { get; set; }
        public string CateTiTle { get; set; }
    }
}