using SQ021_First_Web_App.Models.Entity;

namespace SQ021_First_Web_App.Services.Interfaces
{
    public interface IImageService
    {
        Dictionary<int, string> UploadImage(IFormFile photo, Dog dog);
        bool DeleteImage(string existingUrl);
    }
}
