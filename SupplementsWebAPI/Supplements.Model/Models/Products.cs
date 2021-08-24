using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class Products
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
        public string PhotoAsBase64 { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }

        public int ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
    }
}
