using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.ViewModels
{
    public class BarReviewDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public int BarId { get; set; }

        public BarDTO bar { get; set; }
    } 
}
