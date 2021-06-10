using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace Hospital_IS.DoctorRole.DoctorConverters
{ 

    [ValueConversion(typeof(RoomType), typeof(String))]
    public class RoomTypeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(String))
                throw new InvalidOperationException("The target must be a boolean");

            String ret = null;
            RoomType type = (RoomType)value;
            switch (type)
            {
                case RoomType.ConsultingRoom:
                    ret = "Soba za konsultacije";
                    break;
                case RoomType.OperationRoom:
                    ret = "Operaiona sala";
                    break;
                case RoomType.RecoveryRoom:
                    ret = "Soba za oporavak";
                    break;
                case RoomType.StorageRoom:
                    ret = "Magacin";
                    break;
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
