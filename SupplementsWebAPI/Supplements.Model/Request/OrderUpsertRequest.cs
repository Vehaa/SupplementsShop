using Supplements.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class OrderUpsertRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int OrderStatusId { get; set; }


        public List<Products> orderProductList { get; set; }
        public double ShippingPrice { get; set; }
        public string OrderStatusName { get; set; }
        public string Reason { get; set; }
    }
}
