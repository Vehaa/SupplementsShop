using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class EmployeeUpsertRequest
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Ime ne smije biti duže od 50 karaktera!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Prezime ne smije biti duže od 50 karaktera!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username je obavezno polje!")]
        [MaxLength(50, ErrorMessage = "Polje Username ne smije biti duže od 50 karaktera!")]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        [Required(ErrorMessage = "Email je obavezno polje!")]
        [EmailAddress(ErrorMessage = "Email mora biti u formatu email adrese!")]
        [MaxLength(50, ErrorMessage = "Polje Email ne smije biti duže od 50 karaktera!")]
        public string Email { get; set; }
        [MaxLength(20, ErrorMessage = "Polje Telefon ne smije biti duže od 20 karaktera!")]
        [Phone(ErrorMessage = "Telefon mora biti u formatu broja telefona!")]
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }
        public bool Comments { get; set; }
        public byte[] Picture { get; set; }
        public byte[] PictureThumb { get; set; }
        public string? Address { get; set; }



        public int RoleId { get; set; }
        public int CityId { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Polje Password ne smije biti duže od 50 karaktera!")]
        public string Password { get; set; }
        [MaxLength(50, ErrorMessage = "Polje Password Potvrda ne smije biti duže od 50 karaktera!")]
        [Compare("Password", ErrorMessage = "Polje Password i Password Potvrda se moraju podudarati!")]
        public string PasswordConfirmation { get; set; }
    }
}
