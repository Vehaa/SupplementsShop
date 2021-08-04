using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supplements.Model.Request;
using Supplements.Model.Models;
namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseCRUDController<ProductCategories,ProductCategorySearchRequest,ProductCategoryUpsertRequest,ProductCategoryUpsertRequest>
    {
        public ProductCategoryController(ICRUDService<ProductCategories, ProductCategorySearchRequest, ProductCategoryUpsertRequest, ProductCategoryUpsertRequest> service) :base(service)
        {

        }
    }
}
