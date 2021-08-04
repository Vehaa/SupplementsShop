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
    public class ProductSubCategoryController : BaseCRUDController<ProductSubCategories, ProductSubCategorySearchRequest, ProductSubCategoryUpsertRequest, ProductSubCategoryUpsertRequest>
    {
        public ProductSubCategoryController(ICRUDService<ProductSubCategories, ProductSubCategorySearchRequest, ProductSubCategoryUpsertRequest, ProductSubCategoryUpsertRequest> service) : base(service)
        {
        }
    }
}
