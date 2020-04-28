using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using VSLee.IEXSharp;
using VSLee.IEXSharp.Model.Shared.Response;
using VSLee.IEXSharp.Model.Stock.Request;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
