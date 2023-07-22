using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO
{
    public class CreateCommentDTO
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public Guid BlogId { get; set; }
    }
}
