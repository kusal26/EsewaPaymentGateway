using Esewa_Integration.Services.Khalti;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Esewa_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallBackController : ControllerBase
    {
        private readonly IKhaltiApi _khaltiApi;

        public CallBackController(
            IKhaltiApi khaltiApi)
        {
            _khaltiApi = khaltiApi;
        }
        [HttpGet("KhaltiCallBack")]
        public async Task<IActionResult> PaymentSuccessCallback(
        [FromQuery] string pidx,
        [FromQuery] string? txnId,
        [FromQuery] decimal amount,
        [FromQuery(Name = "total_amount")]
         decimal totalAmount,
        [FromQuery] string status,
        [FromQuery] string mobile,
        [FromQuery(Name = "tidx")]
         string transactionIdx,
        [FromQuery(Name = "purchase_order_id")] string purchaseOrderId,
        [FromQuery(Name = "purchase_order_name")] string purchaseOrderName,
        [FromQuery(Name = "transaction_id")] string transactionId)
        {
            try
            {
                var dto = new { pidx = pidx };
                var res = await _khaltiApi.PaymentVerificationAsync(dto);
                return Ok(res);
            }
            catch (ApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
