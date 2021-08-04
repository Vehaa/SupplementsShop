using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class RoleService : BaseCRUDService<Supplements.Model.Models.Roles, RoleSearchRequest, Database.Roles, RoleUpsertRequest, RoleUpsertRequest>
    {
        public RoleService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
