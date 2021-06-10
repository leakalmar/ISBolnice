using System;
using System.Globalization;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.ValidationRules
{
    public class DateFormat : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            DateTime dummy;
            CultureInfo sr = new CultureInfo("sr-Latn-RS");
            if(DateTime.TryParseExact(charString, "dd.M.yyyy.", sr,
                                 DateTimeStyles.None, out dummy))
            {
                return new ValidationResult(true, null);
                
            }
            else
            {
                return new ValidationResult(false, $"Format datuma je dd.M.yyyy.");
            }
        }
    }
}
