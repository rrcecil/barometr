using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class UserBarService
    {
        private UserBarRepository _userBarRepo;
        public UserBarService(UserBarRepository repo)
        {
            _userBarRepo = repo;
        }

        public void Add(UserBar userBar)
        {
            _userBarRepo.Add(userBar);
            _userBarRepo.SaveChanges();
        }
    }
}
