using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplementsWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCRUDController<T, TSearch,TInsert,TUpdate>
    {
        private readonly ICRUDService<T, TSearch, TInsert, TUpdate> _service = null;
        public BaseCRUDController(ICRUDService<T, TSearch,TInsert,TUpdate> service)
        {
            _service = service;
        }

        [HttpPost]
        public T Insert(TInsert request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public T Update(int id,[FromBody]TUpdate request)
        {
            return _service.Update(id, request);
        }

        [HttpGet]
        public List<T> Get([FromQuery] TSearch search)
        {
            return _service.Get(search);
        }

        [HttpGet("{id}")]
        public T GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
             _service.Delete(id);
        }
    }
}
