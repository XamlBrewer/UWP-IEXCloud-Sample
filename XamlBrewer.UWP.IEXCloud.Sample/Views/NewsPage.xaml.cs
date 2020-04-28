using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
            Loaded += NewsPage_Loaded;
        }

        private async void NewsPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                var response = await iexCloudClient.Stock.NewsAsync("MSFT");
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                }
                else
                {
                    NewsItems.ItemsSource = response.Data;
                }
            }

            ProgressRing.IsActive = false;
        }
    }
}
