using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using AIF.Services;

namespace AIF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class S3Controller : ControllerBase
    {
        private readonly IS3Service _s3Service;

        public S3Controller(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadToS3(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    string fileExtension = Path.GetExtension(file.FileName);
                    await _s3Service.UploadFileAsync(fileBytes, fileExtension);
                }

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to upload file. Error: " + ex.Message);
            }
        }


        [HttpGet("AllFiles")]
        public async Task<ActionResult> GetAllFiles()
        {
            try
            {
                var fileList = await _s3Service.GetAllFilesAsync();
                return Ok(fileList);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve file list. Error: " + ex.Message);
            }
        }

        [HttpGet("DownloadFileFromS3")]
        public async Task<IActionResult> DownloadFromS3(string fileName)
        {
            try
            {
                var fileBytes = await _s3Service.DownloadFileAsync(fileName, ".csv");
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to download file. Error: " + ex.Message);
            }
        }
    }
}
