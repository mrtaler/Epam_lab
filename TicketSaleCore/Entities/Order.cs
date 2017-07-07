using System.Collections.Generic;

namespace Entities
{
    public class Order
    {
        public Order()
        {
            OrderTickets=new List<Ticket>();
        }

        public int Id { get; set; }

        public virtual OrderStatus Status { get; set; }

        public string TrackNo { get; set; }

        public string BuyerId { get; set; }
        public virtual AppUser Buyer { get; set; }

        public  List<Ticket> OrderTickets { get; set; }
    }

    //public class OrderConfiguration : EntityTypeConfiguration<Order>
    //{
    //    public OrderConfiguration()
    //    {
    //        this.HasKey(t => t.Id);

    //        this.HasRequired<User>(t => t.Buyer)
    //            .WithMany(t => t.Orders)
    //            .HasForeignKey(t => t.BuyerId);


    //    }
    //}
}
