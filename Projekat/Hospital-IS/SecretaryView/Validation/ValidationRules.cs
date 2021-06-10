using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Polje je obavezno.");
                else
                    return new ValidationResult(false, "Field cannot be empty.");
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Prvo slovo mora biti veliko.");
                else
                    return new ValidationResult(false, "First letter has to be uppercase.");
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Polje sme da sadrži samo slova.");
                else
                    return new ValidationResult(false, "Field can conatain only letters.");
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Broj telefona nije u dobrom formatu.");
                else
                    return new ValidationResult(false, "Incorrect format.");

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
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Uneti datum je nepostojeći.");
                    else
                        return new ValidationResult(false, "Date doesn't exist.");
                }

                if (date > DateTime.Now.AddYears(-13) || date < DateTime.Now.AddYears(-90))
                {
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Datum nije validan.");
                    else
                        return new ValidationResult(false, "Date isn't valid.");
                }

                return new ValidationResult(true, null);
            }
            else
            {
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Datum nije u dobrom formatu (dd.mm.yyyy.).");
                else
                    return new ValidationResult(false, "Date is not in correct format (dd.mm.yyyy.).");
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Email adresa nije u dobrom formatu.");
                else
                    return new ValidationResult(false, "Incorrect format.");
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
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Adresa nije u dobrom formatu.");
                else
                    return new ValidationResult(false, "Incorrect format.");
            }
        }
    }

    class IsAppointmentDurationValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^[0-9]+$");
            var s = value as string;
            if (rgx.IsMatch(s.ToString()))
            {
                int duration;
                try
                {
                    duration = Int32.Parse(s);
                    if (duration < 30)
                    {
                        if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                            return new ValidationResult(false, "Minimalna dužina trajanja termina je 30 min.");
                        else
                            return new ValidationResult(false, "Shortest possible appointment time is 30 min.");
                    }
                    else if (duration > 12 * 60)
                    {
                        if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                            return new ValidationResult(false, "Unesite validnu dužinu trajanja.");
                        else
                            return new ValidationResult(false, "Enter valid appointment duration.");
                    }
                }
                catch
                {
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Unesite validnu dužinu trajanja.");
                    else
                        return new ValidationResult(false, "Enter valid appointment duration.");
                }

                return new ValidationResult(true, null);
            }
            else
            {
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Mora se uneti broj.");
                else
                    return new ValidationResult(false, "Only numbers are allowed.");
            }

        }
    }

    class IsVacationTimeValid : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex rgx = new Regex(@"^[0-9]{2}\.[0-9]{2}\.[0-9]{4}\.$");
            var s = value as string;
            DateTime date;
            if (string.IsNullOrEmpty(s))
                return new ValidationResult(true, null);

            if (rgx.IsMatch(s.ToString().Trim()))
            {
                try
                {
                    date = DateTime.ParseExact(s.ToString(), "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Uneti datum je nepostojeći.");
                    else
                        return new ValidationResult(false, "Date doesn't exist.");
                }

                if (date < DateTime.Now || date < DateTime.Now.AddYears(-90))
                {
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Datum nije validan.");
                    else
                        return new ValidationResult(false, "Date isn't valid.");
                }


                if (date < DateTime.Now.AddDays(14))
                {
                    if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                        return new ValidationResult(false, "Godišnji se mora najavaiti bar dve nedelje unapred.");
                    else
                        return new ValidationResult(false, "Vacation has to be announced at least two weeks in advance.");
                }

                MessageBox.Show("Upozorenje: Svi termini u naznačenom periodu će biti otkazani!");
                return new ValidationResult(true, null);
            }
            else
            {
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    return new ValidationResult(false, "Datum nije u dobrom formatu (dd.mm.yyyy.).");
                else
                    return new ValidationResult(false, "Date is not in correct format (dd.mm.yyyy.).");
            }

        }
    }
}
