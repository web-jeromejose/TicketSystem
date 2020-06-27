using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    /// <summary>
    /// Utility for seeding a GoldenTicket Context
    /// </summary>
    public static class SeedData
    {
        private static ApplicationUser[] _ApplicationUsers = {
            new ApplicationUser {
                FirstName = "Madeline",
                LastName = "Booth",
                IsAdmin = true
            },
            new ApplicationUser {
                FirstName = "Charles",
                LastName = "Woods",
                IsAdmin = true
            },
            new ApplicationUser {
                FirstName = "Nico",
                LastName = "Perkins",
                IsAdmin = true
            },
            new ApplicationUser {
                FirstName = "Marie",
                LastName = "Wilson",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Nancy",
                LastName = "Mays",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Taryn",
                LastName = "Norman",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Kieran",
                LastName = "Lam",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Natalya",
                LastName = "Lynch",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Gavin",
                LastName = "Preston",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Kira",
                LastName = "Paul",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Shyla",
                LastName = "Turner",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Ana",
                LastName = "Wise",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Rylan",
                LastName = "Bryan",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Cailyn",
                LastName = "Melton",
                IsAdmin = false
            },
            new ApplicationUser {
                FirstName = "Rory",
                LastName = "Clark",
                IsAdmin = false
            }
        };

        private static Client[] _clients = {
            new Client {
                Address = "19 Nicolls Court Rego Park, NY 11374",
                Company = "Openlane",
                EmailAddress = "Openlane",
                FirstName = "Elizebeth",
                LastName = "Salgado",
                PhoneNumber = "(738) 531-3531"
            },
            new Client {
                Address = "14 Front St. Winston Salem, NC 27103",
                Company = "Yearin",
                EmailAddress = "Yearin",
                FirstName = "Maddie",
                LastName = "Streater",
                PhoneNumber = "(485) 563-0017"
            },
            new Client {
                Address = "12 Galvin Lane Snohomish, WA 98290",
                Company = "Goodsilron",
                EmailAddress = "Goodsilron",
                FirstName = "Chrissy",
                LastName = "Noffsinger",
                PhoneNumber = "(755) 616-9245"
            },
            new Client {
                Address = "9 Honey Creek Road Victoria, TX 77904",
                Company = "Condax",
                EmailAddress = "Condax",
                FirstName = "Eufemia",
                LastName = "Max",
                PhoneNumber = "(637) 949-5699"
            },
            new Client {
                Address = "791 West Lower River Street Peabody, MA 01960",
                Company = "Opentech",
                EmailAddress = "Opentech",
                FirstName = "Teresa",
                LastName = "Honea",
                PhoneNumber = "(469) 853-6224"
            },
            new Client {
                Address = "67 Blue Spring St. Hillsboro, OR 97124",
                Company = "Golddex",
                EmailAddress = "Golddex",
                FirstName = "Kendrick",
                LastName = "Haydon",
                PhoneNumber = "(695) 261-7236"
            },
            new Client {
                Address = "61 Old Buttonwood Drive Grand Rapids, MI 49503",
                Company = "year-job",
                EmailAddress = "year-job",
                FirstName = "Napoleon",
                LastName = "Bernardo",
                PhoneNumber = "(205) 749-6785"
            },
            new Client {
                Address = "769 Elizabeth St. Greenfield, IN 46140",
                Company = "Isdom",
                EmailAddress = "Isdom",
                FirstName = "Jule",
                LastName = "Rigdon",
                PhoneNumber = "(295) 676-0045"
            },
            new Client {
                Address = "8728 Clay Ave. New City, NY 10956",
                Company = "Gogozoom",
                EmailAddress = "Gogozoom",
                FirstName = "Michaela",
                LastName = "Spady",
                PhoneNumber = "(684) 255-3602"
            },
            new Client {
                Address = "95 Wayne St. Reno, NV 89523",
                Company = "Y-corporation",
                EmailAddress = "Y-corporation",
                FirstName = "Derek",
                LastName = "Raley",
                PhoneNumber = "(366) 249-6137"
            },
            new Client {
                Address = "444 Saxon Court Deland, FL 32720",
                Company = "Nam-zim",
                EmailAddress = "Nam-zim",
                FirstName = "Lindsy",
                LastName = "Messineo",
                PhoneNumber = "(292) 765-7653"
            },
            new Client {
                Address = "9931 Roberts Ave. Columbus, GA 31904",
                Company = "Donquadtech",
                EmailAddress = "Donquadtech",
                FirstName = "Reggie",
                LastName = "Strohm",
                PhoneNumber = "(313) 365-0177"
            },
            new Client {
                Address = "611 Oakwood Rd. Hudson, NH 03051",
                Company = "Warephase",
                EmailAddress = "Warephase",
                FirstName = "Sheilah",
                LastName = "Troia",
                PhoneNumber = "(366) 970-8567"
            },
            new Client {
                Address = "210C Snake Hill Street Ambler, PA 19002",
                Company = "Donware",
                EmailAddress = "Donware",
                FirstName = "Vivien",
                LastName = "Modesto",
                PhoneNumber = "(725) 774-7198"
            },
            new Client {
                Address = "287 Grand St. North Olmsted, OH 44070",
                Company = "Faxquote",
                EmailAddress = "Faxquote",
                FirstName = "Evia",
                LastName = "Days",
                PhoneNumber = "(764) 373-1146"
            }
        };

        /// <summary>
        /// Initializes the ticket system with data
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="userManager">admin</param>
        /// <param name="roleManager"></param>
        public static async System.Threading.Tasks.Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, string UserPassword)
        {
            // Look for any data.

            //// 1. this is Alternative for data seeding using EFCOre
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            if (context.Tickets.Any())
            {
                return;   // DB has been seeded
            }

            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var randGenerator = new Random();

            foreach (var client in _clients)
            {
                client.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12));
            }

            await context.Clients.AddRangeAsync(_clients);

            await context.SaveChangesAsync();

            var role = roleManager.FindByNameAsync(DataConstants.AdministratorRole).Result;
            if (role == null)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = DataConstants.AdministratorRole });
            }

            foreach (var technician in _ApplicationUsers)
            {
                technician.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-36, -25));
                technician.EmailConfirmed = true;
                technician.UserName = $"{technician.FirstName.Trim()}@gmail.com";
                technician.Email = $"{technician.FirstName.Trim()}@gmail.com";

                IdentityResult result = await userManager.CreateAsync(technician, UserPassword.ToString());
                if (technician.IsAdmin)
                {
                    await userManager.AddToRoleAsync(technician, DataConstants.AdministratorRole);
                }
            }

            foreach (var client in context.Clients)
            {
                var ticketCount = randGenerator.Next(0, 15);
                for (var i = 0; i < ticketCount; i++)
                {
                    await context.Tickets.AddAsync(new Ticket
                    {
                        ClientId = client.Id,
                        Title = $"{client.Company}: Case {i}",
                        Description = $"Super awesome ticket {i}",
                        Complexity = i % 3 + 1,
                        IsUrgent = randGenerator.Next(0, 5) == 0,
                        Notes = "Terrible client to work with",
                        Open = randGenerator.Next(0, 2) == 0,
                        DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12))
                    });
                }
            }
            await context.SaveChangesAsync();

            foreach (var ticket in context.Tickets)
            {
                var workTimesCount = randGenerator.Next(0, 10);
                for (var i = 0; i < workTimesCount; i++)
                {
                    var start = ticket.DateAdded.AddHours(randGenerator.Next(1, 60));
                    var end = start.AddMinutes(randGenerator.Next(15, 60));
                    await context.TechnicianTicketTimes.AddAsync(new TechnicianTicketTime
                    {
                        Start = start,
                        End = end,
                        TechnicianId = context.Users.OrderBy(t => Guid.NewGuid()).Take(1).First().UserName,
                        TicketId = ticket.Id
                    });
                }
            }
            await context.SaveChangesAsync();
        }

        public static async System.Threading.Tasks.Task CreateAdminAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, string UserPassword)
        {
            try
            {
                var admin = userManager.FindByNameAsync(DataConstants.RootUsername).Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = DataConstants.RootUsername,
                        FirstName = DataConstants.RootUsername,
                        LastName = DataConstants.RootUsername,
                        DateAdded = DateTime.Now.AddYears(-2),
                        Email = "mesekarome@gmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        IsAdmin = true
                    };
                    var password = UserPassword.ToString();
                    IdentityResult result = await userManager.CreateAsync(admin, password);
                }
                if (!userManager.IsInRoleAsync(admin, DataConstants.AdministratorRole).Result)
                {
                    userManager.AddToRoleAsync(admin, DataConstants.AdministratorRole).Wait();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}