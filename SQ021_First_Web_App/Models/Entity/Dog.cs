using System.ComponentModel.DataAnnotations;

namespace SQ021_First_Web_App.Models.Entity
{
    public class Dog
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        public string? PhotoUrl { get; set; }
    }
}
