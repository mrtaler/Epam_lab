using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSaleCore.Models.DBConfig;

namespace TicketSaleCore.Models
{
    public class City
    {
        public City()
        {
            Venues=new HashSet<Venue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }
    //public class CityConfiguration : EntityTypeConfiguration<City>
    //{
    //    public override void Map(EntityTypeBuilder<City> builder)
    //    {
    //        builder.HasKey(t => t.Id);
    //    }
    //}
}
