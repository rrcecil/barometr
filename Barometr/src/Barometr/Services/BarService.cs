using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class BarService
    {
        private BarReviewService _reviewService;
        private BarRepository _barRepo;
        private BarReviewRepository _reviewRepo;
        public BarService(BarRepository repo, BarReviewRepository reviewRepo, BarReviewService reviewService)
        {
            _barRepo = repo;
            _reviewRepo = reviewRepo;
            _reviewService = reviewService;
        }
        public IList<BarDTO> GetBarDTO()
        {
            return (from b in _barRepo.List()
                    select new BarDTO
                    {
                        Id = b.Id,
                        Name = b.Name,
                        PhoneNumber = b.PhoneNumber,
                        Photo = b.Photo,
                        Latitude = b.Latitude,
                        Longitude = b.Longitude,
                        HappyHour = b.HappyHour,
                        GoogleBarId = b.GoogleBarId,
                        PlaceId = b.PlaceId,
                        Reviews = (from r in b.Reviews
                                   select new BarReviewDTO()
                                   {
                                       Id = r.Id,
                                       Comment = r.Comment,
                                       Rating = r.Rating

                                   }).ToList()
                    }).ToList();
        }
        public double GetAverageRating(int id)
        {
            var something = _reviewRepo.List().Where(r => r.BarId == id).Select(r => r.Rating);
            var ratingAverage = 0d;
            try
            {
                ratingAverage = something.Average();
            }
            catch
            {
                ratingAverage = 0;
            }

            var roundedRating = Math.Round(ratingAverage);
            return roundedRating;
        }

        public BarDTO GetBarById(int id)
        {
            if (_barRepo.List().Count() == 0)
            {
                return new BarDTO
                {
                    Name = "No bars in database"
                };
            }
            else
            {
                var bar = _barRepo.List().Where(b => b.Id == id).Select(b => new BarDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    PhoneNumber = b.PhoneNumber,
                    Photo = b.Photo,
                    Latitude = b.Latitude,
                    Longitude = b.Longitude,
                    HappyHour = b.HappyHour,
                    GoogleBarId = b.GoogleBarId,
                    Rating = GetAverageRating(b.Id),
                    PlaceId = b.PlaceId,
                    Reviews = (from r in b.Reviews
                               select new BarReviewDTO()
                               {
                                   Username = r.User.Name,
                                   DatePosted = _reviewService.GetPostTimelapse(r.DatePosted),
                                   Id = r.Id,
                                   Comment = r.Comment,
                                   Rating = (double)((int)r.Rating)
                               }).ToList()

                }).FirstOrDefault();
                return bar;
            }

        }

        public Bar GetActualBarById(int id)
        {
            return _barRepo.List().FirstOrDefault(b => b.Id == id);
        }

        public int AddBar(BarDTO bardto)
        {

            var bar = new Bar
            {
                Id = bardto.Id,
                Name = bardto.Name,
                PhoneNumber = bardto.PhoneNumber,
                Latitude = bardto.Latitude,
                Longitude = bardto.Longitude,
                HappyHour = bardto.HappyHour,
                PlaceId = bardto.PlaceId,
                GoogleBarId = bardto.GoogleBarId,
                Photo = bardto.Photo,
                Menu = new List<Drink>()
            };

            if (!IsBarDuplicate(bardto.GoogleBarId))
            {
                _barRepo.Add(bar);
                _barRepo.SaveChanges();
                int barId = bar.Id;
                return barId;
            }
            else
            {
                int barId = _barRepo.List().FirstOrDefault(b => b.GoogleBarId == bardto.GoogleBarId).Id;
                return barId;
            }
        }

        private bool IsBarDuplicate(string gbi)
        {
            var barExists = _barRepo.List().FirstOrDefault(b => b.GoogleBarId == gbi);
            if (barExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // update method
        public void UpdateBar(BarDTO bar)
        {
            var orig = _barRepo.GetBarById(bar.Id);
            orig.Id = bar.Id;
            orig.Name = bar.Name;
            orig.Photo = bar.Photo;
            orig.PhoneNumber = bar.PhoneNumber;
            orig.Latitude = bar.Latitude;
            orig.Longitude = bar.Longitude;
            orig.HappyHour = bar.HappyHour;
            _barRepo.SaveChanges();
        }

        // update method
        public void UpdateBar(int id, string phoneNumber)
        {
            var orig = _barRepo.GetBarById(id);
            orig.PhoneNumber = phoneNumber;
            _barRepo.SaveChanges();
        }

        //delete method
        public void DeleteBar(int id)
        {
            var orig = _barRepo.GetBarById(id);
            _barRepo.Delete(orig);
            _barRepo.SaveChanges();
        }

        //random method bar of the day
        public int RandomBar()
        {
            int num = (int)DateTime.Today.ToBinary();
            Random rand = new Random(num);
            int BarCount = (from b in _barRepo.List()
                            select b).Count();
            int randomBar = rand.Next(1, BarCount + 1);
            return randomBar;
        }

        public BarDTO GetBarByUserName(string user)
        {
            var bar = _barRepo.GetBarByUsername(user);

            return new BarDTO
            {
                HappyHour = bar.HappyHour,
                Id = bar.Id,
                Latitude = bar.Latitude,
                Longitude = bar.Longitude,
                Name = bar.Name,
                Photo = bar.Photo,
                PhoneNumber = bar.PhoneNumber,
                PlaceId = bar.PlaceId,
                //Reviews = (from r in bar.Reviews
                //           select new BarReviewDTO()
                //           {

                //               Id = r.Id,
                //               Comment = r.Comment,
                //               Rating = r.Rating
                //           }).ToList()
            };
        }
    }
}
