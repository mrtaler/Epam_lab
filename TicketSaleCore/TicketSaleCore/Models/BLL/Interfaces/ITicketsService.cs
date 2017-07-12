using System.Collections.Generic;
using TicketSaleCore.Features.Tickets.Tickets.ViewModels;
using TicketSaleCore.Models.BLL.Infrastructure;
using TicketSaleCore.Models.Entities;
using static TicketSaleCore.Models.BLL.Infrastructure.TicketStatusEnum;

namespace TicketSaleCore.Models.BLL.Interfaces
{
    public interface ITicketsService : IBllService<Ticket>
    {
        /// <summary>
        /// Get all event tickets 
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>event ticket list</returns>
        TicketIndexViewModel GetTicketByEvent(int? eventId);
      /// <summary>
        /// Get all User Ticket
        /// </summary>
        /// <returns></returns>
        IEnumerable<Ticket> GetAllUserTickets(string userId, TicketStatus param);
    }
}
