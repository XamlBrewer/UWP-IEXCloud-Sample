using System;
using VSLee.IEXSharp.Model.Stock.Request;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            this.InitializeComponent();

            Loaded += HistoryPage_Loaded;
        }

        private async void HistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                var response = await iexCloudClient.Stock.HistoricalPriceAsync("MSFT");
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                }
                else
                {
                    HistoricPrices.ItemsSource = response.Data;
                    CandleSticks.ItemsSource = response.Data;
                }
            }

            ProgressRing.IsActive = false;
        }
    }
}
