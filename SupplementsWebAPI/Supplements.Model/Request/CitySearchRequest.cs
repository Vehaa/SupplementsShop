using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class CitySearchRequest
    {
        public int? CityId { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
    }
}
