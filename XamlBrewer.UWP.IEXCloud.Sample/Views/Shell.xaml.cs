﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavigationView.Header = "Settings";
                return;
            }

            var item = args.InvokedItemContainer as WinUI.NavigationViewItem;

            ContentFrame.Navigate(Type.GetType(item.Tag.ToString()));
            NavigationView.Header = item.Content;
        }

        private void SettingsTip_CloseButtonClick(WinUI.TeachingTip sender, object args)
        {
            ContentFrame.Navigate(typeof(SettingsPage));
            NavigationView.Header = "Settings";
        }
    }
}
