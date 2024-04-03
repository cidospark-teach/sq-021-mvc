using System.ComponentModel.DataAnnotations;

namespace SQ021_First_Web_App.Models.ViewModels
{
    public class UpdateDogViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "MaxLen is 15, MinLen is 3")]
        public string? Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 15, ErrorMessage = "MaxLen is 150, MinLen is 15")]
        public string? Description { get; set; }
    }
}
