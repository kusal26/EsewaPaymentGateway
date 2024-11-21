using Esewa_Integration.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esewa_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IEsewaService _esewaService;

        public PaymentController(IEsewaService esewaService)
        {
            _esewaService = esewaService;
        }
        [HttpPost]
        public IActionResult PayViaESewa(string amount)
        {
            _esewaService.MakePayment(amount);
            return Ok();
        }
        [HttpGet("Verify")]
        public async Task<IActionResult> VerifyPayment(string data)
        {
            var (success, dto) = await _esewaService.VeirfyPayment(data);
            if (!success)
            {
                return BadRequest("Internal Error");
            }
            return Ok(dto);
        }
    }
}
