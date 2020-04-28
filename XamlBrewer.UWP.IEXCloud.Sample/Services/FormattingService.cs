using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace XamlBrewer.UWP.IEXCloud.Sample.Services
{
    public static class FormattingService
    {
        public static string NumberAsArrow(decimal number)
        {
            if (number > 0)
            {
                return "\uEB11";
            }

            if (number < 0)
            {
                return "\uEB0F";
            }

            return "";
        }

        public static Color NumberAsColor(decimal number)
        {
            if (number > 0)
            {
                return Colors.OliveDrab;
            }

            if (number < 0)
            {
                return Colors.IndianRed;
            }

            return Colors.Black;
        }

        public static Brush NumberAsBrush(decimal number)
        {
            return new SolidColorBrush(NumberAsColor(number));
        }
    }
}
