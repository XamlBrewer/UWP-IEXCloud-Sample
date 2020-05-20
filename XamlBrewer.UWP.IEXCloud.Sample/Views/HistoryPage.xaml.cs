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
                                            .First();
            }
            catch (Exception)
            {
                // Synchronization failed.
                SymbolsTab.SelectedIndex = 0;
            }
        }

        private async void SymbolsTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _symbol = (e.AddedItems.First() as WinUI.TabViewItem).Header.ToString();

            // Here's where we should try to sync from tab to menu without starting a loop.
            // ...

            ProgressRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    var response = await iexCloudClient.StockPrices.HistoricalPriceAsync(_symbol);
                    if (response.ErrorMessage != null)
                    {
                        HistoricPrices.ItemsSource = null;
                        CandleSticks.ItemsSource = null;
                    }
                    else
                    {
                        HistoricPrices.ItemsSource = response.Data;
                        CandleSticks.ItemsSource = response.Data;
                    }
                }
                catch (Exception ex)
                {
                    HistoricPrices.ItemsSource = null;
                    CandleSticks.ItemsSource = null;
                }
            }

            ProgressRing.IsActive = false;
        }

        private async void SymbolsTab_AddTabButtonClick(WinUI.TabView sender, object args)
        {
            var textBox = new TextBox { AcceptsReturn = false, Text = "GOOGL" };
            var dialog = new ContentDialog
            {
                Content = textBox,
                Title = "Add a new stock symbol",
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Let's see",
                SecondaryButtonText = "No thanks"
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                var newTab = new WinUI.TabViewItem();
                newTab.Header = textBox.Text;
                sender.TabItems.Add(newTab);
                sender.SelectedIndex = sender.TabItems.Count - 1;
            }
        }
    }
}
