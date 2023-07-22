using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO
{
    public class CreateAuthorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
    }

    public class UpdateAuthorDTO
    {
        [Required]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
