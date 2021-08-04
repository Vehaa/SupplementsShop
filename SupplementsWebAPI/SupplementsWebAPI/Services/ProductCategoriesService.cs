using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;

namespace SupplementsWebAPI.Services
{
    public class ProductCategoriesService:BaseCRUDService<Supplements.Model.Models.ProductCategories,ProductCategorySearchRequest,Database.ProductCategories,ProductCategoryUpsertRequest,ProductCategoryUpsertRequest> 
    {

        public ProductCategoriesService(SupplementsContext context, IMapper mapper):base(context,mapper)
        {
                
        }
    }
}
