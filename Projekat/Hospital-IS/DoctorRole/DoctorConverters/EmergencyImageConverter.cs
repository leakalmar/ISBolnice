using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Hospital_IS.DoctorRole.DoctorConverters
{

    [ValueConversion(typeof(bool), typeof(Object))]
    public class EmergencyImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Object))
                throw new InvalidOperationException("The target must be a boolean");

            Image ret = new Image();
            bool emergency = (bool)value;
            if (emergency)
            {
                ret.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/redsiren.png"));
            }
            else
            {
                ret.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/siren.png"));
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
