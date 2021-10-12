using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class EarningReport
    {
        public int EarningReportId { get; set; }

        public int  ProductSold { get; set; }
        public int TotalOrders { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public double Total { get; set; }
    }
}
