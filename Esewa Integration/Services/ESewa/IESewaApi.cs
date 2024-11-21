using Esewa_Integration.Dtos.ESewa;
using Refit;

namespace Esewa_Integration.Services.ESewa;
public interface IESewaApi
{
    [Get("/?product_code={productCode}&total_amount={totalAmount}&transaction_uuid={transactionUuid}")]
    Task<ESewaStatusCheckDto> VerifyPaymentAsync(
      string productCode,
      string totalAmount,
      string transactionUuid);
}
