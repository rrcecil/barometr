using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;

namespace Barometr.Services
{
    public class RequestService
    {
        private RequestsRepository _repo;
        private BarRepository _barRepo;

        public RequestService(RequestsRepository repo, BarRepository barRepo)
        {
            _repo = repo;
            _barRepo = barRepo;
        }

        public ICollection<RequestsDTO> GetRequests()
        {
            var requests = _repo.GetRequests();

            var alLRequests = new List<RequestsDTO>();
            foreach (var request in requests)
            {
                var requestDTO = new RequestsDTO
                {
                    Id = request.Id,
                    UserName = request.User.UserName,
                    UserEmail = request.User.Email,
                    BarName = request.Bar.Name,
                    DateRequested = request.DateRequested
                };

                alLRequests.Add(requestDTO);

            }
            return alLRequests;
        }

        public void AddRequest(Bar bar, string user)
        {
            var applicationUser = _repo.GetUserByUsername(user);

            var request = new Request
            {
                Bar = bar,
                User = applicationUser,
                DateRequested = DateTime.Now
            };

            _repo.Add(request);
            _repo.SaveChanges();
        }

        public Request GetRequestById(int id)
        {
            return _repo.GetRequests().FirstOrDefault(r => r.Id == id);
        }

        public void DeleteRequest(RequestsDTO dto)
        {
            var request = GetRequestById(dto.Id);

            _repo.Delete(request);
            _repo.SaveChanges();

        }
    }
}
