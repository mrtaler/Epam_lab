
namespace TicketSaleCore.Models.Entities
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    /// <summary>
    /// The app user.
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUser"/> class.
        /// </summary>
        public AppUser()
            : base()
        {
            this.Orders = new HashSet<Order>();
            this.Tickets = new HashSet<Ticket>();
        }

        // public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the localization.
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        // public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the tickets.
        /// </summary>
        public ICollection<Ticket> Tickets { get; set; }
    }
}
