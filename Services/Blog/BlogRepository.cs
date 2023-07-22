using BlogApi.Data;
using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Author;
using BlogApi.Services.BlogImage;
using BlogApi.Services.Category;
using BlogApi.Services.Cloudinary;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Services.Blog
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MyDBContext _context;
        private readonly ICloudinaryRepository _cloudinaryRepository;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IBlogImageRepository _blogImageRepo;

        public BlogRepository(MyDBContext context, ICloudinaryRepository cloudinaryRepository,
            ICategoryRepository categoryRepository, IAuthorRepository authorRepository,
            IBlogImageRepository blogImageRepo)
        {
            _context = context;
            _cloudinaryRepository = cloudinaryRepository;
            _categoryRepo = categoryRepository;
            _authorRepo = authorRepository;
            _blogImageRepo = blogImageRepo;
        }

        public ApiResponse<BlogRM> Create(CreateBlogDTO dto)
        {
            var author = _authorRepo.Get(dto.AuthorId).Data;
            var category = _categoryRepo.Get(dto.CategoryId);

            var uploadImageResult = _cloudinaryRepository.UploadImage(dto.CoverImage, "blogs");

            var blog = new Data.Blog
            {
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                Content = dto.Content,
                Title = dto.Title,
                CoverImage = uploadImageResult.PublicId
            };

            _context.Add(blog);
            _context.SaveChanges();

            if (dto.Images != null)
            {
                var images = _blogImageRepo.Create(dto.Images, blog.Id);

                for (int i = 0; i < images.Count; i++)
                {
                    blog.Content = blog.Content.Replace(dto.ImageIds[i].ToString(), images[i].Id.ToString());
                }

            };

            _context.SaveChanges();

            var imgs = _blogImageRepo.GetBlogImages(blog.Id);
            return new ApiResponse<BlogRM>
            {
                Message = "Create blog success",
                Data = new BlogRM
                {
                    Id = blog.Id,
                    Author = new AuthorRM
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Avatar = _cloudinaryRepository.GetURL(author.Avatar)
                    },
                    Category = new CategoryRM
                    {
                        Id = category.Id,
                        Name = category.Name
                    },
                    Content = blog.Content,
                    Title = blog.Title,
                    CoverImage = uploadImageResult.Url.ToString(),
                    CreatedAt = blog.CreatedAt,
                    NumbOfavorite = blog.NumOfFavorite,
                    NumOfViews = blog.NumOfViews,
                    Images = imgs,
                }
            };
        }

        public ApiResponse<string> Delete(Guid Id)
        {
            var blog = _context.Blogs.SingleOrDefault(e => e.Id == Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Blog not found");

            // delete comment

            _blogImageRepo.DeleteImagesOfBlog(Id);

            _cloudinaryRepository.Delete(blog.CoverImage);

            _context.Remove(blog);
            _context.SaveChanges();

            return new ApiResponse<string>
            {
                Message = "Delete blog success",
                Data = Id.ToString(),
            };
        }

        public ApiResponse<BlogRM> Get(Guid Id)
        {
            var blog = _context.Blogs.Include(e => e.Author).Include(e => e.Category).SingleOrDefault(e => e.Id == Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Blog not found");

            var imgs = _blogImageRepo.GetBlogImages(Id);

            return new ApiResponse<BlogRM>
            {
                Message = "Get blog success",
                Data = new BlogRM
                {
                    Id = blog.Id,
                    Author = new AuthorRM
                    {
                        Id = blog.Author.Id,
                        Name = blog.Author.Name,
                        Avatar = _cloudinaryRepository.GetURL(blog.Author.Avatar)
                    },
                    Category = new CategoryRM
                    {
                        Id = blog.Category.Id,
                        Name = blog.Category.Name
                    },
                    Content = blog.Content,
                    Title = blog.Title,
                    CoverImage = _cloudinaryRepository.GetURL(blog.CoverImage),
                    CreatedAt = blog.CreatedAt,
                    NumbOfavorite = blog.NumOfFavorite,
                    NumOfViews = blog.NumOfViews,
                    Images = imgs,
                }
            };
        }

        public ApiResponse<BlogRM> Update(UpdateBlogDTO dto)
        {

            var blog = _context.Blogs.SingleOrDefault(b => b.Id == dto.Id)
                ?? throw new HttpException(HttpStatusCode.NotFound, "Blog not found");

            if (dto.Title != null)
            {
                blog.Title = dto.Title;
            }

            if (dto.Content != null)
            {
                blog.Content = dto.Content;
            }

            if (dto.CoverImage != null)
            {
                var uploadImageResult = _cloudinaryRepository.UploadImage(dto.CoverImage, "blogs");

                _cloudinaryRepository.Delete(blog.CoverImage);

                blog.CoverImage = uploadImageResult.PublicId;
            }

            if (dto.CategoryId != null)
            {
                _ = _categoryRepo.Get((Guid)dto.CategoryId);
                blog.CategoryId = (Guid)dto.CategoryId;
            }

            if (dto.Images != null && dto.ImageIds != null)
            {
                var images = _blogImageRepo.Create(dto.Images, blog.Id);

                for (int i = 0; i < images.Count; i++)
                {
                    blog.Content = blog.Content.Replace(dto.ImageIds[i].ToString(), images[i].Id.ToString());
                }
            }

            var imgs = _blogImageRepo.GetBlogImages(dto.Id);

            for (int i = imgs.Count - 1; i >= 0; i--)
            {
                var existed = blog.Content.Contains(imgs[i].Id.ToString());

                if (!existed)
                {
                    _blogImageRepo.Delete(imgs[i].Id);
                    imgs.RemoveAt(i);
                }
            }

            _context.SaveChanges();

            var author = _authorRepo.Get(blog.AuthorId).Data;
            var category = _categoryRepo.Get(blog.CategoryId);

            return new ApiResponse<BlogRM>
            {
                Message = "Create blog success",
                Data = new BlogRM
                {
                    Id = blog.Id,
                    Author = new AuthorRM
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Avatar = _cloudinaryRepository.GetURL(author.Avatar)
                    },
                    Category = new CategoryRM
                    {
                        Id = category.Id,
                        Name = category.Name
                    },
                    Content = blog.Content,
                    Title = blog.Title,
                    CoverImage = _cloudinaryRepository.GetURL(blog.CoverImage),
                    CreatedAt = blog.CreatedAt,
                    NumbOfavorite = blog.NumOfFavorite,
                    NumOfViews = blog.NumOfViews,
                    Images = imgs,
                }
            };
        }
    }
}
