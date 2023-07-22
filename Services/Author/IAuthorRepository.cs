using BlogApi.DTO;
using BlogApi.ReadModel;

namespace BlogApi.Services.Author
{
    public interface IAuthorRepository
    {
        public ApiResponse<AuthorRM> Get(Guid Id);
        public ApiResponse<AuthorRM> Create(CreateAuthorDTO createAuthorDTO);
        public ApiResponse<AuthorRM> Update(UpdateAuthorDTO updateAuthorDTO);
        public ApiResponse<string> Delete(Guid Id);
    }
}
