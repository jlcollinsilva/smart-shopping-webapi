using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;

namespace ShoppingSmartApp.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Default admin user
            var adminuser = await userManager.FindByNameAsync("systemadmin@shoppingsmart.com");
            if (adminuser == null)
            {
                // create user
                adminuser = new ApplicationUser
                {
                    UserName = "systemadmin@shoppingsmart.com",
                    Email = "systemadmin@shoppingsmart.com"
                };
                await userManager.CreateAsync(adminuser, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(adminuser, new Claim("IsAdmin", "true"));
            }

            // Default user Consumer
            var usrconsumer = await userManager.FindByNameAsync("usrconsumer@shoppingsmart.com");
            if (usrconsumer == null)
            {
                // create user
                usrconsumer = new ApplicationUser
                {
                    UserName = "usrconsumer@shoppingsmart.com",
                    Email = "usrconsumer@shoppingsmart.com"
                };
                await userManager.CreateAsync(usrconsumer, "Secret123!");
                // add claims
                await userManager.AddClaimAsync(usrconsumer, new Claim("IsConsumer", "true"));
            }
        }

    }
}
