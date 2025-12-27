using Avesta.CryptoQuotes.Api.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace Avesta.CryptoQuotes.UnitTests;

public class ExchangeRatesServiceTests
{
    [Fact]
    public async Task ConvertFromBaseCurrency_ReturnsExpectedRates()
    {
        // Arrange
        var fakeJsonResponse = """
            {            
                "rates": { "USD": 0.95, "EUR": 1, "GBP": 0.85, "BRL": 5.0, "AUD": 1.6 }
            }
            """;

        var handler = new FakeHttpMessageHandler(_ =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeJsonResponse, Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.exchangeratesapi.io/")
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ExchangeRates:ApiKey"] = "FAKE_KEY",
                ["ExchangeRates:BaseCurrency"] = "EUR"
            })
            .Build();

        var service = new ExchangeRatesService(httpClient, config);

        // Act
        var result = await service.ConvertFromBaseToFiatsAsync(100);

        // Assert
        Assert.Equal(95m, result["USD"]);
        Assert.Equal(100m, result["EUR"]);
        Assert.Equal(85m, result["GBP"]);
        Assert.Equal(500m, result["BRL"]);
        Assert.Equal(160m, result["AUD"]);
    }
}