using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class DrinkReviewRepository: GenericRepository<DrinkReview>
    {
        public DrinkReviewRepository(ApplicationDbContext db) : base(db)
        {

        }

        public DrinkReview GetReviewById(int id)
        {
            return _db.DrinkReviews.FirstOrDefault(r => r.Id == id);
        }
    }
}

