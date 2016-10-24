using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BarId { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
