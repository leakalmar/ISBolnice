using DTOs;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.DoctorConverters
{
    public class DoctorAppointmentConverter
    {
        public AppointmentRowDTO ConvertModelToViewModel(DoctorAppointment appointemnt)
        {
            AppointmentRowDTO viewModel = new AppointmentRowDTO(new DoctorAppointmentDTO(appointemnt),true);

            return viewModel;
        }

        public ObservableCollection<AppointmentRowDTO> ConvertCollectionToViewModel(List<DoctorAppointment> appointments)
        {
            ObservableCollection<AppointmentRowDTO> vmAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertModelToViewModel(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
