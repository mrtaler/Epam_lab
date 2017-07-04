using System.Collections.Generic;

namespace TicketSaleCore.Models.Entities
{
    public class Venue
    {
        public Venue()
        {
            Events=new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int CityFk { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
    //public class VenueConfiguration : EntityTypeConfiguration<Venue>
    //{
    //    public VenueConfiguration()
    //    {
    //        this.HasKey(t => t.Id);

    //        this.HasRequired<City>(t => t.City)
    //            .WithMany(t => t.Venues)
    //            .HasForeignKey(t => t.CityId);

    //    }
    //}
}
