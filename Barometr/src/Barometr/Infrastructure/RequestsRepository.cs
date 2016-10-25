using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barometr.Data;
using Barometr.Models;

namespace Barometr.Infrastructure
{
    public class RequestsRepository : GenericRepository<Request> 
    {
        public RequestsRepository(ApplicationDbContext db) : base(db)
        {
            
        }

        public IQueryable<Request> GetRequests()
        {
            return _db.Requests;
        }
    }
}
