using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Services
{
    public class ProductsService : BaseCRUDService<Supplements.Model.Models.Products, ProductSearchRequest, Database.Products, ProductUpsertRequest, ProductUpsertRequest>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductsService(SupplementsContext context, IMapper mapper, IWebHostEnvironment hostingEnvironment) : base(context, mapper)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public override Supplements.Model.Models.Products Insert(ProductUpsertRequest request)
        {
            
                var entity = _mapper.Map<Products>(request);

                var filePathName = Path.GetFileNameWithoutExtension(request.Name) + "-" +
                DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
                Path.GetExtension(request.Name);

                if (!string.IsNullOrEmpty(request.PhotoAsBase64))
                {
                    if (request.PhotoAsBase64.Contains(","))
                    {
                        request.PhotoAsBase64 = request.PhotoAsBase64.Substring(request.PhotoAsBase64.IndexOf(",") + 1);

                    }
                    byte[] bytes = Convert.FromBase64String(request.PhotoAsBase64);

                    request.Photo = bytes;

                }
                else
                {
                    string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "noProductImage.png";
                    byte[] b = System.IO.File.ReadAllBytes(path);
                    request.PhotoAsBase64 = Convert.ToBase64String(b);
                    request.Photo = b;
                }

                var dis = (request.Discount * request.UnitPrice) / 100;
                request.TotalPrice = request.UnitPrice - dis;
                request.Counter = 0;
                entity.TotalPrice = request.TotalPrice;
                entity.Counter = request.Counter;
                entity.PhotoAsBase64 = request.PhotoAsBase64;
                entity.Photo = request.Photo;
                _context.Products.Add(entity);
                _context.SaveChanges();

                return _mapper.Map<Supplements.Model.Models.Products>(entity);


        }

        public override List<Supplements.Model.Models.Products> Get(ProductSearchRequest search)
        {
            var query = _context.Set<Products>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                query = query.Where(x => x.Name.StartsWith(search.Name.ToLower()));
            }
            if (search.BrandId != null)
            {
                query = query.Where(x => x.BrandId == search.BrandId);
            }
            if (search.ProductCategoryId != null)
            {
                query = query.Where(x => x.ProductCategoryId == search.ProductCategoryId);
            }
            if (search.ProductSubCategoryId != null)
            {
                query = query.Where(x => x.ProductSubCategoryId == search.ProductSubCategoryId);
            }
            var list = query.OrderBy(x => x.Name).ToList();



            List<Supplements.Model.Models.Products> result = _mapper.Map<List<Supplements.Model.Models.Products>>(list);

            

            return result;
        }
    }
}
