using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlBrewer.Fluent;
using XamlBrewer.UWP.IEXCloud.Sample.Services;

namespace XamlBrewer.UWP.IEXCloud.Sample.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            ContentGrid.Translation += new Vector3(0, 0, 6);
            SandboxContentGrid.Translation += new Vector3(0, 0, 6);
            SettingsGrid.RegisterImplicitAnimations();

            Loaded += SettingsPage_Loaded;
        }

        private Settings Settings => new Settings();

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SharedShadow.Receivers.Add(ShadowCatcher);
        }

        private async void TestTokensButton_Click(object sender, RoutedEventArgs e)
        {
            TestTokensTest.Text = String.Empty;
            TestTokensRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    var response = await iexCloudClient.StockPrices.QuoteAsync("MSFT");
                    if (response.ErrorMessage == null)
                    {
                        TestTokensTest.Text = "Connection and tokens are OK.";
                    }
                    else
                    {
                        TestTokensTest.Text = response.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    TestTokensTest.Text = ex.Message;
                }
            }

            TestTokensRing.IsActive = false;
        }

        private async void TestSandBoxTokensButton_Click(object sender, RoutedEventArgs e)
        {
            TestSandboxTokensTest.Text = String.Empty;
            TestSandboxTokensRing.IsActive = true;

            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    var response = await iexCloudClient.StockPrices.QuoteAsync("MSFT");
                    if (response.ErrorMessage == null)
                    {
                        TestSandboxTokensTest.Text = "Connection and tokens are OK.";
                    }
                    else
                    {
                        TestSandboxTokensTest.Text = response.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    TestSandboxTokensTest.Text = ex.Message;
                }
            }

            TestSandboxTokensRing.IsActive = false;
        }
    }
}
