using Esewa_Integration.Dtos.ESewa;

namespace Esewa_Integration.Services.ESewa;
public interface IEsewaService
{
    // Task MakePayment(string input);
    Task<(bool Success, ESewaStatusCheckDto? dto)> VeirfyPayment(string input);
}
