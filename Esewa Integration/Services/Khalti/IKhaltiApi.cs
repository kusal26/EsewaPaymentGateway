using Esewa_Integration.Dtos.Khalti;
using Refit;

namespace Esewa_Integration.Services.Khalti;

public interface IKhaltiApi
{
    [Post("/epayment/initiate/")]
    Task<PaymentRequestSuccessResponseDto> InitiatePaymentAsync([Body] PaymentRequestDto paymentRequest);
    [Post("/epayment/lookup/")]
    Task<PaymentVerificationResponseDto> PaymentVerificationAsync([Body] object dto);
}
