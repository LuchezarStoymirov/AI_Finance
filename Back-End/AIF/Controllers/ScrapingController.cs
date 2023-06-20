using AIF.Services;
using Microsoft.AspNetCore.Mvc;
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
            var currencies = await _scrapingService.GetTopCurrenciesAsync();
            return Ok(currencies);
        }
    }
}
