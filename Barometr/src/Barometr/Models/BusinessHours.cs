using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class BusinessHours
    {
        public int Id { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public int Day { get; set; }

        [ForeignKey("BarId")]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
