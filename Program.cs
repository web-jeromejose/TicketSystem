using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketSystem;
using TicketSystem.Data;

namespace TicketSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            #region ForDB SEEDDATA

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetService<RoleManager<IdentityRole>>();


                    if (configuration.GetValue<bool>("useSeedData"))
                    {
                        SeedData.Initialize(context, userManager, roleManager);
                    }
                    else
                    {
                        context.Database.Migrate();
                        var role = roleManager.FindByNameAsync(DataConstants.AdministratorRole).Result;
                        if (role == null)
                        {
                            roleManager.CreateAsync(new IdentityRole(DataConstants.AdministratorRole));
                        }
                    }

                    var admin = userManager.FindByNameAsync(DataConstants.RootUsername).Result;
                    if (admin == null)
                    {
                        admin = new ApplicationUser
                        {
                            UserName = DataConstants.RootUsername,
                            FirstName = DataConstants.RootUsername,
                            LastName = DataConstants.RootUsername,
                            DateAdded = DateTime.Now.AddYears(-2),
                            IsAdmin = true
                        };
                        userManager.CreateAsync(admin, configuration["adminPassword"]).Wait();
                    }
                    if (!userManager.IsInRoleAsync(admin, DataConstants.AdministratorRole).Result)
                    {
                        userManager.AddToRoleAsync(admin, DataConstants.AdministratorRole).Wait();
                    }



                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            #endregion ForDBInitializer

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
