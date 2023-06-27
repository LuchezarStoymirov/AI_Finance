using System.Collections.Generic;
using System.Threading.Tasks;
using AIF.Dtos;

namespace AIF.Data.Interfaces
{
    public interface IScrapingService
    {
        Task<List<ScrapingDto>> GetTopCurrenciesAsync();
        byte[] ExportToCSV(List<ScrapingDto> currencies);
    }
}
