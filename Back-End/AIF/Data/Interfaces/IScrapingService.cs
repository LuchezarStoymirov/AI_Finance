using System.Collections.Generic;
using System.Threading.Tasks;
using AIF.Dtos;

namespace AIF.Services
{
    public interface IScrapingService
    {
        Task<List<ScrapingDto>> GetTopCurrenciesAsync();
    }
}
