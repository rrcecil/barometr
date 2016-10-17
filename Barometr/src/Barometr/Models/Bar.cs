using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Bar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatLong { get; set; }
        public string HappyHour { get; set; } // property?

        public ICollection<Review> Reviews { get; set; }
    }
}
