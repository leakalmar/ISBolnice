using Model;
using System;
using System.Windows.Data;

namespace Hospital_IS.DoctorConverters
{
    public class EnableInstructionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            { return null; }

            bool ret;
            Doctor logedInDoctor = (Doctor)values[0];
            Doctor appointmentDoctor = null;
            Doctor emergencyAppointmentDoctor = null;
            string appointmentCause = (string)values[3];

            try
            {
                appointmentDoctor = (Doctor)values[1];
                emergencyAppointmentDoctor = (Doctor)values[2];

#pragma warning disable CS0168 // Variable is declared but never used
               } catch(Exception e) {  }
#pragma warning restore CS0168 // Variable is declared but never used

            if (appointmentDoctor != null)
            {
                if (appointmentDoctor.Id.Equals(logedInDoctor.Id))
                {
                    ret = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(appointmentCause) || string.IsNullOrEmpty(appointmentCause))
                    {
                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }
            else
            {
                if (emergencyAppointmentDoctor.Id.Equals(logedInDoctor.Id))
                {
                    ret = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(appointmentCause) || string.IsNullOrEmpty(appointmentCause))
                    {
                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }
            }


            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
