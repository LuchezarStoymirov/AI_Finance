using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class CryptocurrencyController : ControllerBase
{
    private readonly CryptocurrencyScraper _scraper;

    public CryptocurrencyController()
    {
        _scraper = new CryptocurrencyScraper();
    }

    [HttpGet("top-cryptocurrencies")]
    public IActionResult GetTopCryptocurrencies()
    {
        List<Cryptocurrency> cryptocurrencies = _scraper.ScrapeTopCryptocurrencies();
        return Ok(cryptocurrencies);
    }
}
