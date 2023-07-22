using BlogApi.Data;
using BlogApi.DTO;
using BlogApi.ReadModel;
using System.Net;

namespace BlogApi.Services.OTP
{
    public class OtpRepository : IOtpRepository
    {
        private readonly MyDBContext _context;

        public OtpRepository(MyDBContext context)
        {
            _context = context;
        }
        public string Create(CreateOtpDTO createOtpDTO)
        {
            var existedOTP = _context.OTPs.SingleOrDefault(o => o.Email == createOtpDTO.Email);

            if (existedOTP != null && existedOTP.ExpiredAt > DateTime.UtcNow)
            {
                return existedOTP.OTPCode;
            }

            if (existedOTP != null)
            {
                _context.Remove(existedOTP);
                _context.SaveChanges();
            }


            Random rnd = new Random();
            var otp = rnd.Next(100000, 1000000);
            var existed = true;

            while (existed)
            {
                var existedCode = _context.OTPs.SingleOrDefault(o => o.OTPCode == otp.ToString());

                if (existedCode != null)
                {
                    otp = rnd.Next(100000, 1000000);
                }
                else
                {
                    existed = false;
                }
            }

            var newOTP = new Data.OTP
            {
                Email = createOtpDTO.Email,
                OTPCode = otp.ToString(),
            };

            _context.Add(newOTP);
            _context.SaveChanges();

            return newOTP.OTPCode;
        }

        public bool Validate(ValidateOtpDTO validateOtpDTO)
        {
            var otp = _context.OTPs.SingleOrDefault(o => o.Email == validateOtpDTO.Email)
                ?? throw new HttpException(HttpStatusCode.NotFound, "No OTP found for this email");
            if (otp != null)
            {
                if (otp.OTPCode != validateOtpDTO.OTPCode)
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "This OTP is not correct");
                }

                if (otp.ExpiredAt < DateTime.UtcNow)
                {
                    throw new HttpException(HttpStatusCode.NotAcceptable, "This OTP has expired");
                }

                _context.Remove(otp);
                _context.SaveChanges();
            }

            return true;
        }
    }
}
