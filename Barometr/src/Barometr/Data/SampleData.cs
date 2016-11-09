using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Barometr.Models;

namespace Barometr.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure Stephen (IsAdmin)
            var admin = await userManager.FindByEmailAsync("Admin@Barometr.com");
            if (admin == null)
            {
                // create user
                admin = new ApplicationUser
                {
                    Name = "Admin",
                    UserName = "Admin@Barometr.com",
                    Email = "Admin@Barometr.com",
                    Profile = new Profile
                        {
                            Name = "Admin",
                            Faction = "Beer",
                            DOB = DateTime.UtcNow,
                            Location = "Multiverse"
                        },
                    UserBar = new List<UserBar>(),
                    BarReviews = new List<BarReview>(),
                    DrinkReviews = new List<DrinkReview>()

                };
                await userManager.CreateAsync(admin, "Password123!");

                // add claims
                await userManager.AddClaimAsync(admin, new Claim("IsAdmin", "true"));
            }
        }

    }
}
