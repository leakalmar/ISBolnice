using System;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(typeof(DateTime), typeof(bool))]
    public class IsTodayAppointmentConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            bool ret;
            DateTime date = (DateTime)value;
            if (date.Date.Equals(DateTime.Today))
            {
                ret = false;
            }
            else
            {
                ret = true;
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
