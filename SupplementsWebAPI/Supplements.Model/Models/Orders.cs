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
        public Users Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int OrderStatusId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
