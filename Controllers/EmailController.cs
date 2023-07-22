using BlogApi.DTO;
using BlogApi.Email;
using BlogApi.ReadModel;
using BlogApi.Services.OTP;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IOtpRepository _otpRepository;

        public EmailController(IEmailSender emailSender, IOtpRepository otpRepository)
        {
            _emailSender = emailSender;
            _otpRepository = otpRepository;
        }

        [HttpPost()]
        public IActionResult SendOTP([FromBody] CreateOtpDTO dto)
        {
            try
            {
                var otp = _otpRepository.Create(dto);

                var message = new Message(new string[] { dto.Email }, "OTP Code", "Your OTP is " + otp);
                _emailSender.SendEmail(message);

                return Ok(new ApiResponse<string>
                {
                    Data = "Send OTP success",
                    Message = "Send OTP success"
                });
            }
            catch (Exception)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "Can not send OTP");
            }
        }

        [HttpPost("Validate")]
        public IActionResult ValidateOTP([FromBody] ValidateOtpDTO dto)
        {
            try
            {
                var result = _otpRepository.Validate(dto);

                return Ok(new ApiResponse<string>
                {
                    Message = "Validate OTP success",
                    Data = "Validate OTP success",
                });
            }
            catch (Exception ex)
            {
                if (ex is not HttpException)
                {
                    return BadRequest("Can not validate OTP");
                }

                throw;
            }
        }
    }
}
