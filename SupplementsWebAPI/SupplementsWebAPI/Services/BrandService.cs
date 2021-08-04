using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class BrandService : BaseCRUDService<Supplements.Model.Models.Brands, BrandSearchRequest, Database.Brands, BrandUpsertRequest, BrandUpsertRequest>
    {
        public BrandService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
