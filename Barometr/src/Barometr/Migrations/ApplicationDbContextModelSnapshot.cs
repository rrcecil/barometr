using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Barometr.Data;

namespace Barometr.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Barometr.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Barometr.Models.Bar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("BarDrinkBarId");

                    b.Property<int?>("BarDrinkDrinkId");

                    b.Property<string>("GoogleBarId");

                    b.Property<string>("HappyHour");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("BarDrinkBarId", "BarDrinkDrinkId");

                    b.ToTable("Bars");
                });

            modelBuilder.Entity("Barometr.Models.BarDrink", b =>
                {
                    b.Property<int>("BarId");

                    b.Property<int>("DrinkId");

                    b.HasKey("BarId", "DrinkId");

                    b.HasIndex("BarId");

                    b.HasIndex("DrinkId");

                    b.ToTable("BarDrinks");
                });

            modelBuilder.Entity("Barometr.Models.BarReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BarId");

                    b.Property<string>("Comment");

                    b.Property<double>("Rating");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("UserId");

                    b.ToTable("BarReviews");
                });

            modelBuilder.Entity("Barometr.Models.BusinessHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BarId");

                    b.Property<string>("CloseTime");

                    b.Property<int>("Day");

                    b.Property<string>("OpenTime");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.ToTable("BusinessHours");
                });

            modelBuilder.Entity("Barometr.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Abv");

                    b.Property<int?>("BarDrinkBarId");

                    b.Property<int?>("BarDrinkDrinkId");

                    b.Property<int?>("BarId");

                    b.Property<string>("Ingredient");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("BarDrinkBarId", "BarDrinkDrinkId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("Barometr.Models.DrinkReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("DrinkId");

                    b.Property<int>("Rating");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.HasIndex("UserId");

                    b.ToTable("DrinkReviews");
                });

            modelBuilder.Entity("Barometr.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Faction");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Barometr.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BarId");

                    b.Property<DateTime>("DateRequested");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Barometr.Models.UserBar", b =>
                {
                    b.Property<int>("BarId");

                    b.Property<string>("UserId");

                    b.HasKey("BarId", "UserId");

                    b.HasIndex("BarId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBars");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Barometr.Models.Bar", b =>
                {
                    b.HasOne("Barometr.Models.BarDrink")
                        .WithMany("Bars")
                        .HasForeignKey("BarDrinkBarId", "BarDrinkDrinkId");
                });

            modelBuilder.Entity("Barometr.Models.BarDrink", b =>
                {
                    b.HasOne("Barometr.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barometr.Models.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Barometr.Models.BarReview", b =>
                {
                    b.HasOne("Barometr.Models.Bar", "Bar")
                        .WithMany("Reviews")
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barometr.Models.ApplicationUser", "User")
                        .WithMany("BarReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Barometr.Models.BusinessHours", b =>
                {
                    b.HasOne("Barometr.Models.Bar", "Bar")
                        .WithMany("BusinessHours")
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Barometr.Models.Drink", b =>
                {
                    b.HasOne("Barometr.Models.Bar")
                        .WithMany("Menu")
                        .HasForeignKey("BarId");

                    b.HasOne("Barometr.Models.BarDrink")
                        .WithMany("Drinks")
                        .HasForeignKey("BarDrinkBarId", "BarDrinkDrinkId");
                });

            modelBuilder.Entity("Barometr.Models.DrinkReview", b =>
                {
                    b.HasOne("Barometr.Models.Drink", "Drink")
                        .WithMany("Reviews")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barometr.Models.ApplicationUser", "User")
                        .WithMany("DrinkReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Barometr.Models.Profile", b =>
                {
                    b.HasOne("Barometr.Models.ApplicationUser", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Barometr.Models.Profile", "UserId");
                });

            modelBuilder.Entity("Barometr.Models.UserBar", b =>
                {
                    b.HasOne("Barometr.Models.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barometr.Models.ApplicationUser", "User")
                        .WithMany("UserBar")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Barometr.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Barometr.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Barometr.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
