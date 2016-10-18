using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class DrinkService
    {
        private DrinkRepository _drinkRepo;
        private ReviewRepository _reviewRepo;

        public DrinkService(DrinkRepository drinkRepo, ReviewRepository reviewRepo)
        {
            _drinkRepo = drinkRepo;
            _reviewRepo = reviewRepo;
        }

        public List<DrinkDTO> GetAllDrinks()
        {
            var result = _drinkRepo.GetDrinks().Select(d => ProjectToViewModel(d)).ToList();

            return result;
        }

        public DrinkDTO GetDrinkById(int Id)
        {
            var result = _drinkRepo.GetDrinks().Where(d => d.Id == Id).Select(d => ProjectToViewModel(d)).FirstOrDefault();

            return result;
        }

        public void AddDrink (DrinkDTO d)
        {

            _drinkRepo.Add(ProjectToModel(d));
            _drinkRepo.SaveChanges();
        }

        private Drink ProjectToModel (DrinkDTO d)
        {
            return new Drink
            {
                Name = d.Name,
                Type = d.Type,
                Ingredient = d.Ingredient,
                Abv = d.Abv
            };
        }


        private DrinkDTO ProjectToViewModel(Drink d)
        {
            return new DrinkDTO
            {
                Id = d.Id,
                Ingredient = d.Ingredient,
                Name = d.Name,
                Abv = d.Abv,
                Type = d.Type,
                Reviews = _reviewRepo.List().Where(r => r.DrinkId == d.Id).Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    Username = r.User.UserName,
                    Type = r.Type
                }).ToList()
            };
        }
    }
}
