using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Models;
using Supplements.Model.Request;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationsController : BaseCRUDController<Supplements.Model.Models.Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest>
    {
        private readonly ICRUDService<Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest> _service;

        public EvaluationsController(ICRUDService<Evaluations, EvaluationSearchRequest, EvaluationUpsertRequest, EvaluationUpsertRequest> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Klijent")]
        public override IActionResult Insert(EvaluationUpsertRequest request)
        {
            try
            {
                return Ok(_service.Insert(request));

            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public override IActionResult Update(int id, [FromBody] EvaluationUpsertRequest request)
        {
            try
            {
                return Ok(_service.Update(id, request));

            }
            catch (ValidationException e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public override void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
