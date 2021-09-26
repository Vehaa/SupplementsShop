using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class OrderDetailsService : BaseCRUDService<Supplements.Model.Models.OrderDetails, OrderDetailsSearchRequest, Database.OrderDetails, OrderDetailsUpsertRequest, OrderDetailsUpsertRequest>
    {
        public OrderDetailsService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Supplements.Model.Models.OrderDetails> Get(OrderDetailsSearchRequest search)
        {
            var query = _context.Set<OrderDetails>().AsQueryable();

            if (search.OrderId != null)
            {
                query = query.Where(x => x.OrderId == search.OrderId);
            }
            var list = query.ToList();

            List<Supplements.Model.Models.OrderDetails> result = _mapper.Map<List<Supplements.Model.Models.OrderDetails>>(list);

            return result;

        }
    }
}
