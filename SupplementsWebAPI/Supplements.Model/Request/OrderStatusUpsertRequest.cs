using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class OrderStatusUpsertRequest
    {
        public int OrderStatusId { get; set; }
        public string StatusName { get; set; }
    }
}
