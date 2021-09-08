using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class CityService : BaseCRUDService<Supplements.Model.Models.Cities, CitySearchRequest, Database.Cities, CityUpsertRequest, CityUpsertRequest>
    {
        public CityService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override List<Supplements.Model.Models.Cities> Get(CitySearchRequest search)
        {
            var query = _context.Set<Cities>().AsQueryable();

            var list = query.OrderBy(x=>x.Name).ToList();

            List<Supplements.Model.Models.Cities> result = _mapper.Map<List<Supplements.Model.Models.Cities>>(list);

            return result;
        }

        public override Supplements.Model.Models.Cities Insert(CityUpsertRequest request)
        {
            string errorMessage = null;
            var gradovi = _context.Cities.ToList();
            foreach (var item in gradovi)
            {
                if (item.Name == request.Name && item.PostalCode == request.PostalCode)
                    errorMessage = "Unešeni grad već postoji u bazi!";
            }
            if (errorMessage != null)
            {
                throw new ValidationException(errorMessage);
            }

            var entity = _mapper.Map<Cities>(request);

            entity.Name = request.Name;
            entity.PostalCode = request.PostalCode;

            _context.Cities.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Supplements.Model.Models.Cities>(entity);
        }
    }
}
