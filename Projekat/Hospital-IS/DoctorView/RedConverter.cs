using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Hospital_IS.DoctorView
{
    [ValueConversion(/*sourceType*/ typeof(String), /*targetType*/ typeof(int))]
    public class RedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }

            return ((String)value).Any(x => !char.IsLetter(x)) ? 5 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
