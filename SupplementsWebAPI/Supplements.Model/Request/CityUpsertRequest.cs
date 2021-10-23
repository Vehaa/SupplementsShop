using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class CityUpsertRequest
    {
        public int CityId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje!")]
        [MinLength(2)]
        [MaxLength(50, ErrorMessage = "Polje Naziv ne smije biti duže od 50 karaktera!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Poštanski broj je obavezno polje!")]
        [MinLength(3)]
        [MaxLength(12, ErrorMessage = "Polje Poštanski broj ne smije biti duže od 12 karaktera!")]
        public string PostalCode { get; set; }
    }
}
