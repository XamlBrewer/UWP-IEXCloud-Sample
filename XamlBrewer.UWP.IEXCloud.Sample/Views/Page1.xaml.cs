using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using VSLee.IEXSharp;
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
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();

            Loaded += Page1_Loaded;
        }

        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            SharedShadow.Receivers.Add(ShadowCatcher);
            ContentGrid.Translation += new Vector3(0, 0, 6);
        }
    }
}
