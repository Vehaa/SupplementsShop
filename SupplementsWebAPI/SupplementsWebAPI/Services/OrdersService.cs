using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class OrdersService : BaseCRUDService<Supplements.Model.Models.Orders, OrderSearchRequest, Database.Orders, OrderUpsertRequest, OrderUpsertRequest>
    {
        public OrdersService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
