using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO
{
    public class CreateBlogDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile CoverImage { get; set; }
        public List<Guid> ImageIds { get; set; }
        public IFormFileCollection Images { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }

    public class UpdateBlogDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? CoverImage { get; set; }
        public List<Guid>? ImageIds { get; set; }
        public IFormFileCollection? Images { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
