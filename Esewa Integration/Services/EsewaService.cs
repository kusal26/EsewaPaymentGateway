using Esewa_Integration.Dtos;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Esewa_Integration.Services;
public class EsewaService : IEsewaService
{

    //public async Task MakePayment(string input)
    //{
    //    var secretKey = "8gBm/:&EnhH.1/q";
    //    var requestDto = new EsewaPaymentRequestDto();

    //    using HttpClient client = new HttpClient();
    //    client.BaseAddress = new Uri("https://rc-epay.esewa.com.np/api/epay/main/v2/form");

    //    requestDto.amount = input;
    //    requestDto.total_amount = Convert.ToString(int.Parse(requestDto.tax_amount) + int.Parse(input));
    //    var signatureField = $"total_amount={requestDto.total_amount},transaction_uuid={requestDto.transaction_uuid},product_code={requestDto.product_code}";
    //    requestDto.signature = await GenerateHmacSignature(secretKey, signatureField);

    //    var json = JsonSerializer.Serialize(requestDto);
    //    var data = new StringContent(json, Encoding.UTF8, "application/json");
    //    var response = await client.PostAsync("", data);
    //    var responseContent = await response.Content.ReadAsStringAsync();

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        var errorMessage = await response.Content.ReadAsStringAsync();
    //        var statusCode = response.StatusCode;
    //    }
    //}
    public async Task<(bool Success, ESewaStatusCheckDto? dto)> VeirfyPayment(string input)
    {
        try
        {
            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(input));
            var paymentData = JsonSerializer.Deserialize<EsewaResponseDto>(decodedString);
            using var client = new HttpClient();
            var requestUrl = new Uri($"https://uat.esewa.com.np/api/epay/transaction/status/?product_code={paymentData.product_code}&total_amount={paymentData.total_amount}&transaction_uuid={paymentData.transaction_uuid}");
            var response = await client.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<ESewaStatusCheckDto>(responseContent);
            if (!response.IsSuccessStatusCode)
            {
                return (false, null);
            }
            return (true, dto);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private Task<string> GenerateHmacSignature(string key, string data)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        // Use HMAC-SHA256 to generate the signature
        using (var hmac = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmac.ComputeHash(dataBytes);

            // Convert hash bytes to a Base64 or Hex string
            return Task.FromResult(Convert.ToBase64String(hashBytes));
        }
    }
}
