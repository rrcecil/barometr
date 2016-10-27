using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class DrinkRepository : GenericRepository<Drink>
    {
        public DrinkRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}