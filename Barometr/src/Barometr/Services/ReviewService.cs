using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class ReviewService
    {
        private ReviewRepository _repo;

        public ReviewService(ReviewRepository repo)
        {
            _repo = repo;
        }

        public List<ReviewDTO> GetMyReviews(string UserId)
        {
            var result = _repo.GetReviews().Where(r => r.UserId == UserId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }

        public ReviewDTO GetReviewById (int Id)
        {
            var result = _repo.GetReviews().Where(r => r.Id == Id).Select(r => ProjectToViewModel(r)).FirstOrDefault();

            return result;
        }

        public List<ReviewDTO> GetReviewsByBar (int BarId)
        {
            var result = _repo.GetReviews().Where(r => r.BarId == BarId).Select(r => ProjectToViewModel(r)).ToList();                   //added this
            return result;
        }

        public List<ReviewDTO> GetReviewsByDrink (int DrinkId)
        {
            var result = _repo.GetReviews().Where(r => r.DrinkId == DrinkId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }

        public void AddReview(ReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);
            
            _repo.Add(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }

        public void UpdateReview(ReviewDTO r)
        {
            var review = _repo.List().FirstOrDefault(re => re.Id == r.Id);

            review.Comment = r.Comment;
            review.Rating = r.Rating;
             
            _repo.SaveChanges();
        }

        public void DeleteReview(ReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);
            _repo.Delete(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }
        //TODO: Needs logic to determine if review is drink or bar review
        private Review ProjectToModel(ReviewDTO r, string UserId)
        {
            return new Review
            {
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = UserId,
                BarId = r.BarId,
                DrinkId = 1
            };
        }
        private ReviewDTO ProjectToViewModel(Review r)
        {
            return new ReviewDTO
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                Type = r.Type,
                Username = r.User.UserName
            };
        }

        public ICollection<ReviewDTO> GetReviewByName(string user)
        {
            var review = _repo.GetReviews().Where(r => r.User.UserName == user).Select(r => new ReviewDTO
            {
                Comment = r.Comment,
                Id = r.Id,
                Rating = r.Rating,
                Type = r.Type,
                Username = r.User.UserName
            }).ToList();

            if (review.Count > 0)
                return review;

            var tempReview = new List<ReviewDTO>();
            var rev = new ReviewDTO
            {
                Comment = "Your profile has no reviews."
            };
            tempReview.Add(rev);

            return tempReview;
        }
    }
}
