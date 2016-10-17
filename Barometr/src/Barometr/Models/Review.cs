using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string Type { get; set; } // bar or drink

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string BarId { get; set; }
        [ForeignKey("BarId")]
        public Bar Bar { get; set; }

        public string DrinkId { get; set; }
        [ForeignKey("DrinkId")]
        public Drink Drink { get; set; }
    }
}
