using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.ValidationRules
{
    public class NumbersInString : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (charString.Any(char.IsDigit))
            {
                return new ValidationResult(false, $"Polje ne sme sadržati brojeve.");
            }
            return new ValidationResult(true, null);
        }
    }
}
