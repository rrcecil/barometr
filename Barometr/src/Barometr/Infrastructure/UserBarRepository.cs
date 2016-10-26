using Barometr.Data;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class UserBarRepository : GenericRepository<UserBar>
    {
        public UserBarRepository(ApplicationDbContext db) : base(db)
        {


        }
        public IQueryable<UserBar> GetUserBars()
        {
            return _db.UserBars;
        }

   
    }
}