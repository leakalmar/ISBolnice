using System;
using System.Globalization;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() 
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentCulture;
        }

        public ResourceDictionary ThemeDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }

}
