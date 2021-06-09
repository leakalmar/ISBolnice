using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using Service;
using System;
using System.ComponentModel;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UCPatientsView ucp;
        public GuestsView gv;
        private PatientDTO _patient { get; set; } = new PatientDTO();
        public PatientDTO Patient
        {
            get { return _patient; }
            set
            {
                if (value != _patient)
                {
                    _patient = value;
                    OnPropertyChanged("Patient");
                }
            }
        }

        private String _birthDate;
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

        public PatientRegistration(UCPatientsView ucp, GuestsView gv)
        {
            InitializeComponent();

            if (gv != null)
            {
                _patient = (PatientDTO)gv.dataGridGuests.SelectedItem;
                checkBox.Visibility = Visibility.Collapsed;
                this.gv = gv;
            }

            this.DataContext = this;
            this.ucp = ucp;
            btnConfirm.IsEnabled = false;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            if (!ShouldConfirmationButtonBeEnabled())
            {
                btnConfirm.IsEnabled = false;
                return;
            }

            if (string.IsNullOrEmpty(passwordBox.Password.ToString()))
            {
                txtPasswordMatch.Visibility = Visibility.Visible;
            }



            if (checkBox.IsChecked == true)
            {
                _patient.IsGuest = true;
                _patient.BirthDate = DateTime.Now;
            }

            if (comboBox.SelectedIndex == 0)
                _patient.Gender = "Žensko";
            else if (comboBox.SelectedIndex == 1)
                _patient.Gender = "Muško";

            try
            {
                DateTime birthDate = DateTime.ParseExact(birthdateTxt.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                _patient.BirthDate = birthDate;
            }
            catch (Exception ex)
            {
            }

            _patient.Password = passwordBox.Password.ToString();

            if (checkBox.Visibility != Visibility.Collapsed)
            {
                _patient.Id = UserService.Instance.GenerateUserID();
                SecretaryManagementController.Instance.AddPatient(_patient);
            }
            else 
            {
                _patient.IsGuest = false;
                SecretaryManagementController.Instance.UpdatePatient(_patient);
                gv.RefreshGrid();
            }

            ucp.RefreshGrid();

            this.Close();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void CheckPassword(object sender, RoutedEventArgs e)
        {
            if (!passwordBox.Password.ToString().Equals(passwordRepeated.Password.ToString()) || string.IsNullOrEmpty(passwordBox.Password.ToString()))
            {
                txtPasswordMatch.Visibility = Visibility.Visible;
                btnConfirm.IsEnabled = false;
            }
            else
                txtPasswordMatch.Visibility = Visibility.Hidden;

            if (ShouldConfirmationButtonBeEnabled()) 
            {
                btnConfirm.IsEnabled = true;
            }
        }

        private bool ShouldConfirmationButtonBeEnabled() {
            if ((string.IsNullOrEmpty(nameTxt.Text) || string.IsNullOrEmpty(surnameTxt.Text) || string.IsNullOrEmpty(birthdateTxt.Text)
                || string.IsNullOrEmpty(addressTxt.Text) || string.IsNullOrEmpty(phoneTxt.Text) || string.IsNullOrEmpty(emailTxt.Text)
                || comboBox.SelectedIndex == -1 || txtPasswordMatch.Visibility.Equals(Visibility.Visible)) && checkBox.IsChecked == false)
            {
                nameTxt.Text = nameTxt.Text;
                surnameTxt.Text = surnameTxt.Text;
                birthdateTxt.Text = birthdateTxt.Text;
                addressTxt.Text = addressTxt.Text;
                phoneTxt.Text = phoneTxt.Text;
                emailTxt.Text = emailTxt.Text;
                if (string.IsNullOrEmpty(passwordBox.Password.ToString()))
                {
                    txtPasswordMatch.Visibility = Visibility.Visible;
                }
                return false;
            }
            else
            {
                btnConfirm.IsEnabled = true;
                return true;
            }
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
                btnConfirm.IsEnabled = true;
        }
    }
}
