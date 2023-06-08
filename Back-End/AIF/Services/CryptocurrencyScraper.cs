using HtmlAgilityPack;
using System.Collections.Generic;

public class CryptocurrencyScraper
{
    public List<Cryptocurrency> ScrapeTopCryptocurrencies()
    {
        List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();

        string url = "https://www.investing.com/";
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);

        HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//table[@id='cross_rate_markets_stocks_1']/tbody/tr");

        if (rows != null)
        {
            foreach (HtmlNode row in rows)
            {
                HtmlNode nameNode = row.SelectSingleNode(".//td[@class='bold left noWrap elp plusIconTd']/a");
                HtmlNode priceNode = row.SelectSingleNode(".//td[@class='pid-1057396-last']");

                string name = nameNode?.InnerText.Trim();
                string price = priceNode?.InnerText.Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(price))
                {
                    cryptocurrencies.Add(new Cryptocurrency
                    {
                        Name = name,
                        Price = price
                    });
                }
            }
        }

        return cryptocurrencies;
    }
}

public class Cryptocurrency
{
    public string Name { get; set; }
    public string Price { get; set; }
}
