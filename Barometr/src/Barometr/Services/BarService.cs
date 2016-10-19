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
            Random rand = new Random((int)DateTime.Today.ToBinary()); 
            //Random random = new Random();
            int BarCount = (from b in _barRepo.List()
                            select b).Count();
            int randomBar = rand.Next(1, BarCount);
            return randomBar;
        }

    }    
}
   