using Count.Data;
using Count.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Seeder
{
    public class RoleSeeder : IRoleSeeder
    {
        public async Task SeedAsync(CountDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await SeedRolesAsync(roleManager, "Admin");
            await SeedUserWithRoleAdminAsync(userManager);

        }


        private async Task SeedUserWithRoleAdminAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Admin");
            if (user == null)
            {
                var result = await userManager.CreateAsync(new User
                {
                    UserName="Admin",
                    Email="kalinina.grace@gmail.com",
                    EmailConfirmed=true,
                    FirstName="Gratsiya",
                    LastName="Kalinina",
                    Country=0

                }, "Admin_12345");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    user = await userManager.FindByNameAsync("Admin");
                    await userManager.AddToRoleAsync(user, "Admin"); 
                }
            }

        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, string v)
        {

            var role = await roleManager.FindByNameAsync(v);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(v));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }


    }
}
