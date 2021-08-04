using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class ProductSubCategories
    {
        public int ProductSubCategoryId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
