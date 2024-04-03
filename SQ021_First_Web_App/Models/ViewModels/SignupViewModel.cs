using System.ComponentModel.DataAnnotations;

namespace SQ021_First_Web_App.Models.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength =3, ErrorMessage ="First name must be 15 length max and 3 length min")]
        public string FirstName { get; set; } = "";

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be 15 length max and 3 length min")]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatched!")]
        public string ConfirmPassword { get; set; } = "";
    }
}
