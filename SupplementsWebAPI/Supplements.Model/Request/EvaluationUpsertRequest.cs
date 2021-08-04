using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class EvaluationUpsertRequest
    {
        public int EvaluationId { get; set; }
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
