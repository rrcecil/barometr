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
        private ProfileService _profileService;
        private DrinkService _service;
        public DrinksController(DrinkService service, ProfileService profileService)
        {
            _service = service;
            _profileService = profileService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<DrinkDTO> Get()
        {
            return _service.GetAllDrinks();
        }

        // GET api/values/5
        [HttpGet("menu")]
        public ICollection<DrinkDTO> GetBarDrinks()
        {
            return _service.GetDrinksByUserName(User.Identity.Name);
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
            _service.AddDrink(value, User.Identity.Name);
        }

        // POST api/values
        [HttpPost("addTo")]
        public void PostToList([FromBody]DrinkDTO value)
        {
            _service.AddDrinkToList(value, User.Identity.Name);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]DrinkDTO value)
        {
            _service.UpdateDrink(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteDrink(id);
        }

        [HttpGet("randomDrink")]
        public string GetRandomDrink()
        {
            var username = User.Identity.Name;
            return _service.RandomDrink(username);
        }
    }
}
