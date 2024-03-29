﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class OrderDetailsSearchRequest
    {
        public int?OrderDetailsId { get; set; }
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public double? ShippingPrice { get; set; }
        public double? TotalPrice { get; set; }

        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
