using Microsoft.EntityFrameworkCore;
using SQ021_First_Web_App.Models.Entity;
using SQ021_First_Web_App.Services.Interfaces;

namespace SQ021_First_Web_App.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnv;
        //private readonly DbContext _ctx;
        public ImageService(IWebHostEnvironment webHostEnvironment/*, DbContext context*/)
        {
            _webHostEnv = webHostEnvironment;
            //_ctx = context;
        }

        public bool DeleteImage(string existingUrl)
        {
            if (string.IsNullOrEmpty(existingUrl)) throw new ArgumentNullException(nameof(existingUrl));

            if(File.Exists(existingUrl))
            {
                File.Delete(existingUrl);
                return true;
            }

            return false;
        }

        public Dictionary<int, string> UploadImage(IFormFile photo, Dog dog)
        {

            var response = new Dictionary<int, string>();

            string[] allowedImageTypes = ["image/jpg", "image/jpeg", "image/png"];
            
            if(photo == null) throw new ArgumentNullException(nameof(photo));

            if(photo.Length < 0 || photo.Length > 300000)
            {
                response.Add(400, "Invalid size");
                return response;
            }

            if(!allowedImageTypes.Contains(photo.ContentType))
            {
                response.Add(400, "Invalid image type");
                return response;
            }

            // set the host env path
            var hostEnvPath = _webHostEnv.WebRootPath + "/images";

            // set unique name for your photo
            var uniqueName = Guid.NewGuid().ToString() + "_" + photo.FileName; 
            
            // combine both name and path to get full path
            var fullPath = Path.Combine(hostEnvPath, uniqueName);

            // copy the filestream into the full path
            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fs);
            }

            response.Add(200, $"~/images/{uniqueName}");
            return response;
        }
    }
}
