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
    public class ProfilesController : Controller
    {
        private ProfileService _service;
        public ProfilesController(ProfileService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        [Authorize]
        public ProfileDTO GetMyProfile()
        {
            return _service.GetProfileByEmail(User.Identity.Name);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProfileDTO Get(int id)
        {
            return _service.GetProfileById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ProfileDTO value)
        {
            _service.AddProfile(value, User.Identity.Name);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ProfileDTO value)
        {
            _service.UpdateProfile(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //_service.DeleteProfile(id);
        }
    }
}
