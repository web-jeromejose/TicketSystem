using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
        public static async Task Main(string[] args)
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
                        await SeedData.InitializeAsync(context, userManager, roleManager, configuration["userDefaultPassword"].ToString());
                    }
                    else
                    {
                        context.Database.Migrate();
                        var role = roleManager.FindByNameAsync(DataConstants.AdministratorRole).Result;
                        if (role == null)
                        {
                            // roleManager.CreateAsync(new IdentityRole(DataConstants.AdministratorRole));
                            await roleManager.CreateAsync(new IdentityRole { Name = DataConstants.AdministratorRole });
                        }
                    }

                    //add admin
                    await SeedData.CreateAdminAsync(context, userManager, roleManager, configuration["adminDefaultPassword"].ToString());
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            #endregion ForDB SEEDDATA

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