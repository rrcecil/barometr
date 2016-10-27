using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class DrinkReviewService
    {
        private DrinkReviewRepository _repo;

        public DrinkReviewService(DrinkReviewRepository repo)
        {
            _repo = repo;
        }

        public List<DrinkReviewDTO> GetReviewsByDrink(int DrinkId)
        {
            var result = _repo.List().Where(r => r.DrinkId == DrinkId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }
        public List<DrinkReviewDTO> GetMyReviews(string UserId)
        {
            var result = _repo.List().Where(r => r.UserId == UserId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }

        public DrinkReviewDTO GetReviewById(int Id)
        {
            var result = _repo.List().Where(r => r.Id == Id).Select(r => ProjectToViewModel(r)).FirstOrDefault();

            return result;
        }

        public List<DrinkReviewDTO> GetReviewsByBar(int BarId)
        {
            var result = _repo.List().Where(r => r.DrinkId == BarId).Select(r => ProjectToViewModel(r)).ToList();                   //added this
            return result;
        }



        public void AddReview(DrinkReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);

            _repo.Add(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }

        public void UpdateReview(DrinkReviewDTO r)
        {
            var review = _repo.List().FirstOrDefault(re => re.Id == r.Id);

            review.Comment = r.Comment;
            review.Rating = r.Rating;

            _repo.SaveChanges();
        }

        public void DeleteReview(DrinkReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);
            _repo.Delete(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }
        //TODO: Needs logic to determine if review is drink or bar review
        private DrinkReview ProjectToModel(DrinkReviewDTO r, string UserId)
        {
            return new DrinkReview
            {
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = UserId,
                DrinkId = r.DrinkId
            };
        }
        private DrinkReviewDTO ProjectToViewModel(DrinkReview r)
        {
            return new DrinkReviewDTO
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                Username = r.User.UserName
            };
        }

        public ICollection<DrinkReviewDTO> GetDrinkReviewByName(string user)
        {
            var drinkreview = _repo.List().Where(r => r.User.UserName == user).Select(r => new DrinkReviewDTO
            {
                Comment = r.Comment,
                Id = r.Id,
                Rating = r.Rating,
                Username = r.User.UserName
            }).ToList();

            if (drinkreview.Count > 0)
                return drinkreview;

            var tempReview = new List<DrinkReviewDTO>();
            var rev = new DrinkReviewDTO
            {
                Comment = "Your profile has no reviews."
            };
            tempReview.Add(rev);

            return tempReview;
        }
    }
}
