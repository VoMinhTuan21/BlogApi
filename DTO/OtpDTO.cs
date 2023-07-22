using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTO
{
    public class CreateOtpDTO
    {
        [Required]
        public string Email { get; set; }
    }

    public class ValidateOtpDTO : CreateOtpDTO
    {
        [Required]
        [MaxLength(6)]
        public string OTPCode { get; set; }
    }
}
