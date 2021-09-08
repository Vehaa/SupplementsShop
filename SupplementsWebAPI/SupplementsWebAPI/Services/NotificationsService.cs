using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class NotificationsService : BaseCRUDService<Supplements.Model.Models.Notifications, NotificationSearchRequest, Database.Notifications, NotificationUpsertRequest, NotificationUpsertRequest>
    {
       
        public NotificationsService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Supplements.Model.Models.Notifications> Get(NotificationSearchRequest search)
        {
            var query = _context.Set<Notifications>().AsQueryable();

            if (search.CustomerId>0)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);

            }

            var list=query.OrderByDescending(x => x.DateTime);


            List<Supplements.Model.Models.Notifications> result = _mapper.Map<List<Supplements.Model.Models.Notifications>>(list);

           

            return result;
        }
    }
}
