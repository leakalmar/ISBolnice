using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.ValidationRules
{
    public class NotAllowedLetters : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (charString.Any(char.IsLetter))
            {
                return new ValidationResult(false, $"Polje ne sme sadržati slova.");
            }
            return new ValidationResult(true, null);
        }
    }
}
