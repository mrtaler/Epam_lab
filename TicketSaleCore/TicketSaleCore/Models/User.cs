﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketSaleCore.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
