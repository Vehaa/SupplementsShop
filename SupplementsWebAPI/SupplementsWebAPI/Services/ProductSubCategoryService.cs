using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class ProductSubCategoryService : BaseCRUDService<Supplements.Model.Models.ProductSubCategories, ProductSubCategorySearchRequest, Database.ProductSubCategories, ProductSubCategoryUpsertRequest, ProductSubCategoryUpsertRequest>
    {
        public ProductSubCategoryService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
