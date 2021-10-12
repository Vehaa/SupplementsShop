using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Supplements.Model.Request;
using SupplementsWebAPI.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

                }
                else
                {
                    string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "noProductImage.png";
                    byte[] b = System.IO.File.ReadAllBytes(path);
                    request.PhotoAsBase64 = Convert.ToBase64String(b);
                }

                var dis = (request.Discount * request.UnitPrice) / 100;
                request.TotalPrice = request.UnitPrice - dis;
                request.Counter = 0;
                entity.TotalPrice = request.TotalPrice;
                entity.Counter = request.Counter;
                entity.PhotoAsBase64 = request.PhotoAsBase64;
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

            var list = query.ToList();

            if (search.FilterName != null)
            {
                if (search.FilterName.ToLower() == "nazivdesc")
                {
                    list=list.OrderByDescending(x => x.Name).ToList();


                }
                if (search.FilterName.ToLower() == "nazivasc")
                {
                    list = list.OrderBy(x => x.Name).ToList();

                }
                if (search.FilterName.ToLower() == "cijenadesc")
                {
                    list = list.OrderByDescending(x => x.TotalPrice).ToList();


                }
                if (search.FilterName.ToLower() == "cijenaasc")
                {
                    list = list.OrderBy(x => x.TotalPrice).ToList();

                }
                if (search.FilterName.ToLower() == "snizeni")
                {
                    list = list.OrderByDescending(x => x.Discount).ToList();

                }
                if (search.FilterName.ToLower() == "snizeni")
                {
                    list = list.OrderByDescending(x => x.Discount).ToList();

                }
            }
            else
            {
                list = list.OrderByDescending(x => x.Counter).ToList();

            }


            List<Supplements.Model.Models.Products> result = _mapper.Map<List<Supplements.Model.Models.Products>>(list);
            var ocjene = _context.Evaluations.ToList();

            foreach (var item in result)
            {
                item.AvgRating = 0;
                int brojac = 0;
                foreach (var o in ocjene)
                {
                    if (item.ProductId == o.ProductId)
                    {
                        brojac++;
                        item.AvgRating += o.Rate;
                    }
                }
                if (item.AvgRating < 1)
                    item.AvgRating = 0;
                else
                item.AvgRating /= brojac;
            }

            if (search.FilterName != null)
            {
                if (search.FilterName.ToLower() == "najbolji")
                {
                    result = result.OrderByDescending(x => x.AvgRating).ToList();
                }
            }
            
            

            return result;
        }

        public override Supplements.Model.Models.Products Update(int id,ProductUpsertRequest request)
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

            }
            else
            {
                string path = _hostingEnvironment.WebRootPath + "/MyImages/" + "noProductImage.png";
                byte[] b = System.IO.File.ReadAllBytes(path);
                request.PhotoAsBase64 = Convert.ToBase64String(b);
            }
            var dis = (request.Discount * request.UnitPrice) / 100;
            request.TotalPrice = request.UnitPrice - dis;
            entity.TotalPrice = request.TotalPrice;
            entity.PhotoAsBase64 = request.PhotoAsBase64;

            _context.Products.Update(entity);
            _context.SaveChanges();

            return _mapper.Map<Supplements.Model.Models.Products>(entity);
        }

      

        public override Supplements.Model.Models.Products GetById(int id)
        {
            var entity = _context.Products.Find(id);
            if (entity == null)
            {
                throw new ValidationException("Korisnik nije pronađen!");
            }
            var result = _mapper.Map<Supplements.Model.Models.Products>(entity);
            var ocjene = _context.Evaluations.Where(x => x.ProductId == entity.ProductId).ToList();
            if (ocjene.Count() < 1)
                result.AvgRating = 0;
            else
            {
                foreach (var item in ocjene)
                {
                    result.AvgRating += item.Rate;
                }
                result.AvgRating /= ocjene.Count();
            }

            return result;

        }
    }
}
