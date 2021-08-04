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
        public UsersController(ICRUDService<Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest> service) : base(service)
        {
        }
    }
}
