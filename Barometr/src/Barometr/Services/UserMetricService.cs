using Barometr.Infrastructure;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class UserMetricService
    {
        private UserMetricRepository _repo;
        private ReviewRepository _reviewRepo;
        private BarRepository _barRepo;
        private DrinkRepository _drinkRepo;
        public UserMetricService(UserMetricRepository repo, ReviewRepository reviewRepo, BarRepository barRepo, DrinkRepository drinkRepo)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _barRepo = barRepo;
            _drinkRepo = drinkRepo;
        }





        public int BarReviewCount(string username)
        {
            var user = _repo.GetUserByUsername(username);
            int barReviewCount = (from b in _reviewRepo.GetReviews().Where(b => b.UserId == user.Id)
                                  select b).Count();
            return barReviewCount;

        }

        //TODO drinkreviewcount
        public int DrinkReviewCount(string username)
        {
            var user = _repo.GetUserByUsername(username);
            int DrinkReviewCount = (from d in _reviewRepo.GetReviews().Where(b => b.UserId == user.Id)
                                    select d).Count();
            return DrinkReviewCount;

        }
    }
}

                                 

