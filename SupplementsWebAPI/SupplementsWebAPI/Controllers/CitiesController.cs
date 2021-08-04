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
    public class CitiesController : BaseCRUDController<Supplements.Model.Models.Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public CitiesController(ICRUDService<Cities, CitySearchRequest, CityUpsertRequest, CityUpsertRequest> service) : base(service)
        {
        }
    }
}
