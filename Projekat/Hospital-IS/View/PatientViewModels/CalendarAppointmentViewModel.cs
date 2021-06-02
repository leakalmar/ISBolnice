using Controllers;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.View.PatientViewModels
{
    public class CalendarAppointmentViewModel : BindableBase
    {
        private List<DoctorAppointment> FutureAppointments { get; set; }
        public ObservableCollection<CalendarDaysDTO> Days { get; set; }
        public ObservableCollection<string> DayNames { get; set; }
        public MyICommand ShowHome { get; set; }
        public CalendarAppointmentViewModel()
        {
            FutureAppointments = DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id);
            DayNames = new ObservableCollection<string> { "Ponedeljak", "Utorak", "Sreda", "Četvrtak", "Petak", "Subota", "Nedelja" };
            Days = new ObservableCollection<CalendarDaysDTO>();
            ShowHome = new MyICommand(Home);
            BuildCalendar(DateTime.Today);
        }

        public void BuildCalendar(DateTime targetDate)
        {
            Days.Clear();

            //Calculate when the first day of the month is and work out an 
            //offset so we can fill in any boxes before that.
            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);
            int offset = DayOfWeekNumber(d.DayOfWeek);
            if (offset != 1) d = d.AddDays(-offset+1);

            //Show 6 weeks each with 7 days = 42
            for (int box = 1; box <= 42; box++)
            {
                CalendarDaysDTO day = new CalendarDaysDTO(d, targetDate.Month == d.Month);
                day.IsToday = d == DateTime.Today;
                CheckForAppointment(day);
                Days.Add(day);
                d = d.AddDays(1);
            }
        }

        private void CheckForAppointment(CalendarDaysDTO day)
        {
            foreach (DoctorAppointment doctorAppointment in FutureAppointments)
            {
                if (doctorAppointment.AppointmentStart.Date.Equals(day.Date))
                {
                    day.AppointmentInformation += "Pregled kod doktora " + doctorAppointment.Doctor.Name + " "
                        + doctorAppointment.Doctor.Surname + " u " + doctorAppointment.AppointmentStart.TimeOfDay + "h.\n";
                }
            }
        }

        private static int DayOfWeekNumber(DayOfWeek dow)
        {
            return Convert.ToInt32(dow.ToString("D"));
        }

        private void Home()
        {
            HomePatientViewModel homeViewModel = new HomePatientViewModel();
            PatientMainWindowView.Instance.PatientMainView.CurrentViewModel = homeViewModel;
        }
    }
}
