using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Users Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int? EmployeeId { get; set; }

    }
}
