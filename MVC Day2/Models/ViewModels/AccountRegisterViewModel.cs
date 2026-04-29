using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Display(Name = "User Name")]
        [Required]
        public required string Username { get; set; }

        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare(otherProperty: "Password")]
        public required string ConfirmPassword { get; set; }

        public string? Address { get; set; }
    }
}
