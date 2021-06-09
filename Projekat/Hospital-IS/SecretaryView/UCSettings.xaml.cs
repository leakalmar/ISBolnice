using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl
    {
        public UCSettings()
        {
            InitializeComponent();
        }

        private void cbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbTheme_GotFocus(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;

            if (cbTheme.SelectedIndex == 0)
            {
                app.ChangeTheme(new Uri("SecretaryView/Themes/LightTheme.xaml", UriKind.Relative));
                SecretaryMainWindow.Instance.miLight.IsChecked = true;
                SecretaryMainWindow.Instance.miDark.IsChecked = false;
            }
            else
            {
                app.ChangeTheme(new Uri("SecretaryView/Themes/DarkTheme.xaml", UriKind.Relative));
                SecretaryMainWindow.Instance.miLight.IsChecked = false;
                SecretaryMainWindow.Instance.miDark.IsChecked = true;
            }
        }
    }
}
