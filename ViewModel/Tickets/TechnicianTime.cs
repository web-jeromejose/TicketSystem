using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.ViewModel
{
    /// <summary>
    /// Tech time model
    /// </summary>
    public class TechnicianTime
    {
        /// <summary>
        /// Get's technician
        /// </summary>
        /// <returns>technician</returns>
        public ApplicationUser Technician { get; set; }

        /// <summary>
        /// get's time
        /// </summary>
        /// <returns>technician's ticket time</returns>
        public TechnicianTicketTime Time { get; set; }
    }
}