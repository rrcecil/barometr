using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class DrinkReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int DrinkId { get; set; }
        [ForeignKey("DrinkId")]
        public Drink Drink { get; set; }
    }
}
