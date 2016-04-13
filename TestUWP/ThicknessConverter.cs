using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TestUWP
{
    public class ThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Rect)
            {
                var rect = (Rect)value;
                return new Thickness(rect.Left, rect.Top, 0, 0);
            }

            return new Thickness(0, 0, 0, 0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) { throw new NotImplementedException(); }
    }

}