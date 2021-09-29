using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class BrandSearchRequest
    {
        public int? BrandId { get; set; }

        public string Name { get; set; }
    }
}
