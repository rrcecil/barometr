using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Barometr.Services
{
    public class RequestService
    {
        private RequestsRepository _repo;
        private BarRepository _barRepo;
        private UserBarRepository _userBarRepo;
        private UserManager<ApplicationUser> _userManager;

        public RequestService(RequestsRepository repo, BarRepository barRepo, UserBarRepository userBarRepo, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _repo = repo;
            _barRepo = barRepo;
            _userBarRepo = userBarRepo;
        }

        public ICollection<RequestsDTO> GetRequests()
        {
            var requests = _repo.List();

            var allRequests = new List<RequestsDTO>();
            foreach (var request in requests)
            {
                var bar = _barRepo.GetBarById(request.BarId);
                var user = _repo.GetUserById(request.UserId);
                var requestDTO = new RequestsDTO
                {
                    Id = request.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    PhoneNumber = bar.PhoneNumber,
                    BarName = bar.Name,
                    DateRequested = request.DateRequested
                };

                allRequests.Add(requestDTO);

            }
            return allRequests;
        }

        public void AddRequest(int id, string user)
        {
            var bar = _barRepo.GetBarById(id);
            var applicationUser = _repo.GetUserByUsername(user);

            var request = new Request
            {
                BarId = bar.Id,
                UserId = applicationUser.Id,
                DateRequested = DateTime.Now
            };

            _repo.Add(request);
            _repo.SaveChanges();
        }

        public Request GetRequestById(int id)
        {
            return _repo.List().FirstOrDefault(r => r.Id == id);
        }

        public void DeleteRequest(int id)
        {
            var request = GetRequestById(id);

            _repo.Delete(request);
            _repo.SaveChanges();

        }

        public async Task<bool> ConfirmRequest(int id)
        {
            //Grabs request by ID
            var request = GetRequestById(id);
            //Find the User of the Request
            var user = _repo.GetUserById(request.UserId);
            var bar = _barRepo.GetBarById(request.BarId);
            //Adds claim to make the user a UserAdmin
            var result = await _userManager.AddClaimAsync(user, new Claim("IsUserAdmin", "true"));
            //Create a new UserBar with the information from the request.
            UserBar userBar = new UserBar
            {
                Bar = bar,
                User = user,
                BarId = bar.Id,
                UserId = user.Id
            };
            //Add to UserBar database
            _userBarRepo.Add(userBar);
            _userBarRepo.SaveChanges();

            //Delete the request after we add the new UserBar to the database.
            DeleteRequest(id);

            return result.Succeeded;
        }

        public int GetRequestsAmount()
        {
            return _repo.List().Count();
        }
    }
}
