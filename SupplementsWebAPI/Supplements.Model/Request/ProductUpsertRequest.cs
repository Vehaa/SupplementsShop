using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class ProductUpsertRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public int UnitOnOrder { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public int BrandId { get; set; }
        public int Counter { get; set; }


        public int ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
    }
}
