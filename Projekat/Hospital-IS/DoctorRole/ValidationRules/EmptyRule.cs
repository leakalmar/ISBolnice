using System.Globalization;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.ValidationRules
{
    public class EmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if(charString.Length == 0)
            {
                return new ValidationResult(false, $"Polje ne sme biti prazno.");
            }
            return new ValidationResult(true, null);
        }
    }
}
