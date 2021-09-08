using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : BaseCRUDController<Supplements.Model.Models.Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest>
    {
        public OrdersController(ICRUDService<Orders, OrderSearchRequest, OrderUpsertRequest, OrderUpsertRequest> service) : base(service)
        {
        }
    }
}
