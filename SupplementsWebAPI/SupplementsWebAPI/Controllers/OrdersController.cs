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
    public class OrdersController : BaseCRUDController<Supplements.Model.Models.Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest>
    {
        private readonly ICRUDService<Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest> _service = null;

        public OrdersController(ICRUDService<Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Klijent")]
        public override IActionResult Insert(OrderUpsertRequest request)
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik")]
        public override IActionResult Update(int id, [FromBody] OrderUpsertRequest request)
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
