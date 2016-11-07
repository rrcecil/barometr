using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class UserBarService
    {
        private BarReviewRepository _barReviewRepo;
        private BarRepository _barRepo;
        private UserBarRepository _userBarRepo;
        public UserBarService(UserBarRepository userBarRepo, BarRepository barRepo, BarReviewRepository barReviewRepo)
        {
            _userBarRepo = userBarRepo;
            _barRepo = barRepo;
            _barReviewRepo = barReviewRepo;
        }

        public void Add(string Username, BarDTO b)
        {
            var GoogleBarId = b.GoogleBarId;


            var User = _userBarRepo.GetUserByUsername(Username);
            var UserId = User.Id;
            var UserBar = new UserBar
            {
                UserId = UserId,
                BarId = GetBarIdByGoogleBarId(GoogleBarId)
            };
            _userBarRepo.Add(UserBar);
            _userBarRepo.SaveChanges();
        }
        public List<UserBar> GetUserBars(string UserId)
        {
            var result = _userBarRepo.List().Where(b => b.UserId == UserId).ToList();
            return result;
        }
        public int GetBarIdByGoogleBarId(string GoogleBarId)
        {
            var BarId = _barRepo.List().FirstOrDefault(b => b.GoogleBarId == GoogleBarId).Id;

            return BarId;
        }


        public void AddClaim(UserBar userBar)
        {
            _userBarRepo.Add(userBar);
            _userBarRepo.SaveChanges();
        }

        public List<BarDTO> GetBarByUser(string UserName)
        {
            var userId = _userBarRepo.GetUserByUsername(UserName).Id;
            var userbar = _userBarRepo.List().Where(u => u.UserId == userId).Select(u =>u.BarId).ToList();
            var bar = _barRepo.List().Where(b => userbar.Contains(b.Id)).Select(b => new BarDTO
            {
                Id = b.Id,
                Name = b.Name,
                HappyHour = b.HappyHour,

                
            }).ToList();

            return bar;
        }

        public IEnumerable<BarDTO> GetMyBarsByUser(string userName)
        {
            var userId = _userBarRepo.GetUserByUsername(userName).Id;
        }
    }
}










            


        






