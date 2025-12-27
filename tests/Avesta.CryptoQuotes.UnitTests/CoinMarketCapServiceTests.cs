using Avesta.CryptoQuotes.Api.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace Avesta.CryptoQuotes.UnitTests;

public class CoinMarketCapServiceTests
{
    [Fact]
    public async Task GetPriceAsync_ReturnsExpectedUsdPrice()
    {
        // Arrange
        var fakeJsonResponse = """
            {"data":{"BTC":{"quote":{"EUR":{"price":50000}}}}}
            """;

        var handler = new FakeHttpMessageHandler(_ =>
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeJsonResponse, Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://pro-api.coinmarketcap.com/")
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["CoinMarketCap:ApiKey"] = "FAKE_KEY"
            })
            .Build();

        var service = new CoinMarketCapService(
            httpClient,
            configuration: config
        );

        // Act
        var price = await service.GetPriceAsync("BTC");

        // Assert
        Assert.Equal(50000m, price);
    }
}