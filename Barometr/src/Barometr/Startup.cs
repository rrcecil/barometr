using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Barometr.Data;
using Barometr.Models;
using Barometr.Services;
using Barometr.Infrastructure;

namespace Barometr
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<BarRepository>();
            services.AddScoped<UserBarRepository>();
            services.AddScoped<FavoriteBarRepository>();
            services.AddScoped<RequestsRepository>();
            services.AddScoped<DrinkRepository>();
            services.AddScoped<ProfileRepository>();
            services.AddScoped<BarReviewRepository>();
            services.AddScoped<DrinkReviewRepository>();
            services.AddScoped<BusinessHoursRepository>();

            //Services
            services.AddScoped<BarService>();
            services.AddScoped<UserBarService>();
            services.AddScoped<FavoriteBarService>();
            services.AddScoped<RequestService>();
            services.AddScoped<DrinkService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<BarReviewService>();
            services.AddScoped<DrinkReviewService>();
            services.AddScoped<UserMetricService>();
            services.AddScoped<BusinessHoursService>();



            // add security policies
            services.AddAuthorization(options =>
                        {
                            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin"));
                            options.AddPolicy("UserAdminOnly", policy => policy.RequireClaim("IsUserAdmin"));
                        });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "bower_components")),
                RequestPath = "/bower_components"
            });

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseTwitterAuthentication(new TwitterOptions {
                ConsumerKey = "R3KdnL7Tco0Kqb1xRmgTzNgdo",
                ConsumerSecret = "sqij1XwJpvJbc8QegZ9ypXXyrX3Vsz8AYBbsFJRfQdjYyRKaxz"
            });

            app.UseFacebookAuthentication(new FacebookOptions {
                AppId = "1153085474773635",
                AppSecret = "723a9ff9e94e21c778658779203bbd6d"
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{*path}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });

           // initialize sample data
           SampleData.Initialize(app.ApplicationServices).Wait();

        }
    }
}
