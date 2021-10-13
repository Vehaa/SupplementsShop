using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : BaseCRUDController<Supplements.Model.Models.Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest>
    {
        private readonly ICRUDService<Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest> _service = null;

        public UsersController(ICRUDService<Users, UsersSearchRequest, UsersUpsertRequest, UsersUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Get([FromQuery] UsersSearchRequest search)
        {

            return Ok(_service.Get(search));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Insert(UsersUpsertRequest request)
        {
            try
            {
                return Ok(_service.Insert(request));

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik,Klijent")]
        public override IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik,Klijent")]
        public override IActionResult Update(int id,UsersUpsertRequest request)
        {
            try
            {
                return Ok(_service.Update(id,request));

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
