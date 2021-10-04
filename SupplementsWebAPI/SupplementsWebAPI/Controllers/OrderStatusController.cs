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
    public class OrderStatusController : BaseCRUDController<Supplements.Model.Models.OrderStatus, OrderStatusSearchRequest, OrderStatusUpsertRequest, OrderStatusUpsertRequest>
    {
        private readonly ICRUDService<OrderStatus, OrderStatusSearchRequest, OrderStatusUpsertRequest, OrderStatusUpsertRequest> _service = null;

        public OrderStatusController(ICRUDService<OrderStatus, OrderStatusSearchRequest, OrderStatusUpsertRequest, OrderStatusUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Klijent, Administrator, Uposlenik")]
        public override IActionResult Get(OrderStatusSearchRequest request)
        {
            try
            {
                return Ok(_service.Get(request));

            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
