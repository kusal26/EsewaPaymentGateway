using Esewa_Integration.Dtos.ESewa;
using Refit;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Esewa_Integration.Services.ESewa;
public class EsewaService : IEsewaService
{
    private readonly IConfiguration _configuration;

    public EsewaService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }
    #region MakePayment
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
    #endregion
    public async Task<(bool Success, ESewaStatusCheckDto? dto)> VeirfyPayment(string input)
    {
        try
        {
            var secretKey = _configuration.GetValue<string>("ESewa:SecretKey");
            var statusCheckUrl = _configuration.GetValue<string>("ESewa:StatusCheckUrl");

            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(input));
            var paymentData = JsonSerializer.Deserialize<EsewaResponseDto>(decodedString);

            //var isSignatureVerified = VerifyIncomingHmacSignature(secretKey, paymentData);
            //if (!isSignatureVerified)
            //{
            //    throw new Exception("Signature Verification Failed");
            //}

            var eSewaApi = RestService.For<IESewaApi>(statusCheckUrl);
            var dto = await eSewaApi.VerifyPaymentAsync(
                paymentData.product_code,
                paymentData.total_amount,
                paymentData.transaction_uuid);


            return (true, dto);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return (false, null);
            }

            throw;
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

    private bool VerifyIncomingHmacSignature(string key, EsewaResponseDto paymentData)
    {
        try
        {
            string signedFieldNames = paymentData.signed_field_names;
            string providedSignature = paymentData.signature;

            // Dynamically build the string to be signed based on signed_field_names
            string[] fields = signedFieldNames.Split(',');
            var signedStringBuilder = new StringBuilder();

            foreach (var field in fields)
            {
                if (signedStringBuilder.Length > 0)
                    signedStringBuilder.Append(",");

                var property = paymentData.GetType().GetProperty(field);
                if (property == null)
                {
                    throw new Exception($"Field '{field}' specified in 'signed_field_names' is missing in the response.");
                }

                var value = property.GetValue(paymentData)?.ToString();
                signedStringBuilder.Append(value);
            }

            string signedString = signedStringBuilder.ToString();

            // Calculate the HMAC-SHA256 signature
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(signedString);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                string calculatedSignature = Convert.ToBase64String(hashBytes);

                // Compare the calculated signature with the provided signature
                if (calculatedSignature == providedSignature)
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
