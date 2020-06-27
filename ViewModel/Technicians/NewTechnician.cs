using System;
using System.ComponentModel.DataAnnotations;
using TicketSystem.Data;

namespace TicketSystem.ViewModel
{
    /// <summary>
    /// For adding a new technician
    /// </summary>
    public class NewTechnician : ApplicationUser
    {
        /// <summary>
        /// The password of the technician
        /// </summary>
        [Required]
        public string Password { get; set; }

        [Required]
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}