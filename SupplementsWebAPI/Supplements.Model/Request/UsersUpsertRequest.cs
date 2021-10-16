using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class UsersUpsertRequest
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Ime ne smije biti duže od 50 karaktera!")]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Prezime ne smije biti duže od 50 karaktera!")]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Username ne smije biti duže od 50 karaktera!")]
        [MinLength(5)]

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        [Required(ErrorMessage = "Email je obavezno polje!")]
        [EmailAddress(ErrorMessage = "Email mora biti u formatu email adrese!")]
        [MaxLength(50, ErrorMessage = "Polje Email ne smije biti duže od 50 karaktera!")]
        [MinLength(10)]

        public string Email { get; set; }
        [MaxLength(15, ErrorMessage = "Polje Telefon ne smije biti duže od 15 karaktera!")]
        [Phone(ErrorMessage = "Telefon mora biti u formatu broja telefona!")]
        [MinLength(9)]

        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }
        public bool Comments { get; set; }
        public byte[] Picture { get; set; }
        public string PhotoAsBase64 { get; set; }
        [Required(ErrorMessage = "Adresa je obavezno polje!")]
        [MaxLength(30, ErrorMessage = "Polje Adresa ne smije biti duže od 30 karaktera!")]
        [MinLength(5)]
        public string Address { get; set; }



        public int CityId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        public string OldPassword { get; set; }

    }
}
