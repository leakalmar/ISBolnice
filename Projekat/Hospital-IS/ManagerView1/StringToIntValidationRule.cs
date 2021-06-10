using System.Globalization;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                int r;
                if (int.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Unesite samo brojeve.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }

    }


    public class MinMaxValidationRule : ValidationRule
    {
        public int Min
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            /* value = Convert.ToInt32(value);
         if (value is int)
         {
             int d = (int)value;
             if (d < Min || d > Max)
             {
                 return new ValidationResult(false, "Vrijednost izvan opsega " + "(" + Convert.ToString(Min) + "," + Convert.ToString(Max) + ")");
             }
             else
             {
                 return new ValidationResult(true, null);
             }
         }
         else
         {
             return new ValidationResult(false, "Unknown error occured.");

         }
            */
            return new ValidationResult(true, "Unknown error occured.");

        }

    }


}
