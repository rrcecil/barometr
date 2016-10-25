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
        [Authorize(Policy = "AdminOnly")]
        public ICollection<RequestsDTO> GetRequests()
        {
            return _service.GetRequests();
        }

        [HttpPost("{id}")]
        public void AddRequest(int id)
        {
            _service.AddRequest(id, User.Identity.Name);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Accept/{id}")]
        public async Task<bool> ConfirmRequest(int id)
        {
            return await _service.ConfirmRequest(id);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Deny/{id}")]
        public void DenyRequest(int id)
        {
            _service.DeleteRequest(id);
        }

        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        public void DeleteRequest(RequestsDTO dto)
        {
            _service.DeleteRequest(dto.Id);
        }
    }
}
