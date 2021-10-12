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
    public class ProductsController : BaseCRUDController<Supplements.Model.Models.Products, ProductSearchRequest, ProductUpsertRequest, ProductUpsertRequest>
    {
        private readonly ICRUDService<Products, ProductSearchRequest, ProductUpsertRequest, ProductUpsertRequest> _service;
        private readonly IReports _reportService;

        public ProductsController(ICRUDService<Products, ProductSearchRequest, ProductUpsertRequest, ProductUpsertRequest> service, IReports reportService) : base(service)
        {
            _service = service;
            _reportService = reportService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Insert(ProductUpsertRequest request)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Update(int id, [FromBody] ProductUpsertRequest request)
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

        
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override void Delete(int id)
        {
            _service.Delete(id);
        }

       
    }
}
