namespace BlogApi.ReadModel
{
    public class OtpRM
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string OTPCode { get; set; }
    }
}
