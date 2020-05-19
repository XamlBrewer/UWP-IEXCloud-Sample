using System;
using System.Collections.Generic;
using VSLee.IEXSharp.Model.Shared.Response;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class WatchListPage : Page
    {
        private List<string> _stocks = new List<string> { "GOOGL", "AMZN", "AAPL", "IBM", "JNJ", "KIN-BB", "MRK", "MSFT", "PFE", "RAND-NA", "RANJF", "SBUX" };

        public WatchListPage()
        {
            this.InitializeComponent();

            Loaded += WatchListPage_LoadedAsync;
        }

        private async void WatchListPage_LoadedAsync(object sender, RoutedEventArgs e)
        {
            var result = new List<Quote>();

            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    foreach (var item in _stocks)
                    {
                        var response = await iexCloudClient.Stock.QuoteAsync(item);
                        if (response.ErrorMessage == null)
                        {
                            result.Add(response.Data);
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            Quotes.ItemsSource = result;

            ProgressRing.IsActive = false;
        }
    }
}
