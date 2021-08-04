using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Evaluations
    {
        [Key]
        public int EvaluationId { get; set; }
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public DateTime DateTime { get; set; }
    }
}
