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
    public class BaseCRUDController<T, TSearch,TInsert,TUpdate>:ControllerBase
    {
        private readonly ICRUDService<T, TSearch, TInsert, TUpdate> _service = null;
        public BaseCRUDController(ICRUDService<T, TSearch,TInsert,TUpdate> service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual IActionResult Insert(TInsert request)
        {
            return Ok(_service.Insert(request));
        }

        [HttpPut("{id}")]
        public virtual IActionResult Update(int id,[FromBody]TUpdate request)
        {
            return Ok(_service.Update(id, request));
        }

        [HttpGet]
        public virtual List<T> Get([FromQuery] TSearch search)
        {
            return _service.Get(search);
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {           
               _service.Delete(id);          
        }
    }
}
