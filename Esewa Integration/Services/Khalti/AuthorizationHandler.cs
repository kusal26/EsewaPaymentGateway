namespace Esewa_Integration.Services.Khalti
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly string _secretKey;

        public AuthorizationHandler(string secretKey)
        {
            _secretKey = secretKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Key {_secretKey}");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
