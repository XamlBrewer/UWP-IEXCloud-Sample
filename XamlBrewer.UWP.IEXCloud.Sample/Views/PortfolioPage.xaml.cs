using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.Fluent;
using XamlBrewer.UWP.IEXCloud.Sample.Models;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class PortfolioPage : Page
    {
        public PortfolioPage()
        {
            this.InitializeComponent();

            Loaded += PortfolioPage_Loaded;
        }

        private async void PortfolioPage_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<PortfolioItem>
                {
                new PortfolioItem
                    {
                        Symbol = "AAPL",
                        BuyDate = new DateTime(2020, 1, 1),
                        Quantity = 100,
                        BuyPrice = 120
                    },
                new PortfolioItem
                    {
                        Symbol = "MSFT",
                        BuyDate = new DateTime(2020, 1, 1),
                        Quantity = 100,
                        BuyPrice = 120
                    }
                };

            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                foreach (var item in items)
                {
                    // Needs to be inside the loop. 
                    // The IEXSharp ExecutorREST helper changes the QueryStringBuilder and blocks reuse.
                    var queryStringBuilder = new VSLee.IEXSharp.Helper.QueryStringBuilder();
                    queryStringBuilder.Add("chartCloseOnly", "true");
                    queryStringBuilder.Add("chartSimplify", "true");

                    var response = await iexCloudClient.Stock.HistoricalPriceAsync(item.Symbol, VSLee.IEXSharp.Model.Stock.Request.ChartRange._3m, queryStringBuilder);
                    if (response.ErrorMessage != null)
                    {
                        Console.WriteLine(response.ErrorMessage);
                    }
                    else
                    {
                        item.HistoricalPrices = response.Data;
                    }
                }
            }

            PortfolioGrid.ItemsSource = items;
            await Task.Delay(1000); // Give the GridView some time to render the containers.

            ProgressRing.IsActive = false;

            PortfolioGrid.RegisterImplicitAnimations();
        }
    }
}
