
using System.Net.Http.Headers;
using System.Text.Json;

namespace Avesta.CryptoQuotes.Api.Services;

public class CoinMarketCapService : ICoinMarketCapService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public CoinMarketCapService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["CoinMarketCap:ApiKey"]
            ?? throw new InvalidOperationException("CoinMarketCap API key is missing.");
    }

    public async Task<decimal> GetPriceAsync(string symbol)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={symbol}&convert=EUR"
        );

        request.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var json = await JsonDocument.ParseAsync(stream);

        return json.RootElement
            .GetProperty("data")
            .GetProperty(symbol)
            .GetProperty("quote")
            
            // Changed the base currency from USD to EUR because of api.exchangeratesapi.io limitations on free plan
            //.GetProperty("USD") 
            .GetProperty("EUR")

            .GetProperty("price")
            .GetDecimal();
    }
}
