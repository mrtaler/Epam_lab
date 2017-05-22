﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class User: IdentityUser
    {
       // public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

   
}
