using Microsoft.AspNetCore.Identity;

namespace SQ021_First_Web_App.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string ProfilePhoto { get; set; } = "";
    }
}
