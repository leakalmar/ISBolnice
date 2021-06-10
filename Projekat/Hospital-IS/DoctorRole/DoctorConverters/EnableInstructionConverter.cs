using Hospital_IS.DTOs;
using Model;
using System;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{
    public class EnableInstructionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
            { return null; }

            bool ret;
            DoctorDTO logedInDoctor = (DoctorDTO)values[0];
            DoctorDTO appointmentDoctor = null;
            DoctorDTO emergencyAppointmentDoctor = null;
            string appointmentCause = (string)values[3];

            try
            {
                appointmentDoctor = (DoctorDTO)values[1];

#pragma warning disable CS0168 // Variable is declared but never used
               } catch(Exception e) {  }
#pragma warning restore CS0168 // Variable is declared but never 

            try
            {
                emergencyAppointmentDoctor = (DoctorDTO)values[2];

#pragma warning disable CS0168 // Variable is declared but never used
            }
            catch (Exception e) { }
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
