using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SupplementsWebAPI.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        public string Authenticate(LoginModel model);
        public Users Register(UsersUpsertRequest model);
        public Users UpdatePassword(int id, PasswordRequest model);
        public Users UpdateProfile(int id, UsersUpsertRequest request);
     
    }
}
