using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Location { get; set; }
        public DateTime DOB { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }

}
