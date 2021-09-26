using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

    }
}
