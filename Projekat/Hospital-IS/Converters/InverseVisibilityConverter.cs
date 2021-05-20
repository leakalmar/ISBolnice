using System;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.Converters
{
    [ValueConversion(typeof(Visibility), typeof(Visibility))]
    public class InverseVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            Visibility ret;
            Visibility visibility = (Visibility)value;
            if(visibility == Visibility.Visible)
            {
                ret = Visibility.Collapsed;
            }
            else
            {
                ret = Visibility.Visible;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
