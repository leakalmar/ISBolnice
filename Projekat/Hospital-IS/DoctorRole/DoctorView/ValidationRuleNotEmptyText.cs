using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public class ValidationRuleNotEmptyText : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is String)
            {
                String text = (String)value;
                //ako ne sadrzi slovo onda je true
                if (Regex.IsMatch(text, @"")) return new ValidationResult(false, "Opis uputa mora biti popunjen.");
                else return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
