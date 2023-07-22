using BlogApi.ReadModel;
using CloudinaryDotNet.Actions;
using System.Net;

namespace BlogApi.Services.Cloudinary
{
    public class CloudinaryRepository : ICloudinaryRepository
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryRepository(CloudinaryDotNet.Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public ImageUploadResult UploadImage(IFormFile file, string folderName)
        {

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new CloudinaryDotNet.FileDescription(file.Name, stream),
                        Folder = "DominoBlog/" + folderName,
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if (uploadResult.Error != null)
                throw new HttpException(HttpStatusCode.BadRequest, "Cannot upload image");

            return uploadResult;

        }

        public string GetURL(string publicId)
        {
            var result = _cloudinary.Api.UrlImgUp.BuildUrl(publicId);

            return result;
        }

        public string Delete(string publicId)
        {

            var deleteParams = new DeletionParams(publicId);

            var result = _cloudinary.Destroy(deleteParams);

            if (result.Error != null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "Cannot delete image");
            }

            return result.Result;

        }
    }
}
