using Esewa_Integration.Dtos;

namespace Esewa_Integration.Services;
public interface IEsewaService
{
    // Task MakePayment(string input);
    Task<(bool Success, ESewaStatusCheckDto? dto)> VeirfyPayment(string input);
}
