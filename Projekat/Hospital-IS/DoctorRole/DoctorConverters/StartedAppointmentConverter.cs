using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(/*sourceType*/ typeof(bool), /*targetType*/ typeof(Visibility))]
    class StartedAppointmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
            { return null; }

            Visibility ret;
            bool IsFinished = (bool)value;
            if (IsFinished)
            {
                ret = Visibility.Collapsed;
            }
            else
            {
                ret = Visibility.Visible;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
