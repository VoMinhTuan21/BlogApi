using BlogApi.Data;
using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Blog;

namespace BlogApi.Services.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyDBContext _context;
        private readonly IBlogRepository _blogRepo;

        public CommentRepository(MyDBContext context, IBlogRepository blogRepo)
        {
            _context = context;
            _blogRepo = blogRepo;
        }

        public ApiResponse<CommentRM> Create(CreateCommentDTO dto)
        {
            _ = _blogRepo.Get(dto.BlogId);

            var newComment = new Data.Comment
            {
                Content = dto.Content,
                UserEmail = dto.UserEmail,
                UserName = dto.UserName,
                BlogId = dto.BlogId
            };

            _context.Add(newComment);
            _context.SaveChanges();

            return new ApiResponse<CommentRM>
            {
                Message = "Create comment success",
                Data = new CommentRM
                {
                    Id = newComment.Id,
                    Content = newComment.Content,
                    UserEmail = newComment.UserEmail,
                    UserName = newComment.UserName,
                    CreatedAt = newComment.CreatedAt,
                }
            };
        }
    }
}
