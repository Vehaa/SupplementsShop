using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class ProductCategoryUpsertRequest
    {
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Naziv ne smije biti veći od 50 karaktera!")]
        public string Name { get; set; }
    }
}
