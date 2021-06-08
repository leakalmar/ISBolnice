using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    [ValueConversion(typeof(Specialty), typeof(Visibility))]
    public class SpecialtyToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a boolean");

            Visibility ret;
            Specialty specialty = (Specialty)value;
            if (specialty.Name.Equals(DoctorMainWindowModel.Instance.Doctor.Specialty))
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
