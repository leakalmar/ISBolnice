using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace Hospital_IS.SecretaryView.Localization
{
    public class LocalizedStrings
    {
        private LocalizedStrings()
        {

        }

        public static LocalizedStrings Instance { get; } = new LocalizedStrings();

        public void SetCulture(string cultureCode)
        {
            var newCulture = new CultureInfo(cultureCode);
            LocalizeDictionary.Instance.Culture = newCulture;
        }

        public string this[string key]
        {
            get
            {
                var result = LocalizeDictionary.Instance.GetLocalizedObject("StorageCalc", "Strings", key, LocalizeDictionary.Instance.Culture);
                return result as string;
            }
        }
    }
}
