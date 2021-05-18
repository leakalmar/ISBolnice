using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class HomePatientViewModel : BindableBase 
    {

        public HomePatientViewModel()
        {
            public DoctorAppointment rescheduledApp;
            public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }
        }
    }
}
