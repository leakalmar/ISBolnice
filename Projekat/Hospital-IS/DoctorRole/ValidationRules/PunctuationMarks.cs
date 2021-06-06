using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.ValidationRules
{
    public class PunctuationMarks : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (!charString.All(char.IsLetter))
            {
                return new ValidationResult(false, $"Polje mora sadržati isključivo slova.");
            }
            return new ValidationResult(true, null);
        }
    }
}
