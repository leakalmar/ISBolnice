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
           CultureInfo enUS = new CultureInfo("en-US");
            if(DateTime.TryParseExact(charString, "dd.MM.yyyy", enUS,
                                 DateTimeStyles.None, out dummy))
            {
                return new ValidationResult(true, null);
                
            }
            else
            {
                return new ValidationResult(false, $"Neispravan format datuma.");
            }
        }
    }
}
