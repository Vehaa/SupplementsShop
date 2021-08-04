using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class NotificationsService : BaseCRUDService<Supplements.Model.Models.Notifications, NotificationSearchRequest, Database.Notifications, NotificationUpsertRequest, NotificationUpsertRequest>
    {
        public NotificationsService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
