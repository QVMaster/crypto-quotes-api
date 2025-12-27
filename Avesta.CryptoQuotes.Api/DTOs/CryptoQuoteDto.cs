
namespace Avesta.CryptoQuotes.Api.DTOs;

public class CryptoQuoteDto
{
    // BTC, ETH, etc.
    public required string Symbol { get; set; }

    // USD, EUR, BRL, GBP, AUD
    public required IDictionary<string, decimal> Quotes { get; set; }

    // Time of when the quote was retrieved
    public DateTimeOffset AsOf { get; set; }
}
