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
        private BarRepository _barRepo;
        private BarReviewRepository _reviewRepo;
        public BarService(BarRepository repo, BarReviewRepository reviewRepo)
        {
            _barRepo = repo;
            _reviewRepo = reviewRepo;
        }
        public IList<BarDTO> GetBarDTO()
        {
            return (from b in _barRepo.List()
                    select new BarDTO
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Latitude = b.Latitude,
                        Longitude = b.Longitude,
                        HappyHour = b.HappyHour,
                        Reviews = (from r in b.Reviews
                                   select new BarReviewDTO()
                                   {
                                       Id = r.Id,
                                       Comment = r.Comment,
                                       Rating = r.Rating

                                   }).ToList()
                    }).ToList();
        }

        public BarDTO GetBarById(int id)
        {
            var bar = _barRepo.List().Where(b => b.Id == id).Select(b => new BarDTO
            {
                Id = b.Id,
                Name = b.Name,
                Latitude = b.Latitude,
                Longitude = b.Longitude,
                HappyHour = b.HappyHour,
                Reviews = (from r in b.Reviews
                           select new BarReviewDTO()
                           {
                               Id = r.Id,
                               Comment = r.Comment,
                               Rating = r.Rating


                           }).ToList()

            }).FirstOrDefault();
            return bar;
        }

        public Bar GetActualBarById(int id)
        {
            return _barRepo.List().FirstOrDefault(b => b.Id == id);
        }

        public void AddBar(BarDTO bardto)
        {

            var bar = new Bar
            {
                Name = bardto.Name,
                Latitude = bardto.Latitude,
                Longitude = bardto.Longitude,
                HappyHour = bardto.HappyHour,

            };
            _barRepo.Add(bar);
            _barRepo.SaveChanges();

        }


        // update method
        public void UpdateBar(BarDTO bar)
        {
            var orig = _barRepo.GetBarById(bar.Id);
            orig.Name = bar.Name;
            orig.Latitude = bar.Latitude;
            orig.Longitude = bar.Longitude;
            orig.HappyHour = bar.HappyHour;
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
            //Random random = new Random();
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
                Reviews = (from r in bar.Reviews
                           select new BarReviewDTO()
                           {
                               Id = r.Id,
                               Comment = r.Comment,
                               Rating = r.Rating
                           }).ToList()
            };
        }
    }
}
