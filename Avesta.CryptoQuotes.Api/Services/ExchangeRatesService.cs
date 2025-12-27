using System.Text.Json;

namespace Avesta.CryptoQuotes.Api.Services;

/// <summary>
/// Adapter for the external ExchangeRates API.
/// The external API uses the field name 'rates'.
/// In our domain model, this data is exposed as 'Quotes'
/// to keep naming consistent across pricing providers.
/// </summary>
public class ExchangeRatesService : IExchangeRatesService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseCurrency;
    private readonly string _apiKey;

    private static readonly string[] TargetCurrencies =
        { "USD", "EUR", "GBP", "BRL", "AUD" };

    public ExchangeRatesService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseCurrency = configuration["ExchangeRates:BaseCurrency"] ?? "EUR";
        _apiKey = configuration["ExchangeRates:ApiKey"]
            ?? throw new InvalidOperationException("ExchangeRates API key is missing.");
    }

    public async Task<IDictionary<string, decimal>> ConvertFromBaseToFiatsAsync(decimal baseAmount)
    {
        var symbols = string.Join(",", TargetCurrencies);

        var response = await _httpClient.GetAsync(
            $"https://api.exchangeratesapi.io/latest?base={_baseCurrency}&symbols={symbols}&access_key={_apiKey}"
        );

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var json = await JsonDocument.ParseAsync(stream);
                
        var rates = json.RootElement.GetProperty("rates");

        var result = new Dictionary<string, decimal>();

        foreach (var currency in TargetCurrencies)
        {
            var rate = rates.GetProperty(currency).GetDecimal();
            result[currency] = baseAmount * rate;
        }

        return result;
    }
}
