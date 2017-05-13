using System.ComponentModel.DataAnnotations;

namespace TicketSaleCore.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
