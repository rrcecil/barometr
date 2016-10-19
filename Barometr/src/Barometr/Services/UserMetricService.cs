using Barometr.Infrastructure;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class UserMetricService
    {
        private UserMetricRepository _repo;
        public UserMetricService(UserMetricRepository repo)
        {
            _repo = repo;
        }

        public IList<ApplicationUser> GetUsers(int id)
        {

        }
    }
}
