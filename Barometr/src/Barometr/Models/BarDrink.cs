﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Models
{
    public class BarDrink
    {
        [ForeignKey("BarId")]
        public Bar Bar { get; set; }
        public int BarId { get; set; }

        [ForeignKey("DrinkId")]
        public Drink Drink  { get; set; }
        public int DrinkId { get; set; }

        public ICollection<Bar> Bars { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}
