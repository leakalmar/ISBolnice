using System;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            Visibility ret;
            bool visibility = (bool)value;
            if(visibility)
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
