using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class BarReviewRepository : GenericRepository<BarReview>
    {
        public BarReviewRepository(ApplicationDbContext db) : base(db)
        {
        }
        
        public BarReview GetReviewById(int id)
        {
            return _db.BarReviews.FirstOrDefault(r => r.Id == id);
        }
    }
}