using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Count.Data;
using Count.Models;
using Count.App.Seeder;
using Count.App.Services.Inderfaces;
using Count.App.Services;
using Count.App.Areas.Admin.Services.Interfaces;
using Count.App.Areas.Admin.Services;

namespace Count.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CountDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CountDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminPostService, AdminPostService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminBmiService, AdminBmiService>();
            services.AddScoped<IAdminDayService, AdminDayService>();
            services.AddScoped<IAdminFoodService, AdminFoodService>();
            services.AddScoped<IAdminMealService, AdminMealService>();
            services.AddScoped<IBmiService, BmiService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IUserService, UserService>();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope=app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<CountDbContext>();
                dbContext.Database.Migrate();
                new RoleSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult(); 

            }
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    name:"area","Admin",
                    pattern:"{area:exists }/{controller}/{action}/{id?}");
            });
        }
    }
}
