using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : BaseCRUDController<Supplements.Model.Models.Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest>
    {
        private readonly ICRUDService<Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest> _service = null;

        public UsersController(ICRUDService<Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest> service, IJwtAuthenticationManager jwt) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override List<Users> Get([FromQuery] UsersSearchRequest search)
        {
            return _service.Get(search);
        }
    }
}
