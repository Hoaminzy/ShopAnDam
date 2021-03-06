﻿using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models
{
    [Serializable]
    public class CartItem
    {
        public Customer customer { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}