using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Barometr.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class UserMetricController : Controller
    {
        private UserMetricService _service;
        public UserMetricController(UserMetricService service)
        {
            _service = service;
        }

        [HttpGet("barReviewCount")]
        public IActionResult GetBarReviewCount()
        {
            var username = User.Identity.Name;

            var barReviewCount =_service.BarReviewCount(username);
            return Ok(barReviewCount);
        }

        
        [HttpGet("drinkReviewCount")]
        public IActionResult GetDrinkReviewCount()
        {
            var username = User.Identity.Name;

           var drinkReviewCount = _service.DrinkReviewCount(username);
            return Ok(drinkReviewCount);
        }

     
    }
}
