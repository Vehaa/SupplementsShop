using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class PasswordRequest
    {
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Polje Password ne smije biti duže od 50 karaktera!")]
        [MinLength(5)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Polje Password Potvrda ne smije biti duže od 50 karaktera!")]
        [MinLength(5)]
        public string PasswordConfirmation { get; set; }

        public string OldPassword { get; set; }

    }
}
