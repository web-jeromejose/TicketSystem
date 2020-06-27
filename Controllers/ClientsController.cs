using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.ViewModel;

namespace TicketSystem.Controllers
{
    /// <summary>
    /// Controller for Clients
    /// </summary>
    [Authorize]
    public partial class ClientsController : Controller
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initializes _context
        /// </summary>
        /// <param name="context">context of client</param>
        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Shows all clients
        /// </summary>
        /// <returns>clients page</returns>
        [HttpGet]
        public async Task<IActionResult> AllAsync()
        {
            //var clients = await _context.Clients
            //    .GroupJoin(
            //    _context.Tickets.Where(ticket => ticket.Open)
            //    , client => client.Id, ticket => ticket.ClientId,
            //    (client, tickets) =>
            //    new ClientDetails
            //    {
            //        Client = client,
            //        Tickets = tickets
            //    ,
            //        OpenTicketCount = tickets.Count()
            //    })
            //    .OrderByDescending(details => details.Tickets.Count())
            //    .ToListAsync();

            try
            {
                IEnumerable<Client> client = await _context.Clients
                .FromSqlRaw(@"SELECT c.* FROM dbo.Clients c ")
                .AsNoTracking().ToListAsync();
                List<ClientDetails> clientdetails = new List<ClientDetails>();
                foreach (var item in client)
                {
                    var tickets = await _context.Tickets
                        .Where(ticket => ticket.ClientId == item.Id)
                        .Where(ticket => ticket.Open == true)
                        .ToListAsync();

                    var details = new ClientDetails
                    {
                        Client = item,
                        OpenTicketCount = tickets.Count,
                        Tickets = tickets
                    };

                    clientdetails.Add(details);
                }
                return View(clientdetails);
            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }

        /// <summary>
        /// Opens a client's details
        /// </summary>
        /// <param name="id">The id of the client</param>
        /// <returns>The client</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            var tickets = await _context.Tickets.Where(ticket => ticket.ClientId == id).ToListAsync();

            var details = new ClientDetails
            {
                Client = client,
                OpenTicketCount = tickets.Count,
                Tickets = tickets
            };
            return View(details);
        }

        /// <summary>
        /// Gets the add client view
        /// </summary>
        /// <returns>The add client view.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds a client to the database
        /// </summary>
        /// <param name="client">The client to add</param>
        /// <returns>The added client</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Client client)
        {
            client.DateAdded = DateTime.Now;
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Open), new { id = client.Id });
        }

        /// <summary>
        /// Gets view for adding a ticket.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public IActionResult AddTicket([FromRoute] Guid id)
        {
            return View(new Ticket { ClientId = id });
        }

        /// <summary>
        /// Adds a ticket
        /// </summary>
        /// <param name="ticket">The ticket to be added</param>
        /// <returns>The added ticket</returns>
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromForm] Ticket ticket)
        {
            ticket.DateAdded = DateTime.Now;
            ticket.IsUrgent = false;
            ticket.Open = true;

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TicketsController.Open), "Tickets", new { id = ticket.Id });
        }
    }
}