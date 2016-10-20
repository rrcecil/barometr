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
        private BarRepository _barRepo;

        public DrinkService(DrinkRepository drinkRepo, ReviewRepository reviewRepo, BarRepository barRepo)
        {
            _drinkRepo = drinkRepo;
            _reviewRepo = reviewRepo;
            _barRepo = barRepo;
        }

        public List<DrinkDTO> GetAllDrinks()
        {
            var result = _drinkRepo.GetDrinks().Select(d => ProjectToViewModel(d)).ToList();

            return result;
        }

        public DrinkDTO GetDrinkById(int id)
        {
            var result = _drinkRepo.GetDrinks().Where(d => d.Id == id).Select(d => ProjectToViewModel(d)).FirstOrDefault();

            return result;
        }

        public List<DrinkDTO> GetDrinksByUserName(string user)
        {
            var userOwner = _barRepo.GetBarByUsername(user) ?? new Bar
            {
                HappyHour = "false",
                Name = "New Test Bar",
                Longitude = .001M,
                Latitude = .012M,
                Menu = new List<Drink>()
            };

            return userOwner.Menu.Select(drink => new DrinkDTO
            {
                Abv = drink.Abv,
                Id = drink.Id,
                Ingredient = drink.Ingredient,
                Name = drink.Name,
                Type = drink.Type
                
            }).ToList();
        }

        public void AddDrink (DrinkDTO d, string user)
        {
            var drink = ProjectToModel(d);
            _drinkRepo.Add(drink);
            var bar = _barRepo.GetBarByUsername(user);
            bar.Menu.Add(drink);
            _drinkRepo.SaveChanges();
        }

        public void AddDrinkToList(DrinkDTO d, string user)
        {
            var drink = ProjectToModel(d);
            var bar = _barRepo.GetBarByUsername(user);
            bar.Menu.Add(drink);
        }

        private Drink ProjectToModel (DrinkDTO d)
        {
            return new Drink
            {
                Name = d.Name,
                Type = d.Type,
                Ingredient = d.Ingredient,
                Abv = d.Abv,
                Id = d.Id
            };
        }

        public void UpdateDrink(DrinkDTO d)
        {
            var drink = _drinkRepo.List().FirstOrDefault(dr => dr.Id == d.Id);

            drink.Abv = d.Abv;
            drink.Ingredient = d.Ingredient;
            drink.Name = d.Name;
            drink.Type = d.Type;
             
            _drinkRepo.SaveChanges();
        }

        public void DeleteDrink(DrinkDTO d)
        {

            _drinkRepo.Delete(ProjectToModel(d));
            _drinkRepo.SaveChanges();
        }

        private DrinkDTO ProjectToViewModel(Drink d)
        {
            return new DrinkDTO
            {
                Id = d.Id,
                Ingredient = d.Ingredient,
                Name = d.Name,
                Abv = d.Abv,
                Type = d.Type
            };
        }
    }
}
