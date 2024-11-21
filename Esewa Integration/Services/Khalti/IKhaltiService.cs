using Esewa_Integration.Dtos;
using Esewa_Integration.Dtos.Khalti;

namespace Esewa_Integration.Services.Khalti;
public interface IKhaltiService
{
    Task<ResponseModel<PaymentRequestSuccessResponseDto>> PaymentRequest(PaymentRequestDto input);

}
