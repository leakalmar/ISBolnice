using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Hospital_IS.Converters
{
    class MarkerConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Thickness))
            { return null; }
            if (value[0] == DependencyProperty.UnsetValue)
            {
                Thickness ret;
                Visibility IsStarted = (Visibility)value[0];
                int index = (int)value[1];
                if (IsStarted == Visibility.Visible)
                {
                    if (index == 5)
                    {
                        index = 0;
                    }
                    ret = new Thickness(120 * index, 0, 0, 0);
                }
                else
                {
                    if (index == 5)
                    {
                        index = 1;
                    }
                    ret = new Thickness(120 * index - 120, 0, 0, 0);
                }
                return ret;
            }
            else
            {
                Thickness ret;
                Visibility IsStarted = (Visibility)value[0];
                int index = (int)value[1];
                if (IsStarted == Visibility.Visible)
                {
                    if (index == 5)
                    {
                        index = 0;
                    }
                    ret = new Thickness(120 * index, 0, 0, 0);
                }
                else
                {
                    if (index == 5)
                    {
                        index = 1;
                    }
                    ret = new Thickness(120 * index - 120, 0, 0, 0);
                }
                return ret;
            }
            return null;
            
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
