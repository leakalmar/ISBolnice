using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

    class AreCharactersSuitableValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^[\p{L}]+$|^$");
            var s = value as string;
            if (rgx.IsMatch(s.ToString().Trim()))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Polje sme da sadrži samo slova.");
            }
        }
    }

    class IsPhoneNumberValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^([0-9]{9,10})|(\+{1}[0-9]{11,14})$");
            var s = value as string;
            if (rgx.IsMatch(s.ToString().Trim()))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Broj telefona nije u dobrom formatu.");
            }
        }
    }
    class IsBirthdateValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^[0-9]{2}\.[0-9]{2}\.[0-9]{4}\.$");
            var s = value as string;
            DateTime date;
            if (rgx.IsMatch(s.ToString().Trim()))
            {
                try
                {
                    date = DateTime.ParseExact(s.ToString(), "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    return new ValidationResult(false, "Uneti datum je nepostojeći.");
                }

                if (date > DateTime.Now.AddYears(-13) || date < DateTime.Now.AddYears(-90))
                    return new ValidationResult(false, "Datum nije validan.");

                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Datum nije u dobrom formatu (dd.mm.yyyy.).");
            }

        }
    }

    class IsEmailValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;
            MailAddress email;
            try
            {
                email = new MailAddress(s.ToString());
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Email adresa nije u dobrom formatu.");
            }

            return new ValidationResult(true, null);
        }
    }
    class IsAddressValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^[\p{L}\ \-0-9]+\,{1}[\p{L}\ \-0-9]+$");
            var s = value as string;
            if (rgx.IsMatch(s.ToString().Trim()))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Adresa nije u dobrom formatu.");
            }
        }
    }
}
