﻿namespace AIF.Models
{
    public class DemoModel
    {
        public DemoModel(string symbol, decimal lastPrice)
        {
            this.Symbol = symbol;
            this.LastPrice = lastPrice;
        }

        public int Id { get; set; }

        public string Symbol { get; set; }

        public decimal LastPrice { get; set; }   
    }
}
