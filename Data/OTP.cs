namespace BlogApi.Data
{
    public class OTP
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string OTPCode { get; set; }
        public DateTime ExpiredAt { get; set; } = DateTime.UtcNow;
    }
}
