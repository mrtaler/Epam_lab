using System.Collections.Generic;

namespace TicketSaleCore.Models.Entities
{
    public class OrderStatus
    {
        public OrderStatus()
        {
            Orders=new HashSet<Order>();
        }
        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
    //public class StatusConfiguration : EntityTypeConfiguration<Status>
    //{
    //    public StatusConfiguration()
    //    {
    //        this.HasKey(t => t.Id);

    //        this.HasMany<Order>(t => t.Orders)
    //            .WithRequired(t => t.Status);
    //    }
    //}
}
