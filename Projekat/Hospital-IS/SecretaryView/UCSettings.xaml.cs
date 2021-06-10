using Hospital_IS.SecretaryView.Localization;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private String _name;
        private String _surname;
        private String _birthDate;
        private String _address;
        private String _phone;
        private String _email;
        public UCSettings()
        {
            InitializeComponent();
            SetSecretaryInfo();

            this.DataContext = this;
        }

        private void SetSecretaryInfo()
        {
            _name = "Nikola";
            nameTxt.Text = "Nikola";
            _surname = "Nikolić";
            surnameTxt.Text = "Nikolić";
            _address = "Vardarska 20, Novi Sad";
            addressTxt.Text = "Vardarska 20, Novi Sad";
            _phone = "0692224534";
            phoneTxt.Text = "0692224534";
            _email = "nikola@gmail.com";
            emailTxt.Text = "nikola@gmail.com";

            _birthDate = "24.11.1992.";
            birthdateTxt.Text = "24.11.1992.";
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

        private void cbLanguage_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cbLanguage.SelectedIndex == 0)
            {
                LocalizedStrings.Instance.SetCulture("sr-LATN-CS");
                SecretaryMainWindow.Instance.miSerbian.IsChecked = true;
                SecretaryMainWindow.Instance.miEnglish.IsChecked = false;
                SecretaryMainWindow.Instance.SetSearchField("sr");
            }
            else
            {
                LocalizedStrings.Instance.SetCulture("en-US");
                SecretaryMainWindow.Instance.miSerbian.IsChecked = false;
                SecretaryMainWindow.Instance.miEnglish.IsChecked = true;
                SecretaryMainWindow.Instance.SetSearchField("en");
            }
        }

        private void UndoAllChanges(object sender, RoutedEventArgs e)
        {
            SetSecretaryInfo();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String SecretaryName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }
        public String Surname
        {
            get { return _surname; }
            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        public String BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value != _birthDate)
                {
                    _birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }
        public String Address
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        public String Phone
        {
            get { return _phone; }
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }
        public String Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private void ReportIssue(object sender, RoutedEventArgs e)
        {
            CreateFeedbackMessage cfm = new CreateFeedbackMessage();
            cfm.ShowDialog();
        }
    }
}
