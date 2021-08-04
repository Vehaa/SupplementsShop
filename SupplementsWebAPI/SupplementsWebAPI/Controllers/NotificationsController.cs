using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseCRUDController<Supplements.Model.Models.Notifications, NotificationSearchRequest, NotificationUpsertRequest, NotificationUpsertRequest>
    {
        public NotificationsController(ICRUDService<Notifications, NotificationSearchRequest, NotificationUpsertRequest, NotificationUpsertRequest> service) : base(service)
        {
        }
    }
}
