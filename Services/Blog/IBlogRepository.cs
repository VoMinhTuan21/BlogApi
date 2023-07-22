using BlogApi.DTO;
using BlogApi.ReadModel;

namespace BlogApi.Services.Blog
{
    public interface IBlogRepository
    {
        public ApiResponse<BlogRM> Create(CreateBlogDTO dto);
        public ApiResponse<BlogRM> Update(UpdateBlogDTO dto);
        public ApiResponse<BlogRM> Get(Guid Id);
        public ApiResponse<string> Delete(Guid id);
    }
}
