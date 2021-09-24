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

        public override Supplements.Model.Models.Comments Insert(CommentUpsertRequest request)
        {
            request.DateTime = DateTime.Now;
            var entity = _mapper.Map<Comments>(request);
            _context.Comments.Add(entity);
            _context.SaveChanges();
            var result= _mapper.Map<Supplements.Model.Models.Comments>(entity);

            return result;
        }

        public override List<Supplements.Model.Models.Comments> Get(CommentSearchRequest search)
        {
            var query = _context.Set<Comments>().AsQueryable();
            if (search.ProductId != null)
            {
                query = query.Where(x => x.ProductId == search.ProductId);
            }

            if (search.UserId != null)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            var list = query.OrderByDescending(x => x.DateTime).ToList();

            List<Supplements.Model.Models.Comments> result = _mapper.Map<List<Supplements.Model.Models.Comments>>(list);

            foreach (var item in result)
            {
                item.ClientsInfo = _context.Users.Where(x => x.UserId == item.UserId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
            }
            return result;

        }
    }
}
