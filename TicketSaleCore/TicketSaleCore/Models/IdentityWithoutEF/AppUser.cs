using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketSaleCore.Models.IdentityWithoutEF
{
    public class AppUser: IdentityUser
    {
        public AppUser()
        {
            Orders=new HashSet<Order>();
            Tickets= new HashSet<Ticket>();
        }



        // public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }
       // public string PhoneNumber { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

   
}
