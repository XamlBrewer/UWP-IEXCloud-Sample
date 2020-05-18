using System;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Controls;
using XamlBrewer.UWP.IEXCloud.Sample.Services;
using XamlBrewer.UWP.IEXCloud.Sample.Views;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace XamlBrewer.UWP.IEXCloud.Sample
{
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
                titleBar.BackgroundColor = Colors.Transparent;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.SlateGray;
                titleBar.ButtonForegroundColor = Color.FromArgb(255, 188, 143, 143);
            }

            this.InitializeComponent();

            Loaded += Shell_Loaded;
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            if (String.IsNullOrEmpty(settings.PublishableKey) && String.IsNullOrEmpty(settings.PublishableSandBoxKey))
            {
                SettingsTip.Target = NavigationView.SettingsItem as FrameworkElement;
                SettingsTip.PreferredPlacement = WinUI.TeachingTipPlacementMode.TopLeft;
                SettingsTip.IsOpen = true;
            }
        }

        private void NavigationView_ItemInvoked(WinUI.NavigationView sender, WinUI.NavigationViewItemInvokedEventArgs args)
        {
            // Similar code to SelectionChanged, but with a 'click to refresh' behavior.
        }

        private void SettingsTip_CloseButtonClick(WinUI.TeachingTip sender, object args)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            NavigationView.Header = "Settings";
        }

        private void NavigationView_SelectionChanged(WinUI.NavigationView sender, WinUI.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavigationView.Header = "Settings";
                return;
            }

            var item = args.SelectedItemContainer as WinUI.NavigationViewItem;

            if (item.Tag != null)
            {
                ContentFrame.Navigate(Type.GetType(item.Tag.ToString()), item.Content);
                NavigationView.Header = sender.SelectedItemsPath().First().Content;
            }
        }
    }
}
