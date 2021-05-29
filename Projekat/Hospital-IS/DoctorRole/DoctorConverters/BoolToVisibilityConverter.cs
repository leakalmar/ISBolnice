using System;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            Visibility ret;
            bool show = (bool)value;
            if (show)
            {
                ret = Visibility.Visible;
            }
            else
            {
                ret = Visibility.Collapsed;
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
