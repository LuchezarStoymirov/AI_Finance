using AIF.Data.Interfaces;
using AIF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AIF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScrapingController : ControllerBase
    {
        private readonly IScrapingService _scrapingService;
        private readonly IAWSS3Service _s3Service;

        public ScrapingController(IScrapingService scrapingService, IAWSS3Service s3Service)
        {
            _scrapingService = scrapingService;
            _s3Service = s3Service;
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

        [HttpGet("export/csv")]
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    await _s3Service.UploadFileAsync(file.FileName, memoryStream);
                }

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to upload file. Error: " + ex.Message);
            }
        }
    }
}
