using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public double ShippingPrice { get; set; }
        public int OrderStatusId { get; set; }

        public string OrderStatusName { get; set; }
        public List<OrderDetails> OrderDetailsList { get; set; }

        public string Reason { get; set; }

        public Users Client { get; set; }
    }
}
