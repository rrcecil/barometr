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
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string HappyHour { get; set; } // property?
        public List<Drink> Menu { get; set; } 

        public ICollection<Review> Reviews { get; set; }
    }
}
