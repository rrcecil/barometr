using System.Collections.Generic;
using Barometr.Models;
using Barometr.Services;
using Barometr.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class FavoriteBarController : Controller
    {
        private FavoriteBarService _service;
        public FavoriteBarController(FavoriteBarService service)
        {
            _service = service;
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<UserBar> GetUserBars(string UserId)
        //{
        //    var username = User.Identity.Name;
        //    return _service.GetUserBars(username);
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public FavoriteBar GetByBarId(int BarId)
        {
            return null;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]BarDTO value)
        {
            _service.Add(User.Identity.Name, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("favoriteBars")]
        [Authorize]
        public IEnumerable<BarDTO> GetBarByUser()
        {
            var UserName = User.Identity.Name;
            return _service.GetBarByUser(UserName);

        }
    }
}