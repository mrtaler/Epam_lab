using System.Collections.Generic;

namespace Entities
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
