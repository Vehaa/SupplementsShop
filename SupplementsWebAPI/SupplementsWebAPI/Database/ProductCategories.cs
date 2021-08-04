using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class ProductCategories
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

    }
}
