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
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string HappyHour { get; set; } // property?
        public List<Drink> Menu { get; set; } 
        public string GoogleBarId { get; set; }
        public string PlaceId { get; set; }

        public ICollection<BusinessHours> BusinessHours { get; set; }
        public ICollection<BarReview> Reviews { get; set; }
    }
}
