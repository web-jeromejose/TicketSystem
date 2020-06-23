using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string FullName { get; set; }
        public virtual string PHContactNumber { get; set; }
        public virtual int Age { get; set; }

        public bool IsAdmin { get; set; }

        /// <summary>
        /// The first name of the Technician
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the Technician
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Date the technician was hired
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Gets pay rate for this technician
        /// </summary>
        /// <returns>Pay rate</returns>
        public int GetPayRate()
        {
            return 30 + 10 * (int)((DateTime.Now - DateAdded).TotalDays / 365);
        }

    }
}