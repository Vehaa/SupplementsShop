using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class ProductSubCategorySearchRequest
    {
        public int? ProductSubCategoryId { get; set; }
        public string Name { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
