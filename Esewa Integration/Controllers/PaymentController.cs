using Esewa_Integration.Dtos.Khalti;
using Esewa_Integration.Services.ESewa;
using Esewa_Integration.Services.Khalti;
using Microsoft.AspNetCore.Mvc;

namespace Esewa_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IEsewaService _esewaService;
        public readonly IKhaltiService _khaltiService;

        public readonly IConfiguration _configuration;

        public PaymentController(
            IEsewaService esewaService,
            IKhaltiService khaltiService,
            IConfiguration configuration
            )
        {
            _esewaService = esewaService;
            _khaltiService = khaltiService;
            _configuration = configuration;
        }
        //ESewa Payment Verification
        [HttpGet("ESewaPaymentVerification")]
        public async Task<IActionResult> VerifyPayment(string data)
        {
            var (success, dto) = await _esewaService.VeirfyPayment(data);
            if (!success)
            {
                return BadRequest("Internal Error");
            }
            return Ok(dto);
        }

        [HttpPost("InitiateKhaltiPayment")]
        public async Task<IActionResult> InitiateKhaltiPayment(int amount, string productName)
        {
            var returnUrl = _configuration.GetValue<string>("Khalti:ReturnUrl");
            var websiteUrl = _configuration.GetValue<string>("Khalti:WebsiteUrl");
            var requestDto = new PaymentRequestDto
            {
                return_url = returnUrl,
                website_url = websiteUrl,
                purchase_order_id = Guid.NewGuid().ToString(),
                amount = amount,
                purchase_order_name = productName
            };
            var response = await _khaltiService.PaymentRequest(requestDto);
            return Ok(response);
        }
    }
}
