using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.Models.BLL.Infrastructure
{
    public class BllValidationException : Exception
    {
        public string Property
        {
            get; protected set;
        }
        public BllValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
