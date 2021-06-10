using Controllers;
using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateDoctorView.xaml
    /// </summary>
    public partial class UpdateDoctorView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DoctorDTO Doctor { get; set; } = new DoctorDTO();
        public ObservableCollection<string> Specialties { get; set; } = new ObservableCollection<string>();
        public UCDoctorsView udv;
        public IDoctorAppointmentTarget doctorAppointmentTarget = SecretaryMainWindow.Instance.target;

        private String _name;
        private String _surname;
        private String _birthDate;
        private String _address;
        private String _phone;
        private String _email;
        private String _vacationStart;

        private WorkDayShift oldShift;
        public UpdateDoctorView(DoctorDTO doctor, UCDoctorsView udv)
        {
            InitializeComponent();
            this.udv = udv;
            Doctor = doctor;
            oldShift = doctor.WorkShift;
            SetDoctorInfo();
            
            this.DataContext = this;
        }
        private void SetDoctorInfo()
        {
            _name = Doctor.Name;
            txtName.Text = Doctor.Name;
            _surname = Doctor.Surname;
            txtSurname.Text = Doctor.Surname;
            _birthDate = Doctor.BirthDate.ToString("dd.MM.yyyy.");
            txtBirthDate.Text = Doctor.BirthDate.ToString("dd.MM.yyyy.");
            _address = Doctor.Address;
            txtAddress.Text = Doctor.Address;
            _phone = Doctor.Phone;
            txtTelephone.Text = Doctor.Phone;
            _email = Doctor.Email;
            txtEmail.Text = Doctor.Email;


            Specialties = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());
            cbSpecialty.SelectedItem = Doctor.Specialty;

            if (Doctor.WorkShift.Equals(WorkDayShift.FirstShift))
                shiftComboBox.SelectedIndex = 0;
            else if (Doctor.WorkShift.Equals(WorkDayShift.SecondShift))
                shiftComboBox.SelectedIndex = 1;

            if (!Doctor.VacationTimeStart.Equals(new DateTime(1,1,1)))
            {
                _vacationStart = Doctor.VacationTimeStart.ToString("dd.MM.yyyy.");
                vacationStartTxt.Text = Doctor.VacationTimeStart.ToString("dd.MM.yyyy.");
                vacationEndTxt.Text = Doctor.VacationTimeStart.AddDays(14).ToString("dd.MM.yyyy.");
            }
            else
            {
                _vacationStart = "";
                vacationStartTxt.Text = "";
            }
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

            if (oldShift != Doctor.WorkShift)
                CancelFutureAppointments();

            DoctorController.Instance.UpdateDoctor(Doctor);

            udv.RefreshGrid();
            this.Close();
        }

        private void CancelFutureAppointments()
        {
            List<DoctorAppointmentDTO> appointments = doctorAppointmentTarget.GetFutureAppointmentsForDoctor(Doctor.Id);
            for (int i = 0; i < appointments.Count; i++)
            {
                NotificationController.Instance.SendAppointmentCancelationNotification(appointments[i]);
                doctorAppointmentTarget.DeleteDoctorAppointment(appointments[i]);
            }
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
                try
                {
                    DateTime vacationStart = DateTime.ParseExact(vacationStartTxt.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                    DateTime vacationEnd = vacationStart.AddDays(14);
                    vacationEndTxt.Text = vacationEnd.ToString("dd.MM.yyyy.");
                }
                catch (Exception ex)
                {
                }
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

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String DoctorName
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("DoctorName");
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
        public String VacationStart
        {
            get { return _vacationStart; }
            set
            {
                if (value != _vacationStart)
                {
                    _vacationStart = value;
                    OnPropertyChanged("VacationStart");
                }
            }
        }

        private void shiftComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ukoliko promenite smenu svi ne obavljeni termini će biti otkazani!");
        }
    }
}
