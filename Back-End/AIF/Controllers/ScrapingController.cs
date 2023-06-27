using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetTopCurrenciesAsync()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString();
                var isValid = await _authService.ValidateTokenAsync(token);

                if (!isValid)
                {
                    return Unauthorized("Invalid or missing token");
                }

                var currencies = await _scrapingService.GetTopCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve top currencies. Error: " + ex.Message);
            }
        }

        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportToCSV()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString();
                var isValid = await _authService.ValidateTokenAsync(token);

                if (!isValid)
                {
                    return Unauthorized("Invalid or missing token");
                }

                var currencies = await _scrapingService.GetTopCurrenciesAsync();
                var csvBytes = _scrapingService.ExportToCSV(currencies);
                return File(csvBytes, "text/csv", "top_currencies.csv");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to export top currencies. Error: " + ex.Message);
            }
        }

    }
}
