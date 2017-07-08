using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicketSaleCore.Features.Cities.ViewModels
{
    public class CityEditCreateViewModel
    {
        public int Id
        {
            get; set;
        }
        [BindRequired]
        public string Name
        {
            get; set;
        }
    }
}
