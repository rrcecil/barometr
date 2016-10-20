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
    public class BarReviewService
    {
        private BarReviewRepository _repo;

        public BarReviewService(BarReviewRepository repo)
        {
            _repo = repo;
        }

        public List<BarReviewDTO> GetMyReviews(string UserId)
        {
            var result = _repo.GetReviews().Where(r => r.UserId == UserId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }

        public BarReviewDTO GetReviewById (int Id)
        {
            var result = _repo.GetReviews().Where(r => r.Id == Id).Select(r => ProjectToViewModel(r)).FirstOrDefault();

            return result;
        }

        public List<BarReviewDTO> GetReviewsByBar (int BarId)
        {
            var result = _repo.GetReviews().Where(r => r.BarId == BarId).Select(r => ProjectToViewModel(r)).ToList();                   //added this
            return result;
        }

        

        public void AddReview(BarReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);
            
            _repo.Add(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }

        public void UpdateReview(BarReviewDTO r)
        {
            var review = _repo.List().FirstOrDefault(re => re.Id == r.Id);

            review.Comment = r.Comment;
            review.Rating = r.Rating;
             
            _repo.SaveChanges();
        }

        public void DeleteReview(BarReviewDTO r, string UserName)
        {
            var User = _repo.GetUserByUsername(UserName);
            _repo.Delete(ProjectToModel(r, User.Id));
            _repo.SaveChanges();
        }
        //TODO: Needs logic to determine if review is drink or bar review
        private BarReview ProjectToModel(BarReviewDTO r, string UserId)
        {
            return new BarReview
            {
                Comment = r.Comment,
                Rating = r.Rating,
                UserId = UserId,
                BarId = r.BarId
            };
        }
        private BarReviewDTO ProjectToViewModel(BarReview r)
        {
            return new BarReviewDTO
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                Username = r.User.UserName
            };
        }

        public ICollection<BarReviewDTO> GetReviewByName(string user)
        {
            var review = _repo.GetReviews().Where(r => r.User.UserName == user).Select(r => new BarReviewDTO
            {
                Comment = r.Comment,
                Id = r.Id,
                Rating = r.Rating,
                Username = r.User.UserName
            }).ToList();

            if (review.Count > 0)
                return review;

            var tempReview = new List<BarReviewDTO>();
            var rev = new BarReviewDTO
            {
                Comment = "Your profile has no reviews."
            };
            tempReview.Add(rev);

            return tempReview;
        }
    }
}
