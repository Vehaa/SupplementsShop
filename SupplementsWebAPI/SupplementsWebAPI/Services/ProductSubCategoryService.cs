using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public override List<Supplements.Model.Models.ProductSubCategories> Get(ProductSubCategorySearchRequest search)
        {
            var query = _context.Set<ProductSubCategories>().AsQueryable();

            if (search.ProductCategoryId != null)
            {
                query = query.Where(x => x.ProductCategoryId == search.ProductCategoryId);
            }
           
            var list = query.OrderBy(x => x.Name).ToList();

            List<Supplements.Model.Models.ProductSubCategories> result = _mapper.Map<List<Supplements.Model.Models.ProductSubCategories>>(list);

            return result;

        }
    }
}
