using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using SupplementsWebAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _service;

        public UserController(IJwtAuthenticationManager service)
        {
            _service = service;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UsersUpsertRequest model)
        {
            try
            {
                return Ok(_service.Register(model));

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {

                var token = _service.Authenticate(model);
                return Ok(token);
            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);

            }
        }



        
    }
}
