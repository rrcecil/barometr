using Barometr.Data;
using Barometr.Models;
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


    }
}