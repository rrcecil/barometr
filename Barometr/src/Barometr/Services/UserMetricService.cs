using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class UserMetricService
    {
       

        private BarReviewRepository _barRepo;
        private DrinkReviewRepository _drinkRepo;

        public UserMetricService( BarReviewRepository barRepo, DrinkReviewRepository drinkRepo)
        {
        
            _barRepo = barRepo;
            _drinkRepo = drinkRepo;
        }

        public int BarReviewCount(string username)
        {
            var user = _barRepo.GetUserByUsername(username);
            int barReviewCount = (from b in _barRepo.GetReviews().Where(b => b.UserId == user.Id)
                                  select b).Count();
            return barReviewCount;

        }

       
        public int DrinkReviewCount(string username)
        {
            var user = _barRepo.GetUserByUsername(username);
            int DrinkReviewCount = (from d in _drinkRepo.GetReviews().Where(b => b.UserId == user.Id)
                                    select d).Count();
            return DrinkReviewCount;

        }

        
    }
}









