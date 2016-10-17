using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class UserBar
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [ForeignKey("BarId")]
        public Bar Bar { get; set; }
        public int BarId { get; set; }
    }

}
