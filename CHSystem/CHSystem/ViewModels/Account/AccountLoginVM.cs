using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}