using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }

    public class UpdateCategoryDTO : CreateCategoryDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
