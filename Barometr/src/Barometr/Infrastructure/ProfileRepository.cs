using Barometr.Data;
using Barometr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Infrastructure
{
    public class ProfileRepository : GenericRepository<Profile>
    {
        public ProfileRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IQueryable<Profile> GetProfiles()
        {
            return _db.Profiles;
        }


        public Profile GetProfileById(int id)
        {
            return _db.Profiles.FirstOrDefault(p => p.Id == id);
        }
    }
}