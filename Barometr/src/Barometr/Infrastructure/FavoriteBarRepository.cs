using Barometr.Data;
using Barometr.Models;

namespace Barometr.Infrastructure
{
    public class FavoriteBarRepository : GenericRepository<FavoriteBar>
    {
        public FavoriteBarRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}