using BlogApi.DTO;
using BlogApi.ReadModel;

namespace BlogApi.Services.Category
{
    public interface ICategoryRepository
    {
        public ApiResponse<CategoryRM> Create(CreateCategoryDTO dto);
        public ApiResponse<List<CategoryRM>> GetAll();
        public ApiResponse<CategoryRM> Update(Guid Id, UpdateCategoryDTO dto);
        public ApiResponse<string> Delete(Guid Id);
        public CategoryRM Get(Guid Id);
    }
}
