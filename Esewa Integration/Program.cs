using Esewa_Integration.Services.ESewa;
using Esewa_Integration.Services.Khalti;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var khaltiSecretKey = configuration.GetValue<string>("Khalti:live_secret_key");
var khaltiBaseUrl = configuration.GetValue<string>("Khalti:BaseUrl");

builder.Services.AddControllers();

builder.Services.AddTransient<IEsewaService, EsewaService>();
builder.Services.AddTransient<IKhaltiService, KhaltiService>();
builder.Services.AddTransient<AuthorizationHandler>(provider =>
    new AuthorizationHandler(khaltiSecretKey));

builder.Services.AddRefitClient<IKhaltiApi>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(khaltiBaseUrl))
    .AddHttpMessageHandler<AuthorizationHandler>();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

