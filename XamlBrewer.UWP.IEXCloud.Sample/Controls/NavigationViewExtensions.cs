using System.Collections.Generic;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace XamlBrewer.UWP.IEXCloud.Sample.Controls
{
    public static class NavigationViewExtensions
    {
        public static List<WinUI.NavigationViewItem> SelectedItemsPath(this WinUI.NavigationView navigationView)
        {
            var result = new List<WinUI.NavigationViewItem>();
            GetSelectedItems(navigationView.MenuItems, ref result);

            return result;
        }

        private static void GetSelectedItems(IList<object> items, ref List<WinUI.NavigationViewItem> result)
        {
            foreach (WinUI.NavigationViewItem item in items)
            {
                if (item.IsSelected)
                {
                    result.Insert(0, item);
                }
                else
                {
                    if (item.MenuItems?.Count > 0)
                    {
                        var count = result.Count;
                        GetSelectedItems(item.MenuItems, ref result);
                        if (result.Count > count)
                        {
                            result.Insert(0, item);
                        }
                    }
                }
            }
        }
    }
}
