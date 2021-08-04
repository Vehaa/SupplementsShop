using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class CommentsService : BaseCRUDService<Supplements.Model.Models.Comments, CommentSearchRequest, Database.Comments, CommentUpsertRequest, CommentUpsertRequest>
    {
        public CommentsService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
