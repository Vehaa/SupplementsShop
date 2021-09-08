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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentsController : BaseCRUDController<Supplements.Model.Models.Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest>
    {
        public CommentsController(ICRUDService<Comments, CommentSearchRequest, CommentUpsertRequest, CommentUpsertRequest> service) : base(service)
        {
        }
    }
}
