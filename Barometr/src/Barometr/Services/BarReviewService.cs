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
        private BarReviewRepository _barReviewRepo;
        private BarRepository _barRepo;

        public BarReviewService(BarReviewRepository repo, BarRepository barRepo)
        {
            _barReviewRepo = repo;
            _barRepo = barRepo;
        }

        public List<BarReviewDTO> GetMyReviews(string UserId)
        {
            var result = _barReviewRepo.List().Where(r => r.UserId == UserId).Select(r => ProjectToViewModel(r)).ToList();

            return result;
        }

        public BarReviewDTO GetReviewById (int Id)
        {
            var result = _barReviewRepo.List().Where(r => r.Id == Id).Select(r => ProjectToViewModel(r)).FirstOrDefault();

            return result;
        }

        public List<BarReviewDTO> GetReviewsByBar (int BarId)
        {
            var result = _barReviewRepo.List().Where(r => r.BarId == BarId).Select(r => ProjectToViewModel(r)).ToList();                   //added this
            return result;
        }

        

        public void AddReview(BarReviewDTO r, string UserName)
        {
            var User = _barReviewRepo.GetUserByUsername(UserName);
            
            _barReviewRepo.Add(ProjectToModel(r, User.Id));
            _barReviewRepo.SaveChanges();
        }

        public void UpdateReview(BarReviewDTO r)
        {
            var review = _barReviewRepo.List().FirstOrDefault(re => re.Id == r.Id);

            review.Comment = r.Comment;
            review.Rating = (int)r.Rating;
             
            _barReviewRepo.SaveChanges();
        }

        public void DeleteReview(BarReviewDTO r, string UserName)
        {
            var User = _barReviewRepo.GetUserByUsername(UserName);
            _barReviewRepo.Delete(ProjectToModel(r, User.Id));
            _barReviewRepo.SaveChanges();
        }
       
        private BarReview ProjectToModel(BarReviewDTO r, string UserId)
        {
            return new BarReview
            {
                Comment = r.Comment,
                Rating = (int)r.Rating,
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
        public double GetAverageRating(int id)
        {
            var ratingAverage = _barReviewRepo.List().Where(r => r.BarId == id).Select(r => r.Rating).Average();
            var roundedRating = Math.Round(ratingAverage);
            return roundedRating;
        }

        public ICollection<BarReviewDTO> GetReviewByName(string user)
        {

            var review = _barReviewRepo.List().Where(r => r.User.UserName == user).Select(r => new BarReviewDTO
            {

          bar  = new BarDTO()
          {
              Name = r.Bar.Name
          },

                Comment = r.Comment,
                Id = r.Id,
                Rating = r.Rating,
                Username = r.User.UserName,
              

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







