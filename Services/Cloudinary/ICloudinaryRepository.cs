using CloudinaryDotNet.Actions;

namespace BlogApi.Services.Cloudinary
{
    public interface ICloudinaryRepository
    {
        public ImageUploadResult UploadImage(IFormFile file, string folderName);
        public string GetURL(string publicId);
        public string Delete(string publicId);
    }
}
