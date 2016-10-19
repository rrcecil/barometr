using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class UserMetricRepository : GenericRepository<ApplicationUser>
    {
        public UserMetricRepository(ApplicationDbContext db) : base(db)
        {
    }
            public IQueryable<ApplicationUser> GetUsers()
        {
            return _db.Users;
        }

        
       
    }
}
