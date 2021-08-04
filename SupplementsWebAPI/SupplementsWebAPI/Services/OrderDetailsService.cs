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
    }
}
