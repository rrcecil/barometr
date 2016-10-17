using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.ViewModels
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Location { get; set; }
        public DateTime DOB { get; set; }

    }
}
