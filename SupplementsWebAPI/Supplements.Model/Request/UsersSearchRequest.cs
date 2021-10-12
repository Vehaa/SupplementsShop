using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplements.Model.Request
{
    public class UsersSearchRequest
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool? Status { get; set; }
        public bool? Comments { get; set; }
        public string Address { get; set; }



        public int? RoleId { get; set; }
        public int? CityId { get; set; }


    }
}
