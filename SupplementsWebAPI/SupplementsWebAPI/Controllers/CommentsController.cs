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
    public class CommentsController : BaseCRUDController<Supplements.Model.Models.Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest>
    {
        private readonly ICRUDService<Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest> _service = null;

        public CommentsController(ICRUDService<Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest> service) : base(service)
        {
            _service = service;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Uposlenik,Klijent")]
        public override IActionResult Insert(CommentUpsertRequest request)
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
    }
}
