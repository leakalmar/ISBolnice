using Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.Enums;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateDoctorView.xaml
    /// </summary>
    public partial class UpdateDoctorView : Window
    {
        public DoctorDTO Doctor { get; set; } = new DoctorDTO();
        public ObservableCollection<string> Specialties { get; set; } = new ObservableCollection<string>();
        public UCDoctorsView udv;
        public UpdateDoctorView(DoctorDTO doctor, UCDoctorsView udv)
        {
            InitializeComponent();
            this.udv = udv;
            Doctor = doctor;

            SetDoctorInfo();
            
            this.DataContext = this;
        }
        private void SetDoctorInfo()
        {
            txtName.Text = Doctor.Name;
            txtSurname.Text = Doctor.Surname;
            txtBirthDate.Text = Doctor.BirthDate.ToString("dd.MM.yyyy.");
            txtAddress.Text = Doctor.Address;
            txtTelephone.Text = Doctor.Phone;
            txtEmail.Text = Doctor.Email;


            Specialties = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());
            cbSpecialty.SelectedItem = Doctor.Specialty;

            if (Doctor.WorkShift.Equals(WorkDayShift.FirstShift))
                shiftComboBox.SelectedIndex = 0;
            else if (Doctor.WorkShift.Equals(WorkDayShift.SecondShift))
                shiftComboBox.SelectedIndex = 1;

            vacationStartTxt.Text = Doctor.VacationTimeStart.ToString("dd.MM.yyyy.");
            vacationEndTxt.Text = Doctor.VacationTimeStart.AddDays(14).ToString("dd.MM.yyyy.");
        }

        private void UpdateDoctor(object sender, RoutedEventArgs e)
        {
            Doctor.Name = txtName.Text;
            Doctor.Surname = txtSurname.Text;
            Doctor.Address = txtAddress.Text;
            Doctor.Phone = txtTelephone.Text;
            Doctor.Email = txtEmail.Text;

            try
            {
                DateTime birthDate = DateTime.ParseExact(txtBirthDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                Doctor.BirthDate = birthDate;
                DateTime vacationTimeStart = DateTime.ParseExact(vacationStartTxt.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                Doctor.VacationTimeStart = vacationTimeStart;
            }
            catch (Exception ex)
            {
            }

            if (shiftComboBox.SelectedIndex == 0)
                Doctor.WorkShift = WorkDayShift.FirstShift;
            else if (shiftComboBox.SelectedIndex == 1)
                Doctor.WorkShift = WorkDayShift.SecondShift;

            if (cbSpecialty.SelectedIndex != -1)
                Doctor.Specialty = Specialties[cbSpecialty.SelectedIndex];
            else
                Doctor.Specialty = "";

            DoctorController.Instance.UpdateDoctor(Doctor);

            udv.RefreshGrid();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            udv.RefreshGrid();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            udv.RefreshGrid();
            this.Close();
        }

        private void vacationStartTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(vacationStartTxt.Text))
            {
                DateTime vacationStart = DateTime.ParseExact(vacationStartTxt.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                DateTime vacationEnd = vacationStart.AddDays(14);
                vacationEndTxt.Text = vacationEnd.ToString("dd.MM.yyyy.");
            }
        }

        private void DeleteDoctor(object sender, RoutedEventArgs e)
        {
            DeleteDoctorView ddv = new DeleteDoctorView(Doctor, this);
            ddv.ShowDialog();
        }
        private void UndoAllChanges(object sender, RoutedEventArgs e)
        {
            udv.RefreshGrid();
            SetDoctorInfo();
        }
    }
}
