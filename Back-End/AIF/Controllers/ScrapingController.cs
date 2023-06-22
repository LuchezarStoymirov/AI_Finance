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

        public ScrapingController(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }

        [HttpGet]
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

        [HttpGet("export")]
        public async Task<IActionResult> ExportTopCurrenciesAsync()
        {
            try
            {
                var currencies = await _scrapingService.GetTopCurrenciesAsync();

                // Export to Excel
                var excelData = _scrapingService.ExportToExcel(currencies);

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "currencies.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to export top currencies. Error: " + ex.Message);
            }
        }
    }
}
