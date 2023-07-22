using BlogApi.DTO;
using BlogApi.ReadModel;

namespace BlogApi.Services.Comment
{
    public interface ICommentRepository
    {
        public ApiResponse<CommentRM> Create(CreateCommentDTO dto);
    }
}
