using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLee.IEXSharp.Model.Stock.Response;

namespace XamlBrewer.UWP.IEXCloud.Sample.Models
{
    public class PorfolioItem
    {
        public string Symbol { get; set; }

        public DateTime BuyDate { get; set; }

        public int Quantity { get; set; }

        public double BuyPrice { get; set; }

        public IEnumerable<HistoricalPriceResponse> HistoricalPrices { get; set; }
    }
}
