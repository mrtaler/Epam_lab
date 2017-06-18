using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSaleCore.Models;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.ViewModels
{
    public class UserTicketsViewModel
    {
        /// <summary>
        /// find users ticket
        /// </summary>
        /// <param name="context">Iunit of work</param>
        /// <param name="id">User Id</param>
        /// <param name="userTag">User Tag</param>
        public UserTicketsViewModel(IUnitOfWork context, string id,bool userTag)
        {
           
            //Find All User Selling Ticket
            SellingTickets = context.Tickets
                .Where(p => p.Seller.Id == id)
                .Where(z => z.Order == null)
                .Include(p => p.Event)
                .Include(p => p.Order)
                .Include(p => p.Seller).ToList();
            if (userTag)
            {
            //Find All User Selling Ticket Waiting for conformation
            WaitforConformationTickets = context.Tickets
                .Include(p => p.Order)
                .ThenInclude(p => p.Status)
                .Include(z => z.Order.Buyer)
                .Include(p => p.Seller)
                .Include(p => p.Event)
                .Where(p => p.Seller.Id == id)
                .Where(p => p.Order.Status.StatusName == "Waiting for conformation")
                .ToList();

            //Find All User Selling Ticket Sold
            ConfirmedTickets = context.Tickets
                .Include(p => p.Order)
                .ThenInclude(p => p.Status)
                .Include(z => z.Order.Buyer)
                .Include(p => p.Seller)
                .Include(p => p.Event)
                .Where(p => p.Seller.Id == id)
                .Where(p => p.Order.Status.StatusName == "Confirmed")
                .ToList();
            }
            else
            {
                WaitforConformationTickets=new List<Ticket>();
                ConfirmedTickets =new List<Ticket>();
            }
            UserCompareTag = userTag;
        }


        public List<Ticket> SellingTickets { get; set; }
        public List<Ticket> WaitforConformationTickets { get; set; }
        public List<Ticket> ConfirmedTickets { get; set; }

        public bool UserCompareTag { get; set; }
    }
}
