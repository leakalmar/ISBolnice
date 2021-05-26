using Controllers;
using Hospital_IS.DTOs;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DoctorRegistration.xaml
    /// </summary>
    public partial class DoctorRegistration : Window
    {
        public DoctorDTO Doctor { get; set; } = new DoctorDTO();
        public ObservableCollection<string> Specialties { get; set; } = new ObservableCollection<string>();
        public UCDoctorsView udv;
        public DoctorRegistration(UCDoctorsView udv)
        {
            InitializeComponent();
            this.udv = udv;
            Specialties = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());
            this.DataContext = this;
        }

        private void AddDoctor(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime birthDate = DateTime.ParseExact(birthdateTxt.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                Doctor.BirthDate = birthDate;
            }
            catch (Exception ex)
            {
            }

            Doctor.Id = UserService.Instance.GenerateUserID();
            Doctor.Specialty = Specialties[cbSpecialty.SelectedIndex];
            Doctor.Password = passwordBox.ToString();

            DoctorController.Instance.AddDoctor(Doctor);
            udv.RefreshGrid();
            this.Close();
        }


        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
