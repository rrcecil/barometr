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
        private BarRepository _barRepo;
        private UserBarRepository _userBarRepo;
        public UserBarService(UserBarRepository userBarRepo, BarRepository barRepo)
        {
            _userBarRepo = userBarRepo;
            _barRepo = barRepo;
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
            var result = _userBarRepo.GetUserBars().Where(b => b.UserId == UserId ).ToList();
            return result;
        }
        public int GetBarIdByGoogleBarId(string GoogleBarId)
        {
            var BarId = _barRepo.List().FirstOrDefault(b => b.GoogleBarId == GoogleBarId).Id;
            
            return BarId;
        }
    }
}
