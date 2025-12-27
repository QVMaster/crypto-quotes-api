# Crypto Quotes API

A simple ASP.NET Core Web API built as part of a technical assignment to retrieve the latest exchange rates for a given cryptocurrency against multiple currencies.

## Tech Stack
- ASP.NET Core Web API
- CoinMarketCap API
- ExchangeRates API

## How to Run
1. Configure API keys in `appsettings.json`
2. Run the application: 
   `dotnet run`

## Swagger
Once the application is running, Swagger UI is available at:
`https://localhost:{port}/swagger/index.html`
It provides interactive documentation and allows testing the API endpoints.

## Health Checks
A health check endpoint is exposed at:
`/health`
It can be used to monitor the health of the application.
and verifies the availability of the external APIs used by the application.

## Tests
Unit tests are implemented to validate the behavior of the services interacting with external APIs.
External HTTP calls are fully mocked to ensure deterministic and reliable test results.
Run tests using:
`dotnet test`

## Notes
- The implementation focuses strictly on the requirements described in the assignment.
- Caching, persistence, and other non-required concerns were intentionally left out.
- API keys are not included in the repository for security reasons and must be provided via configuration.
- Required configuration keys:
  - `CoinMarketCap:ApiKey`
  - `ExchangeRates:ApiKey`
