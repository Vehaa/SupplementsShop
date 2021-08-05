using AutoMapper;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class BrandService : BaseCRUDService<Supplements.Model.Models.Brands, BrandSearchRequest, Database.Brands, BrandUpsertRequest, BrandUpsertRequest>
    {
        public BrandService(SupplementsContext context, IMapper mapper) : base(context, mapper)
        {
        }
        string FILE_PATH = @"C:\Zavrsni\";
        public override Supplements.Model.Models.Brands Insert(BrandUpsertRequest request)
        {
            
            var entity = _mapper.Map<Brands>(request);

            var filePathName =Path.GetFileNameWithoutExtension(request.Name) + "-" +
            DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
            Path.GetExtension(request.Name);

            if (!string.IsNullOrEmpty(request.LogoAsBase64))
            {
                if (request.LogoAsBase64.Contains(","))
                {
                    request.LogoAsBase64 = request.LogoAsBase64.Substring(request.LogoAsBase64.IndexOf(",") + 1);
                }

                request.LogoAsByteArray = Convert.FromBase64String(request.LogoAsBase64);

                using (var fs = new FileStream(filePathName, FileMode.CreateNew))
                {
                    fs.Write(request.LogoAsByteArray, 0, request.LogoAsByteArray.Length);
                }
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.LogoAsBase64 = request.LogoAsBase64;
            entity.LogoAsByteArray = request.LogoAsByteArray;

            _context.Brands.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Supplements.Model.Models.Brands>(entity);           
        }
    }
}
