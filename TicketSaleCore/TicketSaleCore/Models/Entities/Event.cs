using System;
using System.Collections.Generic;

namespace TicketSaleCore.Models.Entities
{
    public class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }
        public Event(Event eEvent)
        {
            Tickets = eEvent.Tickets;
            Id = eEvent.Id;
            Name = eEvent.Name;
            Date = eEvent.Date;
            Banner = eEvent.Banner;
            Description = eEvent.Description;
            VenueId = eEvent.VenueId;
            Venue = eEvent.Venue;

        }


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public  int EventsTypeId { get; set; }
        public virtual EventsType EventsType { get; set; }

        public string Banner { get; set; }
        public string Description { get; set; }

        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
    //public class EventConfiguration : EntityTypeConfiguration<Event>
    //{
    //    public override void Map(EntityTypeBuilder<Event> builder)
    //    {
    //        builder.HasKey(t => t.Id);

    //        builder.HasOne<Venue>(t => t.Venue)
    //            .WithMany(t => t.Events)
    //            .HasForeignKey(t => t.VenueId);
    //    }
    //}
}
/*@if (item.Item_Image!=null){
                      @Html.Raw("<img style='height:60px;' src=\"data:image/jpeg;base64," 
+ Convert.ToBase64String(item.Item_Image) + "\" />")}*/
