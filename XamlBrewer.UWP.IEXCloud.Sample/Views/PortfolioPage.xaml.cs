using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
            //
            // Test with one item. Grid comes later ...
            //

            var item = new PorfolioItem
            {
                Symbol = "MSFT",
                BuyDate = new DateTime(2020, 1, 1),
                Quantity = 100,
                BuyPrice = 120
            };

            ProgressRing.IsActive = true;

            var queryStringBuilder = new VSLee.IEXSharp.Helper.QueryStringBuilder();
            queryStringBuilder.Add("chartCloseOnly", "true");
            queryStringBuilder.Add("chartSimplify", "true");

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                var response = await iexCloudClient.Stock.HistoricalPriceAsync(item.Symbol,VSLee.IEXSharp.Model.Stock.Request.ChartRange._3m, queryStringBuilder);
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                }
                else
                {
                    item.HistoricalPrices = response.Data;
                    VerticalAxis.Minimum = (double)item.HistoricalPrices.Select(h => h.close).Min();
                    VerticalAxis.Maximum = (double)item.HistoricalPrices.Select(h => h.close).Max();
                    HistoricalPrices.ItemsSource = item.HistoricalPrices;
                }
            }

            ProgressRing.IsActive = false;
        }
    }
}
