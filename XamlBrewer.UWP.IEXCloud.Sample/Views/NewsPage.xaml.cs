using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class NewsPage : Page
    {
        private string _symbol;
        
        public NewsPage()
        {
            this.InitializeComponent();
            Loaded += NewsPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _symbol = e.Parameter.ToString();
        }

        private async void NewsPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                var response = await iexCloudClient.Stock.NewsAsync(_symbol);
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
