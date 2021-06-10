using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(typeof(AppointmentType), typeof(String))]
    public class AppTypeToString : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(String))
                throw new InvalidOperationException("The target must be a boolean");

            AppointmentType type = (AppointmentType)value;
            switch (type)
            {
                case AppointmentType.CheckUp:
                    return "Pregled";
                case AppointmentType.Operation:
                    return "Operacija";
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            String type = (String)value;
            switch (type)
            {
                case "Pregled":
                    return AppointmentType.CheckUp;
                case "Operacija":
                    return AppointmentType.Operation;
            }
            return null;
        }

        #endregion
    }
}
