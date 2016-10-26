using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Barometr.Services;
using Barometr.ViewModels;
using Barometr.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class UserBarsController : Controller
    {
        private UserBarService _service;
        public UserBarsController(UserBarService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<UserBar> GetUserBars(string UserId)
        {
            var username = User.Identity.Name;
            return _service.GetUserBars(username);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserBar GetByBarId(int BarId)
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

        [HttpGet("userBars")]
        public IEnumerable<BarDTO> GetBarByUser()
        {
            var UserName = User.Identity.Name;
            return _service.GetBarByUser(UserName);
           
        }
    }
}
