using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class BusinessHoursRepository : GenericRepository<BusinessHours>
    {
        public BusinessHoursRepository(ApplicationDbContext db) : base(db)
        {

        }

        public IQueryable<BusinessHours> GetHoursByBarId (int barId)
        {
            return _db.BusinessHours.Where(bh => bh.BarId == barId);
        }
    }
}
