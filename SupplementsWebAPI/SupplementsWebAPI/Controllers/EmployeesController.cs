using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")]
    public class EmployeesController : BaseCRUDController<Supplements.Model.Models.Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest>
    {
        

        public EmployeesController(ICRUDService<Users, EmployeeSearchRequest, EmployeeUpsertRequest, EmployeeUpsertRequest> service) : base(service)
        {
            
        }
    }
}
