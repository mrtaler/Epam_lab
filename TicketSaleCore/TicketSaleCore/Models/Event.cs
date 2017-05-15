using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public byte[] Banner { get; set; }
        public int Description { get; set; }

    }
}
/*@if (item.Item_Image!=null){
                      @Html.Raw("<img style='height:60px;' src=\"data:image/jpeg;base64," 
+ Convert.ToBase64String(item.Item_Image) + "\" />")}*/
