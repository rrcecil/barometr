using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barometr.Models;
using Microsoft.AspNetCore.Mvc;
using Barometr.ViewModels;
using Barometr.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Barometr.Controllers
{
    [Route("api/[controller]")]
    public class RequestsController : Controller
    {
        private RequestService _service;
        public RequestsController(RequestService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "isAdmin")]
        public ICollection<RequestsDTO> GetRequests()
        {
            return _service.GetRequests();
        }

        [HttpPost]
        [Authorize(Policy = "isAdmin")]
        public void AddRequest(Bar bar)
        {
            _service.AddRequest(bar, User.Identity.Name);
        }

        [HttpDelete]
        [Authorize(Policy = "isAdmin")]
        public void DeleteRequest(RequestsDTO dto)
        {
            _service.DeleteRequest(dto);
        }
    }
}
