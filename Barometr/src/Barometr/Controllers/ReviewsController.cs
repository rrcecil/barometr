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
    public class ReviewsController : Controller
    {
        private ReviewService _service;
        public ReviewsController(ReviewService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ReviewDTO> Get()
        {
            var UserId = User.Identity.Name;
            return _service.GetMyReviews(UserId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ReviewDTO Get(int id)
        {
            return _service.GetReviewById(id);
        }

        [HttpGet("drink/{id}")]
        public ICollection<ReviewDTO> GetDrinkReview(int id)
        {
            return _service.GetReviewsByDrink(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ReviewDTO value)
        {
            _service.AddReview(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ReviewDTO value)
        {
            _service.UpdateReview(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(ReviewDTO value)
        {
            _service.DeleteReview(value);
        }
    }
}
