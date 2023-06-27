using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AIF.Data.Interfaces;
using AIF.Dtos;
using HtmlAgilityPack;

namespace AIF.Services
{
    public class ScrapingService : IScrapingService
    {
        public async Task<List<ScrapingDto>> GetTopCurrenciesAsync()
        {
            try
            {
                var currencies = new List<ScrapingDto>();

                var httpClient = new HttpClient();

                for (var pageNumber = 1; pageNumber <= 5; pageNumber++)
                {
                    var url = $"https://coinranking.com/?page={pageNumber}";

                    var html = await httpClient.GetStringAsync(url);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    var currencyRows = doc.DocumentNode.SelectNodes("//tr[@class='table__row table__row--click table__row--full-width']");

                    if (currencyRows == null || currencyRows.Count == 0)
                    {
                        break;
                    }

                    foreach (var row in currencyRows)
                    {
                        var nameCell = row.SelectSingleNode(".//span[@class='profile__subtitle-name']");
                        var priceCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--responsive']//div[@class='valuta valuta--light']");
                        var marketCapCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--s-hide']//div[@class='valuta valuta--light']");
                        var changeCell = row.SelectSingleNode(".//td[contains(@class, 'table__cell--right')]//div[contains(@class, 'change--light')]");
                        var imageCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--3-of-8 table__cell--s-11-of-20']//img[@class='profile__logo']");

                        var currencyName = nameCell?.InnerText.Trim();
                        var price = RemoveNewLines(priceCell?.InnerText);
                        var marketCap = RemoveNewLines(marketCapCell?.InnerText);
                        var change = RemoveNewLines(changeCell?.InnerText);
                        var imageUrl = imageCell?.GetAttributeValue("src", "");

                        var currencyDto = new ScrapingDto
                        {
                            Name = currencyName,
                            Price = price,
                            MarketCap = marketCap,
                            Change = change,
                            ImageUrl = imageUrl
                        };

                        currencies.Add(currencyDto);
                    }
                }

                return currencies;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve top currencies.", ex);
            }
        }

        public byte[] ExportToCSV(List<ScrapingDto> currencies)
        {
            try
            {
                var csvBuilder = new StringBuilder();

                csvBuilder.AppendLine("Name,Price,MarketCap,Change");

                foreach (var currency in currencies)
                {
                    csvBuilder.AppendLine($"{EscapeField(currency.Name)},{EscapeField(currency.Price)},{EscapeField(currency.MarketCap)},{EscapeField(currency.Change)}");
                }

                var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

                return csvBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to export top currencies.", ex);
            }
        }

        private string RemoveNewLines(string input)
        {
            return string.Join(" ", input.Split('\n').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)));
        }

        private string EscapeField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            if (field.Contains(",") || field.Contains("\""))
            {
                field = field.Replace("\"", "\"\"");

                field = $"\"{field}\"";
            }

            return field;
        }
    }
}
