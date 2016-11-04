using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class BarReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime DatePosted { get; set; }
      

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int BarId { get; set; }
        [ForeignKey("BarId")]
        public Bar Bar { get; set; }

        
    }
}
