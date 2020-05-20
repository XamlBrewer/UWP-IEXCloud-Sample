using System;
using System.Collections.Generic;
using System.Linq;
using IEXSharp.Model.StockPrices.Response;

namespace XamlBrewer.UWP.IEXCloud.Sample.Models
{
    public class PortfolioItem
    {
        public string Symbol { get; set; }

        public DateTime BuyDate { get; set; }

        public int Quantity { get; set; }

        public double BuyPrice { get; set; }

        public IEnumerable<HistoricalPriceResponse> HistoricalPrices { get; set; }

        public double Minimum => (double)HistoricalPrices.Select(h => h.close).Min();

        public double Maximum => (double)HistoricalPrices.Select(h => h.close).Max();

        public double OriginalValue => BuyPrice * Quantity;

        public double CurrentValue => (double)HistoricalPrices.Last().close * Quantity;

        public double Performance => (CurrentValue / OriginalValue) - 1;
    }
}
