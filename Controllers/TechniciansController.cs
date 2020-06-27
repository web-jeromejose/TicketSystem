using System;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Controllers
{
    /// <summary>
    /// Controller for technicians
    /// </summary>
    [Authorize(Roles = DataConstants.AdministratorRole)]
    public class TechniciansController : Controller
    {
        private ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// intializes _context
        /// </summary>
        /// <param name="context">context of the technician</param>
        /// <param name="userManager">the usermanager</param>
        public TechniciansController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all technicians
        /// </summary>
        /// <returns>A list of all technicians</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var technicians = await _context.Users.ToListAsync();
            return View(technicians);
        }

        /// <summary>
        /// Gets the view for adding a technician
        /// </summary>
        /// <returns>The add technician view</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds a technician
        /// </summary>
        /// <returns>The technician list</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] NewTechnician newTechnician)
        {
            if (!ModelState.IsValid)
                return View(newTechnician);

            var technician = new ApplicationUser
            {
                DateAdded = DateTime.Now,
                // UserName = $"{newTechnician.FirstName}.{newTechnician.LastName}",
                UserName = newTechnician.Email.Trim(),
                Email = newTechnician.Email.Trim(),
                FirstName = newTechnician.FirstName.Trim(),
                LastName = newTechnician.LastName.Trim(),
                EmailConfirmed = true,
                IsAdmin = newTechnician.IsAdmin
            };
            IdentityResult result = await _userManager.CreateAsync(technician, newTechnician.Password);
            if (result.Succeeded)
            {
                if (technician.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(technician, DataConstants.AdministratorRole);
                }
            }
            else
            {
                Errors(result);
                return View(newTechnician);
            }

            return RedirectToAction(nameof(All));
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}