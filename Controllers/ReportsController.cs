using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.ViewModel;

namespace TicketSystem.Controllers
{
    /// <summary>
    /// For handling reports
    /// </summary>
    [Authorize(Roles = DataConstants.AdministratorRole)]
    public class ReportsController : Controller
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initializes this controller
        /// </summary>
        /// <param name="context">context of the technician</param>
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the report view
        /// </summary>
        /// <returns>The report view</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var _detail = _context.Users
                .Join(
                _context.TechnicianTicketTimes,
                 u => u.UserName,
                  time => time.TechnicianId,
                  (u, time) => new { u, Time = 8 }
                ).AsEnumerable()
              .Select(techTime => (techTime.u, techTime.Time)).Distinct()
                  .ToList()
                ;


            var grouped_detailUser = _detail;

            var details = new ReportDetails
            {
                AverageQueueLength = await _context.Tickets.Where(ticket => ticket.Open).CountAsync(),
                AverageWait = new TimeSpan(10, 5,40),
                EmptyQueuePercentage = 10,
                TicketsNotAddressedSameDay = 15
                ,
                TechnicianIdleHours = (List<(ApplicationUser Technician, int HoursIdle)>)grouped_detailUser

                // ,TechnicianIdleHours = await _context.Users

                //.GroupJoin(
                //    _context.TechnicianTicketTimes,
                //    technician => technician.UserName,
                //    time => time.TechnicianId,
                //    (technician, times) =>
                //    new { Technician = technician, Time = 8 })

                //.AsEnumerable()
                // .Select(techTime => (techTime.Technician, techTime.Time))
                // .ToList()
            };

            return View(details);
        }
    }
}