using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class BarRepository : GenericRepository<Bar>
    {
        public BarRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IQueryable<Bar> GetBars()
        {
            return _db.Bars;
        }


    }
}