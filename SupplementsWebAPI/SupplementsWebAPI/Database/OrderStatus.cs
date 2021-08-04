using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string StatusName { get; set; }
    }
}
