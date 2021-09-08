using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supplements.Model.Request;
using Supplements.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.ComponentModel.DataAnnotations;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseCRUDController<ProductCategories,ProductCategorySearchRequest,ProductCategoryUpsertRequest,ProductCategoryUpsertRequest>
    {
        private readonly ICRUDService<ProductCategories, ProductCategorySearchRequest, ProductCategoryUpsertRequest, ProductCategoryUpsertRequest> _service;

        public ProductCategoryController(ICRUDService<ProductCategories, ProductCategorySearchRequest, ProductCategoryUpsertRequest, ProductCategoryUpsertRequest> service) :base(service)
        {

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Insert(ProductCategoryUpsertRequest request)
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
        public override IActionResult Update(int id, [FromBody] ProductCategoryUpsertRequest request)
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
