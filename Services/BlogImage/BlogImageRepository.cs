using BlogApi.Data;
using BlogApi.ReadModel;
using BlogApi.Services.Cloudinary;
using System.Net;

namespace BlogApi.Services.BlogImage
{
    public class BlogImageRepository : IBlogImageRepository
    {
        private readonly MyDBContext _context;
        private readonly ICloudinaryRepository _cloudinaryRepo;

        public BlogImageRepository(MyDBContext context, ICloudinaryRepository cloudinaryRepo)
        {
            _context = context;
            _cloudinaryRepo = cloudinaryRepo;
        }

        public List<BlogImageRM> Create(IFormFileCollection images, Guid BlogId)
        {
            var result = new List<BlogImageRM>();
            foreach (var item in images)
            {
                var uploadImageResult = _cloudinaryRepo.UploadImage(item, "blogs");
                var newBlogImage = new Data.BlogImage
                {
                    BlogId = BlogId,
                    Source = uploadImageResult.PublicId
                };

                result.Add(new BlogImageRM
                {
                    Id = newBlogImage.Id,
                    Source = uploadImageResult.SecureUrl.ToString()
                });

                _context.Add(newBlogImage);
            }

            _context.SaveChanges();

            return result;
        }

        public void Delete(Guid BlogImageId)
        {
            var blogImage = _context.BlogImages.SingleOrDefault(e => e.Id == BlogImageId)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Blog Image not found");

            _context.Remove(blogImage);
            _context.SaveChanges();
        }

        public void DeleteImagesOfBlog(Guid BlogId)
        {
            var images = _context.BlogImages.Where(e => e.BlogId == BlogId);
            foreach (var image in images)
            {
                _cloudinaryRepo.Delete(image.Source);
            };

            _context.RemoveRange(images);
            _context.SaveChanges();
        }

        public List<BlogImageRM> GetBlogImages(Guid BlogId)
        {
            var images = _context.BlogImages.Where(bi => bi.BlogId == BlogId).Select(e => new BlogImageRM
            {
                Id = e.Id,
                Source = _cloudinaryRepo.GetURL(e.Source)
            }).ToList();

            return images;
        }
    }
}
