using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public override List<Supplements.Model.Models.ProductCategories> Get(ProductCategorySearchRequest search)
        {
            var query = _context.Set<Database.ProductCategories>().AsQueryable();
            var list = query.OrderBy(x => x.Name).ToList();
            var sub = _context.Set<Database.ProductSubCategories>().AsQueryable().Include(x=>x.ProductCategory);




            List<Supplements.Model.Models.ProductCategories> result = _mapper.Map<List<Supplements.Model.Models.ProductCategories>>(list);
            List<Supplements.Model.Models.ProductSubCategories> sub2 = _mapper.Map<List<Supplements.Model.Models.ProductSubCategories>>(sub);

            foreach (var item in result)
            {
                if (item.SubCategory == null)
                {
                    item.SubCategory = new List<Supplements.Model.Models.ProductSubCategories>();
                    foreach (var subcat in sub2)
                    {
                        if (item.ProductCategoryId == subcat.ProductCategoryId)
                        {
                            item.SubCategory.Add(subcat);
                        }
                    }
                }
            }
            return result;
        }
    }
}
