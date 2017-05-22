namespace TicketSaleCore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public virtual Status Status { get; set; }

        public string TrackNo { get; set; }

        public string BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public int TicketFk { get; set; }
        public virtual Ticket Ticket { get; set; }
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
