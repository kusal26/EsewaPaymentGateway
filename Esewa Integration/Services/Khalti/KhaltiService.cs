using Esewa_Integration.Dtos;
using Esewa_Integration.Dtos.Khalti;

namespace Esewa_Integration.Services.Khalti;

public class KhaltiService : IKhaltiService
{
    private readonly IKhaltiApi _khaltiApi;
    public KhaltiService(
        IKhaltiApi khaltiApi)
    {
        _khaltiApi = khaltiApi;
    }
    public async Task<ResponseModel<PaymentRequestSuccessResponseDto>> PaymentRequest(PaymentRequestDto input)
    {
        try
        {
            input.customer_info = new CustomerInfo
            {
                name = "Ram Bahadur",
                email = "test@khalti.com",
                phone = "9800000001"
            };
            input.product_details =
            [
                new ProductDetails
                {  identity= "1234567890",
                  name= "Khalti logo",
                  total_price= 1300,
                  quantity= 1,
                     unit_price= 1300
                }
            ];
            input.amount_breakdown = [
                new AmountBreakdown
                {

                  label= "Mark Price",
                  amount=1000
              },
                  new AmountBreakdown
                    {
                      label= "VAT",
                      amount= 300
                  }

                ];
            var response = await _khaltiApi.InitiatePaymentAsync(input);
            return new ResponseModel<PaymentRequestSuccessResponseDto>(response);
        }
        catch (Exception ex)
        {
            return new ResponseModel<PaymentRequestSuccessResponseDto>("Internal Error");
        }
    }
}
