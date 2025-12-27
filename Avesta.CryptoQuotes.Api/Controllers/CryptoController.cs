using Avesta.CryptoQuotes.Api.DTOs;
using Avesta.CryptoQuotes.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Avesta.CryptoQuotes.Api.Controllers;

[Route("api/crypto")]
[ApiController]
public class CryptoController : ControllerBase
{
    private readonly ICoinMarketCapService _coinMarketCapService;
    private readonly IExchangeRatesService _exchangeRatesService;

    public CryptoController(ICoinMarketCapService coinMarketCapService, IExchangeRatesService exchangeRatesService)
    {
        _coinMarketCapService = coinMarketCapService;
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet("{symbol}/quotes")]
    public async Task<ActionResult<CryptoQuoteDto>> GetQuotes(string symbol)
    {
        var usdPrice = await _coinMarketCapService.GetPriceAsync(symbol);
        var quotes = await _exchangeRatesService.ConvertFromBaseToFiatsAsync(usdPrice);

        var quotesDto = new CryptoQuoteDto
        {
            Symbol = symbol.ToUpper(),
            Quotes = quotes,
            AsOf = DateTimeOffset.UtcNow
        };

        return Ok(quotesDto);
    }

}
