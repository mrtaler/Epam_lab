﻿using System;
using System.Collections.Generic;
using System.Text;
using TicketSaleCore.Features.Tickets.ViewModels;
using TicketSaleCore.Models.Entities;
using TicketSaleCore.ViewModels;

namespace TicketSaleCore.Models.BLL.Interfaces
{
 public   interface ITicketsService
    {
        /// <summary>
        /// get all tickets without condition
        /// </summary>
        /// <returns>ticket list</returns>
        IEnumerable<Ticket> GetAllTicket();
        /// <summary>
        /// Get all event tickets 
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>event ticket list</returns>
        TicketIndexViewModel GetTicketByEvent(int? eventId);
        /// <summary>
        /// Get ticket
        /// </summary>
        /// <param name="ticketId">ticket id</param>
        /// <returns>ticket</returns>
        Ticket GetTicket(int? ticketId);
        /// <summary>
        /// check ticket exist
        /// </summary>
        /// <param name="id">ticket id</param>
        /// <returns></returns>
        bool TicketExists(int id);
        void Dispose();
    }
}
