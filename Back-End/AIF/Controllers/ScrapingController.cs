using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AIF.Attributes;

namespace AIF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapingController : ControllerBase
    {
        private readonly IScrapingService _scrapingService;
        private readonly IAuthService _authService;

        public ScrapingController(IScrapingService scrapingService, IAuthService authService)
        {
            _scrapingService = scrapingService;
            _authService = authService;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> GetTopCurrenciesAsync()
        {
            try
            {
                var currencies = await _scrapingService.GetTopCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve top currencies. Error: " + ex.Message);
            }
        }

        [HttpGet("export/csv")]
        [JwtAuthorize]
        public async Task<IActionResult> ExportToCSV()
        {
            try
            {
                var currencies = await _scrapingService.GetTopCurrenciesAsync();
                var csvBytes = _scrapingService.ExportToCSV(currencies);
                return File(csvBytes, "text/csv", "top_currencies.csv");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to export top currencies. Error: " + ex.Message);
            }
        }

        [HttpGet("news")]
        public IActionResult GetNews()
        {
            try
            {
                string url = "https://www.coindesk.com/";
                var newsList = _scrapingService.ScrapeNews(url);
                return Ok(newsList);
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
