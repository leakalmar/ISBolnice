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
        public ObservableCollection<string> MonthNames { get; set; }
        public DateTime NewMonth { get; set; }
        private string month;
        public MyICommand ShowHome { get; set; }
        public MyICommand NextMonth { get; set; }
        public MyICommand PreviousMonth { get; set; }
        private DateTime startDate;
        public CalendarAppointmentViewModel()
        {
            FutureAppointments = DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id);
            DayNames = new ObservableCollection<string> { "Ponedeljak", "Utorak", "Sreda", "Četvrtak", "Petak", "Subota", "Nedelja" };
            MonthNames = new ObservableCollection<string> { "Januar", "Februar", "Mart", "April", "Maj", "Jun", "Jul", "Avgust", "Septembar", "Oktobar", "Novembar", "Decembar" };
            Days = new ObservableCollection<CalendarDaysDTO>();
            ShowHome = new MyICommand(Home);
            NextMonth = new MyICommand(NextMon);
            PreviousMonth = new MyICommand(PreviousMon);
            Month = MonthNames[DateTime.Today.Month - 1] + " " + DateTime.Today.Year.ToString() + ".";
            BuildCalendar(DateTime.Today);
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if(startDate != value)
                {
                    startDate = value;
                }
            }
        }

        public string Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
                    OnPropertyChanged("Month");
                }
            }
        }

        public void BuildCalendar(DateTime targetDate)
        {
            Days.Clear();
            NewMonth = new DateTime(targetDate.Year, targetDate.Month, 1);
            StartDate = NewMonth;
            int offset = DayOfWeekNumber(StartDate.DayOfWeek);
            if (offset != 1) StartDate = StartDate.AddDays(-offset+1);
            for (int box = 1; box <= 42; box++)
            {
                CalendarDaysDTO day = new CalendarDaysDTO(StartDate, targetDate.Month == StartDate.Month);
                day.IsToday = StartDate == DateTime.Today;
                CheckForAppointment(day);
                Days.Add(day);
                StartDate = StartDate.AddDays(1);
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

        private void PreviousMon()
        {
            if(NewMonth.Month == 1)
            {
                NewMonth = new DateTime(NewMonth.Year - 1, 12, NewMonth.Day);
            }
            else
            {
                NewMonth = new DateTime(NewMonth.Year, NewMonth.Month - 1, NewMonth.Day);
            }
            Month = MonthNames[NewMonth.Month-1] + " " + NewMonth.Year.ToString() + ".";
            BuildCalendar(NewMonth);
        }

        private void NextMon()
        {
            NewMonth = NewMonth.AddMonths(1);
            Month = MonthNames[NewMonth.Month-1] + " " + NewMonth.Year.ToString() + ".";
            BuildCalendar(NewMonth);
        }

        private void Home()
        {
            HomePatientViewModel homeViewModel = new HomePatientViewModel();
            PatientMainWindowView.Instance.PatientMainView.CurrentViewModel = homeViewModel;
        }
    }
}
