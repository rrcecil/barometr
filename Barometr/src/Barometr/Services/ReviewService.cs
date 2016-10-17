using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ReviewDTO> GetReviewsByDrink (int DrinkId)
        {
            var result = _repo.GetReviews().Where(r => r.DrinkId == DrinkId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
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
    }
}
