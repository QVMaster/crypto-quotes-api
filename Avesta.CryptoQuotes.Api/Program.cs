using Avesta.CryptoQuotes.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ICoinMarketCapService, CoinMarketCapService>();
builder.Services.AddHttpClient<IExchangeRatesService, ExchangeRatesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authorization middleware is intentionally disabled.
// The current user story does not include authentication/authorization,
// but this is kept here to show where it would be enabled in a secured API.
//app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();