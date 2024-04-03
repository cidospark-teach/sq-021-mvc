using System.ComponentModel.DataAnnotations;

namespace SQ021_First_Web_App.Models.ViewModels
{
    public class ImageUploadViewModel
    {
        [Required]
        public string Id { get; set; } = "";

        public IFormFile Photo { get; set; }

        public string DogName { get; set; } = "No name";
    }
}
