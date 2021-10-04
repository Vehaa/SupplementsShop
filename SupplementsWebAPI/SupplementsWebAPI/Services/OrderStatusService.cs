using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class OrderStatusService : BaseCRUDService<Supplements.Model.Models.OrderStatus, OrderStatusSearchRequest, Database.OrderStatus, OrderStatusUpsertRequest, OrderStatusUpsertRequest>
    {
        public OrderStatusService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Supplements.Model.Models.OrderStatus> Get(OrderStatusSearchRequest search)
        {
            var query = _context.Set<OrderStatus>().AsQueryable();

            if (search.OrderStatusId != null)
            {
                query = query.Where(x => x.OrderStatusId == search.OrderStatusId);
            }
            var list = query.OrderBy(x => x.OrderStatusId).ToList();

            List<Supplements.Model.Models.OrderStatus> result = _mapper.Map<List<Supplements.Model.Models.OrderStatus>>(list);

            return result;
        }
    }
}
