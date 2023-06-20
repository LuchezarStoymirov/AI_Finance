using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AIF.Dtos;
using HtmlAgilityPack;

namespace AIF.Services
{
    public class CryptoService
    {
        public async Task<List<CurrencyDto>> GetTopCurrenciesAsync()
        {
            try
            {
                var url = "https://coinranking.com/";
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);

                Console.WriteLine(html); // Print the HTML content to the console

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var currencyRows = doc.DocumentNode.SelectNodes("//tr[@class='table__row table__row--click table__row--full-width']");

                if (currencyRows == null)
                {
                    throw new Exception("Table rows not found.");
                }

                var currencies = new List<CurrencyDto>();

                foreach (var row in currencyRows.Take(5))
                {
                    var nameCell = row.SelectSingleNode(".//span[@class='profile__subtitle-name']");
                    var priceCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--responsive']//div[@class='valuta valuta--light']");

                    if (nameCell != null && priceCell != null)
                    {
                        var currencyName = nameCell.InnerText.Trim();
                        var price = priceCell.InnerText.Trim();

                        currencies.Add(new CurrencyDto { Name = currencyName, Price = price });
                    }
                }

                return currencies;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error message
                throw new Exception("Failed to retrieve top currencies.", ex);
            }
        }
    }
}
