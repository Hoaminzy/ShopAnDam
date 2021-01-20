using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.ViewModel
{
    public class ArticleViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Title { get; set; }
        public string Images1 { get; set; }
        public string Description { get; set; }
        public  string Content { get; set; }
        public bool status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string TopicName { get; set; }
        public string HinhAnh { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }

        public string comment { get; set; }
    }
}