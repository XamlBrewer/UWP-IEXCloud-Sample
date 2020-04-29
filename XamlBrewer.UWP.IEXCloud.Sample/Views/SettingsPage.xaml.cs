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
            ContentGrid2.Translation += new Vector3(0, 0, 6);
            SettingsGrid.RegisterImplicitAnimations();

            Loaded += Page1_Loaded;
        }

        private Settings Settings => new Settings();

        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            SharedShadow.Receivers.Add(ShadowCatcher);

        }

        private async void TestTokensButton_Click(object sender, RoutedEventArgs e)
        {
            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    var response = await iexCloudClient.Stock.QuoteAsync("MSFT");
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
        }

        private async void TestSandBoxTokensButton_Click(object sender, RoutedEventArgs e)
        {
            using (var iexCloudClient = IEXCloudService.GetClient())
            {
                try
                {
                    var response = await iexCloudClient.Stock.QuoteAsync("MSFT");
                    if (response.ErrorMessage == null)
                    {
                        TestTokensTest2.Text = "Connection and tokens are OK.";
                    }
                    else
                    {
                        TestTokensTest2.Text = response.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    TestTokensTest2.Text = ex.Message;
                }
            }
        }
    }
}
