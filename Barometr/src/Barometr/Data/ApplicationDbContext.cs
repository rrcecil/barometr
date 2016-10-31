using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Barometr.Models;

namespace Barometr.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BarReview> BarReviews { get; set; }
        public DbSet<DrinkReview> DrinkReviews { get; set; }
        public DbSet<UserBar> UserBars { get; set; }
        public DbSet<BarDrink> BarDrinks { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<BusinessHours> BusinessHours { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasMany(u => u.BarReviews).WithOne(r => r.User).IsRequired();
            builder.Entity<ApplicationUser>().HasMany(u => u.DrinkReviews).WithOne(r => r.User).IsRequired();
            builder.Entity<Drink>().HasMany(d => d.Reviews).WithOne(r => r.Drink);
            builder.Entity<Bar>().HasMany(b => b.Reviews).WithOne(r => r.Bar);
            builder.Entity<Profile>().HasOne(p => p.User).WithOne(u => u.Profile);
            builder.Entity<Bar>().HasMany(h => h.BusinessHours).WithOne(h => h.Bar);

            builder.Entity<UserBar>().HasKey(x => new { x.BarId, x.UserId });
            builder.Entity<BarDrink>().HasKey(x => new { x.BarId, x.DrinkId });


            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
