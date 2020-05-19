using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinUI = Microsoft.UI.Xaml.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Services;
using System.Linq;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class HistoryPage : Page
    {
        private string _symbol;

        public HistoryPage()
        {
            this.InitializeComponent();
            Loaded += HistoryPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _symbol = e.Parameter.ToString();
            try
            {
                // Synchronize menu to tab.
                SymbolsTab.SelectedItem = SymbolsTab
                                            .TabItems
                                            .Where(ti => (ti as WinUI.TabViewItem).Header.ToString() == _symbol)
                                            .FirstOrDefault();
            }
            catch (Exception)
            {
                // Synchronization failed.
            }
        }

        private async void HistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                var response = await iexCloudClient.Stock.HistoricalPriceAsync(_symbol);
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
