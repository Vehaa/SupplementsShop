using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
    }
}
