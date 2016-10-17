using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.ViewModels
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }

    }
}
