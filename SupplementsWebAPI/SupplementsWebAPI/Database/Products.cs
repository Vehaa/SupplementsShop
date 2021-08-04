using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public int UnitOnOrder { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public byte[] Photo { get; set; }
        public int BrandId { get; set; }
        public Brands Brands { get; set; }
        public int Counter { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategories ProductCategory { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public ProductCategories ProductSubCategory { get; set; }
    }
}
