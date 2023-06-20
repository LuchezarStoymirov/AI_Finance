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

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var currencyRows = doc.DocumentNode.SelectNodes("//tr[@class='table__row table__row--click table__row--full-width']");

                if (currencyRows == null)
                {
                    throw new Exception("Table rows not found.");
                }

                var currencies = new List<CurrencyDto>();

                foreach (var row in currencyRows.Take(3))
                {
                    var nameCell = row.SelectSingleNode(".//span[@class='profile__subtitle-name']");
                    var priceCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--responsive']//div[@class='valuta valuta--light']");
                    var marketCapCell = row.SelectSingleNode(".//td[@class='table__cell table__cell--2-of-8 table__cell--s-hide']//div[@class='valuta valuta--light']");
                    var changeCell = row.SelectSingleNode(".//td[contains(@class, 'table__cell--right')]//div[contains(@class, 'change--light')]");

                    if (nameCell != null && priceCell != null && marketCapCell != null && changeCell != null)
                    {
                        var currencyName = nameCell.InnerText.Trim();
                        var price = RemoveNewLines(priceCell.InnerText);
                        var marketCap = RemoveNewLines(marketCapCell.InnerText);
                        var change = RemoveNewLines(changeCell.InnerText);

                        currencies.Add(new CurrencyDto
                        {
                            Name = currencyName,
                            Price = price,
                            MarketCap = marketCap,
                            Change = change
                        });
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

        private string RemoveNewLines(string input)
        {
            return string.Join("", input.Split(new[] { '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
