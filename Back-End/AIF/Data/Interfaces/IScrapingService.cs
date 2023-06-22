using AIF.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIF.Services
{
    public interface IScrapingService
    {
        Task<List<ScrapingDto>> GetTopCurrenciesAsync();
        byte[] ExportToExcel(List<ScrapingDto> currencies);
    }
}
