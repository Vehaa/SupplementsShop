﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class BrandService : BaseCRUDService<Supplements.Model.Models.Brands, BrandSearchRequest, Database.Brands, BrandUpsertRequest, BrandUpsertRequest>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BrandService(SupplementsContext context, IMapper mapper, IWebHostEnvironment hostingEnvironment) : base(context, mapper)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public override List<Supplements.Model.Models.Brands> Get(BrandSearchRequest search)
        {
            var query = _context.Set<Brands>().AsQueryable();
            var list = query.OrderBy(x => x.Name).ToList();



            List<Supplements.Model.Models.Brands> result = _mapper.Map<List<Supplements.Model.Models.Brands>>(list);

            foreach (var item in result)
            {
                item.ProductCounter = _context.Products.Where(x => x.BrandId == item.BrandId).Count();
            }

            return result;
        }
        public override Supplements.Model.Models.Brands Insert(BrandUpsertRequest request)
        {
            var brands = _context.Brands.ToList();

            string errorMessage = null;
            foreach (var item in brands)
            {
                if(item.Name==request.Name)
                {
                   
                    errorMessage = "Brend sa unešenim imenom već postoji u bazi!";
                }

              
            }
            if (errorMessage!=null)
            {

                throw new ValidationException(errorMessage);

            }
            var entity = _mapper.Map<Brands>(request);
            var filePathName = Path.GetFileNameWithoutExtension(request.Name) + "-" +
            DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
            Path.GetExtension(request.Name);

            if (!string.IsNullOrEmpty(request.LogoAsBase64))
            {
                if (request.LogoAsBase64.Contains(","))
                {
                    request.LogoAsBase64 = request.LogoAsBase64.Substring(request.LogoAsBase64.IndexOf(",") + 1);

                }
                byte[] bytes = Convert.FromBase64String(request.LogoAsBase64);

                request.LogoAsByteArray = bytes;

            }
            else
            {
                string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "nologo.png";
                byte[] b = System.IO.File.ReadAllBytes(path);
                request.LogoAsBase64 = Convert.ToBase64String(b);
                request.LogoAsByteArray = b;
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
