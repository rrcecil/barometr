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
    public class BarReviewsController : Controller
    { 
        private BarReviewService _service;
        public BarReviewsController(BarReviewService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<BarReviewDTO> Get()
        {
            var UserId = User.Identity.Name;
            return _service.GetMyReviews(UserId);
        }

        // GET api/values/5
        [HttpGet("myReviews")]
        public ICollection<BarReviewDTO> GetByUser()
        {
            return _service.GetReviewByName(User.Identity.Name);
        }

        //[HttpGet("drink/{id}")]
        //public ICollection<BarReviewDTO> GetDrinkReview(int id)
        //{
        //    return _service.GetReviewsByDrink(id);
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]BarReviewDTO value)

        {
            var userName = User.Identity.Name;
            _service.AddReview(value, userName);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]BarReviewDTO value)
        {
            _service.UpdateReview(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(BarReviewDTO value)
        {
            var userName = User.Identity.Name;
            _service.DeleteReview(value, userName);
        }
    }
}
