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
    public class EvaluationsController : BaseCRUDController<Supplements.Model.Models.Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest>
    {
        public EvaluationsController(ICRUDService<Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest> service) : base(service)
        {
        }
    }
}
