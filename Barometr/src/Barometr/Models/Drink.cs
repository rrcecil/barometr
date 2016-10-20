using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Ingredient { get; set; }
        public double Abv { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }   // type beer or drink

        public ICollection<DrinkReview> Reviews { get; set; }
    }
}
