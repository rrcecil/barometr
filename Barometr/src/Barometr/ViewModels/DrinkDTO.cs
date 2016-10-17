using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.ViewModels
{
    public class DrinkDTO
    {
        public int Id { get; set; }
        public string Ingredient { get; set; }
        public double Abv { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
         
    }
}
