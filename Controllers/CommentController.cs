using BlogApi.DTO;
using BlogApi.ReadModel;
using BlogApi.Services.Comment;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentDTO dto)
        {
            try
            {
                var result = _commentRepo.Create(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Cannot create commnet");
                }
                throw;
            }
        }
    }
}
