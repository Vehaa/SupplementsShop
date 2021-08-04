using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Database
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }
        public bool Comments { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoThumb { get; set; }
        public string? Address { get; set; }


        public int RoleId { get; set; }
        public Roles Role { get; set; }
        public int CityId { get; set; }
        public Cities City { get; set; }

    }
}
