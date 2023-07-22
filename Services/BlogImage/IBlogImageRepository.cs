using BlogApi.ReadModel;

namespace BlogApi.Services.BlogImage
{
    public interface IBlogImageRepository
    {
        public List<BlogImageRM> Create(IFormFileCollection images, Guid BlogId);
        public List<BlogImageRM> GetBlogImages(Guid BlogId);
        public void Delete(Guid BlogImageId);
        public void DeleteImagesOfBlog(Guid BlogId);
    }
}
