using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class OrderDetailsUpsertRequest
    {
        public int OrderDetailsId { get; set; }
        [Required(ErrorMessage = "Cijena je obavezno polje!")]
        [MaxLength(200000, ErrorMessage = "Polje Cijena ne smije biti duže od 200 000!")]
        public double UnitPrice { get; set; }

        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double ShippingPrice { get; set; }
        public double TotalPrice { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
