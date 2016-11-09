﻿using Barometr.Infrastructure;
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
        private ProfileRepository _profileRepo;
        private DrinkRepository _drinkRepo;
        private DrinkReviewRepository _reviewRepo;
        private BarRepository _barRepo;

        public DrinkService(DrinkRepository drinkRepo, DrinkReviewRepository reviewRepo, BarRepository barRepo, ProfileRepository profileRepo)
        {
            _drinkRepo = drinkRepo;
            _reviewRepo = reviewRepo;
            _barRepo = barRepo;
            _profileRepo = profileRepo;
        }

        public List<DrinkDTO> GetAllDrinks()
        {
            var result = _drinkRepo.List().Select(d => ProjectToViewModel(d)).ToList();

            return result;
        }

        public DrinkDTO GetDrinkById(int id)
        {
            var result = _drinkRepo.List().Where(d => d.Id == id).Select(d => ProjectToViewModel(d)).FirstOrDefault();

            return result;
        }

        public List<DrinkDTO> GetDrinksByUserName(string user)
        {
            var userOwner = _barRepo.GetBarByUsername(user) ?? new Bar
            {
                HappyHour = "false",
                Name = "New Test Bar",
                PhoneNumber = "",
                Photo = "",
                Longitude = .001,
                Latitude = .012,
                Menu = new List<Drink>()
            };

            //Prevent Null Reference Errors
            if (userOwner.Menu == null)
                userOwner.Menu = new List<Drink>();

            return userOwner.Menu.Select(drink => new DrinkDTO
            {
                Abv = drink.Abv,
                Id = drink.Id,
                Ingredient = drink.Ingredient,
                Name = drink.Name,
                Type = drink.Type

            }).ToList();
        }

        public void AddDrink(DrinkDTO d, string user)
        {
            var drink = ProjectToModel(d);
            _drinkRepo.Add(drink);
            var bar = _barRepo.GetBarByUsername(user);

            //Prevent null ref exceptions from Menu not being initalized.
            if (bar.Menu == null)
            {
                bar.Menu = new List<Drink>();
            }
            bar.Menu.Add(drink);
            _drinkRepo.SaveChanges();
        }

        public void AddDrinkToList(DrinkDTO d, string user)
        {
            var drink = ProjectToModel(d);
            var bar = _barRepo.GetBarByUsername(user);
            if (bar.Menu == null)
            {
                bar.Menu = new List<Drink>();
            }
            bar.Menu.Add(drink);
        }

        private Drink ProjectToModel(DrinkDTO d)
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

    
        public void DeleteDrink(int id)
        {
            var orig = _drinkRepo.GetDrinkById(id);
            _drinkRepo.Delete(orig);
            _drinkRepo.SaveChanges();

            //public void DeleteDrink(DrinkDTO ProjectToModel(d));
            //_drinkRepo.Delete(ProjectToModel(d));
            //_drinkRepo.SaveChanges();
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
//method: if user picks spirit, display random drink based on user's preference
        public string RandomDrink(string username)
        {  
            var User = _drinkRepo.GetUserByUsername(username);
            int num = (int)DateTime.Today.ToBinary();
            Random random = new Random(num);
            int DrinkCount = (from d in _drinkRepo.List()
                              select d).Count();
            var userFaction = _profileRepo.List().Where(p => p.UserId == User.Id).Select(p => p.Faction).FirstOrDefault();

            if (DrinkCount == 0)
            {
                return "No drinks in Database";
            }

            List<string> drinkList = _drinkRepo.List().Where(d => d.Type == userFaction).Select(d => d.Name).ToList();
            if (drinkList.Count == 0)
            {
                return "No drinks found with same faction.";
            }
            int randomDrink = random.Next(DrinkCount - 1);
            return drinkList[randomDrink];

        }

    }
}




           



         


