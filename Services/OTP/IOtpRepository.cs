using BlogApi.DTO;

namespace BlogApi.Services.OTP
{
    public interface IOtpRepository
    {
        public string Create(CreateOtpDTO createOtpDTO);
        public bool Validate(ValidateOtpDTO validateOtpDTO);
    }
}
