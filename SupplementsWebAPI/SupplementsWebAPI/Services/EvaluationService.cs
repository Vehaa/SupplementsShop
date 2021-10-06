using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class EvaluationService : BaseCRUDService<Supplements.Model.Models.Evaluations, EvaluationSearchRequest, Database.Evaluations, EvaluationUpsertRequest, EvaluationUpsertRequest>
    {
        public EvaluationService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public override Supplements.Model.Models.Evaluations Insert(EvaluationUpsertRequest request)
        {
            var entity = _mapper.Map<Evaluations>(request);
            var ocjene = _context.Evaluations.ToList();
            string errorMessage = null;

            foreach (var item in ocjene)
            {
                if (item.ProductId == request.ProductId && item.UserId == request.UserId)
                    errorMessage = "Birani proizvod ste već ocjenili ocjenom " + item.Rate +".";
            }

            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }

            entity.DateTime = DateTime.Now;
            _context.Evaluations.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Supplements.Model.Models.Evaluations>(entity);

        }
    }
}
