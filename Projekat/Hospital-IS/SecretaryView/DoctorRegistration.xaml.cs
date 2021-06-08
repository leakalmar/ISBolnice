using Controllers;
using Hospital_IS.DTOs;
using Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DoctorRegistration.xaml
    /// </summary>
    public partial class DoctorRegistration : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Specialties { get; set; } = new ObservableCollection<string>();
        public UCDoctorsView udv;

        private DoctorDTO _doctor { get; set; } = new DoctorDTO();

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
        public DoctorDTO Doctor
        {
            get { return _doctor; }
            set
            {
                if (value != _doctor)
                {
                    _doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }
        public DoctorRegistration(UCDoctorsView udv)
        {
            InitializeComponent();
            this.udv = udv;
            Specialties = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());
            this.DataContext = this;
            btnConfirm.IsEnabled = false;
        }

        private void AddDoctor(object sender, RoutedEventArgs e)
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

            try
            {
                DateTime birthDate = DateTime.ParseExact(txtBirthDate.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                _doctor.BirthDate = birthDate;
            }
            catch (Exception ex)
            {
            }

            _doctor.Id = UserService.Instance.GenerateUserID();

            if (cbSpecialty.SelectedIndex != -1)
                _doctor.Specialty = Specialties[cbSpecialty.SelectedIndex];
            else
                _doctor.Specialty = Specialties[0];

            _doctor.Password = passwordBox.ToString();

            DoctorController.Instance.AddDoctor(_doctor);
            udv.RefreshGrid();
            this.Close();
        }


        private void Close(object sender, RoutedEventArgs e)
        {
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

        private bool ShouldConfirmationButtonBeEnabled()
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtBirthDate.Text)
                || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || txtPasswordMatch.Visibility.Equals(Visibility.Visible))
            {
                txtName.Text = txtName.Text;
                txtSurname.Text = txtSurname.Text;
                txtBirthDate.Text = txtBirthDate.Text;
                txtAddress.Text = txtAddress.Text;
                txtPhone.Text = txtPhone.Text;
                txtEmail.Text = txtEmail.Text;
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
    }
}
