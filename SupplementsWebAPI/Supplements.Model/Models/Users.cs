using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Models
{
    public class Users
    {
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
        public string PhotoAsBase64 { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }


        public int RoleId { get; set; }
        public int CityId { get; set; }

        public string Role { get; set; }

        public string password { get; set; }
        public string passwordConfirmation { get; set; }


        public double TotalMoney { get; set; }
        public int TotalOrders { get; set; }
    }
}
