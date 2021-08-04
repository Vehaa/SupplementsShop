using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class BrandUpsertRequest
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Naziv ne smije biti duže od 50 karaktera!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Opis je obavezno polje!")]
        [MaxLength(500, ErrorMessage = "Polje Opis ne smije biti duže od 500 karaktera!")]
        public string Description { get; set; }
        public byte[] Logo { get; set; }
    }
}
