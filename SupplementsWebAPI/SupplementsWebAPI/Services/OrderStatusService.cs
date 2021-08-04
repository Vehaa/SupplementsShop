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
    }
}
