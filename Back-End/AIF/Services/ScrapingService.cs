using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Swashbuckle.AspNetCore;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;


namespace AIF.Services
{
    [Route ("api")]
    [ApiController]
	public class ScrapingService: Controller
	{
        public class Scraper
        {
            public async Task<List<string>> ScrapeStockPrices(string url)
            {
                List<string> stockPrices = new List<string>();

                // Create an HttpClient instance to fetch the HTML content
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Fetch the HTML content of the target webpage
                        string htmlContent = await client.GetStringAsync(url);

                        // Load the HTML content into HtmlDocument
                        HtmlDocument htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(htmlContent);

                        // Use XPath or CSS selectors to extract the stock prices
                        // Modify the extraction logic as per the specific HTML structure
                        string xpath = "//div[@class='header-wrap quote-header-bg']//span[@class='last-sale']";
                        HtmlNodeCollection priceNodes = htmlDoc.DocumentNode.SelectNodes(xpath);

                        // Process the extracted stock prices
                        if (priceNodes != null)
                        {
                            foreach (HtmlNode priceNode in priceNodes)
                            {
                                string price = priceNode.InnerText;
                                stockPrices.Add(price);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No stock prices found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                return stockPrices;
            }
        }

        [HttpGet("scrape")]
        public async Task<IActionResult> ScrapeWebsite(string url)
        {
            Scraper scraper = new Scraper();
            List<string> prices = await scraper.ScrapeStockPrices(url);
            return Ok(prices);
        }
    }
}

