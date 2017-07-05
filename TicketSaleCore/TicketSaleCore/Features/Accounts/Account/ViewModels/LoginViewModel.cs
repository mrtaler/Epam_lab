using System.ComponentModel.DataAnnotations;

namespace TicketSaleCore.Features.Accounts.Account.ViewModels
{
    public class LoginViewModel
    {
       
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
