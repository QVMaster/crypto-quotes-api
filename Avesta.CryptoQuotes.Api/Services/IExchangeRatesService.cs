
namespace Avesta.CryptoQuotes.Api.Services;

public interface IExchangeRatesService
{
    Task<IDictionary<string, decimal>> ConvertFromBaseToFiatsAsync(decimal usdAmount);
}
