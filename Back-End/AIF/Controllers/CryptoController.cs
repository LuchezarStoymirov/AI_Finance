using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AIF.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet("topcurrencies")]
        public async Task<ActionResult> GetTopCurrenciesAsync()
        {
            try
            {
                var currencies = await _cryptoService.GetTopCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving top currencies.");
            }
        }
    }
}
