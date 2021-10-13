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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")]
    public class EmployeesController : BaseCRUDController<Supplements.Model.Models.Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest>
    {
        private readonly ICRUDService<Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest> _service = null;


        public EmployeesController(ICRUDService<Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public override IActionResult Get([FromQuery] EmployeeSearchRequest search)
        {

            return Ok(_service.Get(search));
        }
        [HttpPost]
        public override IActionResult Insert(EmployeeUpsertRequest request)
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
        [HttpPut("{id}")]
        public override IActionResult Update(int id, EmployeeUpsertRequest request)
        {
            try
            {
                return Ok(_service.Update(id, request));

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
