using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Barometr.Services;
using Barometr.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class DrinkReviewsController : Controller
    {
        private DrinkReviewService _service;
        public DrinkReviewsController(DrinkReviewService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<DrinkReviewDTO> Get()
        {
            var UserId = User.Identity.Name;
            return _service.GetMyReviews(UserId);
        }

        // GET api/values/5
        [HttpGet("{myReviews}")]
        public ICollection<DrinkReviewDTO> GetByUser()
        {
            return _service.GetReviewByName(User.Identity.Name);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DrinkReviewDTO value)
        {
            var userName = User.Identity.Name;
            _service.AddReview(value, userName);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]DrinkReviewDTO value)
        {
            _service.UpdateReview(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(DrinkReviewDTO value)
        {
            var userName = User.Identity.Name;
            _service.DeleteReview(value, userName);
        }
    }
}
