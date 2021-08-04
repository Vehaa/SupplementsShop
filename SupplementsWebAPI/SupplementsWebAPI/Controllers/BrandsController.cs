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
    public class BrandsController : BaseCRUDController<Supplements.Model.Models.Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest>
    {
        public BrandsController(ICRUDService<Brands, BrandSearchRequest, BrandUpsertRequest, BrandUpsertRequest> service) : base(service)
        {
        }
    }
}
