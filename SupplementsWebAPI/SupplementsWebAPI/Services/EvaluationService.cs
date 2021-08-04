﻿using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class EvaluationService : BaseCRUDService<Supplements.Model.Models.Evaluations, EvaluationSearchRequest, Database.Evaluations, EvaluationUpsertRequest, EvaluationUpsertRequest>
    {
        public EvaluationService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
