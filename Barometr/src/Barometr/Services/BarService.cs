using Barometr.Infrastructure;
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
        private ReviewRepository _reviewRepo;
        public BarService(BarRepository repo, ReviewRepository reviewRepo )
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
                                   select new ReviewDTO()
                                   {
                                       Id = r.Id,
                                       Comment = r.Comment,
                                       Rating = r.Rating,
                                       Type = r.Type
                                       
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
                           select new ReviewDTO()
                           {
                               Id = r.Id,
                               Comment = r.Comment,
                               Rating = r.Rating,
                               Type = r.Type

                           }).ToList()

            }).FirstOrDefault();
            return bar;
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


    }    
}
   