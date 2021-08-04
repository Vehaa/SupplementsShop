using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class CityService : BaseCRUDService<Supplements.Model.Models.Cities, CitySearchRequest, Database.Cities, CityUpsertRequest, CityUpsertRequest>
    {
        public CityService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Supplements.Model.Models.Cities> Get(CitySearchRequest search)
        {
            var query = _context.Set<Cities>().AsQueryable();

            var list = query.OrderBy(x=>x.Name).ToList();

            List<Supplements.Model.Models.Cities> result = _mapper.Map<List<Supplements.Model.Models.Cities>>(list);

            return result;
        }
    }
}
