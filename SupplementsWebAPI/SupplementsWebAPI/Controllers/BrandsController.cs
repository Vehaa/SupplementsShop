using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Brands = Supplements.Model.Models.Brands;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseCRUDController<Supplements.Model.Models.Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest>
    {
        private readonly ICRUDService<Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest> _service = null;

        public BrandsController(ICRUDService<Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Uposlenik")]
        public override IActionResult Insert(BrandUpsertRequest request)
        {
            try
            {
                return Ok(_service.Insert(request));

            }
            catch (ValidationException e )
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Uposlenik")]
        public override IActionResult Update(int id, [FromBody] BrandUpsertRequest request)
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

        

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Uposlenik")]
        public override void Delete(int id)
        {
           _service.Delete(id);
        }

    }
}
