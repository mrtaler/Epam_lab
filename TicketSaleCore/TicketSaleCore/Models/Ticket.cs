using System.Collections.Generic;
using TicketSaleCore.Models.IdentityWithoutEF;

namespace TicketSaleCore.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; }
        public string SellerNotes { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public string SellerId { get; set; }
        public AppUser Seller { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

    }
    //public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    //{
    //    public TicketConfiguration()
    //    {
    //        this.HasKey(t => t.Id);

    //        this.HasRequired<Event>(t => t.Event)
    //            .WithMany(t => t.Tickets)
    //            .HasForeignKey(t => t.EventId);

    //        this.HasRequired<User>(t => t.Seller)
    //           .WithMany(t => t.Tickets)
    //           .HasForeignKey(t => t.SellerId);

    //        this.HasOptional(t => t.Order)
    //     .WithRequired(tt => tt.Ticket);
    //    }
    //}
}
