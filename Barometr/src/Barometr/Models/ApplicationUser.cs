using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Barometr.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public ICollection<UserBar> UserBar { get; set; }

        public ICollection<BarReview> BarReviews { get; set; }

        public ICollection<DrinkReview> DrinkReviews { get; set; }

        public Profile Profile { get; set; }
    }
}
