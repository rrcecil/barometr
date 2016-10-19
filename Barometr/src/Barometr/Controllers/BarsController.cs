using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Barometr.ViewModels;
using Barometr.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class BarsController : Controller
    {
        private BarService _service;
        public BarsController(BarService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<BarDTO> Get()
        {
            return _service.GetBarDTO();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BarDTO Get(int id)
        {
            return _service.GetBarById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]BarDTO value)
        {
            //_service.AddBar(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]BarDTO value)
        {
            _service.UpdateBar(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            _service.DeleteBar(id);
        }

        //random bar of the day method
        [HttpGet("/random")]
        public BarDTO GetRandomBar()
        {
            return _service.GetBarById(_service.RandomBar());
        }
    }


}
