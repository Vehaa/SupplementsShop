﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class ProductCategories
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public List<ProductSubCategories> SubCategory { get; set; }
    }
}
