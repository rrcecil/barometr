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

        public Bar GetBarById(int id)
        {
            return _db.Bars.FirstOrDefault(b => b.Id == id);
        }

        public Bar GetBarByUsername(string user)
        {
            var bar = _db.UserBars.Where(u => u.User.UserName == user).Select(u => u.Bar).FirstOrDefault();

            if (bar == null)
            {
                return new Bar
                {
                    Name = "New Test Bar",
                    PhoneNumber = "",
                    Longitude = .001,
                    Latitude = .012,
                    HappyHour = "5PM",
                    Menu = new List<Drink>(),
                    Reviews = new List<BarReview>()
                };
            }
            return bar;
        }

    }
}