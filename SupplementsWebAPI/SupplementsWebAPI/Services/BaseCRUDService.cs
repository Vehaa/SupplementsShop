using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplementsWebAPI.Database;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SupplementsWebAPI.Services
{
    public class BaseCRUDService<TModel, TSearch, TDatabase, TInsert, TUpdate> : ICRUDService<TModel, TSearch, TInsert, TUpdate> where TDatabase : class
    {
        protected readonly SupplementsContext _context;
        protected readonly IMapper _mapper;
        public BaseCRUDService(SupplementsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual TModel Insert(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            _context.Set<TDatabase>().Add(entity);
            _context.SaveChanges();

            return _mapper.Map<TModel>(entity);
        }

        public virtual TModel Update(int id, TUpdate request)
        {

            var entity = _context.Set<TDatabase>().Find(id);
            _context.Set<TDatabase>().Attach(entity);
            _context.Set<TDatabase>().Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<TModel>(entity);




        }

        public virtual List<TModel> Get(TSearch search)
        {
            var list = _context.Set<TDatabase>().ToList();

            return _mapper.Map<List<TModel>>(list);
        }

        public virtual TModel GetById(int id)
        {

            var entity = _context.Set<TDatabase>().Find(id);

            return _mapper.Map<TModel>(entity);



        }

        public virtual void Delete(int id)
        {

            var entity = _context.Set<TDatabase>().Find(id);

            _context.Set<TDatabase>().Remove(entity);
            _context.SaveChanges();



        }
    }
}
