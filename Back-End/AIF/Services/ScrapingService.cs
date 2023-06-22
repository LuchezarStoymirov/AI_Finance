using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                var url = "https://coinranking.com/";
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var currencyRows = doc.DocumentNode.SelectNodes("//tr[@class='table__row table__row--click table__row--full-width']");

                if (currencyRows == null)
                {
                    throw new Exception("Table rows not found.");
                }

                var currencies = new List<ScrapingDto>();

                foreach (var row in currencyRows.Take(3))
                {
                    var nameCell = row.SelectSingleNode(".//span[@class='profile__subtitle-name']");
                    var priceCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--responsive']//div[@class='valuta valuta--light']");
                    var marketCapCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--s-hide']//div[@class='valuta valuta--light']");
                    var changeCell = row.SelectSingleNode(".//td[contains(@class, 'table__cell--right')]//div[contains(@class, 'change--light')]");
                    var imageCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--3-of-8 table__cell--s-11-of-20']//img[@class='profile__logo']");

                    if (nameCell != null && priceCell != null && marketCapCell != null && changeCell != null && imageCell != null)
                    {
                        var currencyName = nameCell.InnerText.Trim();
                        var price = RemoveNewLines(priceCell.InnerText);
                        var marketCap = RemoveNewLines(marketCapCell.InnerText);
                        var change = RemoveNewLines(changeCell.InnerText);
                        var imageUrl = imageCell.GetAttributeValue("src", "");

                        currencies.Add(new ScrapingDto
                        {
                            Name = currencyName,
                            Price = price,
                            MarketCap = marketCap,
                            Change = change,
                            ImageUrl = imageUrl
                        });
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
            var csvContent = new StringBuilder();

            // Append header row
            csvContent.AppendLine("Name,Price,Market Cap,Change");

            // Append data rows
            foreach (var currency in currencies)
            {
                csvContent.AppendLine($"{EscapeCsvField(currency.Name)},{EscapeCsvField(currency.Price)},{EscapeCsvField(currency.MarketCap)},{EscapeCsvField(currency.Change)}");
            }

            // Convert the CSV content to a byte array
            var csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

            return csvBytes;
        }

        private string RemoveNewLines(string input)
        {
            return string.Join("", input.Split(new[] { '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private string EscapeCsvField(string field)
        {
            // If the field contains a comma or double quote, surround it with double quotes and escape any double quotes within the field
            if (field.Contains(',') || field.Contains('"'))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }
    }
}
