using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class ProductSubCategories
    {
        [Key]
        public int ProductSubCategoryId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategories ProductCategory { get; set; }
    }
}
