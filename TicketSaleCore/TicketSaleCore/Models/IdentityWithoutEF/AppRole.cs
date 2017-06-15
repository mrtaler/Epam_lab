﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketSaleCore.Models.IdentityWithoutEF
{
    public class AppRole:IdentityRole
    {
        public AppRole(string name) : base(name)
        {
            
        }
        public string Descr { get; set; }
    }
}