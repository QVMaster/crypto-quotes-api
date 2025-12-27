
namespace Avesta.CryptoQuotes.Api.Services;

public interface ICoinMarketCapService
{
    Task<decimal> GetPriceAsync(string symbol);
}
