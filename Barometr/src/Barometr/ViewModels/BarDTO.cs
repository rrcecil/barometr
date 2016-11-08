using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.ViewModels
{
    public class BarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string HappyHour { get; set; }
        public string GoogleBarId { get; set; }
        public double Rating { get; set; }
        public string PlaceId { get; set; }

        public ICollection<BarReviewDTO> Reviews { get; set; } 

    }
}
