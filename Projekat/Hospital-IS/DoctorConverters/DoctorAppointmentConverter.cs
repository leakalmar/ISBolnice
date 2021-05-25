using DTOs;
using Hospital_IS.Controllers;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital_IS.DoctorConverters
{
    public class DoctorAppointmentConverter
    {
        public AppointmentRowDTO ConvertModelToDTO(DoctorAppointment appointemnt)
        {
            DoctorAppointmentDTO doctorAppointmentDTO = DoctorAppointmentManagementController.Instance.GetAppointmentById(appointemnt.Id);
            AppointmentRowDTO appointmentRowDTO = new AppointmentRowDTO(doctorAppointmentDTO, false);

            return appointmentRowDTO;
        }

        public ObservableCollection<AppointmentRowDTO> ConvertCollectionToDTO(List<DoctorAppointment> appointments)
        {
            ObservableCollection<AppointmentRowDTO> vmAppointments = new ObservableCollection<AppointmentRowDTO>();
            AppointmentRowDTO viewModel;
            foreach (DoctorAppointment d in appointments)
            {
                viewModel = ConvertModelToDTO(d);
                vmAppointments.Add(viewModel);
            }
            return vmAppointments;
        }
    }
}
