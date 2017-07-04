using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.DTO
{
    public class EventsDTO
    {

        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }

        public int EventsTypeId
        {
            get; set;
        }
        // public virtual EventsType EventsType{get; set;}

        public string Banner
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }

        public int VenueId
        {
            get; set;
        }
        // public virtual Venue Venue{get; set;}

        // public virtual ICollection<Ticket> Tickets{get; set;}
    }
}
