using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Barometr.ViewModels;
using Barometr.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class DrinksController : Controller
    {
        private DrinkService _service;
        public DrinksController(DrinkService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<DrinkDTO> Get()
        {
            return _service.GetAllDrinks();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public DrinkDTO Get(int id)
        {
            return _service.GetDrinkById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DrinkDTO value)
        {
            _service.AddDrink(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]DrinkDTO value)
        {
            _service.UpdateDrink(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(DrinkDTO value)
        {
            _service.DeleteDrink(value);
        }
    }
}
