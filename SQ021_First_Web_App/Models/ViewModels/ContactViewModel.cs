using System.ComponentModel.DataAnnotations;

namespace SQ021_First_Web_App.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string RecipientEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
