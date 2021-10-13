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
    public class CitiesController : BaseCRUDController<Supplements.Model.Models.Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        private readonly ICRUDService<Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest> _service;
        public CitiesController(ICRUDService<Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest> service) : base(service)
        {
            _service = service;

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public override IActionResult Insert(CityUpsertRequest request)
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

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override void Delete(int id)
        {
            _service.Delete(id);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Update(int id, [FromBody] CityUpsertRequest request)
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
