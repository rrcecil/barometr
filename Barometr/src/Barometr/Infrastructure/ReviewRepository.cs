using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class ReviewRepository : GenericRepository<Review>
    {
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IQueryable<Review> GetReviews()
        {
            return _db.Reviews;
        }


        public Review GetReviewById(int id)
        {
            return _db.Reviews.FirstOrDefault(r => r.Id == id);
        }
    }
}