using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Request
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Bar Bar { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
