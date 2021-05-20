using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.Converters
{
    public class DoctorAppointmentConverter
    {
        public StartAppointmentDTO ConvertModelToViewModel(DoctorAppointment appointemnt)
        {
            StartAppointmentDTO viewModel = new StartAppointmentDTO();

            viewModel.DoctorAppointment = appointemnt;

            return viewModel;
        }

        public ObservableCollection<StartAppointmentDTO> ConvertCollectionToViewModel(List<DoctorAppointment> appointments)
        {
            ObservableCollection<StartAppointmentDTO> vmAppointments = new ObservableCollection<StartAppointmentDTO>();
            StartAppointmentDTO viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertModelToViewModel(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
