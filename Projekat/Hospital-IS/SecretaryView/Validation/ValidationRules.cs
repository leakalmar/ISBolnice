using System.Globalization;
using System.Windows.Controls;

namespace Hospital_IS.SecretaryView.Validation
{
    class IsEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;
            if (string.IsNullOrEmpty(s))
            {
                return new ValidationResult(false, "Polje je obavezno.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    class IsFirstLetterInUppercaseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;
            if (!s[0].ToString().Equals(s[0].ToString().ToUpper()))
            {
                return new ValidationResult(false, "Prvo slovo mora biti veliko.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
