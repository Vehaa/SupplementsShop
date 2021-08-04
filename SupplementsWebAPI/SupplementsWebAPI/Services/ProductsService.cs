using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class ProductsService : BaseCRUDService<Supplements.Model.Models.Products, ProductSearchRequest, Database.Products, ProductUpsertRequest, ProductUpsertRequest>
    {
        public ProductsService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
