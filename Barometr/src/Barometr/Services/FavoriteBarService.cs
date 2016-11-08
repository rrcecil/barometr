using System.Collections.Generic;
using System.Linq;
using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;

namespace Barometr.Services
{
    public class FavoriteBarService
    {
        private BarReviewRepository _barReviewRepo;
        private BarRepository _barRepo;
        private FavoriteBarRepository _userBarRepo;
        public FavoriteBarService(FavoriteBarRepository userBarRepo, BarRepository barRepo, BarReviewRepository barReviewRepo)
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
            var UserBar = new FavoriteBar
            {
                UserId = UserId,
                BarId = GetBarIdByGoogleBarId(GoogleBarId)
            };
            _userBarRepo.Add(UserBar);
            _userBarRepo.SaveChanges();
        }
        public List<FavoriteBar> GetUserBars(string UserId)
        {
            var result = _userBarRepo.List().Where(b => b.UserId == UserId).ToList();
            return result;
        }
        public int GetBarIdByGoogleBarId(string GoogleBarId)
        {
            var BarId = _barRepo.List().FirstOrDefault(b => b.GoogleBarId == GoogleBarId).Id;

            return BarId;
        }


        public void AddClaim(FavoriteBar userBar)
        {
            _userBarRepo.Add(userBar);
            _userBarRepo.SaveChanges();
        }

        public List<BarDTO> GetBarByUser(string UserName)
        {
            var userId = _userBarRepo.GetUserByUsername(UserName).Id;
            var userbar = _userBarRepo.List().Where(u => u.UserId == userId).Select(u => u.BarId).ToList();
            var bar = _barRepo.List().Where(b => userbar.Contains(b.Id)).Select(b => new BarDTO
            {
                Id = b.Id,
                Name = b.Name,
                PhoneNumber = b.PhoneNumber,
                Photo = b.Photo,
                HappyHour = b.HappyHour,
                GoogleBarId = b.GoogleBarId,
                Latitude = b.Latitude,
                Longitude = b.Longitude

            }).ToList();

            return bar;
        }
    }
}